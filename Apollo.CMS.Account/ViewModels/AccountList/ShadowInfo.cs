// --------------------------------------------------------------------------
// <copyright file="ShadowInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.18">Created</change>
// </history>

namespace Apollo.CMS.Account.ViewModels.AccountList
{
    using System;
    using Apollo.CMS.Account.Models.AccountList;
    using Apollo.CMS.Core.Helpers;
    using Fanex.Common;
    using Fanex.NAP.Core.Web;

    /// <summary>
    /// View model for a shadow item in the shadow list.
    /// </summary>
    [Serializable]
    public class ShadowInfo
    {
        public ShadowInfo()
        {
        }

        public ShadowInfo(RawShadowInfo rawData)
        {
            if (rawData != null)
            {
                this.Id = rawData.Id;
                this.Username = rawData.Username;
                this.CreatedDate = Converter.ToDateTime(rawData.CreatedDate).ToFormatedDateTime();
                this.Status = StatusHelper.GetShadowStatus(rawData.Status);
            }
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string CreatedDate { get; set; }

        public string Status { get; set; }
    }
}