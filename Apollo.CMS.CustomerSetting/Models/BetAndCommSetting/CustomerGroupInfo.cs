// --------------------------------------------------------------------------
// <copyright file="CustomerGroupInfo.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Models.BetAndCommSetting
{
    /// <summary>
    /// Model for saving group information of a customer.
    /// </summary>
    public class CustomerGroupInfo
    {
        public int? CustId { get; set; }

        public string UGroup { get; set; }

        public bool? IsUGroupChanged { get; set; }
    }
}