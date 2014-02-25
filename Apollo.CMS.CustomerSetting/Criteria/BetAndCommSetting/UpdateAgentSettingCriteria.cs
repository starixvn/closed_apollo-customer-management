// --------------------------------------------------------------------------
// <copyright file="UpdateAgentSettingCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.2.24">Created</change>
// </history>

namespace Apollo.CMS.CustomerSetting.Criteria.BetAndCommSetting
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// The criteria contain updated setting data of an agent.
    /// </summary>
    public class UpdateAgentSettingCriteria : CriteriaBase
    {
        public int ActorAgentId { get; set; }

        public int AdminId { get; set; }

        public int AgentId { get; set; }

        public string BetLimit { get; set; }

        public string Discount { get; set; }

        public string PositionTaking { get; set; }

        public string Language { get; set; }

        public bool AllowUpdateMinBet { get; set; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Age_Agent_UpdateSetting";
        }

        /// <summary>
        /// Check validation of the criteria.
        /// </summary>
        /// <returns>Whether or not the criteria is valid.</returns>
        public override bool IsValid()
        {
            return true;
        }
    }
}