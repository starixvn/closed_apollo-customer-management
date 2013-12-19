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

    /// <summary>
    /// Show model.
    /// </summary>
    public class Shadow
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public DateTime? CreatedDate { get; set; }

        public byte? Status { get; set; }
    }
}