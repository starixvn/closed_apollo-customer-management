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
        $this.DOMInstance.divLeftContent = $('#leftContent');
    };

    // Bind event handlers to controls
    var initFields = function () {
        $this.DOMInstance.ddlMemberTypes.change(function () {
            isMember = $this.DOMInstance.ddlMemberTypes.val();
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
                var username = $this.DOMInstance.txtUsername.val();
                $this.GetCustomer(isMember, null, username);
            }
        });

        $this.DOMInstance.txtUsername.on('focus', function (event) {
            $this.DOMInstance.txtUsername.select();
        });

        $this.DOMInstance.btnSearch.on('click', function () {
            var username = $this.DOMInstance.txtUsername.val();
            $this.GetCustomer(isMember, null, username);
        });
    };

    // Re-show the previous selected tab and expand the left panel if it is collapsed.
    var showCurrentTab = function () {
        var currentTabId = $.cookie('left-panel-current-tab');
        var currentTab = $('a[href="{0}"]'.format(currentTabId));

        currentTab.tab('show');
        currentTab.click();
        Layout.expandLefPanel();
    };

    // Get customer information by Id.
    $this.GetCustomer = function (isMember, id, username) {
        var params = {
            isMember: isMember,
            id: id,
            username: username
        };

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
                //showCurrentTab();
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    $this.DOMInstance = {
        ddlMemberTypes: null,
        txtUsername: null,
        btnSearch: null,
        divLeftContent: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};

// Expose to reuse at another page.
var searchBar = new Apollo.CMS.Account.SearchBar();