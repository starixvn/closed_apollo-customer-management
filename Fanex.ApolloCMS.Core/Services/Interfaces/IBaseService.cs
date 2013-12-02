// -----------------------------------------------------------------------
// <copyright file="IBaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Franklin.Nguyen" date="2013.11.29">Created</change>
// </history>

namespace Fanex.ApolloCMS.Core.Services.Interfaces
{
    using System.Collections.Generic;
    using Fanex.NAP.Framework.Data;

    public interface IBaseService
    {
        IEnumerable<T> GetListData<T>(ICriteria criteria);

        T GetData<T>(ICriteria criteria) where T : new();

        ICommandResult UpdateData(ICriteria criteria);
    }
}
