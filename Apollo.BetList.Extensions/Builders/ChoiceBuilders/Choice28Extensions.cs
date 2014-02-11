// -----------------------------------------------------------------------
// <copyright file="Choice28Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.ChoiceBuilders
{
    using System;
    using Apollo.BetList.Extensions.Entities;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.App_GlobalResources;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Choice28 builder.
    /// </summary>
    public class Choice28Extensions : Choice1Extensions
    {
        /// <summary>
        /// Builds the choice column of bet type 3-way handicap to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);
            string teamName = CoreBetList.lblDraw;
            string handicap = string.Empty;
            bool isFavorite = false;

            choice.Handicap = null;

            if (ticket.BetTeam == "1")
            {
                teamName = choice.HomeName;
                handicap = Formatter.FormatSignNumber((int)(ticket.Handicap2 - ticket.Handicap1));
                isFavorite = ticket.Handicap1 > ticket.Handicap2;
            }
            else if (ticket.BetTeam == "2")
            {
                teamName = ticketHelper.GetTeamNameById(ticket.AwayId);
                handicap = Formatter.FormatSignNumber((int)(ticket.Handicap1 - ticket.Handicap2));
                isFavorite = ticket.Handicap1 < ticket.Handicap2;
            }
            else
            {
                teamName = CoreBetList.lblDraw;

                if (ticket.Handicap1 - ticket.Handicap2 > 0)
                {
                    handicap = string.Format("{0} ", CoreBetList.a);
                }
                else if (ticket.Handicap1 - ticket.Handicap2 < 0)
                {
                    handicap = string.Format("{0} ", CoreBetList.h);
                }
                else
                {
                    handicap = string.Empty;
                }

                handicap += Formatter.FormatSignNumber((int)Math.Abs(ticket.Handicap2 - ticket.Handicap1));
            }

            choice.IsFavorite = isFavorite;
            choice.BetTeam = teamName;
            choice.Handicap = string.Format(" ({0})", handicap);
            BuildRTFChoice(choice, ticket, ref rtfHelper);

            return rtfHelper.RTFRenderer.Render();
        }
    }
}