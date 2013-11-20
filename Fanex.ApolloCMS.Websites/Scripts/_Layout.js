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
});