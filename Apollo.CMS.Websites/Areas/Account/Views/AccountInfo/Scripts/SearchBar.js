/* File Created: December 5, 2013 */
$(document).ready(function () {
    var searchBar = new Apollo.CMS.AccountInfo.SearchBar();

    searchBar.InitPage();
});

Namespace.Register("Apollo.CMS.AccountInfo.SearchBar");
Apollo.CMS.AccountInfo.SearchBar = function () {
    var $this = this;
    var urls = {
        GetSuggestedCustomers: CMS.resolveRootUrl('Account/AccountInfo/GetSuggestedCustomer'),
        GetCustomer: CMS.resolveRootUrl('Account/AccountInfo/GetCustomer'),
        GetLeftPanel: CMS.resolveRootUrl('Account/AccountInfo/LeftPanel')
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
                getCustomer(isMember, ui.item.id, null);
            },
            position: { my: "right top", at: "right bottom" }
        });

        // Catch enter key of Search text-box.
        $this.DOMInstance.txtUsername.on('keyup', function (event) {
            if (event.keyCode == 13) {
                var username = $this.DOMInstance.txtUsername.val();
                getCustomer(isMember, null, username);
            }
        });

        $this.DOMInstance.txtUsername.on('focus', function (event) {
            $this.DOMInstance.txtUsername.select();
        });

        $this.DOMInstance.btnSearch.on('click', function () {
            var username = $this.DOMInstance.txtUsername.val();
            getCustomer(isMember, null, username);
        });
    };

    // Get customer information by Id.
    var getCustomer = function (isMember, id, username) {
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
                Layout.collapseExpandLeftPanel(true);
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
}