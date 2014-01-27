// -----------------------------------------------------------------------
// <copyright file="NumberRTFHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.RTFHelpers
{
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Number RTF helper.
    /// </summary>
    public class NumberRTFHelper
    {
        public NumberRTFHelper()
        {
            RTFRenderer = new RtfTextRender();
        }

        public IFont PositiveFont { get; set; }

        public IFont NegativeFont { get; set; }

        public RtfTextRender RTFRenderer { get; set; }
    }
}