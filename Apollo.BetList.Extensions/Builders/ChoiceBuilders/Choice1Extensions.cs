// -----------------------------------------------------------------------
// <copyright file="Choice1Extensions.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Builders.ChoiceBuilders
{
    using System.Diagnostics.CodeAnalysis;
    using Apollo.BetList.Extensions.Entities;
    using Apollo.BetList.Extensions.RTFHelpers;
    using Fanex.BetList.Core.Builder.ChoiceBuilder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Extensions for Choice1 builder.
    /// </summary>
    public class Choice1Extensions : Choice1
    {
        protected const string FirstGoal = "(F)";
        protected const string LastGoal = "(L)";
        protected const string Bet1HFirstGoal = "(1F)";
        protected const string Bet1HLastGoal = "(1L)";

        /// <summary>
        /// Builds the choice column of bet type Handicap to Excel format.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">Ticket helper.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        /// <returns>IRichTextString object indicates the choice column.</returns>
        public virtual IRichTextString BuildRTF(ITicket ticket, ITicketHelper ticketHelper, ChoiceRTFHelper rtfHelper)
        {
            ChoiceWrapper choice = BuildChoiceWrapper(ticket, ticketHelper);

            BuildRTFChoice(choice, ticket, ref rtfHelper);

            return rtfHelper.RTFRenderer.Render();
        }

        /// <summary>
        /// Builds the choice wrapper.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>ChoiceWrapper object.</returns>
        protected ChoiceWrapper BuildChoiceWrapper(ITicket ticket, ITicketHelper ticketHelper)
        {
            ChoiceWrapper choice = new ChoiceWrapper();

            bool isBetHomeTeam = ticket.BetTeam != null && ticket.BetTeam.Contains("h");
            bool isFavorite = ticket.Handicap1 > ticket.Handicap2;
            decimal handicap = ticket.Handicap2 > ticket.Handicap1 ? ticket.Handicap2 : ticket.Handicap1;

            if (!isBetHomeTeam)
            {
                isFavorite = ticket.Handicap1 < ticket.Handicap2;
            }

            choice.LeagueName = ticketHelper.GetLeagueNameById(ticket.LeagueId);
            choice.SportName = ticketHelper.GetSportNameById(ticket.SportTypeId);
            choice.HomeName = ticketHelper.GetTeamNameById(ticket.HomeId).Trim();
            choice.AwayName = ticketHelper.GetTeamNameById(ticket.AwayId).Trim();
            choice.BetTypeName = ticketHelper.GetBetTypeNameById(ticket.BetTypeId);
            choice.BetTeam = isBetHomeTeam ? choice.HomeName : choice.AwayName;
            choice.IsFavorite = isFavorite;
            choice.Handicap = ConvertByBetType.Hdp(isFavorite ? -handicap : handicap);
            choice.Odds = GetHandicapOdds(ticket, ticketHelper);
            choice.LiveScore = string.Format("[{0}-{1}]", ticket.LiveHomeScore, ticket.LiveAwayScore);
            choice.HomeFirstLastGoal = new string[2] { null, null };
            choice.Home1HFirstLastGoal = new string[2] { null, null };
            choice.AwayFirstLastGoal = new string[2] { null, null };
            choice.Away1HFirstLastGoal = new string[2] { null, null };
            choice.IsShowMatch = true;

            choice.HomeName = string.Format("{0}{1}", choice.HomeName, ticket.IsNeutral ? " (N)" : string.Empty);

            if (ticket.EventStatus != null)
            {
                string[] eventStatus = ticket.EventStatus.Split(';');

                if (eventStatus.Length == 1)
                {
                    this.GetFGLG(eventStatus[0], choice.HomeFirstLastGoal, choice.AwayFirstLastGoal, FirstGoal, LastGoal);
                }
                else if (eventStatus.Length == 2)
                {
                    this.GetFGLG(eventStatus[0], choice.Home1HFirstLastGoal, choice.Away1HFirstLastGoal, Bet1HFirstGoal, Bet1HLastGoal);
                    this.GetFGLG(eventStatus[1], choice.HomeFirstLastGoal, choice.AwayFirstLastGoal, FirstGoal, LastGoal);
                }
            }

            return choice;
        }

        /// <summary>
        /// Builds the choice column of bet type Handicap to Excel format.
        /// </summary>
        /// <param name="choice">Choice wrapper.</param>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="rtfHelper">The RTF helper.</param>
        [SuppressMessage("StyleCop.CSharp.Nexcel.NexcelCustomRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Need to review here.")]
        protected void BuildRTFChoice(ChoiceWrapper choice, ITicket ticket, ref ChoiceRTFHelper rtfHelper)
        {
            rtfHelper.RTFRenderer.AddText(choice.BetTeam, choice.IsFavorite ? rtfHelper.FavoriteFont : rtfHelper.UnderdogFont);
            if (!string.IsNullOrEmpty(choice.Handicap))
            {
                rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.Handicap), rtfHelper.HandicapFont);
                rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.Odds), rtfHelper.OddsFont);
            }

            if (ticket.IsLive)
            {
                rtfHelper.RTFRenderer.AddText(string.Format(" {0}\n", choice.LiveScore), rtfHelper.LiveScoreFont);
            }
            else
            {
                rtfHelper.RTFRenderer.AddText("\n", rtfHelper.OddsFont);
            }

            rtfHelper.RTFRenderer.AddText(string.Format("{0}\n", choice.BetTypeName), rtfHelper.BetTypeFont);
            if (choice.IsShowMatch)
            {
                rtfHelper.RTFRenderer.AddText(choice.HomeName, rtfHelper.HomeTeamFont);

                if (!string.IsNullOrEmpty(choice.HomeFirstLastGoal[0]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.HomeFirstLastGoal[0]), rtfHelper.FirstGoalFont);
                }

                if (!string.IsNullOrEmpty(choice.HomeFirstLastGoal[1]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.HomeFirstLastGoal[1]), rtfHelper.LastGoalFont);
                }

                if (!string.IsNullOrEmpty(choice.Home1HFirstLastGoal[0]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.Home1HFirstLastGoal[0]), rtfHelper.FirstGoalFont);
                }

                if (!string.IsNullOrEmpty(choice.Home1HFirstLastGoal[1]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.Home1HFirstLastGoal[1]), rtfHelper.LastGoalFont);
                }

                rtfHelper.RTFRenderer.AddText(" -vs- ", rtfHelper.VsFont);
                rtfHelper.RTFRenderer.AddText(choice.AwayName, rtfHelper.AwayTeamFont);
                if (!string.IsNullOrEmpty(choice.AwayFirstLastGoal[0]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.AwayFirstLastGoal[0]), rtfHelper.FirstGoalFont);
                }

                if (!string.IsNullOrEmpty(choice.AwayFirstLastGoal[1]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.AwayFirstLastGoal[1]), rtfHelper.LastGoalFont);
                }

                if (!string.IsNullOrEmpty(choice.Away1HFirstLastGoal[0]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.Away1HFirstLastGoal[0]), rtfHelper.FirstGoalFont);
                }

                if (!string.IsNullOrEmpty(choice.Away1HFirstLastGoal[1]))
                {
                    rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.Away1HFirstLastGoal[1]), rtfHelper.LastGoalFont);
                }

                rtfHelper.RTFRenderer.AddText("\n", rtfHelper.AwayTeamFont);
            }

            rtfHelper.RTFRenderer.AddText(choice.SportName, rtfHelper.SportFont);
            rtfHelper.RTFRenderer.AddText(string.Format(" {0}", choice.LeagueName), rtfHelper.LeagueFont);
        }

        /// <summary>
        /// Gets the First Goal/Last Goal of teams.
        /// </summary>
        /// <param name="betStatus">The bet status.</param>
        /// <param name="homeFGLG">The home First Goal/Last Goal string.</param>
        /// <param name="awayFGLG">The away First Goal/Last Goal string.</param>
        /// <param name="firstGoalLabel">The label for First Goal.</param>
        /// <param name="lastGoalLabel">The label for Last Goal.</param>
        private void GetFGLG(string betStatus, string[] homeFGLG, string[] awayFGLG, string firstGoalLabel, string lastGoalLabel)
        {
            if (betStatus != null)
            {
                // Format: FL:1-2 or 1FL:1-2
                string[] args = betStatus.Split(new char[] { ':', '-' });

                if (args.Length == 3)
                {
                    if (args[1] == "1")
                    {
                        homeFGLG[0] = firstGoalLabel;
                    }
                    else if (args[1] == "2")
                    {
                        awayFGLG[0] = firstGoalLabel;
                    }

                    if (args[2] == "1")
                    {
                        homeFGLG[1] = lastGoalLabel;
                    }
                    else if (args[2] == "2")
                    {
                        awayFGLG[1] = lastGoalLabel;
                    }
                }
            }
        }

        /// <summary>
        /// Gets the handicap odds.
        /// </summary>
        /// <param name="ticket">The ticket data.</param>
        /// <param name="ticketHelper">The ticket helper.</param>
        /// <returns>Odds value.</returns>
        private string GetHandicapOdds(ITicket ticket, ITicketHelper ticketHelper)
        {
            string result = string.Empty;

            if (ticketHelper.IsShowOddsWithChoice)
            {
                string oddsType = string.IsNullOrEmpty(ticket.OddsType) ? "0" : ticket.OddsType;
                string odds = ConvertByBetType.Odds(ticket.Odds, ticket.BetTypeId, oddsType);

                if (!odds.Equals("-"))
                {
                    result = odds;
                }
            }

            return result;
        }
    }
}