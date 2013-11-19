var Layout = function ($) {

    return {
        filterBar: null,
        pageBody: null,
        page: null,
        frmPage: null,
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
                        collapseExpand.attr('title', 'Expand')
                        mainContent.removeClass().addClass('col-xs-12 col-sm-12 col-md-12 mainContent');
                        if (mainContent.find('#gridMessage.freegrid')) {
                            $("#gridMessage").fanexGrid("resizeGrid");
                        }
                    });
                } else {
                    leftContent.animate({
                        width: "toggle"
                    }, 100, function () {
                        // Animation complete.
                        collapseExpand.removeClass('icon-expand').addClass('icon-collapse');
                        collapseExpand.attr('title', 'Collapse')
                        mainContent.removeClass().addClass('col-xs-9 col-sm-9 col-md-9 mainContent');
                        if (mainContent.find('#gridMessage.freegrid')) {
                            $("#gridMessage").fanexGrid("resizeGrid");
                        }
                    });
                }
            })
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
        }
    };
} (jQuery);

$(document).ready(function () {
    $("select").selectBoxIt();
    // show or hide icon Treeview
    Layout.hideShowIconTreeview();
    var selType = $("select#type");
    selType.bind({
        "change": function (ev, obj) {
            Layout.hideShowIconTreeview();
        }
    });

    // collpase or expand left content
    Layout.collapseExpand();

    /*
    Layout.page = $('#page');
    Layout.frmPage = $('#frm-page');

    Layout.pageBody = $('#body');
    Layout.filterBar = $('#filter-bar');

    // #region Menu
    var menuEvent = 'click';

    $('nav').on(menuEvent, '.menu li', function (e) {
    var $this = $(this),
    _SELECTED = 'selected';

    if ($this.hasClass(_SELECTED)) {
    $this.removeClass(_SELECTED).find('li').removeClass(_SELECTED);
    } else {
    $this.addClass(_SELECTED).siblings('li').removeClass(_SELECTED);
    }

    if (!$this.hasClass('has-kids')) {
    $.cookie('menu-id', $this.attr('mid'), { path: '/' });
    }

    e.stopPropagation();
    }).find('li').each(function () {
    var $this = $(this),
    menuId = $.cookie('menu-id');

    if ($this.attr('mid') == menuId) {
    $this.addClass('current').parents('li').addClass('current');
    }
    });

    $('header').on(menuEvent, '.greeting', function (e) {
    $(this).toggleClass('selected');
    e.stopPropagation();
    });

    $('body').on('click', function () {
    $('nav li, header .greeting').removeClass('selected');
    });
    // #endregion Menu

    $('.pnl-header-toggle').live('click', function () {
    var $this = $(this),
    pnlWrapper = $this.parents('.pnl-wrp'),
    pnlHeader = pnlWrapper.find('.pnl-header'),
    pnlBody = pnlWrapper.find('.pnl-body');

    $this.toggleClass('off');
    pnlHeader.toggleClass('off');
    pnlBody.slideToggle('fast');
    });*/
});