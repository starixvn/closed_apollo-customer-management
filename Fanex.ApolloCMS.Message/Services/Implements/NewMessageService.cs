// --------------------------------------------------------------------------
// <copyright file="NewMessageService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.13">Created</change>
// </history>

namespace Fanex.ApolloCMS.Message.Services.Implements
{
    using System.Collections.Generic;
    using System.Text;
    using Fanex.ApolloCMS.Core.Services;
    using Fanex.ApolloCMS.Message.Criteria.NewMessage;
    using Fanex.ApolloCMS.Message.Models.NewMessage;
    using Fanex.ApolloCMS.Message.Repositories.Implements;
    using Fanex.ApolloCMS.Message.Repositories.Interfaces;
    using Fanex.ApolloCMS.Message.Services.Interfaces;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// New message service.
    /// </summary>
    public class NewMessageService : BaseService, INewMessageService
    {
        private readonly INewMessageRepository _newMessageRepository;

        public NewMessageService() :
            this(null)
        {
        }

        public NewMessageService(INewMessageRepository newMessageRepository)
        {
            this._newMessageRepository = newMessageRepository ?? new NewMessageRepository();
        }

        /// <summary>
        /// Get site info for agent and member site.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of available sites.</returns>
        public IEnumerable<Site> GetAllSites(GetAllSitesCriteria criteria)
        {
            return this._newMessageRepository.GetAllSites(criteria);
        }

        /// <summary>
        /// Gets a specific recipient's information.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>The information of the recipient who has the ID as the passed one.</returns>
        public RecipientInfo GetRecipientInfo(GetRecipientInfoCriteria criteria)
        {
            return this._newMessageRepository.GetRecipientInfo(criteria);
        }

        /// <summary>
        /// Gets up line agents.
        /// </summary>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <returns>List of up line agents.</returns>
        public IEnumerable<Upline> GetUplineById(GetUplineByIdCriteria criteria)
        {
            return this._newMessageRepository.GetUplineById(criteria);
        }

        /// <summary>
        /// Inserts a new message.
        /// </summary>
        /// <param name="criteria">The criteria contains new message's values.</param>
        /// <returns>CommandResult object indicates whether the new message is added successfully or not.</returns>
        public ICommandResult InsertMessage(InsertMessageCriteria criteria)
        {
            criteria.Recipient = ConvertRecipientListToXml(criteria.RecipientList);

            return this._newMessageRepository.InsertMessage(criteria);
        }

        /// <summary>
        /// Converts the recipient list to XML.
        /// </summary>
        /// <param name="recipients">The recipient list.</param>
        /// <returns>XML string represents the recipient list.</returns>
        private string ConvertRecipientListToXml(IEnumerable<Recipient> recipients)
        {
            StringBuilder stringBuilder = new StringBuilder("<ROOT>");
            string recipientNode = "<Recipient TargetId=\"{0}\" TargetName=\"{1}\" IsAgent=\"{2}\"></Recipient>";

            foreach (var item in recipients)
            {
                stringBuilder.AppendFormat(
                                  recipientNode,
                                  item.TargetId,
                                  item.TargetName,
                                  item.IsAgent == true ? 1 : 0);

                if (item.InformUpline)
                {
                    var uplines = this.GetUplineById(new GetUplineByIdCriteria
                    {
                        Id = item.TargetId,
                        IsAgent = item.IsAgent
                    });

                    foreach (var upline in uplines)
                    {
                        stringBuilder.AppendFormat(
                                  recipientNode,
                                  upline.TargetId,
                                  upline.TargetName,
                                  1);
                    }
                }
            }

            stringBuilder.Append("</ROOT>");

            return stringBuilder.ToString();
        }
    }
}