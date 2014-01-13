/* File Created: January 10, 2014 */
$(document).ready(function () {
    var memberBetList = new Apollo.CMS.Account.MemberBetList();

    memberBetList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.MemberBetList");
Apollo.CMS.Account.MemberBetList = function () {
    var $this = this;
    var urls = {
        MemberBetList: CMS.resolveRootUrl('BetList/MemberBetList/GetMemberBetList'),
        TicketDetail: CMS.resolveRootUrl('BetList/MemberBetList/GetTicketDetail')
    };
    var parameters = {
        CustId: null,
        FromDate: null,
        ToDate: null,
        PageIndex: null,
        PageSize: null,
        IsLoadNew: null
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divFilterForm = $('#divFilterForm');
        $this.DOMInstance.dpkFromDate = $('#dpkFromDate', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.dpkToDate = $('#dpkToDate', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.btnGenerate = $('#btnGenerate', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.divBetList = $('#divBetList');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        parameters.CustId = memberId;
        parameters.PageSize = pageSize;

        layoutHelper.CreateDatepicker($this.DOMInstance.dpkFromDate, null, null, function (selectedDate) {
            $this.DOMInstance.dpkToDate.datepicker("option", "minDate", selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        layoutHelper.CreateDatepicker($this.DOMInstance.dpkToDate, null, null, function (selectedDate) {
            $this.DOMInstance.dpkFromDate.datepicker("option", "maxDate", selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        $this.DOMInstance.btnGenerate.on('click', function () {
            parameters.FromDate = $this.DOMInstance.dpkFromDate.datepicker('getDate');
            parameters.ToDate = $this.DOMInstance.dpkToDate.datepicker('getDate');
            parameters.PageIndex = 0;
            parameters.IsLoadNew = true;

            getData();
        });
        $this.DOMInstance.btnGenerate.click();
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
                renderTable(result);
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
                renderTable(result);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    // Render data table for member bet list.
    var renderTable = function (data) {
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
                totalItem: 100, //@@TODO
                itemsPerPageArray: CMS.pageRanges,
                pagerSelectCallBack: handleChangingPage
            },
            gridExpandHeight: 250,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        $this.DOMInstance.divBetList.addClass('hide');

        columns = [{ HeaderText: tableHeaders.No, Width: 70, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.TransTime, Width: 150, Name: 'ColTransTime', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Choice, Width: 180, Name: 'ColChoice', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Odds, Width: 100, Name: 'ColOdds', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Stake, Width: 100, Name: 'ColStake', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.WinLoss, Width: 100, Name: 'ColWinLoss', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.CompanyShare, Width: 150, Name: 'ColCompanyShare', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Feature, Width: 100, Name: 'ColFeature', HeaderAlign: 'center', CellAlign: 'center'}];

        if (data && data.BetItems && data.BetItems.length > 0) {
            var row = null;

            $.each(data.BetItems, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColTransTime', Value: item.TransTimeCol },
                              { Name: 'ColChoice', Value: item.ChoiceCol },
                              { Name: 'ColOdds', Value: item.OddsCol },
                              { Name: 'ColStake', Value: item.Stake },
                              { Name: 'ColWinLoss', Value: item.WinLoss },
                              { Name: 'ColStatus', Value: item.StatusCol },
                              { Name: 'ColCompanyShare', Value: item.CompanyShare },
                              { Name: 'ColFeature', Value: tableHeaders.Details}]
                };

                bodyData.push(row);
            });
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divBetList.removeClass('hide').fanexGrid(gridOptions);
    };

    // Handle paging at server side.
    var handleChangingPage = function (pageIndex, itemsPerPage, data) {
        parameters.PageSize = itemsPerPage;
        parameters.PageIndex = pageIndex;
        parameters.IsLoadNew = false;
        getData();
    };

    $this.DOMInstance = {
        divFilterForm: null,
        dpkFromDate: null,
        dpkToDate: null,
        btnGenerate: null,
        divBetList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};