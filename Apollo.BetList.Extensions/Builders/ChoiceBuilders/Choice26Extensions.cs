// -----------------------------------------------------------------------
// <copyright file="Choice26Extensions.cs" company="Nexcel Solutions Vietnam">
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
    /// Extensions for Choice26 builder.
    /// </summary>
    public class Choice26Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type Both/One/Neither to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);

            choice.Handicap = null;

            if (ticket.BetTeam == "b")
            {
                choice.BetTeam = CoreBetList.lblBoth;
            }
            else if (ticket.BetTeam == "o")
            {
                choice.BetTeam = CoreBetList.lblOne;
            }
            else if (ticket.BetTeam == "n")
            {
                choice.BetTeam = CoreBetList.nogoal;
            }

            return rtfHelper.RTFRenderer.Render();
        }
    }
}