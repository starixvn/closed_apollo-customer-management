// --------------------------------------------------------------------------
// <copyright file="NewMessageRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Repositories.Implements
{
    using System.Collections.Generic;
    using Fanex.ApolloCMS.Core.Repositories;
    using Fanex.ApolloCMS.Message.Criteria.NewMessage;
    using Fanex.ApolloCMS.Message.Models.NewMessage;
    using Fanex.ApolloCMS.Message.Repositories.Interfaces;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// New messages repository.
    /// </summary>
    public class NewMessageRepository : BaseRepository, INewMessageRepository
    {
        /// <summary>
        /// Get site info for agent and member site.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of available sites.</returns>
        public IEnumerable<Site> GetAllSites(GetAllSitesCriteria criteria)
        {
            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();

                return db.Query<Site>(param);
            }
        }

        /// <summary>
        /// Gets a specific recipient's information.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The information of the recipient who has the ID as the passed one.</returns>
        public RecipientInfo GetRecipientInfo(GetRecipientInfoCriteria criteria)
        {
            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();

                return db.QueryEntity<RecipientInfo>(param);
            }
        }

        /// <summary>
        /// Gets up line agents.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of up line agents.</returns>
        public IEnumerable<Upline> GetUplineById(GetUplineByIdCriteria criteria)
        {
            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();

                return db.Query<Upline>(param);
            }
        }

        /// <summary>
        /// Inserts a new message.
        /// </summary>
        /// <param name="criteria">The criteria contains new message's values.</param>
        /// <returns>CommandResult object indicates whether the new message is added successfully or not.</returns>
        public ICommandResult InsertMessage(InsertMessageCriteria criteria)
        {
            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();
                db.ExecuteNonQuery(param);

                return GetCommandResult(param);
            }
        }
    }
}