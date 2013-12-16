// --------------------------------------------------------------------------
// <copyright file="ITreeViewService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.TreeView.Interfaces
{
    using System.Collections.Generic;
    using Apollo.CMS.Account.Criteria.TreeView;
    using Apollo.CMS.Account.Models.TreeView;

    /// <summary>
    /// Tree view service interface.
    /// </summary>
    public interface ITreeViewService
    {
        List<TreeViewModel> GetDataTreeView(GetAllAgentAndAgentDownlineByIdCriteria criteria);

        List<TreeViewModel> GetDataTreeViewBranch(GetAgentDownlineByIdCriteria criteria);
    }
}