// --------------------------------------------------------------------------
// <copyright file="TreeViewService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Services.TreeView.Implements
{
    using System.Collections.Generic;
    using Apollo.CMS.Account.Criteria.TreeView;
    using Apollo.CMS.Account.Models.TreeView;
    using Apollo.CMS.Account.Services.TreeView.Interfaces;
    using Apollo.CMS.Core.Services.Implements;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TreeViewService : BaseService, ITreeViewService
    {
        public IEnumerable<TreeViewModel> GetDataTreeView(TreeViewCriteria criteria)
        {            
            return GetListData<TreeViewModel>(criteria);
        }
    }
}