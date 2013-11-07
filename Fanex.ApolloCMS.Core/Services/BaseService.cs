// --------------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Jason.Ly" date="2013.11.04">Created</change>
// </history>

namespace Fanex.ApolloCMS.Core.Services
{
    using System;
    using Fanex.ApolloCMS.Core.Resources;
    using Fanex.Common.Helper;
    using Fanex.NAP.Core.Criteria.Misc;
    using Fanex.NAP.Core.Service.Interface;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Service Base.
    /// </summary>
    public class BaseService
    {
        public IServiceContext ServiceContext { get; set; }

        public CommandResult InternalError
        {
            get
            {
                return new CommandResult
                {
                    Code = 500,
                    Message = Messages.InternalError
                };
            }
        }
    }
}
