// --------------------------------------------------------------------------
// <copyright file="OddTypeNames.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.14">Created</change>
// </history>

namespace Apollo.CMS.BetList.Constants
{
    public enum OddTypes : int
    {
        /// <summary>
        /// DEC odd type.
        /// </summary>
        Dec = 1,

        /// <summary>
        /// HK odd type.
        /// </summary>
        Hk = 2,

        /// <summary>
        /// Indo odd type.
        /// </summary>
        Indo = 3,

        /// <summary>
        /// MY odd type.
        /// </summary>
        My = 4,

        /// <summary>
        /// US odd type.
        /// </summary>
        Us = 5
    }

    public struct OddTypeNames
    {
        /// <summary>
        /// Dec odd type name.
        /// </summary>
        public const string Dec = "Dec";

        /// <summary>
        /// HK odd type name.
        /// </summary>
        public const string Hk = "HK";

        /// <summary>
        /// Indo odd type name.
        /// </summary>
        public const string Indo = "Indo";

        /// <summary>
        /// MY odd type name.
        /// </summary>
        public const string My = "MY";

        /// <summary>
        /// US odd type name.
        /// </summary>
        public const string Us = "US";
    }
}