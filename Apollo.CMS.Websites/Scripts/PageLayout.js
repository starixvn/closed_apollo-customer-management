/* File Created: January 6, 2014 */
var layoutHelper = window.parent.layoutHelper;
var CMS = window.parent.CMS;
var Validator = window.parent.Validator;
var searchBar = window.parent.searchBar;

$(document).ready(function () {
    var pageLayout = new Apollo.CMS.PageLayoutHelper();

    pageLayout.InitPage();
});

Namespace.Register("Apollo.CMS.PageLayoutHelper");
Apollo.CMS.PageLayoutHelper = function () {
    var $this = this;

    // Query and save DOM elements.
    var cacheDOM = function () {
        $this.DOMInstance.divMainContent = $('#mainContent');
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

    // Bind event handlers.
    var bindEventHandlers = function () {
        var lazyLayout = _.debounce(resizeRighContent, 50);
        $('select').selectBoxIt();
        $(window).resize(lazyLayout);
    };

    // Cached DOM elements.
    $this.DOMInstance = {
        divMainContent: null
    };

    // Initialize page.
    $this.InitPage = function () {
        cacheDOM();
        bindEventHandlers();
    };
};