/* File Created: January 10, 2014 */
$(document).ready(function () {
    var memberBetList = new Apollo.CMS.Account.MemberBetList();

    memberBetList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.MemberBetList");
Apollo.CMS.Account.MemberBetList = function () {
    var $this = this;
    var pageRanges = [10, 50, 100, 200, 500];
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
        $this.DOMInstance.divDetailedWinLossList = $('#divDetailedWinLossList');
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
    var getTicketData = function (transId, transDate) {
        var params = {
            TransId: transId,
            TransDate: moment(transDate)
        };

        $.ajax({
            type: "POST",
            url: urls.TicketDetail,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(params),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                var title = '{0} - {1}: {2}'.format(detailTableHeaders.ViewDetails, detailTableHeaders.RefNo, transId);

                pageLayout.Popup(title, $this.DOMInstance.divDetailedWinLossList, null, true);
                renderDetailTable(result);
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
            showFooter: true,
            showNumberColumn: true,
            showPager: true,
            customPager: true,
            pagerOption: {
                itemsPerPage: parameters.PageSize,
                currentPage: parameters.PageIndex,
                itemsPerPageArray: pageRanges,
                pagerSelectCallBack: handleChangingPage
            },
            gridExpandHeight: 250,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        // Element formats.
        var detailColFormat = '<span class="winloss-details" data-ref-no="{0}" data-trans-date="{1}" />{2}</span>';
        var transTimeColFormat = '<div class="trans-time">{0}</div>';

        $this.DOMInstance.divBetList.addClass('hide');

        // Configure grid's columns.
        columns = [{ HeaderText: tableHeaders.No, Width: 70, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.TransTime, Width: 150, Name: 'ColTransTime', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Choice, Width: 260, Name: 'ColChoice', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Odds, Width: 100, Name: 'ColOdds', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Stake, Width: 120, Name: 'ColStake', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.WinLoss, Width: 120, Name: 'ColWinLoss', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.CompanyShare, Width: 100, Name: 'ColCompanyShare', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Details, Width: 60, Name: 'ColDetails', HeaderAlign: 'center', CellAlign: 'center'}];

        // Push data to grid.
        if (data && data.BetItems && data.BetItems.length > 0) {
            var row = null;

            $.each(data.BetItems, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColTransTime', Value: transTimeColFormat.format(item.TransTimeCol) },
                              { Name: 'ColChoice', Value: item.ChoiceCol },
                              { Name: 'ColOdds', Value: item.OddsCol },
                              { Name: 'ColStake', Value: formatNumberCol(item.Stake) },
                              { Name: 'ColWinLoss', Value: formatNumberCol(item.WinLoss, false, item.Comm) },
                              { Name: 'ColStatus', Value: item.StatusCol },
                              { Name: 'ColCompanyShare', Value: formatNumberCol(item.CompanyShare, true, item.CompanyWinLoss) },
                              { Name: 'ColDetails', Value: detailColFormat.format(item.TransId, item.TransDate, tableHeaders.Details)}]
                };

                bodyData.push(row);
            });

            gridOptions.pagerOption.totalItem = data.TotalItems;
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divBetList.removeClass('hide').fanexGrid(gridOptions);
        handleGridEvents();
    };

    // Handle paging at server side.
    var handleChangingPage = function (pageIndex, itemsPerPage, data) {
        parameters.PageSize = itemsPerPage;
        parameters.PageIndex = pageIndex;
        parameters.IsLoadNew = false;
        getData();
    };

    // Handle events raised in the grid.
    var handleGridEvents = function () {
        $this.DOMInstance.divBetList.find('span.winloss-details').on('click', function () {
            var $obj = $(this);
            var transId = $obj.attr('data-ref-no');
            var transDate = $obj.attr('data-trans-date');

            getTicketData(transId, transDate);
        });
    };

    // Render data table for detailed win loss.
    var renderDetailTable = function (data) {
        var bodyData = [];
        var columns = [];
        var gridOptions = {
            showDistinctRow: false,
            showHighlightRow: false,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        $this.DOMInstance.divDetailedWinLossList.addClass('hide');

        // Configure grid's columns.
        columns = [{ HeaderText: detailTableHeaders.Agent, Width: 250, Name: 'ColAgent', HeaderAlign: 'center', CellAlign: 'left' },
                   { HeaderText: detailTableHeaders.Share, Width: 130, Name: 'ColShare', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: detailTableHeaders.Comm, Width: 130, Name: 'ColComm', HeaderAlign: 'center', CellAlign: 'right'}];

        // Push data to grid.
        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColAgent', Value: item.Username },
                              { Name: 'ColShare', Value: formatNumberCol(item.PositionTaking, true, item.WinLoss) },
                              { Name: 'ColComm', Value: !item.IsSuper ? formatNumberCol(item.Discount, true, item.Comm) : 'N/A'}]
                };

                bodyData.push(row);
            });
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divDetailedWinLossList.removeClass('hide').fanexGrid(gridOptions);
    };

    // Format number column.
    var formatNumberCol = function (firstNumber, isPercent, secondNumber, thirdNumber) {
        var formattedStr = '';
        var cssClass = '';

        if ($.isNumeric(firstNumber)) {
            var percent = isPercent ? '%' : '';

            cssClass = firstNumber < 0 ? 'format-negative' : '';
            firstNumber = isPercent ? firstNumber * 100 : firstNumber;
            formattedStr += '<span class="{0}">{1}{2}</span>'.format(cssClass, numeral(firstNumber).format(CMS.commonConstants.DecimalFormat), percent);
        }

        if ($.isNumeric(secondNumber)) {
            cssClass = secondNumber < 0 ? 'format-negative' : '';
            formattedStr += '<br/><span class="{0}">{1}</span>'.format(cssClass, numeral(secondNumber).format(CMS.commonConstants.DecimalFormat));
        }

        if ($.isNumeric(thirdNumber)) {
            cssClass = thirdNumber < 0 ? 'format-negative' : '';
            formattedStr += '<br/><span class="{0}">{1}</span>'.format(cssClass, numeral(thirdNumber).format(CMS.commonConstants.DecimalFormat));
        }

        return formattedStr;
    }

    $this.DOMInstance = {
        divFilterForm: null,
        dpkFromDate: null,
        dpkToDate: null,
        btnGenerate: null,
        divBetList: null,
        divDetailedWinLossList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};