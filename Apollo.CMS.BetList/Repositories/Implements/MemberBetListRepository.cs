// --------------------------------------------------------------------------
// <copyright file="MemberBetListRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2014.1.15">Created</change>
// </history>

namespace Apollo.CMS.BetList.Repositories.Implements
{
    using System.Collections;
    using System.Collections.Generic;
    using Apollo.CMS.BetList.Criteria.MemberBetList;
    using Apollo.CMS.BetList.Repositories.Interfaces;
    using Fanex.BetList.Core.Entities;
    using Fanex.Common;
    using Fanex.Data;

    /// <summary>
    /// Member bet list repository.
    /// </summary>
    public class MemberBetListRepository : IMemberBetListRepository
    {
        /// <summary>
        /// Gets the system parlay data.
        /// </summary>
        /// <param name="criteria">The criteria that are use to query data.</param>
        /// <returns>SystemParlayData object wraps all needed data to generate system parlay details.</returns>
        public SystemParlayData GetSystemParlayData(GetSystemParlayDetailsCriteria criteria)
        {
            var systemParlayData = new SystemParlayData();
            Hashtable param = criteria.GetSpParams();

            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var multi = db.QueryMultiple(param))
                {
                    IEnumerable<SystemParlayTicket> systemParlayTickets = multi.Read<SystemParlayTicket>();
                    IEnumerable<SystemParlaySerial> systemParlaySerials = multi.Read<SystemParlaySerial>();

                    systemParlayData.TicketList = new List<ISystemParlayTicket>();
                    systemParlayData.SerialList = new List<ISystemParlaySerial>();

                    foreach (var ticket in systemParlayTickets)
                    {
                        systemParlayData.TicketList.Add(ticket);
                    }

                    foreach (var serial in systemParlaySerials)
                    {
                        systemParlayData.SerialList.Add(serial);
                    }

                    if (param.ContainsKey("outstanding"))
                    {
                        systemParlayData.Outstanding = Converter.ToDecimal(param["outstanding"]);
                    }
                }
            }

            return systemParlayData;
        }
    }
}