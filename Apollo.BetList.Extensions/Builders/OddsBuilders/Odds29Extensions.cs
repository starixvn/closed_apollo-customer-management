// -----------------------------------------------------------------------
// <copyright file="Odds29Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.OddsBuilders
{
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Odds1 builder.
    /// </summary>
    public class Odds29Extensions : Odds1Extensions
    {
        /// <summary>
        /// Builds the odd column of bet type System Parlay to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the odds column.</returns>
        public override IRichTextString BuildRTF(ITicket ticket, OddsRTFHelper rtfHelper)
        {
            string _status = ticket.Status.ToLower();
            string oddsName = CacheRef.GetOddsTypeName(ConvertByBetType.OddsType(ticket.BetTypeId, ticket.OddsType));

            FormatOdds(ticket.MasterOdds, _status.Equals(BetStatus.Running), rtfHelper);

            if (!string.IsNullOrEmpty(oddsName))
            {
                rtfHelper.RTFRenderer.AddText(oddsName, rtfHelper.OddsNameFont);
            }

            return rtfHelper.RTFRenderer.Render();
        }

        /// <summary>
        /// Formats the specified odds.
        /// </summary>
        /// <param name="odds">The odds value.</param>
        /// <param name="isRunning">The ticket is running or not.</param>
        /// <param name="textRender">Text render helper.</param>
        private void FormatOdds(object odds, bool isRunning, OddsRTFHelper textRender)
        {
            decimal oddsVal = Cast.AsDecimal(odds);

            if (isRunning)
            {
                textRender.RTFRenderer.AddText("-", textRender.PositiveFont);
            }
            else if (oddsVal < 0)
            {
                textRender.RTFRenderer.AddText(string.Format(Formatter.DECIMALFORMAT3, oddsVal), textRender.NegativeFont);
            }
            else
            {
                textRender.RTFRenderer.AddText(string.Format(Formatter.DECIMALFORMAT3, oddsVal), textRender.PositiveFont);
            }
        }
    }
}