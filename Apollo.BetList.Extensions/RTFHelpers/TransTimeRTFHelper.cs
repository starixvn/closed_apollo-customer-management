// -----------------------------------------------------------------------
// <copyright file="TransTimeRTFHelper.cs" company="Nexcel Solutions Vietnam">
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
    /// Transaction time RTF Helper.
    /// </summary>
    public class TransTimeRTFHelper
    {
        public TransTimeRTFHelper()
        {
            RTFRenderer = new RtfTextRender();
        }

        public IFont TransTimeFont { get; set; }

        public IFont RefNoFont { get; set; }

        public RtfTextRender RTFRenderer { get; set; }
    }
}