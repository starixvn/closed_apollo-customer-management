// --------------------------------------------------------------------------
// <copyright file="UserController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.4.11">Encrypt passed password.</change>
// </history>

namespace Apollo.CMS.Websites.Areas.UserControl.Controllers
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using Apollo.CMS.Core.Controllers;
    using Apollo.CMS.Core.Web;
    using Fanex.Common;
    using Fanex.NAP.Core.Criteria.User;
    using Fanex.NAP.Core.Service.Impl;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Core.Web;

    /// <summary>
    /// User controller.
    /// </summary>
    public class UserController : BaseController
    {
        private const string ChangePasswordFormToken = "_SecureChangePasswordFormToken";
        private readonly IUserService _userService;
        private HashAlgorithm _hashAlgorithm;

        public UserController()
            : this(null)
        {
        }

        public UserController(IUserService userService)
        {
            _userService = userService ?? new UserService { ServiceContext = ServiceContext };
            _hashAlgorithm = new SHA256Managed();
        }

        public string FormToken
        {
            get
            {
                if (Session[ChangePasswordFormToken] == null)
                {
                    return string.Empty;
                }

                return Session[ChangePasswordFormToken].ToString();
            }

            set
            {
                Session[ChangePasswordFormToken] = value;
            }
        }

        #region Change Password

        /// <summary>
        /// Redirect to changing password form.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult ChangePassword()
        {
            GenerateToken();
            return View(new UpdatePasswordCustCriteria { Username = AppSession.CurrentUser.Name });
        }

        /// <summary>
        /// Change password action.
        /// </summary>
        /// <param name="criteria">Criteria using to update password.</param>
        /// <param name="token">Changing password token.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult ChangePassword(UpdatePasswordCustCriteria criteria, string token)
        {
            ////if (ModelState.IsValid && CheckValidToken(token))
            if (CheckValidToken(token))
            {
                var user = _userService.GetById(AppSession.CurrentUser.Id);
                string hashPassword = GetHash(user.Password + token);

                if (hashPassword == criteria.OldUserPwd)
                {
                    criteria.CustId = user.Id;

                    var result = _userService.InsertOrUpdate(criteria);

                    if (result != null && result.IsSuccessful)
                    {
                        ViewBag.Success = true;
                    }
                    else if (result != null && result.IsSuccessful == false)
                    {
                        ViewBag.Message = result.Message;
                    }
                }
                else
                {
                    ViewBag.Message = ViewResources.Wrong_password;
                }
            }

            GenerateToken();

            return View(criteria);
        }

        #endregion Change Password

        #region Profile

        /// <summary>
        /// Redirect to profile page.
        /// </summary>
        /// <returns>Action result.</returns>
        [HttpGet]
        public ActionResult UserProfile()
        {
            var criteria = new UpdateCustCriteria();

            var user = _userService.GetById(AppSession.CurrentUser.Id);

            criteria.CustId = user.Id;
            criteria.Username = user.Username;
            criteria.FirstName = user.FirstName;
            criteria.LastName = user.LastName;
            criteria.Email = user.Email;

            return View(criteria);
        }

        /// <summary>
        /// Update user profile.
        /// </summary>
        /// <param name="criteria">Criteria using to update user profile.</param>
        /// <returns>Action result.</returns>
        [HttpPost]
        public ActionResult UserProfile(UpdateCustCriteria criteria)
        {
            if (ModelState.IsValid)
            {
                criteria.CustId = AppSession.CurrentUser.Id;
                criteria.Username = AppSession.CurrentUser.Username;
                criteria.FirstName = criteria.FirstName ?? string.Empty;
                criteria.LastName = criteria.LastName ?? string.Empty;
                criteria.Email = criteria.Email ?? string.Empty;

                var result = _userService.InsertOrUpdate(criteria);

                if (result != null && result.IsSuccessful)
                {
                    ViewBag.Success = true;
                }
                else if (result != null && result.IsSuccessful == false)
                {
                    ViewBag.Message = result.Message;
                }
            }

            return View(criteria);
        }

        #endregion Profile

        #region Helpers

        /// <summary>
        /// Generates the token for login form.
        /// </summary>
        private void GenerateToken()
        {
            string token = Guid.NewGuid().ToString("N");

            FormToken = token;
            ViewBag.ChangePasswordToken = token;
            AddTokenToCookie(token);
        }

        /// <summary>
        /// Adds the login token to cookie.
        /// </summary>
        /// <param name="token">The changing password token.</param>
        private void AddTokenToCookie(string token)
        {
            // Create the non-persistent secure form token cookie
            var responseCookie = new HttpCookie(ChangePasswordFormToken)
            {
                // Set the HttpOnly property to prevent the cookie from
                // being accessed by client side script
                HttpOnly = true,

                // Add login token to the cookie value
                Value = token
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
        /// Checks if the token is valid or not.
        /// </summary>
        /// <param name="token">The changing password token.</param>
        /// <returns>Boolean value indicates if the login token is valid or not.</returns>
        private bool CheckValidToken(string token)
        {
            return !string.IsNullOrWhiteSpace(token)
                && FormToken == token
                && Request.Cookies[ChangePasswordFormToken] != null
                && token == Request.Cookies[ChangePasswordFormToken].Value;
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
            Session.Remove(ChangePasswordFormToken);

            if (Request.Cookies[ChangePasswordFormToken] != null)
            {
                HttpCookie cookie = new HttpCookie(ChangePasswordFormToken);

                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
        }

        #endregion Helpers
    }
}