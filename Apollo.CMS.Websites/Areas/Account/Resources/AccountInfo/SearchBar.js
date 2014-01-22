/* File Created: December 5, 2013 */
$(document).ready(function () {
    searchBar.InitPage();
});

Namespace.Register("Apollo.CMS.Account.SearchBar");
Apollo.CMS.Account.SearchBar = function () {
    var $this = this;
    var rootUrl = $('#RootURL').val() || '/';
    var urls = {
        GetSuggestedCustomers: rootUrl + 'Account/AccountInfo/GetSuggestedCustomer',
        GetCustomer: rootUrl + 'Account/AccountInfo/GetCustomer',
        GetLeftPanel: rootUrl + 'Account/AccountInfo/LeftPanel'
    };
    var isMember = false;

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.ddlMemberTypes = $('#ddlMemberTypes');
        $this.DOMInstance.txtSearch = $('#txtSearch');
        $this.DOMInstance.btnSearch = $('#btnSearch');
        $this.DOMInstance.btnTreeView = $('#btnTreeView');
        $this.DOMInstance.icoTreeView = $this.DOMInstance.btnTreeView.parent();
        $this.DOMInstance.divLeftContent = $('#leftContent');
    };

    // Bind event handlers to controls
    var initFields = function () {
        // Initialize the state of tree view icon.
        isMember = $this.DOMInstance.ddlMemberTypes.val() === 'true';
        changeTreeViewIconState(isMember);

        $this.DOMInstance.ddlMemberTypes.change(function () {
            isMember = $this.DOMInstance.ddlMemberTypes.val() === 'true';
            changeTreeViewIconState(isMember);
        });

        // Auto-complete for Search text-box.
        $this.DOMInstance.txtSearch.autocomplete({
            source: function (request, response) {
                var params = {
                    IsMember: isMember,
                    Username: request.term
                };

                $.ajax({
                    type: 'POST',
                    url: urls.GetSuggestedCustomers,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(params),
                    success: function (data) {
                        var found = $.map(data, function (item) {
                            return {
                                label: item.Username,
                                value: item.Username,
                                id: item.CustId
                            }
                        });

                        return response(found);
                    }
                });
            },
            select: function (event, ui) {
                $this.GetCustomer(isMember, ui.item.id, null);
            },
            position: { my: 'right top', at: 'right bottom' }
        });

        // Catch enter key of Search text-box.
        $this.DOMInstance.txtSearch.on('keyup', function (event) {
            if (event.keyCode == 13) {
                $this.DOMInstance.txtSearch.blur();
                $this.DOMInstance.btnSearch.click();
            }
        });

        $this.DOMInstance.txtSearch.on('focus', function (event) {
            $this.DOMInstance.txtSearch.select();
        });

        $this.DOMInstance.btnSearch.on('click', function () {
            var username = $this.DOMInstance.txtSearch.val();

            if (!username) {
                var requiredErrorMsg = $this.DOMInstance.txtSearch.attr('data-val-required');
                Validator.showTooltip($this.DOMInstance.txtSearch, requiredErrorMsg, 'nw');
            } else {
                $this.GetCustomer(isMember, null, username);
            }
        });
    };

    // Redirect to tree view
    var redirectToTreeView = function () {
        var username = $this.DOMInstance.txtSearch.val();
        var oldUsername = $.trim($('#divAgentName').text());

        $this.DOMInstance.txtSearch.tipsy('hide');
        layoutHelper.ResetMenuState();
        $this.DOMInstance.icoTreeView.addClass('active');

        if (oldUsername === username) {
            var custId = $('#hdnCustomerId').val();

            layoutHelper.LoadMainContent(rootUrl + 'Account/TreeView?agentId={0}'.format(custId));
        } else {
            layoutHelper.LoadMainContent(rootUrl + 'Account/TreeView?username={0}'.format(username));
        }
    };

    // Change the state of tree view icon.
    var changeTreeViewIconState = function (isMember) {
        if (isMember) {
            $this.DOMInstance.btnTreeView.parent().removeClass('active').addClass('disabled');
            $this.DOMInstance.btnTreeView.removeClass('icon-treeview').addClass('icon-treeview-disabled');
            $this.DOMInstance.btnTreeView.off('click');
        } else {
            $this.DOMInstance.icoTreeView.removeClass('disabled');
            $this.DOMInstance.btnTreeView.removeClass('icon-treeview-disabled').addClass('icon-treeview');
            $this.DOMInstance.btnTreeView.on('click', redirectToTreeView);
        }
    };

    // Set the customer information and update the search bar
    $this.SearchCustomer = function (isMember, id, username) {
        $this.DOMInstance.ddlMemberTypes.val(isMember.toString());
        $this.DOMInstance.ddlMemberTypes.change();
        $this.DOMInstance.txtSearch.val($.trim(username));

        $this.GetCustomer(isMember, id, null);
    };

    // Get customer information by Id.
    $this.GetCustomer = function (isMember, id, username) {
        var params = {
            isMember: isMember,
            id: id,
            username: username
        };

        $this.DOMInstance.txtSearch.tipsy('hide');

        $.ajax({
            type: 'POST',
            url: urls.GetLeftPanel,
            contentType: 'application/json; charset=utf-8',
            dataType: 'html',
            data: JSON.stringify(params),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                $this.DOMInstance.divLeftContent.html(result);
                $.cookie('left-panel-current-tab', '#setting', { path: '/' });
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
                layoutHelper.ExpandLeftPanel();
            }
        });
    };

    $this.DOMInstance = {
        ddlMemberTypes: null,
        txtSearch: null,
        btnSearch: null,
        btnTreeView: null,
        icoTreeView: null,
        divLeftContent: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};

// Expose to reuse at another page.
var searchBar = new Apollo.CMS.Account.SearchBar();