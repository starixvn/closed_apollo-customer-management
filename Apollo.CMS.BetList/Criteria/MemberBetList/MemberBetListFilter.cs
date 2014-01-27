// --------------------------------------------------------------------------
// <copyright file="MemberBetListFilter.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.10">Created</change>
// </history>

namespace Apollo.CMS.BetList.Criteria.MemberBetList
{
    using System;

    /// <summary>
    /// The general criteria that are used to get the bet list from current used database and archive database.
    /// </summary>
    public class MemberBetListFilter
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public bool IsLoadNew { get; set; }
    }
}