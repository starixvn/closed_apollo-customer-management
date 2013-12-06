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
    using Apollo.CMS.Account.Services.TreeView.Implements;
    using Apollo.CMS.Core.Controllers;
    using System.Collections.Generic;
    using Apollo.CMS.Account.Models.TreeView;

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

        public ActionResult Index()
        {
            ViewBag.AgentId = 62;
            return View();
        }

        public JsonResult CreateTreeView()
        {
            var criteria = new TreeViewCriteria()
            {
                AgentId = 6741
            };

            var dataTreeView = _treeViewServicer.GetDataTreeView(criteria);

            TreeViewViewModel hierarchyTrees = new TreeViewViewModel();
            TreeViewViewModel.TreeView objHTree = null;

            foreach (var node in dataTreeView)
            {
                objHTree = new TreeViewViewModel.TreeView();  


            }


            //var children = new List<TreeViewViewModel>();

            //var listChildren = new Dictionary<int, List<TreeViewViewModel>>();
            //var rootData = new TreeViewModel();


            //if (dataTreeView != null && dataTreeView.Any())
            //{
            //    var parentIds = dataTreeView.Where(p => p.ParentId != null).Select(x => x.ParentId).Distinct();

            //    var listParent = dataTreeView.Where(x => parentIds.Contains(x.AgentId.Value));

            //    rootData = dataTreeView.Where(x => x.AgentLevel == 0).FirstOrDefault();

            //    foreach (var item in parentIds)
            //    {
            //        var childrenByParentId = dataTreeView.Where(x => x.ParentId == item.Value);
            //        children = new List<TreeViewViewModel>();
            //        foreach (var child in childrenByParentId)
            //        {
            //            children.Add(new TreeViewViewModel
            //            {
            //                Id = child.AgentId.Value,
            //                Username = child.Username,
            //                HasAgentDownline = child.HasAgentDownline.Value,
            //                Children = null
            //            });
            //        }

            //        listChildren.Add(item.Value, children);
            //    }




            //    var root = new TreeViewViewModel();
            //    children = listChildren.Where(x => x.Key == rootData.AgentId).Select(y => y.Value).FirstOrDefault();
            //    root = new TreeViewViewModel()
            //    {
            //        Id = rootData.AgentId.Value,
            //        Username = rootData.Username,
            //        Children = CreateChildNode(children, listChildren),
            //        Expanded = true
            //    };

            //    var rootList = new List<TreeViewViewModel>();
            //    rootList.Add(root);

            //    return Json(tree, JsonRequestBehavior.AllowGet);
            //}

            return Json(new List<TreeViewViewModel>(), JsonRequestBehavior.AllowGet);
        }

        //private List<TreeViewViewModel> CreateChildNode(List<TreeViewViewModel> listOfChildren, Dictionary<int, List<TreeViewViewModel>> listOfParents)
        //{
        //    var child = new List<TreeViewViewModel>();
        //    var hasAgentDownline = false;
        //    if (listOfChildren != null && listOfChildren.Any())
        //    {
        //        var listOfChild = new List<TreeViewViewModel>();
        //        foreach (var children in listOfChildren)
        //        {
        //            if (listOfParents.Keys.Contains(children.Id))
        //            {
        //                listOfChild = listOfParents[children.Id];
        //                child.Add(new TreeViewViewModel
        //                {
        //                    Id = children.Id,
        //                    Username = children.Username,
        //                    HasAgentDownline = children.HasAgentDownline,
        //                    Children = children.HasAgentDownline ? listOfChild : null
        //                });

        //                hasAgentDownline = children.HasAgentDownline;
        //            }
        //        }

        //        return hasAgentDownline ? CreateChildNode(child[0].Children, listOfParents) : null;
        //    }

        //    return child;
        //}

    }
}