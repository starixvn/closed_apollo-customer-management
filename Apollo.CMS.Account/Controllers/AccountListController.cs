// -----------------------------------------------------------------------
// <copyright file="AccountListController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.18">Created</change>
// </history>

namespace Apollo.CMS.Account.Controllers
{
    using Apollo.CMS.Account.Services.AccountList.Implements;
    using Apollo.CMS.Account.Services.AccountList.Interfaces;
    using Apollo.CMS.Core.Controllers;

    /// <summary>
    /// Account list controller.
    /// </summary>
    public class AccountListController : BaseController
    {
        private readonly IAccountListService _accountListService;

        public AccountListController() :
            this(null)
        {
        }

        public AccountListController(IAccountListService accountListService)
        {
            this._accountListService = accountListService ?? new AccountListService();
        }
    }
}