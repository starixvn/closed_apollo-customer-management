/* File Created: December 18, 2013 */
$(document).ready(function () {
    var agentList = new Apollo.CMS.Account.AccountList.AgentList();

    agentList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountList.AgentList");
Apollo.CMS.Account.AccountList.AgentList = function () {
    var $this = this;
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
    var highLevelAgents = {
        Super: 0,
        MainShareholder: 1,
        Shareholder: 2
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.txtUsername = $('#txtUsername', '#divFilterForm');
        $this.DOMInstance.ddlStatus = $('#ddlStatus', '#divFilterForm');
        $this.DOMInstance.ddlBetStatus = $('#ddlBetStatus', '#divFilterForm');
        $this.DOMInstance.btnSubmit = $('#btnSubmit', '#divFilterForm');
        $this.DOMInstance.divAgentList = $('#divAgentList');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        parameters.AgentId = agentId;
        parameters.PageSize = pageSize;
        parameters.PageIndex = 0;
        parameters.IsOrderByAsc = true;

        $this.DOMInstance.btnSubmit.on('click', function () {
            parameters.Username = $.trim($this.DOMInstance.txtUsername.val());
            parameters.Status = $this.DOMInstance.ddlStatus.val();
            parameters.BetStatus = $this.DOMInstance.ddlBetStatus.val();

            getData();
        });
        $this.DOMInstance.btnSubmit.click();
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

    // Check the agent level and render associate table type.
    var renderTable = function (data, totalItems) {
        if (parameters.AgentLevel == highLevelAgents.Super) {
            //@@TODO: Hide Member List button
            renderMainShareholderTable(data, totalItems);
        } else if (parameters.AgentLevel == highLevelAgents.MainShareholder) {
            //@@TODO: Hide Member List button
            renderShareholderTable(data, totalItems);
        } else {
            renderAgentTable(data, totalItems);
        }
    };

    // Render data table for main shareholder.
    var renderMainShareholderTable = function (data, totalItems) {
        //@@TODO: Implement
    };

    // Render data table for shareholder.
    var renderShareholderTable = function (data) {
        //@@TODO: Implement
    };

    // Render data table for low level agent.
    var renderAgentTable = function (data, totalItems) {
        var bodyData = [];

        $this.DOMInstance.divAgentList.addClass('hide');

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<span class="icon-edit-link" />' },
                              { Name: 'ColUsername', Value: item.Username },
                              { Name: 'ColDownline', Value: item.NumberOfAgent > 0 ? '<span class="link downline" data-agent-id="{0}" data-agent-level="{1}">{2}</span>'.format(item.AgentId, item.AgentLevel, item.NumberOfAgent) : '0' },
                              { Name: 'ColMember', Value: item.NumberOfMember > 0 ? '<span class="link member" data-agent-id="{0}" data-agent-level="{1}">{2}</span>'.format(item.AgentId, item.AgentLevel, item.NumberOfMember) : '0' },
                              { Name: 'ColShadow', Value: item.NumberOfShadow > 0 ? '<span class="link shadow" data-agent-id="{0}" data-agent-level="{1}">{2}</span>'.format(item.AgentId, item.AgentLevel, item.NumberOfShadow) : '0' },
                              { Name: 'ColStatus', Value: '<span class="{0}">{1}</span>'.format(item.RealStatus.toLowerCase().replace(/\./g, ''), item.RealStatus) },
                              { Name: 'ColBetStatus', Value: '<span class="{0}">{1}</span>'.format(item.RealBetStatus.toLowerCase().replace(/\./g, ''), item.RealBetStatus)}]
                };
                bodyData.push(row);
            });
        }

        var gridOptions =
        {
            columns: [{ HeaderText: tableHeaders.No, Width: 50, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Edit, Width: 70, Name: 'ColEdit', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Username, Width: 180, Name: 'ColUsername', HeaderAlign: 'center', CellAlign: 'left', Sort: true, SortDirection: parameters.IsOrderByAsc ? 'asc' : 'desc' },
                      { HeaderText: tableHeaders.Downline, Width: 100, Name: 'ColDownline', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Member, Width: 100, Name: 'ColMember', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Shadow, Width: 100, Name: 'ColShadow', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.BetStatus, Width: 100, Name: 'ColBetStatus', HeaderAlign: 'center', CellAlign: 'center'}],
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
                itemsPerPageArray: [10, 50, 100, 200, 500],
                pagerSelectCallBack: handleChangingPage
            },
            sortOption: {
                serverSortFunction: handleSorting
            },
            gridExpandHeight: 250,
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        $this.DOMInstance.divAgentList.removeClass('hide').fanexGrid(gridOptions);
        bindGridEventhandlers();
    };

    // Bind grid event handlers
    var bindGridEventhandlers = function () {
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
        divAgentList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};