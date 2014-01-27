// -----------------------------------------------------------------------
// <copyright file="ChoiceRTFHelper.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.24">Created</change>
// </history>

namespace Apollo.BetList.Extensions.RTFHelpers
{
    using Fanex.BetList.Core.Utils;
    using NPOI.SS.UserModel;

    /// <summary>
    /// Choice RTF Helper.
    /// </summary>
    public class ChoiceRTFHelper
    {
        public ChoiceRTFHelper()
        {
            RTFRenderer = new RtfTextRender();
        }

        public IFont FavoriteFont { get; set; }

        public IFont UnderdogFont { get; set; }

        public IFont HandicapFont { get; set; }

        public IFont OddsFont { get; set; }

        public IFont LiveScoreFont { get; set; }

        public IFont BetTypeFont { get; set; }

        public IFont HomeTeamFont { get; set; }

        public IFont VsFont { get; set; }

        public IFont AwayTeamFont { get; set; }

        public IFont FirstGoalFont { get; set; }

        public IFont LastGoalFont { get; set; }

        public IFont SportFont { get; set; }

        public IFont LeagueFont { get; set; }

        public RtfTextRender RTFRenderer { get; set; }
    }
}