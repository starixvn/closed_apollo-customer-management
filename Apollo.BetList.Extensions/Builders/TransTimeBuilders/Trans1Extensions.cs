// -----------------------------------------------------------------------
// <copyright file="Trans1Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.TransTimeBuilders
{
    using Apollo.BetList.Extensions.Constants;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Builder.TransBuilder;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Trans1 builder.
    /// </summary>
    public class Trans1Extensions : Trans1
    {
        /// <summary>
        /// Renders the transaction time column to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the transaction time column.</returns>
        public IRichTextString BuildRTF(ITicket ticket, TransTimeRTFHelper rtfHelper)
        {
            rtfHelper.RTFRenderer.AddText(string.Format("{0}: {1}\n", CoreBetList.refno, ticket.TransId), rtfHelper.RefNoFont);
            rtfHelper.RTFRenderer.AddText(ticket.TransDate.ToString(CommonConstants.DateTimeFormat), rtfHelper.TransTimeFont);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}