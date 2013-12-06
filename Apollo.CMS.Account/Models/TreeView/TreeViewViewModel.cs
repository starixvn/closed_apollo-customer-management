// -----------------------------------------------------------------------
// <copyright file="TreeViewViewModel.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Apollo.CMS.Account.Models.TreeView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class TreeViewViewModel : List<TreeViewViewModel.TreeView>
    {
        public class TreeView
        {
            public int Id { get; set; }

            public string Username { get; set; }

            public bool HasAgentDownline { get; set; }

            public List<TreeViewViewModel> Children { get; set; }

            public bool Expanded { get; set; }

            public string Classes { get; set; }
        }
    }
}
