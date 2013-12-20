// --------------------------------------------------------------------------
// <copyright file="Shadow.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.19">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.AccountList
{
    using System;
    using Apollo.CMS.Core.Constants;
    using Apollo.CMS.Core.Helpers;

    /// <summary>
    /// Show model.
    /// </summary>
    public class Shadow
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime? CreatedDate { get; set; }

        public byte? Status { get; set; }

        public string RealStatus
        {
            get
            {
                return StatusHelper.GetShadowStatus((AccountStatus)this.Status);
            }
        }
    }
}