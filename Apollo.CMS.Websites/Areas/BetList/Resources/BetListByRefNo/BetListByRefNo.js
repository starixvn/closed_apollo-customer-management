/* File Created: January 22, 2014 */

/* global Apollo, CMS, Namespace, moment, numeral, pageLayout, searchBar */
/* global tableHeaders, detailTableHeaders, systemParlayDetailsTableHeaders, hasSearchAccountPermission */
/* exported betListByRefNo, OpenIPInfo, showMP, showCombMPDetail, showCombMP */

Namespace.Register('Apollo.CMS.BetList.BetListByRefNo');
Apollo.CMS.BetList.BetListByRefNo = function () {
    'use strict';
    var $this = this;
    var urls = {
        BetListByRefNo: CMS.resolveRootUrl('BetList/BetListByRefNo/GetBetListByRefNo'),
        GetSystemParlayDetails: CMS.resolveRootUrl('BetList/BetListByRefNo/GetSystemParlayDetails'),
        TicketDetail: CMS.resolveRootUrl('BetList/BetListByRefNo/GetTicketDetail')
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divFilterForm = $('#divFilterForm');
        $this.DOMInstance.txtRefNo = $('#txtRefNo', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.btnSearch = $('#btnSearch', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.divBetList = $('#divBetList');
        $this.DOMInstance.divDetailedWinLossList = $('#divDetailedWinLossList');
        $this.DOMInstance.divSystemParlayDetailsWrapper = $('#divSystemParlayDetailsWrapper');
        $this.DOMInstance.divPopup = $('#divPopup');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        $this.DOMInstance.btnSearch.on('click', function () {
            var refNo = $.trim($this.DOMInstance.txtRefNo.val());

            $this.DOMInstance.txtRefNo.val(refNo);
            getData(refNo);
        });
        $this.DOMInstance.txtRefNo.on('keyup', function (event) {
            if (event.keyCode === 13) {
                $this.DOMInstance.btnSearch.click();
            }
        });
        $this.DOMInstance.txtRefNo.focus();
    };

    // Get data
    var getData = function (refNo) {
        var parameter = {
            TransId: refNo
        };

        $.ajax({
            type: 'POST',
            url: urls.BetListByRefNo,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(parameter),
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
            showFooter: true,
            gridExpandHeight: 200,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        // Element formats.
        var transTimeColFormat = '<div class="trans-time">{0}</div>';
        var usernameFormat = '<div {0} data-cust-id="{1}">{2}</div>';
        var choiceColFormat = '<div id="choice_{0}" class="choice {1}">{2}</div>';
        var oddColFormat = '<div class="odds">{0}</div>';
        var detailColFormat = '<div class="winloss-details detail-icon" data-ref-no="{0}" data-trans-date="{1}" />';
        var usernameClass = hasSearchAccountPermission ? 'class="link username"' : '';

        $this.DOMInstance.divBetList.addClass('hide');

        // Configure grid's columns.
        columns = [{ HeaderText: tableHeaders.TransTime, Width: 135, Name: 'ColTransTime', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Member, Width: 120, Name: 'ColMember', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.Choice, Width: 290, Name: 'ColChoice', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Odds, Width: 50, Name: 'ColOdds', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Stake, Width: 80, Name: 'ColStake', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.WinLoss, Width: 80, Name: 'ColWinLoss', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center' },
                   { HeaderText: tableHeaders.CompanyShare, Width: 100, Name: 'ColCompanyShare', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.CompanyComm, Width: 100, Name: 'ColCompanyComm', HeaderAlign: 'center', CellAlign: 'right' },
                   { HeaderText: tableHeaders.Details, Width: 50, Name: 'ColDetails', HeaderAlign: 'center', CellAlign: 'center'}];

        // Push data to grid.
        if (data && data.Ticket) {
            var row = {
                Columns: [{ Name: 'ColTransTime', Value: transTimeColFormat.format(data.Ticket.TransTimeCol) },
                          { Name: 'ColMember', Value: usernameFormat.format(usernameClass, data.Ticket.CustId, data.Ticket.Username) },
                          { Name: 'ColChoice', Value: choiceColFormat.format(data.Ticket.TransId, data.Ticket.IsHandicap ? 'handicap' : '', getChoice(data)) },
                          { Name: 'ColOdds', Value: oddColFormat.format(data.Ticket.OddsCol) },
                          { Name: 'ColStake', Value: formatNumberCol(data.Ticket.Stake, false, null, null) },
                          { Name: 'ColWinLoss', Value: formatNumberCol(data.Ticket.IsRunning ? '-  ' : data.Ticket.WinLoss, false, data.Ticket.Comm, null) },
                          { Name: 'ColStatus', Value: data.Ticket.StatusCol },
                          { Name: 'ColCompanyShare', Value: formatNumberCol(
                                                                data.Ticket.CompanyShare, true,
                                                                data.Ticket.IsRunning ? null : data.Ticket.CompanyWinLoss,
                                                                null) },
                          { Name: 'ColCompanyShare', Value: formatNumberCol('N/A', true, data.Ticket.CompanyComm, null) },
                          { Name: 'ColDetails', Value: detailColFormat.format(data.Ticket.TransId, data.Ticket.TransDate)}]
            };
            bodyData.push(row);
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divBetList.removeClass('hide').fanexGrid(gridOptions);
        handleGridEvents();
    };

    // Handle events raised in the grid.
    var handleGridEvents = function () {
        $this.DOMInstance.divBetList.find('div.winloss-details').on('click', function () {
            var $obj = $(this);
            var transId = $obj.attr('data-ref-no');
            var transDate = $obj.attr('data-trans-date');

            getTicketData(transId, transDate);
        });

        $this.DOMInstance.divBetList.find('.username').on('click', function () {
            var $obj = $(this);
            var custId = $obj.attr('data-cust-id');
            var username = $.trim($obj.text());

            searchBar.SearchCustomer(true, custId, username);
        });

        $this.DOMInstance.divBetList.find('div.time').each(function (index, obj) {
            var $obj = $(obj);
            var dateValue = $obj.text();

            $obj.text(moment(dateValue).format(CMS.commonConstants.MomentDateTimeFormat));
        });
    };

    var getChoice = function (data) {
        var divWrapper = $('<div>').html(data.Ticket.ChoiceCol);

        if (data && data.IsParlayTicket) {
            var parlaySubTicket = '<br/>' + data.ParlaySubTicket;
            var divParlayTicket = divWrapper.children(':last');

            divParlayTicket.removeAttr('style');
            divParlayTicket.addClass('parlay-details');
            divParlayTicket.html(parlaySubTicket);
        }

        return divWrapper.html();
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
                              { Name: 'ColComm', Value: !item.IsSuper ?
                                                        formatNumberCol(item.Discount, true, item.Comm, null) :
                                                        formatNumberCol('N/A', false, item.Comm, null)}]
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

        $this.DOMInstance.divSystemParlayDetails = $('#divSystemParlayDetails', $this.DOMInstance.divPopup);
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
                                { Name: 'ColWinLoss', Value: formatNumberCol(ticket.WinLoss, false, ticket.Comm, null),
                                  MergeRowNumber: ticket.EventNames.length },
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

    // Count number of rows for each date group.
    var countRowsOfDateData = function (ticketList) {
        var counter = 0;

        $.each(ticketList, function (index, item) {
            counter += item.EventNames.length;
        });

        return counter;
    };

    // Gets details of a System parlay ticket.
    $this.GetSystemParlayDetails = function (refNo, winLossDate, transId, betId, custId) {
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
                pageLayout.Popup(title, $this.DOMInstance.divSystemParlayDetailsWrapper, { resizable: false }, true);
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
        txtRefNo: null,
        btnSearch: null,
        divBetList: null,
        divDetailedWinLossList: null,
        divSystemParlayDetailsWrapper: null,
        divSystemParlayDetails: null,
        divPopup: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};

var betListByRefNo = new Apollo.CMS.BetList.BetListByRefNo();

$(document).ready(function () {
    'use strict';

    betListByRefNo.InitPage();
});


// Global function using to get list of bets which are played from given IP.
var OpenIPInfo = function () {
    'use strict';
    return false;
};

// Global function using to get sub-items of mix parlay ticket.
var showMP = function () {
};

// Global function using to get list of sub-tickets of a System Parlay ticket.
var showCombMP = function () {
};

// Global function using to get system parlay details.
var showCombMPDetail = function (refNo, winLossDate, transId, betId, custId) {
    'use strict';
    betListByRefNo.GetSystemParlayDetails(refNo, winLossDate, transId, betId, custId);
};
