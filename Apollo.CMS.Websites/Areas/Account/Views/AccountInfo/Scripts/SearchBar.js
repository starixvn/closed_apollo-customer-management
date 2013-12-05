/* File Created: December 5, 2013 */
$(document).ready(function () {
    var searchBar = new Apollo.CMS.AccountInfo.SearchBar();

    searchBar.InitPage();
});

Namespace.Register("Apollo.CMS.AccountInfo.SearchBar");
Apollo.CMS.AccountInfo.SearchBar = function () {
    var $this = this;
    var urls = {
        GetSuggestedCustomers: CMS.resolveRootUrl('Account/AccountInfo/GetSuggestedCustomer')
    };
    var cacheDOM = function () {
        $this.DOMInstance.ddlMemberTypes = $('#ddlMemberTypes');
        $this.DOMInstance.txtUsername = $('#txtUsername');
        $this.DOMInstance.btnSearch = $('#btnSearch');
    };
    var initFields = function () {
        $this.DOMInstance.txtUsername.autocomplete({
            source: function (request, response) {
                var isMember = $this.DOMInstance.ddlMemberTypes.val();
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
                //@@TODO: Get customer info.
            },
            position: { my: "right top", at: "right bottom" }
        });
    };

    $this.DOMInstance = {
        ddlMemberTypes: null,
        txtUsername: null,
        btnSearch: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
}