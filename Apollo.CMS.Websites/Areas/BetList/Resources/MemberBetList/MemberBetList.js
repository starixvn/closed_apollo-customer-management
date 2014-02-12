/* File Created: January 10, 2014 */

/* global Apollo, CMS, Namespace, moment, numeral, pageLayout, layoutHelper, tableHeaders, detailTableHeaders, systemParlayDetailsTableHeaders */
/* global minDate, today, betListReportTitle, memberId, memberName, pageSize */

$(document).ready(function () {
    'use strict';
    var memberBetList = new Apollo.CMS.BetList.MemberBetList();

    memberBetList.InitPage();
});

Namespace.Register('Apollo.CMS.BetList.MemberBetList');
Apollo.CMS.BetList.MemberBetList = function () {
    var $this = this;
    var pageRanges = [10, 50, 100, 200, 500];
    var urls = {
        MemberBetList: CMS.resolveRootUrl('BetList/MemberBetList/GetMemberBetList'),
        ExportMemberBetList: CMS.resolveRootUrl('BetList/MemberBetList/ExportMemberBetList'),
        TicketDetail: CMS.resolveRootUrl('BetList/MemberBetList/GetTicketDetail'),
        GetMixParlayByRefNo: CMS.resolveRootUrl('BetList/MemberBetList/GetMixParlayByRefNo'),
        GetSystemParlayDetails: CMS.resolveRootUrl('BetList/MemberBetList/GetSystemParlayDetails')
    };
    var parameters = {
        CustId: null,
        Username: null,
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
        $this.DOMInstance.divSystemParlayDetails = $('#divSystemParlayDetails');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        parameters.CustId = memberId;
        parameters.Username = memberName;
        parameters.PageSize = pageSize;

        layoutHelper.CreateDatepicker($this.DOMInstance.dpkFromDate, null, null, function (selectedDate) {
            $this.DOMInstance.dpkToDate.datepicker('option', 'minDate', selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        layoutHelper.CreateDatepicker($this.DOMInstance.dpkToDate, null, null, function (selectedDate) {
            $this.DOMInstance.dpkFromDate.datepicker('option', 'maxDate', selectedDate);
        }).datepicker('option', 'minDate', minDate);

        $this.DOMInstance.btnGenerate.on('click', function () {
            parameters.FromDate = $this.DOMInstance.dpkFromDate.val();
            parameters.ToDate = $this.DOMInstance.dpkToDate.val();
            parameters.PageIndex = 0;
            parameters.IsLoadNew = true;

            getData();
        });
        $this.DOMInstance.btnGenerate.click();
    };

    // Get data
    var getData = function () {
        $.ajax({
            type: 'POST',
            url: urls.MemberBetList,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
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
            type: 'POST',
            url: urls.TicketDetail,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(params),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                var title = '{0} - {1}: {2}'.format(detailTableHeaders.ViewDetails, detailTableHeaders.RefNo, transId);

                pageLayout.Popup(title, $this.DOMInstance.divDetailedWinLossList, { resizable: false }, true);
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
        var reportTitle = betListReportTitle.format(moment(parameters.FromDate).format(CMS.commonConstants.MomentDateFormat),
            moment(parameters.ToDate).format(CMS.commonConstants.MomentDateFormat),
            parameters.Username);
        var bodyData = [];
        var footerData = [];
        var columns = [];
        var gridOptions = {
            showDistinctRow: false,
            showFooter: true,
            showNumberColumn: true,
            showTitle: true,
            showPager: true,
            customPager: true,
            titleInfo: {
                text: reportTitle,
                showExcelButton: true,
                exportExcelFunction: exportBetList
            },
            pagerOption: {
                itemsPerPage: parameters.PageSize,
                currentPage: parameters.PageIndex,
                itemsPerPageArray: pageRanges,
                pagerSelectCallBack: handleChangingPage
            },
            gridExpandHeight: 160,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        // Element formats.
        var transTimeColFormat = '<div class="trans-time">{0}</div>';
        var choiceColFormat = '<div id="choice_{0}" class="choice {1}">{2}</div>';
        var oddColFormat = '<div class="odds">{0}</div>';
        var detailColFormat = '<div class="winloss-details detail-icon" data-ref-no="{0}" data-trans-date="{1}" />';

        $this.DOMInstance.divBetList.addClass('hide');

        // Configure grid's columns.
        columns = [{ HeaderText: tableHeaders.No, Width: 30, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.TransTime, Width: 135, Name: 'ColTransTime', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Choice, Width: 290, Name: 'ColChoice', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Odds, Width: 50, Name: 'ColOdds', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Stake, Width: 80, Name: 'ColStake', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.WinLoss, Width: 80, Name: 'ColWinLoss', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.CompanyShare, Width: 100, Name: 'ColCompanyShare', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.CompanyComm, Width: 100, Name: 'ColCompanyComm', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Details, Width: 45, Name: 'ColDetails', HeaderAlign: 'center', CellAlign: 'center'}];

        // Push data to grid.
        if (data && data.BetItems && data.BetItems.length > 0) {
            var row = null;

            $.each(data.BetItems, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColTransTime', Value: transTimeColFormat.format(item.TransTimeCol) },
                              { Name: 'ColChoice', Value: choiceColFormat.format(item.TransId, item.IsHandicap ? 'handicap' : '', item.ChoiceCol) },
                              { Name: 'ColOdds', Value: oddColFormat.format(item.OddsCol) },
                              { Name: 'ColStake', Value: formatNumberCol(item.Stake, false, null, null) },
                              { Name: 'ColWinLoss', Value: formatNumberCol(item.IsRunning ? '-  ' : item.WinLoss, false, item.Comm, null) },
                              { Name: 'ColStatus', Value: item.StatusCol },
                              { Name: 'ColCompanyShare', Value: formatNumberCol(item.CompanyShare, true, item.IsRunning ? null : item.CompanyWinLoss, null) },
                              { Name: 'ColCompanyShare', Value: formatNumberCol('N/A', true, item.CompanyComm, null) },
                              { Name: 'ColDetails', Value: detailColFormat.format(item.TransId, item.TransDate)}]
                };

                bodyData.push(row);
            });

            row =
            {
                Columns: [{ Name: 'ColNo', Value: '' },
                          { Name: 'ColTransTime', Value: '' },
                          { Name: 'ColChoice', Value: data.BetItems.length },
                          { Name: 'ColOdds', Value: '' },
                          { Name: 'ColStake', Value: formatNumberCol(data.SumStake, false, null, null) },
                          { Name: 'ColWinLoss', Value: formatNumberCol(data.SumWinLoss, false, data.SumComm, null) },
                          { Name: 'ColStatus', Value: '' },
                          { Name: 'ColCompanyShare', Value: formatNumberCol(null, false, data.SumCompanyWinLoss, null) },
                          { Name: 'ColCompanyComm', Value: formatNumberCol(null, true, data.SumCompanyComm, null) },
                          { Name: 'ColDetails', Value: ''}]
            };

            footerData.push(row);

            gridOptions.pagerOption.totalItem = data.TotalItems;
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        gridOptions.footerRows = footerData;
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
        $this.DOMInstance.divBetList.find('div.winloss-details').on('click', function () {
            var $obj = $(this);
            var transId = $obj.attr('data-ref-no');
            var transDate = $obj.attr('data-trans-date');

            getTicketData(transId, transDate);
        });

        $this.DOMInstance.divBetList.find('div.time').each(function (index, obj) {
            var $obj = $(obj);
            var dateValue = $obj.text();

            $obj.text(moment(dateValue).format(CMS.commonConstants.MomentDateTimeFormat));
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
                              { Name: 'ColShare', Value: formatNumberCol(item.PositionTaking, true, item.WinLoss, null) },
                              { Name: 'ColComm', Value: !item.IsSuper ? formatNumberCol(item.Discount, true, item.Comm, null) : formatNumberCol('N/A', false, item.Comm, null)}]
                };

                bodyData.push(row);
            });
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divDetailedWinLossList.removeClass('hide').fanexGrid(gridOptions);
    };

    // Render data table for member bet list.
    var renderSystemParlayDetailsTable = function (data) {
        var bodyData = [];
        var footerData = [];
        var columns = [];
        var gridOptions = {
            showDistinctRow: false,
            showHighlightRow: false,
            showFooter: true,
            bodyRowHeight: 30,
            fullHeight: false,
            gridHeight: 600
        };

        $this.DOMInstance.divSystemParlayDetails.addClass('hide');

        // Configure grid's columns.
        columns = [{ HeaderText: systemParlayDetailsTableHeaders.Date, Width: 80, Name: 'ColDate', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: systemParlayDetailsTableHeaders.Event, Width: 250, Name: 'ColEvent', HeaderAlign: 'center', CellAlign: 'left' },
                   { HeaderText: systemParlayDetailsTableHeaders.Odds, Width: 80, Name: 'ColOdds', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: systemParlayDetailsTableHeaders.Stake, Width: 120, Name: 'ColStake', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: systemParlayDetailsTableHeaders.WinLoss, Width: 120, Name: 'ColWinLoss', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: systemParlayDetailsTableHeaders.Status, Width: 80, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center'}];

        // Push data to grid.
        if (data && data.DailyTicketLists && data.DailyTicketLists.length > 0) {
            var row = null;
            var dateRowCounter = null;
            var groupIndex = -1;
            var statusFormat = '<span class="status">{0}</span>';

            $.each(data.DailyTicketLists, function (dateIndex, dailyTicketList) {
                dateRowCounter = countRowsOfDateData(dailyTicketList.TicketItems);

                $.each(dailyTicketList.TicketItems, function (ticketIndex, ticket) {
                    $.each(ticket.EventNames, function (index, event) {
                        row =
                        {
                            Columns: [{ Name: 'ColEvent', Value: event}]
                        };

                        if (index === 0) {
                            groupIndex++;
                            row.Columns.push(
                                { Name: 'ColOdds', Value: ticket.Odds, MergeRowNumber: ticket.EventNames.length },
                                { Name: 'ColStake', Value: ticket.Stake, MergeRowNumber: ticket.EventNames.length },
                                { Name: 'ColWinLoss', Value: formatNumberCol(ticket.WinLoss, false, ticket.Comm, null), MergeRowNumber: ticket.EventNames.length },
                                { Name: 'ColStatus', Value: statusFormat.format(ticket.Status), MergeRowNumber: ticket.EventNames.length });

                            if (ticketIndex === 0) {
                                row.Columns.unshift({ Name: 'ColDate', Value: dailyTicketList.WinLossDate, MergeRowNumber: dateRowCounter });
                            }
                        }

                        if (groupIndex % 2 === 1) {
                            row.CssClass = 'freegrid-even';
                        }

                        bodyData.push(row);
                    });
                });

                row =
                {
                    Columns: [{ Name: 'ColDate', Value: systemParlayDetailsTableHeaders.Subtotal, MergeColNumber: 4, CellAlign: 'right' },
                              { Name: 'ColWinLoss', Value: formatNumberCol(dailyTicketList.TotalWinLoss, false, dailyTicketList.TotalComm, null) },
                              { Name: 'ColStatus', Value: ''}],
                    CssClass: 'subTotal'
                };
                bodyData.push(row);
            });

            row =
            {
                Columns: [{ Name: 'ColDate', Value: systemParlayDetailsTableHeaders.Total, MergeColNumber: 4, CellAlign: 'right' },
                          { Name: 'ColWinLoss', Value: formatNumberCol(data.TotalWinLoss, false, data.TotalComm, null) },
                          { Name: 'ColStatus', Value: ''}],
                CssClass: 'total'
            };
            footerData.push(row);

            row =
            {
                Columns: [{ Name: 'ColDate', Value: systemParlayDetailsTableHeaders.Outstanding, MergeColNumber: 4, CellAlign: 'right' },
                          { Name: 'ColWinLoss', Value: formatNumberCol(data.Outstanding, false, null, null) },
                          { Name: 'ColStatus', Value: ''}]
            };
            footerData.push(row);
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        gridOptions.footerRows = footerData;
        $this.DOMInstance.divSystemParlayDetails.removeClass('hide').fanexGrid(gridOptions);
    };

    // Count number of rows for each date group.
    var countRowsOfDateData = function (ticketList) {
        var counter = 0;

        $.each(ticketList, function (index, item) {
            counter += item.EventNames.length;
        });

        return counter;
    };

    // Format number column.
    var formatNumberCol = function (firstNumber, isPercent, secondNumber, thirdNumber) {
        var formattedStr = '';
        var cssClass = '';

        if (firstNumber !== null) {
            var percent = '';

            if ($.isNumeric(firstNumber)) {
                percent = isPercent ? '%' : '';
                cssClass = firstNumber < 0 ? 'format-negative' : '';
                firstNumber = isPercent ? firstNumber * 100 : firstNumber;
                firstNumber = numeral(firstNumber).format(CMS.commonConstants.DecimalFormat);
            }

            formattedStr += '<div class="{0}">{1}{2}</div>'.format(cssClass, firstNumber, percent);
        }

        if (secondNumber !== null) {
            if ($.isNumeric(secondNumber)) {
                cssClass = secondNumber < 0 ? 'format-negative' : '';
                secondNumber = numeral(secondNumber).format(CMS.commonConstants.DecimalFormat);
            }

            formattedStr += '<div class="{0}">{1}</div>'.format(cssClass, secondNumber);
        }

        if (thirdNumber !== null) {
            if ($.isNumeric(thirdNumber)) {
                cssClass = thirdNumber < 0 ? 'format-negative' : '';
                thirdNumber = numeral(thirdNumber).format(CMS.commonConstants.DecimalFormat);
            }

            formattedStr += '<div class="{0}">{1}</div>'.format(cssClass, thirdNumber);
        }

        return formattedStr;
    };

    var exportBetList = function () {
        parameters.IsLoadNew = false;
        CMS.downloadFile(urls.ExportMemberBetList, parameters);
    };

    // Global function using to get list of bets which are played from given IP.
    OpenIPInfo = function (ip) {
        return false;
    };

    // Global function using to get sub-items of mix parlay ticket.
    showMP = function (transId, refNo) {
        var divEvent = $('#divEvent_{0}'.format(transId), $this.DOMInstance.divBetList);

        if (divEvent.children().length === 0) {
            var params = {
                RefNo: refNo
            };

            divEvent.addClass('parlay-details');
            $.ajax({
                type: 'POST',
                url: urls.GetMixParlayByRefNo,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(params),
                beforeSend: function () {
                    CMS.loadingBox.show();
                },
                success: function (result) {
                    result = '<br/>' + result;
                    divEvent.html(result);
                    divEvent.slideDown(function () {
                        $this.DOMInstance.divBetList.fanexGrid('resizeGrid');
                    });
                },
                error: function (xhr, status, err) {
                    CMS.handleAjaxError(xhr, status, err);
                },
                complete: function () {
                    CMS.loadingBox.close();
                }
            });
        } else {
            divEvent.slideToggle(function () {
                $this.DOMInstance.divBetList.fanexGrid('resizeGrid');
            });
        }
    };
    showCombMP = function (transId, transIndex, refNo) {
        showMP('{0}{1}'.format(transId, transIndex), refNo);
    };

    // Global function using to get system parlay details.
    showCombMPDetail = function (refNo, winLossDate, transId, betId, custId) {
        var params = {
            RefNo: refNo,
            CustId: custId
        };
        var title = '{0} - {1}'.format(systemParlayDetailsTableHeaders.SystemParlay, transId);

        $.ajax({
            type: 'POST',
            url: urls.GetSystemParlayDetails,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(params),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                pageLayout.Popup(title, $this.DOMInstance.divSystemParlayDetails, { resizable: false }, true);
                renderSystemParlayDetailsTable(result);
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
        divFilterForm: null,
        dpkFromDate: null,
        dpkToDate: null,
        btnGenerate: null,
        divBetList: null,
        divDetailedWinLossList: null,
        divSystemParlayDetails: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};