/* File Created: January 10, 2014 */
$(document).ready(function () {
    var memberBetList = new Apollo.CMS.Account.MemberBetList();

    memberBetList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.MemberBetList");
Apollo.CMS.Account.MemberBetList = function () {
    var $this = this;
    var urls = {
        MemberBetList: CMS.resolveRootUrl('Account/MemberBetList/GetMemberBetList'),
        TicketDetail: CMS.resolveRootUrl('Account/MemberBetList/GetTicketDetail')
    };
    var parameters = {
        MemberId: null,
        DateFrom: null,
        DateTo: null,
        PageIndex: null,
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divFilterForm = $('#divFilterForm');
        $this.DOMInstance.dpkDateFrom = $('#dpkDateFrom', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.dpkDateTo = $('#dpkDateTo', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.btnGenerate = $('#btnGenerate', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.divBetList = $('#divBetList');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        parameters.CustId = custId;
        parameters.PageSize = pageSize;
        parameters.IsOrderByAsc = true;

        layoutHelper.CreateDatepicker($this.DOMInstance.dpkDateFrom, null, null, function (selectedDate) {
            $this.DOMInstance.dpkDateTo.datepicker("option", "minDate", selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        layoutHelper.CreateDatepicker($this.DOMInstance.dpkDateTo, null, null, function (selectedDate) {
            $this.DOMInstance.dpkDateFrom.datepicker("option", "maxDate", selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        $this.DOMInstance.btnSubmit.on('click', function () {
            parameters.MemberId = $.trim($this.DOMInstance.txtUsername.val());
            parameters.DateFrom = $this.DOMInstance.ddlStatus.val();
            parameters.DateTo = $this.DOMInstance.ddlBetStatus.val();
            parameters.PageIndex = 0;

            getData();
        });
        $this.DOMInstance.btnSubmit.click();
    };

    // Get data
    var getData = function () {
        $.ajax({
            type: "POST",
            url: urls.MemberBetList,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(parameters),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                renderTable(result.AgentList, result.TotalItems);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    // Get detailed data of a ticket.
    var getTicketData = function () {
        $.ajax({
            type: "POST",
            url: urls.TicketDetail,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(parameters),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                renderTable(result.AgentList, result.TotalItems);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    // Render data table for agent lisy.
    var renderTable = function (data, totalItems) {
        var bodyData = [];
        var columns = [];
        var gridOptions = {
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

        $this.DOMInstance.divAgentList.addClass('hide');
        hideContextMenu();

        columns = [{ HeaderText: tableHeaders.No, Width: 70, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Edit, Width: 70, Name: 'ColEdit', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Username, Width: 180, Name: 'ColUsername', HeaderAlign: 'center', CellAlign: 'left', Sort: true, SortDirection: parameters.IsOrderByAsc ? 'asc' : 'desc' },
                   { HeaderText: tableHeaders.Downline, Width: 100, Name: 'ColDownline', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Shadow, Width: 100, Name: 'ColShadow', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.BetStatus, Width: 100, Name: 'ColBetStatus', HeaderAlign: 'center', CellAlign: 'center'}];

        if (isSuper) {
            columns.push({ HeaderText: tableHeaders.Currency, Width: 100, Name: 'ColCurrency', HeaderAlign: 'center', CellAlign: 'center' });
        } else if (hasMember) {
            columns.splice(4, 0, { HeaderText: tableHeaders.Member, Width: 100, Name: 'ColMember', HeaderAlign: 'center', CellAlign: 'center' });
        } else {
            $this.DOMInstance.divFilterForm.removeClass('filter-mainAgent').addClass('shareholder').attr('data-original-width', 822);
        }

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<div class="icon-edit-link" data-agent-id="{0}"/>'.format(item.AgentId) },
                              { Name: 'ColUsername', Value: item.Username },
                              { Name: 'ColDownline', Value: item.NumberOfAgent > 0 ? '<span class="link downline" data-agent-id="{0}" data-agent-level="{1}">{2}</span>'.format(item.AgentId, item.AgentLevel, item.NumberOfAgent) : '0' },
                              { Name: 'ColShadow', Value: item.NumberOfShadow > 0 ? '<span class="link shadow" data-agent-id="{0}" data-agent-level="{1}">{2}</span>'.format(item.AgentId, item.AgentLevel, item.NumberOfShadow) : '0' },
                              { Name: 'ColStatus', Value: '<span class="{0}">{1}</span>'.format(item.ActualStatus.toLowerCase().replace(/\./g, ''), item.ActualStatus) },
                              { Name: 'ColBetStatus', Value: '<span class="{0}">{1}</span>'.format(item.ActualBetStatus.toLowerCase().replace(/\./g, ''), item.ActualBetStatus)}]
                };

                if (isSuper) {
                    row.Columns.push({ Name: 'ColCurrency', Value: item.Currency });
                } else if (hasMember) {
                    row.Columns.splice(4, 0, { Name: 'ColMember', Value: item.NumberOfMember > 0 ? '<span class="link member" data-agent-id="{0}" data-agent-level="{1}">{2}</span>'.format(item.AgentId, item.AgentLevel, item.NumberOfMember) : '0' });
                }

                bodyData.push(row);
            });
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divAgentList.removeClass('hide').fanexGrid(gridOptions);
        bindGridEventhandlers();
    };

    // Handle paging at server side.
    var handleChangingPage = function (pageIndex, itemsPerPage, data) {
        parameters.PageSize = itemsPerPage;
        parameters.PageIndex = pageIndex;
        getData();
    };

    $this.DOMInstance = {
        divFilterForm: null,
        dpkDateFrom: null,
        dpkDateTo: null,
        btnGenerate: null,
        divBetList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};