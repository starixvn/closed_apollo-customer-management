$(document).ready(function () {
    layoutHelper.InitPage();
});

Namespace.Register("Apollo.CMS.LayoutHelper");
Apollo.CMS.LayoutHelper = function () {
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
        if ($.browser.msie && $.browser.version == '8.0') {
            if (screen.width > 993) {
                $this.DOMInstance.divLeftPanel.addClass('width-ie8-20');
                $this.DOMInstance.divRightPanel.addClass('width-ie8-80');
            } else {
                $this.DOMInstance.divLeftPanel.removeClass('width-ie8-20');
                $this.DOMInstance.divRightPanel.removeClass('width-ie8-80');
            }
        }
    };

    // Resize the filter form and grid of the right content, if there is any, when the window is resized.
    var resizeRighContent = function () {
        var mainContentWidth = parseInt($this.DOMInstance.divMainContent.css('width'), 10) - 40;
        var gridWidth = 1024;

        $('div.resizable-grid').each(function () {
            var grid = $(this);
            if (grid.children().length > 0) {
                grid.fanexGrid("resizeGrid");
                gridWidth = parseInt(grid.css('width'), 10);
            }
        });

        $('div.resizable-form').each(function () {
            var form = $(this);
            var originalWidth = parseInt(form.attr('data-original-width'), 10);

            if (originalWidth > mainContentWidth) {
                var filterFormWidth = mainContentWidth > gridWidth ? gridWidth : mainContentWidth;

                form.css('width', filterFormWidth);
            } else {
                form.removeAttr('style');
            }
        });
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
                resizeRighContent();
            });
        } else {
            $this.DOMInstance.divLeftContent.animate({
                width: "toggle"
            }, timespan, function () {
                $this.DOMInstance.spanCollapseExpand.removeClass('icon-collapse').addClass('icon-expand');
                $this.DOMInstance.spanCollapseExpand.attr('title', 'Expand');
                $this.DOMInstance.divMainContent.removeClass().addClass('col-xs-12 col-sm-12 col-md-12 mainContent');
                $.cookie('is-expaned', 'false', { path: '/' });

                resizeRighContent();
            });
        }
    };

    // Change behavior of links
    var catchLinkEvents = function () {
        $('a:not([href^="#"])').on('click', function () {
            var linkId = this.id;
            var link = this.href;

            $.cookie('menu-id', linkId, { path: '/' });
            $this.LoadMainContent(link);

            return false;
        }).each(function () {
            var menuId = $.cookie('menu-id');

            if (this.id === menuId) {
                $(this).addClass('active');
            }
        });
    };

    // Bind event handlers.
    var bindEventHandlers = function () {
        var isExpanded = $.cookie('is-expaned');
        var lazyLayout = _.debounce(resizeRighContent, 50);

        $this.DOMInstance.spanCollapseExpand.on('click', function () {
            if ($this.DOMInstance.spanCollapseExpand.hasClass('icon-collapse')) {
                toggleLeftPanel(false);
            } else {
                toggleLeftPanel(true);
            }
        });
        toggleLeftPanel(isExpanded === 'true');
        catchLinkEvents();
        $(window).resize(lazyLayout);
        $this.DOMInstance.ifrMainContent.on('load', function () {
            catchLinkEvents();
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
};
var layoutHelper = new Apollo.CMS.LayoutHelper();