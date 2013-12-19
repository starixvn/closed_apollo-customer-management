// -----------------------------------------------------------------------
// <copyright file="BetStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.05">Created</change>
// </history>

namespace Apollo.CMS.Core.Constants
{
    public enum BetStatus : int
    {
        /// <summary>
        /// All statuses.
        /// </summary>
        All = 0,

        /// <summary>
        /// Status active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Status stopped.
        /// </summary>
        Stopped = 2,

        /// <summary>
        /// Status C lock.
        /// </summary>
        CLock = 4
    }
}