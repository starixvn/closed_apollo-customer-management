// -----------------------------------------------------------------------
// <copyright file="TransTimeWrapper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Entities
{
    /// <summary>
    /// Wrapper for transaction time column.
    /// </summary>
    public class TransTimeWrapper
    {
        public string RefNo { get; set; }

        public string TransTime { get; set; }
    }
}