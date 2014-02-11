/* global moment, Apollo, CMS, Namespace, layoutHelper */

Namespace.Register("Apollo.CMS.LayoutHelper");
Apollo.CMS.LayoutHelper = function () {
    "use strict";
    var $this = this;

    // Query and save DOM elements.
    var cacheDOM = function () {
        $this.DOMInstance.divLeftPanel = $('#searchPanel, #leftContent');
        $this.DOMInstance.divLeftContent = $('#leftContent');
        $this.DOMInstance.divRightPanel = $('#rightHeader, #mainContent');
        $this.DOMInstance.divMainContent = $('#mainContent');
        $this.DOMInstance.spanCollapseExpand = $('#collapseExpand');
        $this.DOMInstance.ifrMainContent = $('#ifrMainContent');
    };

    // Fix the issue related to the width of left panel when rendering by IE.
    var fixLeftPanelWidthForIE = function () {
        // check IE8 and resolution > 993px
        if ($.browser.msie && $.browser.version === '8.0') {
            if (screen.width > 993) {
                $this.DOMInstance.divLeftPanel.addClass('width-ie8-20');
                $this.DOMInstance.divRightPanel.addClass('width-ie8-80');
            } else {
                $this.DOMInstance.divLeftPanel.removeClass('width-ie8-20');
                $this.DOMInstance.divRightPanel.removeClass('width-ie8-80');
            }
        }
    };

    // Toggle left panel
    var toggleLeftPanel = function (isExpanded, timespan) {
        timespan = timespan ? timespan : 100;

        if (isExpanded) {
            $this.DOMInstance.divLeftContent.animate({
                width: 'toggle'
            }, timespan, function () {
                $this.DOMInstance.spanCollapseExpand.removeClass('icon-expand').addClass('icon-collapse');
                $this.DOMInstance.spanCollapseExpand.attr('title', 'Collapse');
                $this.DOMInstance.divMainContent.removeClass().addClass('col-xs-9 col-sm-9 col-md-10 mainContent');
                $.cookie('is-expaned', 'true', { path: '/' });

                fixLeftPanelWidthForIE();
            });
        } else {
            $this.DOMInstance.divLeftContent.animate({
                width: 'toggle'
            }, timespan, function () {
                $this.DOMInstance.spanCollapseExpand.removeClass('icon-collapse').addClass('icon-expand');
                $this.DOMInstance.spanCollapseExpand.attr('title', 'Expand');
                $this.DOMInstance.divMainContent.removeClass().addClass('col-xs-12 col-sm-12 col-md-12 mainContent');
                $.cookie('is-expaned', 'false', { path: '/' });
            });
        }
    };

    // Change behavior of links
    var catchLinkEvents = function () {
        $('a[href]:not([href^="#"]):not([data-is-logout])').live('click', function () {
            var linkHref = this.href;
            var $parent = $(this).closest('ul');
            
            $this.ResetMenuState();
            if ($parent.hasClass('subMenu')) {
                $parent = $parent.parent();
                $parent.addClass('active');
                $parent.removeClass('hovered-menu');
            }

            $this.LoadMainContent(linkHref);

            return false;
        });
    };

    // Bind event handlers.
    var bindEventHandlers = function () {
        var isExpanded = $.cookie('is-expaned');

        $this.DOMInstance.spanCollapseExpand.on('click', function () {
            if ($this.DOMInstance.spanCollapseExpand.hasClass('icon-collapse')) {
                toggleLeftPanel(false);
            } else {
                toggleLeftPanel(true);
            }
        });

        if (isExpanded === 'true') {
            $this.DOMInstance.spanCollapseExpand.click();
        }

        catchLinkEvents();
        $('select').selectBoxIt();

        $('ul.mainMenu > li').hover(function () {
            $(this).addClass('hovered-menu');
        }, function () {
            $(this).removeClass('hovered-menu');
        });

        // Make the click event on the page be the same through the iframe.
        $('html').on('click', function () {
            if (window.frames['MainContent']) {
                $('html', $(window.frames['MainContent'].document)).click();
            }
        }).on('hover', function () {
            if (window.frames['MainContent']) {
                $('html', $(window.frames['MainContent'].document)).hover();
            }
        });
    };

    // Cached DOM elements.
    $this.DOMInstance = {
        divLeftPanel: null,
        divLeftContent: null,
        divRightPanel: null,
        divMainContent: null,
        spanCollapseExpand: null,
        ifrMainContent: null
    };

    // Initialize page.
    $this.InitPage = function () {
        cacheDOM();
        fixLeftPanelWidthForIE();
        bindEventHandlers();
    };
    $this.ExpandLeftPanel = function () {
        if (!$this.DOMInstance.spanCollapseExpand.hasClass('icon-collapse')) {
            toggleLeftPanel(true);
        }
    };
    $this.CreateDatepicker = function ($obj, defaultValue, onSelectHandler, onCloseHandler) {
        var defaultDate = defaultValue ? moment(defaultValue).format(CMS.commonConstants.DateFormat) : 'today';

        $obj.datepicker({
            dateFormat: CMS.commonConstants.DateFormat,
            showOn: 'button',
            buttonImage: CMS.datePickerButtonUrl,
            buttonImageOnly: true,
            changeMonth: true,
            changeYear: true,
            onSelect: onSelectHandler || function () { },
            onClose: onCloseHandler || function () { }
        }).datepicker('setDate', defaultDate);

        $obj.attr('readonly', true);
        $obj.focus(function () {
            $obj.datepicker('show');
        });

        return $obj;
    };
    $this.LoadMainContent = function (link) {
        $this.DOMInstance.ifrMainContent.attr('src', link);
    };
    $this.ResetMenuState = function () {
        $('ul.mainMenu > li.active, #searchTreeView').removeClass('active');
    };
};
var layoutHelper = new Apollo.CMS.LayoutHelper();

$(document).ready(function () {
    "use strict";
    layoutHelper.InitPage();
});