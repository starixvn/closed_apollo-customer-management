// -----------------------------------------------------------------------
// <copyright file="Choice4Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.ChoiceBuilders
{
    using Apollo.BetList.Extensions.Entities;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Choice4 builder.
    /// </summary>
    public class Choice4Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type Correct Score to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);

            choice.Handicap = null;
            choice.BetTeam = (ticket.BetTeam == "0:5" || ticket.BetTeam == "5:0") ? ticket.BetTeam + " UP" : ticket.BetTeam;
            choice.IsFavorite = false;
            BuildRTFChoice(choice, ticket, ref rtfHelper);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}