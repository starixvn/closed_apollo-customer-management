// -----------------------------------------------------------------------
// <copyright file="BetStatusId.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.01.10">Created</change>
// </history>
namespace Apollo.CMS.BetList.Contants
{
    public enum BetStatusId : int
    {
        /// <summary>
        /// Running status.
        /// </summary>
        Running = 0,

        /// <summary>
        /// Waiting status.
        /// </summary>
        Waiting = 1,

        /// <summary>
        /// Reject status.
        /// </summary>
        Reject = 101,

        /// <summary>
        /// Void status.
        /// </summary>
        Void = 102,

        /// <summary>
        /// Refund status.
        /// </summary>
        Refund = 103,

        /// <summary>
        /// Draw status.
        /// </summary>
        Draw = 111,

        /// <summary>
        /// Won status.
        /// </summary>
        Won = 112,

        /// <summary>
        /// Lose status.
        /// </summary>
        Lose = 113
    }
}