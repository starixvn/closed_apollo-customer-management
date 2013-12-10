// --------------------------------------------------------------------------
// <copyright file="TreeViewController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.4">Created</change>
// </history>

namespace Apollo.CMS.Account.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using Apollo.CMS.Account.Criteria.TreeView;
    using Apollo.CMS.Account.Models.TreeView;
    using Apollo.CMS.Account.Services.TreeView.Implements;
    using Apollo.CMS.Core.Controllers;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TreeViewController : BaseController
    {
        private readonly TreeViewService _treeViewServicer;

        public TreeViewController()
        {
            _treeViewServicer = new TreeViewService();
        }

        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// Action result.
        /// </returns>
        public ActionResult Index(int? agentId = null, string username = null)
        {
            ViewBag.AgentId = agentId;
            ViewBag.Username = username;

            return View();
        }

        /// <summary>
        /// Creates the TreeView by node.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <returns>JSON result.</returns>
        public JsonResult CreateTreeViewByNode(int agentId)
        {
            var criteria = new GetAgentDownlineByIdCriteria()
            {
                AgentId = agentId
            };

            var dataTreeView = _treeViewServicer.GetDataTreeViewBranch(criteria);
            var rootData = new TreeViewModel();
            var rootList = new TreeViewViewModel();
            if (dataTreeView != null && dataTreeView.Any())
            {
                TreeViewViewModel hierarchyTrees = new TreeViewViewModel();
                TreeViewViewModel.Node objHTree = null;

                foreach (var node in dataTreeView)
                {
                    objHTree = new TreeViewViewModel.Node()
                    {
                        Id = node.AgentId.Value,
                        Username = node.Username,
                        HasAgentDownline = node.HasAgentDownline.Value,
                        ParentId = node.ParentId.HasValue ? node.ParentId.Value : 0,
                        Children = null
                    };

                    hierarchyTrees.Add(objHTree);
                }

                return Json(hierarchyTrees, JsonRequestBehavior.AllowGet);
            }

            return Json(new TreeViewViewModel(), JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Creates the TreeView.
        /// </summary>
        /// <param name="agentId">The agent identifier.</param>
        /// <param name="username">The username.</param>
        /// <returns>
        /// JSON result.
        /// </returns>
        [SuppressMessage(
            "StyleCop.CSharp.Nexcel.NexcelCustomRules",
            "SP2101:MethodMustNotContainMoreLinesThan",
            Justification = "Reviewed. Suppression is OK here.")]
        public JsonResult CreateTreeView(int? agentId = null, string username = null)
        {
            var criteria = new GetAllAgentAndAgentDonwlineByIdCriteria()
            {
                AgentId = agentId,
                Username = string.IsNullOrWhiteSpace(username) ? null : username
            };

            var dataTreeView = _treeViewServicer.GetDataTreeView(criteria);
            var rootData = new TreeViewModel();
            var rootList = new TreeViewViewModel();
            if (dataTreeView != null && dataTreeView.Any())
            {
                rootData = dataTreeView.Where(x => x.AgentLevel == 0).FirstOrDefault();

                TreeViewViewModel hierarchyTrees = HierarchyTrees(dataTreeView);

                var listParentId = dataTreeView.Where(y => y.ParentId != null).Select(x => x.ParentId).Distinct().Reverse();
                var children = new TreeViewViewModel.Node();
                var childrenNodes = new TreeViewViewModel.Node();
                var child = new TreeViewViewModel();

                foreach (var parentId in listParentId)
                {
                    child = new TreeViewViewModel();

                    var parentUsername = hierarchyTrees.Where(x => x.Id == parentId).FirstOrDefault();

                    var childData = dataTreeView.Where(y => y.ParentId == parentId.Value);

                    foreach (var item in childData)
                    {
                        if (item.AgentId == childrenNodes.Id)
                        {
                            child.Add(childrenNodes);
                        }
                        else
                        {
                            child.Add(new TreeViewViewModel.Node
                            {
                                Id = item.AgentId.Value,
                                Username = item.Username,
                                HasAgentDownline = item.HasAgentDownline.Value,
                                Classes = item.AgentId != agentId && item.Username != username ? string.Empty : "nodeactived"
                            });
                        }
                    }

                    children = new TreeViewViewModel.Node()
                    {
                        Children = child,
                        Id = parentId.Value,
                        Username = parentUsername.Username,
                        HasAgentDownline = parentUsername.HasAgentDownline,
                        Expanded = child != null && child.Any() ? true : false,
                    };

                    childrenNodes = children;
                }

                var root = new TreeViewViewModel.Node()
                {
                    Id = rootData.AgentId.Value,
                    Username = rootData.Username,
                    HasAgentDownline = rootData.HasAgentDownline.Value,
                    Expanded = listParentId != null && listParentId.Any() ? true : false,
                    Children = childrenNodes.Children,
                    Classes = listParentId != null && listParentId.Any() ? string.Empty : "nodeactived"
                };

                rootList.Add(root);
            }

            return Json(rootList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Hierarchies the trees.
        /// </summary>
        /// <param name="dataTreeView">The data TreeView.</param>
        /// <returns>Tree View View Model. </returns>
        private static TreeViewViewModel HierarchyTrees(System.Collections.Generic.List<TreeViewModel> dataTreeView)
        {
            TreeViewViewModel hierarchyTrees = new TreeViewViewModel();
            TreeViewViewModel.Node objHTree = null;

            foreach (var node in dataTreeView)
            {
                objHTree = new TreeViewViewModel.Node()
                {
                    Id = node.AgentId.Value,
                    Username = node.Username,
                    HasAgentDownline = node.HasAgentDownline.Value,
                    ParentId = node.ParentId.HasValue ? node.ParentId.Value : 0,
                    Children = null
                };

                hierarchyTrees.Add(objHTree);
            }

            return hierarchyTrees;
        }
    }
}