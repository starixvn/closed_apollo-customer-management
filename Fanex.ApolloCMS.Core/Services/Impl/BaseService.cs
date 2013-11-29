// -----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.29">Created</change>
// </history>

namespace Fanex.ApolloCMS.Core.Services.Impl
{
    using System.Collections.Generic;
    using Fanex.ApolloCMS.Core.Repositories;
    using Fanex.ApolloCMS.Core.Services.Interface;
    using Fanex.Common.Helper;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class BaseService : BaseRepository, IBaseService
    {
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
    }
}