Namespace.Register("Apollo.CMS.Account.TreeView");

Apollo.CMS.Account.TreeView = function () {

    var self = this;

    self.InitPage = function () {
        self.LoadTreeView();
    };

    self.LoadTreeView = function () {
        var accountTreeView = $("#account-treeview");
        accountTreeView.treeview({
            url: "TreeView/CreateTreeView",
            link: "",
            linkdirectAgents: "",
            agentId: 26,
            showRoot: true,
            rootLink: "TreeView/CreateTreeView"
        });
    };


};

var treeview = new Apollo.CMS.Account.TreeView();

$(document).ready(function () {
    treeview.InitPage();
});