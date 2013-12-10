var Layout = function ($) {
    return {
        collapseExpand: function () {
            var collapseExpand = $('#collapseExpand');
            var isExpanded = $.cookie('is-expaned');

            collapseExpand.on('click', function () {
                if (collapseExpand.hasClass('icon-collapse')) {
                    Layout.collapseExpandLeftPanel(false);
                } else {
                    Layout.collapseExpandLeftPanel(true);
                }
            });

            if (isExpanded == 'false') {
                Layout.collapseExpandLeftPanel(false, 20);
            }
        },
        collapseExpandLeftPanel: function (isExpanded, timespan) {
            var leftContent = $('#leftContent');
            var mainContent = $('#mainContent');
            var collapseExpand = $('#collapseExpand');

            timespan = timespan ? timespan : 100;

            if (isExpanded) {
                leftContent.animate({
                    width: "toggle"
                }, timespan, function () {
                    // Animation complete.
                    collapseExpand.removeClass('icon-expand').addClass('icon-collapse');
                    collapseExpand.attr('title', 'Collapse');
                    mainContent.removeClass().addClass('col-xs-9 col-sm-9 col-md-10 mainContent');
                    $.cookie('is-expaned', 'true', { path: '/' });
                    Layout.fixLaftPanelWidthForIE();
                });
            } else {
                leftContent.animate({
                    width: "toggle"
                }, timespan, function () {
                    // Animation complete.
                    collapseExpand.removeClass('icon-collapse').addClass('icon-expand');
                    collapseExpand.attr('title', 'Expand');
                    mainContent.removeClass().addClass('col-xs-12 col-sm-12 col-md-12 mainContent');
                    $.cookie('is-expaned', 'false', { path: '/' });
                });
            }
        },
        expandLefPanel: function () {
            var collapseExpand = $('#collapseExpand');

            if (!collapseExpand.hasClass('icon-collapse')) {
                Layout.collapseExpandLeftPanel(true);
            }
        },
        fixLaftPanelWidthForIE: function () {
            // check IE8 and resolution > 993px
            if ($.browser.msie && $.browser.version == '8.0') {
                var leftPanel = $('#searchPanel, #leftContent'),
                    rightPanel = $('#rightHeader, #mainContent');
                if (screen.width > 993) {
                    leftPanel.addClass('width-ie8-20');
                    rightPanel.addClass('width-ie8-80');
                } else {
                    leftPanel.removeClass('width-ie8-20');
                    rightPanel.removeClass('width-ie8-80');
                }
            }
        },
        createDatepicker: function ($obj, defaultValue, onSelectHandler, onCloseHandler) {
            var defaultDate = defaultValue ? moment(defaultValue).format(CMS.commonConstants.DateFormat) : 'today';

            $obj.datepicker({
                dateFormat: CMS.commonConstants.DateFormat,
                showOn: 'button',
                buttonImage: CMS.datePickerButtonUrl,
                buttonImageOnly: true,
                changeMonth: true,
                onSelect: onSelectHandler ? onSelectHandler : function () { },
                onClose: onCloseHandler ? onCloseHandler : function () { }
            }).datepicker('setDate', defaultDate);

            $obj.attr('readonly', true);
            $obj.focus(function () {
                $obj.datepicker('show');
            });

            return $obj;
        }
    };
} (jQuery);

$(document).ready(function () {
    Layout.fixLaftPanelWidthForIE();

    $('select').selectBoxIt();
    
    // collpase or expand left content
    Layout.collapseExpand();
    
    // Set active menu css class
    $('ul.mainMenu > li, .containTreeview .treeview').on('click', function (event) {
        var $this = $(this);

        $.cookie('menu-id', $this.attr('id'), { path: '/' });
    }).each(function () {
        var $this = $(this);
        var menuId = $.cookie('menu-id');

        if ($this.attr('id') == menuId) {
            $this.addClass('active');
        }
    });
});