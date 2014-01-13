// -----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.29">Created</change>
// </history>

namespace Apollo.CMS.Core.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Apollo.CMS.Core.Criteria;
    using Apollo.CMS.Core.Models;
    using Apollo.CMS.Core.Web;
    using Fanex.Common.Helper;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Base Service.
    /// </summary>
    public class BaseService
    {
        public const string CachedBalanceUp = "Apollo.ParlayRMS.Core.BalanceUpService.BalanceUp";
        protected readonly IDynamicRepository DynamicRepository;

        public BaseService(IDynamicRepository repository = null)
        {
            DynamicRepository = repository ?? new DynamicRepository();
        }

        protected IDynamicRepository Repository
        {
            get
            {
                return DynamicRepository;
            }
        }

        protected BalanceUp BalanceUp
        {
            get
            {
                var balanceUp = CacheManager.Get<BalanceUp>(CachedBalanceUp);

                if (balanceUp == null)
                {
                    var criteria = new BalanceUpCriteria();
                    balanceUp = DynamicRepository.Get<BalanceUp>(criteria);
                    CacheManager.Set<BalanceUp>(CachedBalanceUp, balanceUp, new TimeSpan(0, 0, 5, 0));
                }

                return balanceUp;
            }
        }

        /// <summary>
        /// Gets the list data.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The list data.</returns>
        public virtual IEnumerable<T> GetListData<T>(ICriteria criteria)
        {
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                return DynamicRepository.Fetch<T>(criteria);
            }
            else
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Gets the list of data and total items.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="criteria">The criteria that are used to query data.</param>
        /// <param name="totalItems">Total items.</param>
        /// <returns>The list of data.</returns>
        public virtual IEnumerable<T> GetListData<T>(ICriteria criteria, out int totalItems)
        {
            IEnumerable<T> result = null;

            Check.Argument.IsNotNull(criteria, "criteria");
            totalItems = 0;

            using (IObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                Hashtable outputParam = null;

                result = db.Query<T>(criteria.GetSpParams());
                outputParam = db.GetOuputParams();

                if (outputParam.ContainsKey("totalitems"))
                {
                    totalItems = (int)outputParam["totalitems"];
                }
            }

            result = result ?? new List<T>();

            return result;
        }

        /// <summary>
        /// Gets data.
        /// </summary>
        /// <typeparam name="T">Generic type.</typeparam>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The data item.</returns>
        public virtual T GetData<T>(ICriteria criteria) where T : new()
        {
            Check.Argument.IsNotNull(criteria, "criteria");

            if (criteria.IsValid())
            {
                return DynamicRepository.Get<T>(criteria);
            }
            else
            {
                return new T();
            }
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The query result.</returns>
        public ICommandResult UpdateData(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                var param = criteria.GetSpParams();

                db.ExecuteNonQuery(param);

                return GetCommandResult(param);
            }
        }

        /// <summary>
        /// Gets the command result.
        /// </summary>
        /// <param name="hash">The hash table.</param>
        /// <returns>CommandResult object contains error code and error message.</returns>
        private CommandResult GetCommandResult(Hashtable hash)
        {
            var errCode = hash["errcode"];
            var errMsg = hash["errmsg"];

            return new CommandResult
            {
                Code = (errCode is DBNull) ? 0 : Convert.ToInt32(errCode),
                Message = (errMsg is DBNull) ? string.Empty : Convert.ToString(errMsg)
            };
        }
    }
}