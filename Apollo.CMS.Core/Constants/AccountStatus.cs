// -----------------------------------------------------------------------
// <copyright file="AccountStatus.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.12.05">Created</change>
// </history>

namespace Apollo.CMS.Core.Constants
{
    public enum AccountStatus : int
    {
        /// <summary>
        /// Status active.
        /// </summary>
        Active = 1,

        /// <summary>
        /// Status stopped.
        /// </summary>
        Stopped = 2,

        /// <summary>
        /// Status locked.
        /// </summary>
        Locked = 3,

        /// <summary>
        /// Status C lock.
        /// </summary>
        CLock = 4
    }
}