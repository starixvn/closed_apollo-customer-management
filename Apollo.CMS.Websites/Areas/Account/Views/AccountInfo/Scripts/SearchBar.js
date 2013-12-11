/* File Created: December 5, 2013 */
$(document).ready(function () {
    searchBar.InitPage();
});

Namespace.Register("Apollo.CMS.Account.SearchBar");
Apollo.CMS.Account.SearchBar = function () {
    var $this = this;
    var urls = {
        GetSuggestedCustomers: '/Account/AccountInfo/GetSuggestedCustomer', //CMS.resolveRootUrl('Account/AccountInfo/GetSuggestedCustomer'),
        GetCustomer: '/Account/AccountInfo/GetCustomer', //CMS.resolveRootUrl('Account/AccountInfo/GetCustomer'),
        GetLeftPanel: '/Account/AccountInfo/LeftPanel' //CMS.resolveRootUrl('Account/AccountInfo/LeftPanel')
    };
    var isMember = false;

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.ddlMemberTypes = $('#ddlMemberTypes');
        $this.DOMInstance.txtUsername = $('#txtUsername');
        $this.DOMInstance.btnSearch = $('#btnSearch');
        $this.DOMInstance.btnTreeView = $('#btnTreeView');
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
        $this.DOMInstance.txtUsername.autocomplete({
            source: function (request, response) {
                var params = {
                    IsMember: isMember,
                    Username: request.term
                };

                $.ajax({
                    type: "POST",
                    url: urls.GetSuggestedCustomers,
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
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
            position: { my: "right top", at: "right bottom" }
        });

        // Catch enter key of Search text-box.
        $this.DOMInstance.txtUsername.on('keyup', function (event) {
            if (event.keyCode == 13) {
                $this.DOMInstance.btnSearch.click();
                $this.DOMInstance.txtUsername.blur();
            }
        });

        $this.DOMInstance.txtUsername.on('focus', function (event) {
            $this.DOMInstance.txtUsername.select();
        });

        $this.DOMInstance.btnSearch.on('click', function () {
            var username = $this.DOMInstance.txtUsername.val();

            if (!username) {
                var requiredErrorMsg = $this.DOMInstance.txtUsername.attr('data-val-required');
                Validator.showTooltip($this.DOMInstance.txtUsername, requiredErrorMsg, 'nw');
            } else {
                $this.GetCustomer(isMember, null, username);
            }
        });
    };

    // Redirect to tree view
    var redirectToTreeView = function () {
        var username = $this.DOMInstance.txtUsername.val();
        var oldUsername = $.trim($('#divAgentName').text());

        $this.DOMInstance.txtUsername.tipsy('hide');

        if (oldUsername === username) {
            var custId = $('#hdnCustomerId').val();

            CMS.navigateToRoot('Account/TreeView?agentId={0}'.format(custId));
        } else {
            CMS.navigateToRoot('Account/TreeView?username={0}'.format(username));
        }
    };

    // Change the state of tree view icon.
    var changeTreeViewIconState = function (isMember) {
        if (isMember) {
            $this.DOMInstance.btnTreeView.parent().removeClass('active').addClass('disabled');
            $this.DOMInstance.btnTreeView.removeClass('icon-treeview').addClass('icon-treeview-disabled');
            $this.DOMInstance.btnTreeView.off('click');
        } else {
            var menuId = $.cookie('menu-id');
            var treeViewIcon = $this.DOMInstance.btnTreeView.parent();

            if (treeViewIcon.attr('id') == menuId) {
                treeViewIcon.addClass('active');
            }

            treeViewIcon.removeClass('disabled');
            $this.DOMInstance.btnTreeView.removeClass('icon-treeview-disabled').addClass('icon-treeview');
            $this.DOMInstance.btnTreeView.on('click', redirectToTreeView);
        }
    };

    // Set the customer information and update the search bar
    $this.SearchCustomer = function (isMember, id, username) {
        $this.DOMInstance.ddlMemberTypes.val(isMember.toString());
        $this.DOMInstance.ddlMemberTypes.change();
        $this.DOMInstance.txtUsername.val($.trim(username));

        $this.GetCustomer(isMember, id, null);
    };

    // Get customer information by Id.
    $this.GetCustomer = function (isMember, id, username) {
        var params = {
            isMember: isMember,
            id: id,
            username: username
        };

        $this.DOMInstance.txtUsername.tipsy('hide');

        $.ajax({
            type: "POST",
            url: urls.GetLeftPanel,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
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
                Layout.expandLefPanel();
            }
        });
    };

    $this.DOMInstance = {
        ddlMemberTypes: null,
        txtUsername: null,
        btnSearch: null,
        btnTreeView: null,
        divLeftContent: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};

// Expose to reuse at another page.
var searchBar = new Apollo.CMS.Account.SearchBar();