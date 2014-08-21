// -----------------------------------------------------------------------
// <copyright file="ChoiceWrapper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.Entities
{
    /// <summary>
    /// Wrapper for choice column.
    /// </summary>
    public class ChoiceWrapper
    {
        public string BetTeam { get; set; }

        public bool IsFavorite { get; set; }

        public string Handicap { get; set; }

        public string Odds { get; set; }

        public string LiveScore { get; set; }

        public string BetTypeName { get; set; }

        public bool IsShowMatch { get; set; }

        public string HomeName { get; set; }

        public string[] HomeFirstLastGoal { get; set; }

        public string[] Home1HFirstLastGoal { get; set; }

        public string AwayName { get; set; }

        public string[] AwayFirstLastGoal { get; set; }

        public string[] Away1HFirstLastGoal { get; set; }

        public string SportName { get; set; }

        public string LeagueName { get; set; }
    }
}