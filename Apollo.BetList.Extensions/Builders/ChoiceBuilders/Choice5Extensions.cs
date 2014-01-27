// -----------------------------------------------------------------------
// <copyright file="Choice5Extensions.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Choice5 builder.
    /// </summary>
    public class Choice5Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type FT.1X2 to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);

            choice.Handicap = null;
            choice.BetTeam = string.Format("{0}.{1}", CoreBetList.ft, ticket.BetTeam.ToUpper());
            choice.IsFavorite = false;
            BuildRTFChoice(choice, ticket, ref rtfHelper);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}