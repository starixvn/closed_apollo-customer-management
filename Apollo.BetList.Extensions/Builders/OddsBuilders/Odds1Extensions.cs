// -----------------------------------------------------------------------
// <copyright file="Odds1Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.OddsBuilders
{
    using System;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Builder.OddsBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Odds1 builder.
    /// </summary>
    public class Odds1Extensions : Odds1
    {
        /// <summary>
        /// Builds the odd column of bet type Handicap to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the odds column.</returns>
        public virtual IRichTextString BuildRTF(ITicket ticket, OddsRTFHelper rtfHelper)
        {
            string oddsType = string.IsNullOrEmpty(ticket.OddsType) ? "0" : ticket.OddsType;
            string oddsName = CacheRef.GetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, oddsType));

            FormatOdds(ticket.Odds, ticket.BetTypeId, oddsType, rtfHelper);
            rtfHelper.RTFRenderer.AddText(string.Format("\n {0}", oddsName), rtfHelper.OddsNameFont);

            return rtfHelper.RTFRenderer.Render();
        }

        /// <summary>
        /// Formats the specified odds.
        /// </summary>
        /// <param name="odds">The odds value.</param>
        /// <param name="betType">Type of the bet.</param>
        /// <param name="oddsType">Type of the odds.</param>
        /// <param name="textRender">Text render helper.</param>
        private void FormatOdds(object odds, object betType, object oddsType, OddsRTFHelper textRender)
        {
            double oddsVal = ConvertByBetType.GetOddsByBetType(Convert.ToDouble(odds), Convert.ToInt32(betType), Convert.ToInt32(oddsType));

            if (oddsVal < 0)
            {
                textRender.RTFRenderer.AddText(string.Format(Formatter.DECIMAL_FORMAT3, oddsVal), textRender.NegativeFont);
            }
            else if (oddsVal == 0)
            {
                textRender.RTFRenderer.AddText("-", textRender.PositiveFont);
            }
            else
            {
                textRender.RTFRenderer.AddText(string.Format(Formatter.DECIMAL_FORMAT3, oddsVal), textRender.PositiveFont);
            }
        }
    }
}