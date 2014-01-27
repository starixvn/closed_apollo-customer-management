// -----------------------------------------------------------------------
// <copyright file="BetListExcelGenerator.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions
{
    using System;
    using Apollo.BetList.Extensions.Builders.ChoiceBuilders;
    using Apollo.BetList.Extensions.Builders.OddsBuilders;
    using Apollo.BetList.Extensions.Builders.StatusBuilder;
    using Apollo.BetList.Extensions.Builders.TransTimeBuilders;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extends BetListExcelBuilder class.
    /// </summary>
    public class BetListExcelGenerator : BetListExcelBuilder
    {
        private const string ChoiceBuilderNamespace = "Apollo.BetList.Extensions.Builders.ChoiceBuilders";
        private const string OddsBuilderNamespace = "Apollo.BetList.Extensions.Builders.OddsBuilders";

        /// <summary>
        /// Generates the trans time.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>Formatted excel string.</returns>
        public IRichTextString BuildTransTime(ITicket ticket, TransTimeRTFHelper rtfHelper)
        {
            Trans1Extensions builder = new Trans1Extensions();

            return builder.BuildRTF(ticket, rtfHelper);
        }

        /// <summary>
        /// Builds the choice column.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>Formatted excel string.</returns>
        public IRichTextString BuildChoice(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            Choice1Extensions builder = null;
            string typeName = string.Format("{0}.Choice{1}Extensions", ChoiceBuilderNamespace, ticket.BetTypeId);

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                builder = new Choice1Extensions();
            }
            else
            {
                builder = Activator.CreateInstance(type) as Choice1Extensions;
            }

            return builder.BuildRTF(ticket, ticketHelper, rtfHelper);
        }

        /// <summary>
        /// Builds the odds column.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>Formatted excel string.</returns>
        public IRichTextString BuildOdds(ITicket ticket, OddsRTFHelper rtfHelper)
        {
            Odds1Extensions builder = null;
            string typeName = string.Format("{0}.Odds{1}Extensions", OddsBuilderNamespace, ticket.BetTypeId);

            Type type = Type.GetType(typeName);

            if (type == null)
            {
                builder = new Odds1Extensions();
            }
            else
            {
                builder = Activator.CreateInstance(type) as Odds1Extensions;
            }

            return builder.BuildRTF(ticket, rtfHelper);
        }

        /// <summary>
        /// Builds the status column.
        /// </summary>
        /// <param name="ticket">The ticket.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <param name="isShowVNIP">Is show IP from Vietnam.</param>
        /// <returns>Formatted excel string.</returns>
        public IRichTextString BuildStatus(ITicket ticket, ITicketHelper ticketHelper, StatusRTFHelper rtfHelper, bool isShowVNIP)
        {
            Status1Extensions builder = new Status1Extensions();

            return builder.BuildRTF(ticket, ticketHelper, rtfHelper, isShowVNIP);
        }
    }
}