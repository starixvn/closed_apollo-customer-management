// -----------------------------------------------------------------------
// <copyright file="Choice20Extensions.cs" company="Nexcel Solutions Vietnam">
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
    /// Extensions for Choice20 builder.
    /// </summary>
    public class Choice20Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type Money Line to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);

            choice.Handicap = null;
            BuildRTFChoice(choice, ticket, ref rtfHelper);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}