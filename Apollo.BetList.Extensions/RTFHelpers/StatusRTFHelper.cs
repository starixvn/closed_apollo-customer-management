// -----------------------------------------------------------------------
// <copyright file="StatusRTFHelper.cs" company="Nexcel Solutions Vietnam">
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
    /// Status RTF Helper.
    /// </summary>
    public class StatusRTFHelper
    {
        public StatusRTFHelper()
        {
            RTFRenderer = new RtfTextRender();
        }

        public IFont StatusFont { get; set; }

        public IFont IPFont { get; set; }

        public RtfTextRender RTFRenderer { get; set; }
    }
}