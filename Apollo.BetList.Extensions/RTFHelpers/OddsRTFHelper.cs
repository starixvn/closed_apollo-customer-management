// -----------------------------------------------------------------------
// <copyright file="OddsRTFHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.RTFHelpers
{
    using NPOI.SS.UserModel;

    /// <summary>
    /// Odds RTF Helper.
    /// </summary>
    public class OddsRTFHelper : NumberRTFHelper
    {
        public OddsRTFHelper()
            : base()
        {
        }

        public IFont OddsNameFont { get; set; }
    }
}