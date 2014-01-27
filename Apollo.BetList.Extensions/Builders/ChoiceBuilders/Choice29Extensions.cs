// -----------------------------------------------------------------------
// <copyright file="Choice29Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.ChoiceBuilders
{
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Choice29 builder.
    /// </summary>
    public class Choice29Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type System Parlay to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            string betTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            string combination = ticketHelper.GetCombinationMPOfferName(ticket.BetTeam);

            rtfHelper.RTFRenderer.AddText(string.Format("{0} - {1}", betTypeName, combination), rtfHelper.UnderdogFont);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}