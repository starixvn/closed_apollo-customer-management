/* File Created: December 5, 2013 */
$(document).ready(function () {
    var accountInfo = new Apollo.CMS.Account.AccountInfo();

    accountInfo.InitPage();
});

Namespace.Register("Apollo.CMS.Account.AccountInfo");
Apollo.CMS.Account.AccountInfo = function () {
    var $this = this;
    var urls = {
        GetUplineAgents: CMS.resolveRootUrl('Account/AccountInfo/GetUplineAgents'),
        GetDownlineAgents: CMS.resolveRootUrl('Account/AccountInfo/GetDownlineAgents'),
        GetDownlineMembers: CMS.resolveRootUrl('Account/AccountInfo/GetDownlineMembers'),
        LoginInternally: CMS.resolveRootUrl('Account/AccountInfo/RedirectToAgentSite?agentId={0}')
    };
    var uplineTimeout = null;

    // Show list of upline agents.
    var showUplineAgents = function () {
        $('#view-more, #icon-more').live('click', function (event) {
            $('#list-agent').slideToggle('fast', function () {
                clearTimeout(uplineTimeout);
                if ($('#list-agent').is(':visible')) {
                    $('#icon-more').addClass('active');
                    uplineTimeout = setTimeout(hideUplineAgent, 3000);
                } else {
                    $('#icon-more').removeClass('active');
                }
            });

            event.stopPropagation();
        });

        $('#list-agent').live('mouseover', function () {
            clearTimeout(uplineTimeout);
        });

        $('#list-agent').live('mouseout', function () {
            uplineTimeout = setTimeout(hideUplineAgent, 3000);
        });

        $('.search-link[data-customer-id]', '#leftContent').live('click', function () {
            var customerId = $(this).attr('data-customer-id');
            var username = $(this).text();
            var isMember = $(this).attr('data-is-member');

            isMember = isMember ? isMember : false;
            searchBar.SearchCustomer(isMember, customerId, username);
        });

        $('.icon-link-home[data-agent-id]', '#leftContent').live('click', function () {
            var agentId = $(this).attr('data-agent-id');

            CMS.openNewTab(urls.LoginInternally.format(agentId));
        });
    };

    // Hide list of upline agents
    var hideUplineAgent = function () {
        $("#list-agent").fadeOut("fast", function () {
            $('#icon-more').removeClass('active');
        });
    };

    // Handle selecting tab event.
    var showTabContent = function () {
        $('a[data-toggle="tab"]', '#leftContent').live('click', function (event) {
            var customerId = $('#hdnCustomerId').val();
            var tabId = $(this).attr('href');
            var tab = $(tabId);
            var url = '';

            $.cookie('left-panel-current-tab', tabId, { path: '/' });

            if (tab.children().length == 0) {
                if (tabId === '#agent') {
                    var params = {
                        ParentId: customerId
                    };

                    getDownline(params, urls.GetDownlineAgents, tab);
                } else if (tabId === '#member') {
                    var params = {
                        AgentId: customerId
                    };

                    getDownline(params, urls.GetDownlineMembers, tab);
                }
            }
        });
    };

    // Bind event handlers to controls
    var bindEventHandlers = function () {
        showTabContent();
        showUplineAgents();
    };

    var initFields = function () {
        var currentTabId = $.cookie('left-panel-current-tab');
        var currentTab = $('a[href="{0}"]'.format(currentTabId));

        currentTab.tab('show');
        currentTab.click();
    };

    // Get downline data.
    var getDownline = function (params, url, container) {

        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: JSON.stringify(params),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                container.html(result);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    $this.InitPage = function () {
        bindEventHandlers();
        initFields();
    };
}