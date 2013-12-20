/* File Created: December 18, 2013 */
$(document).ready(function () {
    var shadowList = new Apollo.CMS.Account.AccountList.ShadowList();

    shadowList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountList.ShadowList");
Apollo.CMS.Account.AccountList.ShadowList = function () {
    var $this = this;
    var urls = {
        ShadowSetting: CMS.resolveRootUrl('Account/ShadowSetting'), // Update later
        GetShadowList: CMS.resolveRootUrl('Account/AccountList/GetShadowList')
    };
    var parameters = {
        AgentId: null,
        PageIndex: null,
        PageSize: null,
        IsOrderByAsc: null
    };

    // Initialize fields and handle events.
    var initFields = function () {
        $this.DOMInstance.divShadowList = $('#divShadowList');

        parameters.AgentId = agentId;
        parameters.PageSize = pageSize;
        parameters.PageIndex = 0;
        parameters.IsOrderByAsc = true;

        getData();
    };

    // Get data
    var getData = function () {
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
                renderShadowTable(result.ShadowList, result.TotalItems);
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
    var renderShadowTable = function (data, totalItems) {
        var bodyData = [];

        $this.DOMInstance.divShadowList.addClass('hide');

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<span class="icon-edit-link" data-cust-id="{0}" title="{1}"/>'.format(item.Id, tableHeaders.ShadowPermission) },
                              { Name: 'ColShadow', Value: item.Username },
                              { Name: 'ColRegisterDate', Value: item.CreatedDate },
                              { Name: 'ColStatus', Value: '<span class="{0}">{1}</span>'.format(item.ActualStatus.toLowerCase().replace(/\./g, ''), item.ActualStatus)}]
                };
                bodyData.push(row);
            });
        }

        var gridOptions =
        {
            columns: [{ HeaderText: tableHeaders.No, Width: 70, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center', FormatNumber: CMS.commonConstants.NumberFormat },
                      { HeaderText: tableHeaders.Edit, Width: 70, Name: 'ColEdit', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Shadow, Width: 200, Name: 'ColShadow', HeaderAlign: 'center', CellAlign: 'left', Sort: true, SortDirection: parameters.IsOrderByAsc ? 'asc' : 'desc' },
                      { HeaderText: tableHeaders.RegisterDate, Width: 190, Name: 'ColRegisterDate', HeaderAlign: 'center', CellAlign: 'center', FormatDateTime: CMS.commonConstants.MomentDateTimeFormat, Timezone: CMS.serverTimezone },
                      { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center'}],
            showFooter: true,
            showNumberColumn: true,
            showHighlightRow: false,
            showDistinctRow: false,
            showPager: true,
            customPager: true,
            pagerOption: {
                itemsPerPage: parameters.PageSize,
                currentPage: parameters.PageIndex,
                totalItem: totalItems,
                itemsPerPageArray: pageRanges,
                pagerSelectCallBack: handleChangingPage
            },
            sortOption: {
                serverSortFunction: handleSorting
            },
            bodyRows: bodyData,
            bodyRowHeight: 30,
            gridExpandHeight: 250
        };

        $this.DOMInstance.divShadowList.removeClass('hide').fanexGrid(gridOptions);
    };

    // Handle paging at server side.
    var handleChangingPage = function (pageIndex, itemsPerPage, data) {
        parameters.PageSize = itemsPerPage;
        parameters.PageIndex = pageIndex;
        getData();
    };

    // Handle sorting at server side.
    var handleSorting = function (sortColumnName, sortDirection) {
        parameters.IsOrderByAsc = sortDirection === 'ASC';
        parameters.PageIndex = 0;
        getData();
    };

    $this.DOMInstance = {
        divShadowList: null
    };
    $this.InitPage = function () {
        initFields();
    };
};