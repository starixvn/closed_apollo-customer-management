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
        /// <returns>Action result.</returns>
        public ActionResult Index()
        {
            ViewBag.AgentId = 579;
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
        /// <returns>JSON result.</returns>
        public JsonResult CreateTreeView(int agentId)
        {
            var criteria = new GetAllAgentAndAgentDonwlineByIdCriteria()
            {
                AgentId = agentId
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
                bool isTheFirst = true;
                foreach (var parentId in listParentId)
                {
                    var child = new TreeViewViewModel();

                    var parentUsername = hierarchyTrees.Where(x => x.Id == parentId).FirstOrDefault();

                    var treeNode = new TreeViewViewModel.Node();
                    if (isTheFirst)
                    {
                        dataTreeView.Where(y => y.ParentId == parentId.Value).ToList().ForEach(x => child.Add(new TreeViewViewModel.Node
                        {
                            Id = x.AgentId.Value,
                            Username = x.Username,
                            HasAgentDownline = x.HasAgentDownline.Value,
                            Classes = x.AgentId == agentId ? "hover" : string.Empty
                        }));

                        isTheFirst = false;
                    }
                    else
                    {
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
                                });
                            }
                        }
                    }

                    children = new TreeViewViewModel.Node()
                    {
                        Children = child,
                        Id = parentId.Value,
                        Username = parentUsername.Username,
                        HasAgentDownline = parentUsername.HasAgentDownline,
                        Expanded = child != null && child.Any() ? true : false
                    };

                    childrenNodes = children;
                }

                var root = new TreeViewViewModel.Node()
                {
                    Id = rootData.AgentId.Value,
                    Username = rootData.Username,
                    HasAgentDownline = rootData.HasAgentDownline.Value,
                    Expanded = true,
                    Children = childrenNodes.Children
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