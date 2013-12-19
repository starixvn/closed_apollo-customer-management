/* File Created: December 18, 2013 */
$(document).ready(function () {
    var shadowList = new Apollo.CMS.Account.AccountList.ShadowList();

    shadowList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountList.ShadowList");
Apollo.CMS.Account.AccountList.ShadowList = function () {
    var $this = this;
    var urls = {
        AgentList: CMS.resolveRootUrl('Account/AccountList/AgentList'),
        MemberList: CMS.resolveRootUrl('Account/AccountList/MemberList'),
        GetShadowList: CMS.resolveRootUrl('Account/AccountList/GetShadowList')
    };
    var parameters = {
        AgentId: null,
        PageIndex: null,
        PageSize: null,
        IsOrderByAsc: null
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divShadowList = $('#divShadowList');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        getData();
    };

    // Get data
    var getData = function () {
        parameters.AgentId = 1;
        parameters.PageSize = 50;

        $.ajax({
            type: "POST",
            url: urls.GetShadowList,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(parameters),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                renderShadowTable(result);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    // Render data table for low level agent.
    var renderShadowTable = function (data) {
        var bodyData = [];

        $this.DOMInstance.divShadowList.addClass('hide');

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<span class="edit-class" data-cust-id="{0}"/>'.format(item.custId) },
                              { Name: 'ColShadow', Value: item.Username },
                              { Name: 'ColRegisterDate', Value: item.RegisterDate },
                              { Name: 'ColStatus', Value: '<span class="{0}">{1}</span>'.format(item.Status.toLowerCase().replace(/./g, ''), item.Status)}]
                };
                bodyData.push(row);
            });
        }

        var gridOptions =
        {
            columns: [{ HeaderText: tableHeaders.No, Width: 50, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center', FormatNumber: CMS.commonConstants.NumberFormat },
                      { HeaderText: tableHeaders.Edit, Width: 70, Name: 'ColEdit', HeaderAlign: 'center', CellAlign: 'left' },
                      { HeaderText: tableHeaders.Shadow, Width: 100, Name: 'ColShadow', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.RegisterDate, Width: 100, Name: 'ColRegisterDate', HeaderAlign: 'center', CellAlign: 'center', FormatDateTime: CMS.commonConstants.MomentDateTimeFormat, Timezone: CMS.serverTimezone },
                      { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center'}],
            showFooter: true,
            gridExpandHeight: 250,
            showNumberColumn: true,
            showPager: true,
            pagerOption: {
                itemsPerPage: 20,
                maxItemsPerPage: 100,
                currentPage: 0,
                startPerPage: 10,
                pageStep: 10
            },
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        $this.DOMInstance.divShadowList.removeClass('hide').fanexGrid(gridOptions);
    };

    $this.DOMInstance = {
        divShadowList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};