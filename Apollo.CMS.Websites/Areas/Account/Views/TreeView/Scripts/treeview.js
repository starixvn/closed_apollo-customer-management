Namespace.Register("Apollo.CMS.Account.TreeView");

Apollo.CMS.Account.TreeView = function () {

    var self = this;

    self.InitPage = function () {
        self.LoadTreeView();
    };

    self.SearchCustomer = function (el) {
        var agentId = $(el).attr("data-agentid");
        var username = $(el).attr("data-agentusername");
        $("#account-treeview ul li span").each(function (index) {
            var $this = $(this);
            if ($this.hasClass("nodeactived")) {
                $this.removeClass("nodeactived");
            }
        });

        $(el).addClass("nodeactived");

        searchBar.SearchCustomer(false, agentId, username);
    };

    self.LoadTreeView = function () {
        var accountTreeView = $("#account-treeview");
        var agentId = accountTreeView.attr("data-agentid");
        var agentUsername = accountTreeView.attr("data-username");
        accountTreeView.treeview({
            urlShowNodeData: "TreeView/CreateTreeViewByNode",
            customeNode: "<span onclick='treeview.SearchCustomer(this);' data-agentid='{1}' data-agentusername='{2}' class='nodetree' href='#'>{0}</span>",            
            showAllData: true,
            urlShowAllData: "TreeView/CreateTreeView?agentid=" + agentId + "&username=" + agentUsername
        });
    };
};


var treeview = new Apollo.CMS.Account.TreeView();

$(document).ready(function () {
    
    treeview.InitPage();
});