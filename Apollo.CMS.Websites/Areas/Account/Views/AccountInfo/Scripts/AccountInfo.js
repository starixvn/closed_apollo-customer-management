/* File Created: December 5, 2013 */
$(document).ready(function () {
    var accountInfo = new Apollo.CMS.Account.AccountInfo();

    accountInfo.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountInfo");
Apollo.CMS.Account.AccountInfo = function () {
    var $this = this;
    var urls = {
        GetSuggestedCustomers: CMS.resolveRootUrl('Account/AccountInfo/GetSuggestedCustomer'),
        GetCustomer: CMS.resolveRootUrl('Account/AccountInfo/GetCustomer'),
        GetLeftPanel: CMS.resolveRootUrl('Account/AccountInfo/LeftPanel')
    };

    // Bind event handlers to controls
    var initFields = function () {
        $('[data-toggle="tab"]').live('click', function (event) {
            console.log(event.target);
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

    $this.InitPage = function () {
        initFields();
    };
}