// -----------------------------------------------------------------------
// <copyright file="Status1Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.StatusBuilder
{
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Builder.StakeBuilder;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Status1 builder.
    /// </summary>
    public class Status1Extensions : Stake29
    {
        /// <summary>
        /// Builds the status column of bet type Handicap to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="isShowVNIP">Show IP from Vietnam or not.</param>
        /// <returns>IRichTextString object indicates the status column.</returns>
        public IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, StatusRTFHelper rtfHelper, bool isShowVNIP)
        {
            string status = ticket.Status.ToUpper();

            if (!string.IsNullOrEmpty(status))
            {
                rtfHelper.RTFRenderer.AddText(status, rtfHelper.StatusFont);
            }

            if (isShowVNIP || !ticketHelper.IsVnIp(ticket.IP))
            {
                rtfHelper.RTFRenderer.AddText(string.Format("\n {0}", ticket.IP), rtfHelper.IPFont);
            }

            return rtfHelper.RTFRenderer.Render();
        }
    }
}