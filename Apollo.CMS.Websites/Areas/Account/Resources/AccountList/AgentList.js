/* File Created: December 18, 2013 */
$(document).ready(function () {
    var agentList = new Apollo.CMS.Account.AccountList.AgentList();

    agentList.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountList.AgentList");
Apollo.CMS.Account.AccountList.AgentList = function () {
    var $this = this;
    var urls = {
        MemberList: CMS.resolveRootUrl('Account/AccountList/MemberList'),
        ShadowList: CMS.resolveRootUrl('Account/AccountList/ShadowList'),
        GetAgentList: CMS.resolveRootUrl('Account/AccountList/GetAgentList')
    };
    var parameters = {
        AgentId: null,
        Username: null,
        Status: null,
        BetStatus: null,
        PageIndex: null,
        PageSize: null,
        OrderDirection: null
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.txtUsername = $('#txtUsername');
        $this.DOMInstance.ddlStatus = $('#ddlStatus');
        $this.DOMInstance.ddlBetStatus = $('#ddlBetStatus');
        $this.DOMInstance.btnSubmit = $('#btnSubmit');
        $this.DOMInstance.divAgentList = $('#divAgentList');
    };

    // Initialize fields and handle events.
    var initFields = function () {
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
        //@@TODO: Call ajax here
        var result = [{
            CustId: 20,
            Username: 'Agent01',
            Downline: 25,
            Member: 5,
            Shadow: 2,
            Status: 'Active',
            BetStatus: 'Stopped'
        }];

        renderTable(result);
    };

    // Check the agent level and render associate table type.
    var renderTable = function (data) {
        //@@TODO: Check agent level here
        renderAgentTable(data);
    };

    // Render data table for main shareholder.
    var renderMainShareholderTable = function (data) {
        //@@TODO: Implement
    };

    // Render data table for shareholder.
    var renderShareholderTable = function (data) {
        //@@TODO: Implement
    };

    // Render data table for low level agent.
    var renderAgentTable = function (data) {
        var bodyData = [];

        $this.DOMInstance.divAgentList.addClass('hide');

        if (data && data.length > 0) {
            var row = null;

            $.each(data, function (index, item) {
                row =
                {
                    Columns: [{ Name: 'ColNo', Value: index + 1 },
                              { Name: 'ColEdit', Value: '<span class="edit-class" />' },
                              { Name: 'ColUsername', Value: item.Username },
                              { Name: 'ColDownline', Value: '<span class="link downline" data-cust-id="{0}">{1}</span>'.format(item.CustId, item.Downline) },
                              { Name: 'ColMember', Value: '<span class="link member" data-cust-id="{0}">{1}</span>'.format(item.CustId, item.Member) },
                              { Name: 'ColShadow', Value: '<span class="link shadow" data-cust-id="{0}">{1}</span>'.format(item.CustId, item.Shadow) },
                              { Name: 'ColStatus', Value: '<span class="{0}">{1}</span>'.format(item.Status.toLowerCase().replace(/./g, ''), item.Status) },
                              { Name: 'ColBetStatus', Value: '<span class="{0}">{1}</span>'.format(item.BetStatus.toLowerCase().replace(/./g, ''), item.BetStatus)}]
                };
                bodyData.push(row);
            });
        }

        var gridOptions =
        {
            columns: [{ HeaderText: tableHeaders.No, Width: 50, Name: 'ColNo', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Edit, Width: 70, Name: 'ColEdit', HeaderAlign: 'center', CellAlign: 'left' },
                      { HeaderText: tableHeaders.Username, Width: 180, Name: 'ColUsername', HeaderAlign: 'center', CellAlign: 'left' },
                      { HeaderText: tableHeaders.Downline, Width: 100, Name: 'ColDownline', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Member, Width: 100, Name: 'ColMember', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Shadow, Width: 100, Name: 'ColShadow', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.Status, Width: 100, Name: 'ColStatus', HeaderAlign: 'center', CellAlign: 'center' },
                      { HeaderText: tableHeaders.BetStatus, Width: 100, Name: 'ColBetStatus', HeaderAlign: 'center', CellAlign: 'center'}],
            showFooter: true,
            gridExpandHeight: 250,
            showNumberColumn: true,
            showPager: true,
            pagerOption: {
                itemsPerPage: 20,
                maxItemsPerPage: 100,
                currentPage: 0,
                startPerPage: 10,
                pageStep: 10
            },
            bodyRowHeight: 30,
            bodyRows: bodyData
        };

        $this.DOMInstance.divAgentList.removeClass('hide').fanexGrid(gridOptions);
    };

    $this.DOMInstance = {
        txtUsername: null,
        ddlStatus: null,
        ddlBetStatus: null,
        btnSubmit: null,
        divAgentListContainer: null,
        divAgentList: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};