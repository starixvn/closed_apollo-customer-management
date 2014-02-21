// --------------------------------------------------------------------------
// <copyright file="BetAndCommSettingFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.20">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.ViewModels.BetAndCommSetting
{
    /// <summary>
    /// View model for filter of bet and commission setting function.
    /// </summary>
    public class BetAndCommSettingFilter
    {
        public int CustId { get; set; }

        public int UplineId { get; set; }

        public string Username { get; set; }

        public bool IsMember { get; set; }
    }
}