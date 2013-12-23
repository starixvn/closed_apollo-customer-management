/* File Created: December 18, 2013 */
$(document).ready(function () {
    var agentList = new Apollo.CMS.Account.AccountList.AgentList();

    agentList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountList.AgentList");
Apollo.CMS.Account.AccountList.AgentList = function () {
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
            if (event.keyCode == 13) {
                $this.DOMInstance.btnSubmit.click();
            }
        });

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
            type: "POST",
            url: urls.GetAgentList,
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
            $this.DOMInstance.divFilterForm.removeClass('filter-mainAgent').addClass('shareholder').attr('data-original-width', 758);
        }

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<span class="icon-edit-link" data-agent-id="{0}"/>'.format(item.AgentId) },
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

    // Bind grid event handlers
    var bindGridEventhandlers = function (event) {
        var contextMenuWidth = $this.DOMInstance.ulContextMenu.width();

        $('.icon-edit-link', $this.DOMInstance.divAgentList).on('click', function (event) {
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

        $('.downline', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var currentAgentLevel = cell.attr('data-agent-level');

            CMS.navigateTo(urls.AgentList.format(currentAgentId, currentAgentLevel));
        });

        $('.member', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var currentAgentLevel = cell.attr('data-agent-level');

            CMS.navigateTo(urls.MemberList.format(currentAgentId, currentAgentLevel));
        });

        $('.shadow', $this.DOMInstance.divAgentList).on('click', function () {
            var cell = $(this);
            var currentAgentId = cell.attr('data-agent-id');
            var currentAgentLevel = cell.attr('data-agent-level');

            CMS.navigateTo(urls.ShadowList.format(currentAgentId, currentAgentLevel));
        });
    };
    var hideContextMenu = function () {
        $this.DOMInstance.linkContextMenuItem.attr('data-agent-id', 0);
        $this.DOMInstance.ulContextMenu.hide();
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
        divAgentList: null,
        ulContextMenu: null,
        linkContextMenuItem: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};