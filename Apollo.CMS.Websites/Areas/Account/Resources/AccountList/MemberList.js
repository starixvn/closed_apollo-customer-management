/* File Created: December 18, 2013 */
$(document).ready(function () {
    var memberList = new Apollo.CMS.Account.AccountList.MemberList();

    memberList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountList.MemberList");
Apollo.CMS.Account.AccountList.MemberList = function () {
    var $this = this;
    var urls = {
        MemberSetting: CMS.resolveRootUrl('Account/MemberSetting'), // Update later
        GetMemberList: CMS.resolveRootUrl('Account/AccountList/GetMemberList')
    };
    var parameters = {
        AgentId: null,
        Username: null,
        Status: null,
        BetStatus: null,
        PageIndex: null,
        PageSize: null,
        IsOrderByAsc: null
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.txtUsername = $('#txtUsername', '#divFilterForm');
        $this.DOMInstance.ddlStatus = $('#ddlStatus', '#divFilterForm');
        $this.DOMInstance.ddlBetStatus = $('#ddlBetStatus', '#divFilterForm');
        $this.DOMInstance.btnSubmit = $('#btnSubmit', '#divFilterForm');
        $this.DOMInstance.divMemberList = $('#divMemberList');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        parameters.AgentId = agentId;
        parameters.PageSize = pageSize;
        parameters.IsOrderByAsc = true;

        $this.DOMInstance.txtUsername.on('keyup', function (event) {
            if (event.keyCode == 13) {
                $this.DOMInstance.btnSubmit.click();
            }
        });
        $this.DOMInstance.txtUsername.focus();

        $this.DOMInstance.btnSubmit.on('click', function () {
            parameters.Username = $.trim($this.DOMInstance.txtUsername.val());
            parameters.Status = $this.DOMInstance.ddlStatus.val();
            parameters.BetStatus = $this.DOMInstance.ddlBetStatus.val();
            parameters.PageIndex = 0;

            getData();
        });
        $this.DOMInstance.btnSubmit.click();
    };

    // Get data
    var getData = function () {
        $.ajax({
            type: "POST",
            url: urls.GetMemberList,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(parameters),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                renderTable(result.MemberList, result.TotalItems);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    // Render member list.
    var renderTable = function (data, totalItems) {
        var bodyData = [];

        $this.DOMInstance.divMemberList.addClass('hide');

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<span class="icon-edit-link" data-cust-id="{0}" title="{1}"/>'.format(item.CustId, tableHeaders.BetAndCommSetting) },
                              { Name: 'ColUsername', Value: '<span class="padding-cell">{0}</span>'.format(item.Username) },
                              { Name: 'ColStatus', Value: '<span class="{0}">{1}</span>'.format(item.ActualStatus.toLowerCase().replace(/\./g, ''), item.ActualStatus) },
                              { Name: 'ColBetStatus', Value: '<span class="{0}">{1}</span>'.format(item.ActualBetStatus.toLowerCase().replace(/\./g, ''), item.ActualBetStatus) },
                              { Name: 'ColCommGroup', Value: item.ActualUserGroup },
                              { Name: 'ColSportCredit', Value: '<span class="padding-cell">{0}</span>'.format(item.CashBalance)}]
                };
                bodyData.push(row);
            });
        }

        var gridOptions =
        {
            columns: [{ HeaderText: tableHeaders.No, Width: 70, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Edit, Width: 70, Name: 'ColEdit', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Username, Width: 180, Name: 'ColUsername', HeaderAlign: 'center', CellAlign: 'left', Sort: true, SortDirection: parameters.IsOrderByAsc ? 'asc' : 'desc' },
                      { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.BetStatus, Width: 100, Name: 'ColBetStatus', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.CommGroup, Width: 100, Name: 'ColCommGroup', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.SportCredit, Width: 150, Name: 'ColSportCredit', HeaderAlign: 'center', CellAlign: 'right', FormatNumber: CMS.commonConstants.DecimalFormat}],
            showDistinctRow: false,
            showHighlightRow: false,
            showFooter: true,
            showNumberColumn: true,
            showPager: true,
            customPager: true,
            pagerOption: {
                itemsPerPage: parameters.PageSize,
                currentPage: parameters.PageIndex,
                totalItem: totalItems,
                itemsPerPageArray: CMS.pageRanges,
                pagerSelectCallBack: handleChangingPage
            },
            sortOption: {
                serverSortFunction: handleSorting
            },
            gridExpandHeight: 250,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        $this.DOMInstance.divMemberList.removeClass('hide').fanexGrid(gridOptions);
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
        txtUsername: null,
        ddlStatus: null,
        ddlBetStatus: null,
        btnSubmit: null,
        divAgentListContainer: null,
        divMemberList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};