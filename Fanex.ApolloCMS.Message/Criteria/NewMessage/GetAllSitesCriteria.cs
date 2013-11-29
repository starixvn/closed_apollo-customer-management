// --------------------------------------------------------------------------
// <copyright file="GetAllSitesCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Criteria.NewMessage
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Gets all sites criteria.
    /// </summary>
    public class GetAllSitesCriteria : CriteriaBase
    {
        /// <summary>
        /// Get Setting Key.
        /// </summary>
        /// <returns>Setting key of the criteria.</returns>
        public override string GetSettingKey()
        {
            return "Acc_Msg_GetAllSite";
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