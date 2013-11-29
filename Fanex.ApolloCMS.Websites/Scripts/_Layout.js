var Layout = function ($) {
    return {
        collapseExpand: function () {
            var leftContent = $('#leftContent'),
                mainContent = $('#mainContent'),
                collapseExpand = $('#collapseExpand');
            collapseExpand.on('click', function () {
                if (collapseExpand.hasClass('icon-collapse')) {
                    leftContent.animate({
                        width: "toggle"
                    }, 100, function () {
                        // Animation complete.
                        collapseExpand.removeClass('icon-collapse').addClass('icon-expand');
                        collapseExpand.attr('title', 'Expand');
                        mainContent.removeClass().addClass('col-xs-12 col-sm-12 col-md-12 mainContent');
                        localStorage.setItem('isCollapse', 'false');
                    });
                } else {
                    leftContent.animate({
                        width: "toggle"
                    }, 100, function () {
                        // Animation complete.
                        collapseExpand.removeClass('icon-expand').addClass('icon-collapse');
                        collapseExpand.attr('title', 'Collapse');
                        mainContent.removeClass().addClass('col-xs-9 col-sm-9 col-md-10 mainContent');
                        localStorage.setItem('isCollapse', 'true');
                        Layout.fixLaftPanelWidthForIE();
                    });
                }
            });

            var isCollapse = localStorage.getItem('isCollapse');
            if (isCollapse == 'false') {
                leftContent.css('display', 'none');
                collapseExpand.removeClass('icon-collapse').addClass('icon-expand');
                collapseExpand.attr('title', 'Expand');
                mainContent.removeClass().addClass('col-xs-12 col-sm-12 col-md-12 mainContent');
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
        hideShowIconTreeview: function () {
            var selType = $("select#type"),
                treeview = $('#treeview'),
                iconTreeView = $('#treeview span');

            if (selType.val() == '1') {
                treeview.addClass('disabled');
                iconTreeView.removeClass('icon-treeview').addClass('icon-treeview-disabled');
            } else {
                treeview.removeClass('disabled');
                iconTreeView.removeClass('icon-treeview-disabled').addClass('icon-treeview');
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
    // show or hide icon Treeview
    Layout.hideShowIconTreeview();
    var selType = $('select#type');
    selType.bind({
        'change': function (ev, obj) {
            Layout.hideShowIconTreeview();
        }
    });

    // collpase or expand left content
    Layout.collapseExpand();

    // Set active menu css class
    $('ul.mainMenu > li').on('click', function (event) {
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