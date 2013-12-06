Namespace.Register("Apollo.CMS.Account.TreeView");

Apollo.CMS.Account.TreeView = function () {

    var self = this;

    self.InitPage = function () {
        self.LoadTreeView();
    };

    self.LoadTreeView = function () {
        var accountTreeView = $("#account-treeview");
        var rootId = accountTreeView.attr("data-agentid");
        accountTreeView.treeview({
            url: "TreeView/CreateTreeViewByNode",
            link: "<span href='#' data-agentid='{1}' data-username='{2}'>{0}</span>",            
            agentId: rootId,
            showRoot: true,
            rootLink: "TreeView/CreateTreeView?agentid=" + rootId
        });
    };


};

var treeview = new Apollo.CMS.Account.TreeView();

$(document).ready(function () {
    treeview.InitPage();
});