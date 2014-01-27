// -----------------------------------------------------------------------
// <copyright file="Choice24Extensions.cs" company="Nexcel Solutions Vietnam">
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
    /// Extensions for Choice24 builder.
    /// </summary>
    public class Choice24Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type Double Chance to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);
            string team = ticket.BetTeam.Contains("1") ? choice.HomeName : choice.AwayName;
            string drawAway = ticket.BetTeam.EndsWith("x") ? CoreBetList.lblDraw : choice.AwayName;

            choice.Handicap = null;
            choice.BetTeam = string.Format("{0} {1} {2}", team, CoreBetList.lblOr.ToLower(), drawAway);
            choice.IsFavorite = false;
            BuildRTFChoice(choice, ticket, ref rtfHelper);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}