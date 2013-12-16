// --------------------------------------------------------------------------
// <copyright file="TreeViewViewModel.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Terry.Nguyen" date="2013.12.6">Created</change>
// </history>

namespace Apollo.CMS.Account.Models.TreeView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Tree view view model.
    /// </summary>
    public class TreeViewViewModel : List<TreeViewViewModel.Node>
    {
        /// <summary>
        /// Tree node class.
        /// </summary>
        public class Node
        {
            public int Id { get; set; }

            public int ParentId { get; set; }

            public string Username { get; set; }

            public bool HasAgentDownline { get; set; }

            public TreeViewViewModel Children { get; set; }

            public bool Expanded { get; set; }

            public string Classes { get; set; }
        }
    }
}
