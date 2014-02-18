/* File Created: December 18, 2013 */

/* global Apollo, CMS, Namespace, layoutHelper, tableHeaders, agentId, pageSize, hasMember, isSuper */

$(document).ready(function () {
    'use strict';
    var agentList = new Apollo.CMS.Account.AccountList.AgentList();

    agentList.InitPage();
});

Namespace.Register('Apollo.CMS.Account.AccountList.AgentList');
Apollo.CMS.Account.AccountList.AgentList = function () {
    'use strict';
    var $this = this;
    var contextMenuTimeout = null;
    var urls = {
        AgentList: CMS.resolveRootUrl('Account/AccountList/AgentList?agentId={0}&agentLevel={1}'),
        MemberList: CMS.resolveRootUrl('Account/AccountList/MemberList?agentId={0}&agentLevel={1}'),
        ShadowList: CMS.resolveRootUrl('Account/AccountList/ShadowList?agentId={0}&agentLevel={1}'),
        GetAgentList: CMS.resolveRootUrl('Account/AccountList/GetAgentList')
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
        $this.DOMInstance.divFilterForm = $('#divFilterForm');
        $this.DOMInstance.txtUsername = $('#txtUsername', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.ddlStatus = $('#ddlStatus', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.ddlBetStatus = $('#ddlBetStatus', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.btnSubmit = $('#btnSubmit', $this.DOMInstance.divFilterForm);
        $this.DOMInstance.divAgentList = $('#divAgentList');
        $this.DOMInstance.ulContextMenu = $('#ulContextMenu');
        $this.DOMInstance.linkContextMenuItem = $this.DOMInstance.ulContextMenu.find('li > a');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        parameters.AgentId = agentId;
        parameters.PageSize = pageSize;
        parameters.IsOrderByAsc = true;

        $this.DOMInstance.txtUsername.on('keyup', function (event) {
            if (event.keyCode === 13) {
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

        $this.DOMInstance.ulContextMenu.on('mouseover', function () {
            clearTimeout(contextMenuTimeout);
        }).on('mouseout', function () {
            contextMenuTimeout = setTimeout(hideContextMenu, 3000);
        });

        $('html').on('click', function () {
            hideContextMenu();
        });
    };

    // Get data
    var getData = function () {
        $.ajax({
            type: 'POST',
            url: urls.GetAgentList,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
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
                   { HeaderText: tableHeaders.Username, Width: 180, Name: 'ColUsername', HeaderAlign: 'center', CellAlign: 'left',
                       Sort: true, SortDirection: parameters.IsOrderByAsc ? 'asc' : 'desc'
                   },
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
            var editFormat = '<div class="icon-edit-link" data-agent-id="{0}"/>';
            var downlineFormat = '<span class="link downline" data-agent-id="{0}" data-agent-level="{1}">{2}</span>';
            var shadowFormat = '<span class="link shadow" data-agent-id="{0}" data-agent-level="{1}">{2}</span>';
            var memberFormat = '<span class="link member" data-agent-id="{0}" data-agent-level="{1}">{2}</span>';
            var statusFormat = '<span class="{0}">{1}</span>';
            var downlineCol = null;
            var shadowCol = null;
            var memberCol = null;

            $.each(data, function (index, item) {
                downlineCol = item.NumberOfAgent > 0 ? downlineFormat.format(item.AgentId, item.AgentLevel, item.NumberOfAgent) : '0';
                shadowCol = item.NumberOfShadow > 0 ? shadowFormat.format(item.AgentId, item.AgentLevel, item.NumberOfShadow) : '0';
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: editFormat.format(item.AgentId) },
                              { Name: 'ColUsername', Value: item.Username },
                              { Name: 'ColDownline', Value: downlineCol },
                              { Name: 'ColShadow', Value: shadowCol },
                              { Name: 'ColStatus', Value: statusFormat.format(item.Status.toLowerCase().replace(/\./g, ''), item.Status) },
                              { Name: 'ColBetStatus', Value: statusFormat.format(item.BetStatus.toLowerCase().replace(/\./g, ''), item.BetStatus)}]
                };

                if (isSuper) {
                    row.Columns.push({ Name: 'ColCurrency', Value: item.Currency });
                } else if (hasMember) {
                    memberCol = item.NumberOfMember > 0 ? memberFormat.format(item.AgentId, item.AgentLevel, item.NumberOfMember) : '0';
                    row.Columns.splice(4, 0, { Name: 'ColMember', Value: memberCol });
                }

                bodyData.push(row);
            });
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        $this.DOMInstance.divAgentList.removeClass('hide').fanexGrid(gridOptions);
        bindGridEventhandlers();
    };

    // Bind grid event handlers
    var bindGridEventhandlers = function () {
        var contextMenuWidth = $this.DOMInstance.ulContextMenu.width();

        $('div.icon-edit-link', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var contextAgentId = $this.DOMInstance.linkContextMenuItem.attr('data-agent-id');
            var cellPosition = cell.offset();

            clearTimeout(contextMenuTimeout);

            if (currentAgentId !== contextAgentId) {
                contextMenuTimeout = setTimeout(hideContextMenu, 3000);
                $this.DOMInstance.linkContextMenuItem.attr('data-agent-id', currentAgentId);
                $this.DOMInstance.ulContextMenu.hide().slideDown('fast').offset({
                    top: cellPosition.top + cell.height() + 5,
                    left: cellPosition.left - contextMenuWidth / 2
                });
            } else {
                hideContextMenu();
            }

            return false;
        });

        $('span.downline', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var currentAgentLevel = cell.attr('data-agent-level');

            layoutHelper.LoadMainContent(urls.AgentList.format(currentAgentId, currentAgentLevel));
        });

        $('span.member', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var currentAgentLevel = cell.attr('data-agent-level');

            layoutHelper.LoadMainContent(urls.MemberList.format(currentAgentId, currentAgentLevel));
        });

        $('span.shadow', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var currentAgentLevel = cell.attr('data-agent-level');

            layoutHelper.LoadMainContent(urls.ShadowList.format(currentAgentId, currentAgentLevel));
        });
    };
    var hideContextMenu = function () {
        $this.DOMInstance.linkContextMenuItem.attr('data-agent-id', 0);
        $this.DOMInstance.ulContextMenu.hide();
    };

    // Handle paging at server side.
    var handleChangingPage = function (pageIndex, itemsPerPage) {
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
        divAgentList: null,
        ulContextMenu: null,
        linkContextMenuItem: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};