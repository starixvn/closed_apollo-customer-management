// --------------------------------------------------------------------------
// <copyright file="INewMessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Apollo.CMS.Message.Services.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.Message.Criteria.NewMessage;
    using Apollo.CMS.Message.Models.NewMessage;
    using Apollo.CMS.Message.ViewModels.NewMessage;
    using Fanex.NAP.Framework.Command;

    public interface INewMessageService
    {
        /// <summary>
        /// Get site info for agent and member site.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of available sites.</returns>
        IEnumerable<Site> GetAllSites(GetAllSitesCriteria criteria);

        /// <summary>
        /// Gets a specific recipient's information.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The information of the recipient who has the ID as the passed one.</returns>
        RecipientInfo GetRecipientInfo(GetRecipientInfoCriteria criteria);

        /// <summary>
        /// Gets up line agents.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of up line agents.</returns>
        IEnumerable<Upline> GetUplineById(GetUplineByIdCriteria criteria);

        /// <summary>
        /// Inserts a new message.
        /// </summary>
        /// <param name="form">The form contains new message's values.</param>
        /// <returns>CommandResult object indicates whether the new message is added successfully or not.</returns>
        ICommandResult InsertMessage(InsertNewMessageForm form);
    }
}