// --------------------------------------------------------------------------
// <copyright file="LoginController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Edward.Nguyen" date="2012.8.28">Add properties _currentContext</change>
//   <change who="Jason.Ly" date="2014.4.11">Encrypt passed password.</change>
// </history>

namespace Apollo.CMS.Websites.Areas.UserLogin.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Apollo.CMS.Core.Web;
    using Fanex.Common.Extensions;
    using Fanex.NAP.Core.Configuration;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Model;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web;
    using Fanex.NAP.Core.Web.Auth;
    using Fanex.NAP.Core.Web.Session;

    /// <summary>
    /// Login controller.
    /// </summary>
    public class LoginController : BaseController
    {
        #region members

        private const string LoginFormToken = "_SecureLoginFormToken";

        private IUserService _userService;
        private IGroupService _groupService;
        private ISiteService _siteService;
        private IMenuItemService _menuItemService;
        private ICurrentSession _currentSession;
        private IMaintenanceService _maintenanceService;
        private HashAlgorithm _hashAlgorithm;

        #endregion members

        #region ctors

        public LoginController()
            : this(null, null, null, null, null, null)
        {
        }

        public LoginController(
            IUserService userService,
            IGroupService groupService,
            ISiteService siteService,
            IMenuItemService menuItemService,
            ICurrentSession currentSession,
            IMaintenanceService maintenanceService)
        {
            _userService = userService ?? new UserService();
            _groupService = groupService ?? new GroupService();
            _siteService = siteService ?? new SiteService();
            _menuItemService = menuItemService ?? new MenuItemService();
            _currentSession = currentSession ?? new CurrentSession();
            _maintenanceService = maintenanceService ?? new MaintenanceService();
            _hashAlgorithm = new SHA256Managed();
        }

        #endregion ctors

        #region Properties

        public string FormToken
        {
            get
            {
                if (Session[LoginFormToken] == null)
                {
                    return string.Empty;
                }

                return Session[LoginFormToken].ToString();
            }

            set
            {
                Session[LoginFormToken] = value;
            }
        }

        #endregion Properties

        #region sign in

        /// <summary>
        /// Redirect to login page.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult Index()
        {
            if (!NapSettings.Settings.IsNapHub && !IsSiteLogin)
            {
                // if login-directly feature is disabled then redirect users to NAP Hub
                return Redirect(NapSettings.Settings.HubUrl);
            }

            var maintenanceInfo = _maintenanceService.GetDowntimeInfo();

            if (maintenanceInfo.IsDowntime)
            {
                return RedirectToAction("Offline", "Common");
            }

            GenerateToken();

            return View();
        }

        /// <summary>
        /// Log the user in.
        /// </summary>
        /// <param name="username">Entered user name.</param>
        /// <param name="password">Entered password.</param>
        /// <param name="loginToken">Login token.</param>
        /// <returns>Action result.</returns>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reused with NAP framework.")]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(string username, string password, string loginToken)
        {
            if (!username.IsNullOrTrimEmpty() && !password.IsNullOrTrimEmpty() && CheckValidToken(loginToken))
            {
                var user = _userService.GetByUsername(username.Trim()) as User;

                if (user != null)
                {
                    string hashPassword = GetHash(user.Password + loginToken);

                    if (password == hashPassword)
                    {
                        var group = user.Group == null ? null : _groupService.GetById(user.Group.Id);
                        var siteLevelList = _siteService.GetSiteLevelByUser(user.Id, Environment); // gets all sites of logging-in user

                        if ((group == null || group.IsActive) && siteLevelList.Where(x => x.IsActive).Any())
                        {
                            var criteria = new UserCheckInUserCriteria
                            {
                                UserId = user.Id,
                                SiteId = this.SiteId,
                                NetSessionId = Session.SessionID,
                                Ip = CurrentContext.IP,
                                LoginDate = DateTime.Now,
                                BrowserName = CurrentContext.BrowserName,
                                CountryCode = CurrentContext.CountryCode,
                                Domain = CurrentContext.DomainName,
                                Platform = CurrentContext.Platform
                            };

                            _userService.UserCheckIn(ref criteria);

                            _currentSession.Level = _userService.GetUserSiteLevel(user.Id, this.SiteId);
                            _currentSession.UserId = user.Id;
                            _currentSession.Username = username;
                            _currentSession.OnUserId = criteria.OnUserId;
                            _currentSession.SessionId = Session.SessionID.ToString();

                            AppSession.CurrentUser = user;

                            // current site is not NAP Hub
                            if (this.SiteId != 0)
                            {
                                CreateUserMenu(user);
                            }

                            CreateAuthInfo(user);

                            // if the user login directly, not through NAP Hub
                            if (this.IsSiteLogin)
                            {
                                bool userIsActiveOnThisSite = false;

                                foreach (var siteLevel in siteLevelList)
                                {
                                    if (siteLevel.Site.Id == this.SiteId && siteLevel.IsActive)
                                    {
                                        userIsActiveOnThisSite = true;
                                    }
                                }

                                if (userIsActiveOnThisSite)
                                {
                                    return Redirect(FormsAuthentication.DefaultUrl);
                                }
                                else
                                {
                                    DoSignOut();
                                    ViewBag.Message = ViewResources.Your_account_was_suspended;
                                }
                            }
                            else
                            {
                                // user has access to multiple sites
                                if (siteLevelList.Where(s => s.IsActive).Count() > 1)
                                {
                                    return RedirectToAction("Site", "UserSite");
                                }

                                var site = siteLevelList.Where(s => s.IsActive).First().Site;

                                var listSiteUrl = _siteService.GetSiteUrlBySiteId(site.Id, this.Environment).Where(u => u.IsActive).ToList();

                                if (listSiteUrl.Count() > 1)
                                {
                                    return RedirectToAction("Site", "UserSite");
                                }

                                return RedirectToAction("Forward", "UserSite", new { id = listSiteUrl.First().SiteUrlId });
                            }
                        }
                        else
                        {
                            // user account was suspended on all sites
                            if (siteLevelList.Count() > 0)
                            {
                                ViewBag.Message = ViewResources.Your_account_was_suspended;
                            }
                            else
                            {
                                // logging-in user belongs to no site
                                ViewBag.Message = ViewResources.You_have_no_authorization;
                            }
                        }
                    }
                    else
                    {
                        // wrong password
                        ViewBag.Message = ViewResources.Invalid_Username_or_Password;
                    }
                }
                else
                {
                    // user doesn't exist
                    ViewBag.Message = ViewResources.Invalid_Username_or_Password;
                }
            }
            else
            {
                ViewBag.Message = "Invalid input!";
            }

            GenerateToken();

            return View();
        }

        #endregion sign in

        #region re check in

        /// <summary>
        /// Recheck whether the user logged in or not.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ReCheckLogin()
        {
            var maintenanceInfo = _maintenanceService.GetDowntimeInfo();

            if (maintenanceInfo.IsDowntime)
            {
                return Json("-1");
            }

            // check if not nap hub
            if (this.SiteId != 0)
            {
                var siteStatus = _siteService.GetStatusById(this.SiteId);

                if (!siteStatus)
                {
                    return Json("1");
                }
            }

            UserReCheckInUserCriteria criteria = new UserReCheckInUserCriteria();
            criteria.OnUserId = Convert.ToInt32(_currentSession.OnUserId);
            _userService.ReUserCheckIn(ref criteria);

            return Json(criteria.IsKickedOut ? "1" : "0");
        }

        #endregion re check in

        #region sign out

        /// <summary>
        /// Log user out and redirect to login page.
        /// </summary>
        /// <returns>Action result.</returns>
        public ActionResult LogOut()
        {
            DoSignOut();

            return Redirect(FormsAuthentication.LoginUrl);
        }

        /// <summary>
        /// Do sign out action.
        /// </summary>
        private void DoSignOut()
        {
            _userService.UserLogOut(new LogOutUserCriteria
            {
                OnUserId = _currentSession.OnUserId
            });
            FormsAuthentication.SignOut();
            _currentSession.ClearAll();
        }

        #endregion sign out

        #region helpers

        /// <summary>
        /// Create user menu.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        private void CreateUserMenu(User user)
        {
            _currentSession.Menu = (IList<MenuItem>)_menuItemService.GetByUserAndSiteIncludeGroup(
                                                                                    user.Id,
                                                                                    SiteId,
                                                                                    Environment,
                                                                                    true);

            AppSession.CurrentUserMenu = _currentSession.Menu;
        }

        /// <summary>
        /// Create authentication info.
        /// </summary>
        /// <param name="user">Logged in user.</param>
        private void CreateAuthInfo(User user)
        {
            var currentDomain = CurrentContext.GetCurrentDomain();

            var authInfo = new AuthInfo();

            authInfo.UserId = user.Id;
            authInfo.OnUserId = this._currentSession.OnUserId;

            var authTicket = new FormsAuthenticationTicket(
                 1,
                user.Username,
                DateTime.Now,
                DateTime.Now.AddMinutes(20000),
                false,
                authInfo.ToXml(),
                currentDomain.Path);

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket)
            {
                HttpOnly = true,
                Path = currentDomain.Path,
                Name = FormsAuthentication.FormsCookieName
            };

            Response.Cookies.Add(authCookie);
        }

        /// <summary>
        /// Adds the login token to cookie.
        /// </summary>
        /// <param name="loginToken">The login token.</param>
        private void AddTokenToCookie(string loginToken)
        {
            // Create the non-persistent secure form token cookie
            var responseCookie = new HttpCookie(LoginFormToken)
            {
                // Set the HttpOnly property to prevent the cookie from
                // being accessed by client side script
                HttpOnly = true,

                // Add login token to the cookie value
                Value = loginToken
            };

            // If we are using SSL, the cookie should be set to secure to
            // prevent it from being sent over HTTP connections
            if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
            {
                responseCookie.Secure = true;
            }

            // Add the cookie to the response
            Response.Cookies.Set(responseCookie);
        }

        /// <summary>
        /// Generates the token for login form.
        /// </summary>
        private void GenerateToken()
        {
            string loginToken = Guid.NewGuid().ToString("N");

            FormToken = loginToken;
            ViewBag.LoginToken = loginToken;
            AddTokenToCookie(loginToken);
        }

        /// <summary>
        /// Checks if the token is valid or not.
        /// </summary>
        /// <param name="loginToken">The login token.</param>
        /// <returns>Boolean value indicates if the login token is valid or not.</returns>
        private bool CheckValidToken(string loginToken)
        {
            return !string.IsNullOrWhiteSpace(loginToken)
                && FormToken == loginToken
                && Request.Cookies[LoginFormToken] != null
                && loginToken == Request.Cookies[LoginFormToken].Value;
        }

        /// <summary>
        /// Gets the hash value of input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>Hash value of input.</returns>
        private string GetHash(string input)
        {
            var encrypted = string.Empty;

            if (!string.IsNullOrWhiteSpace(input))
            {
                var hashData = _hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

                encrypted = BitConverter.ToString(hashData).Replace("-", string.Empty).ToLower();
            }

            return encrypted;
        }

        /// <summary>
        /// Clears the token.
        /// </summary>
        private void ClearToken()
        {
            Session.Remove(LoginFormToken);

            if (Request.Cookies[LoginFormToken] != null)
            {
                HttpCookie cookie = new HttpCookie(LoginFormToken);

                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
        }

        #endregion helpers
    }
}