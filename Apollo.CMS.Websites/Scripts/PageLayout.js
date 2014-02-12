/* File Created: January 6, 2014 */

/* global _, Apollo, Namespace */

/*jshint ignore:start */

var layoutHelper = window.parent.layoutHelper;
var CMS = window.parent.CMS;
var Validator = window.parent.Validator;
var searchBar = window.parent.searchBar;

/*jshint ignore:end */

Namespace.Register('Apollo.CMS.PageLayoutHelper');
Apollo.CMS.PageLayoutHelper = function () {
    'use strict';
    var $this = this;

    // Query and save DOM elements.
    var cacheDOM = function () {
        $this.DOMInstance.divMainContent = $('#mainContent');
        $this.DOMInstance.divDialogPopup = $('<div id="divPopup"></div>').appendTo('body');
    };

    // Resize the filter form and grid of the right content, if there is any, when the window is resized.
    var resizeRighContent = function () {
        var mainContentWidth = parseInt($this.DOMInstance.divMainContent.css('width'), 10) - 40;
        var gridWidth = 1024;

        $('div.resizable-grid').each(function () {
            var grid = $(this);
            if (grid.children().length > 0) {
                grid.fanexGrid('resizeGrid');
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

    // Fix the filter form's width issue when the window is too small.
    var fixFilterForm = function () {
        var titleWidth = parseInt($('h5.title').css('width'), 10);

        $('div.resizable-form').each(function () {
            var $form = $(this);
            var formWidth = parseInt($form.css('width'), 10);

            if (formWidth > titleWidth) {
                $form.css('width', titleWidth);
            }
        });
    };

    // Bind event handlers.
    var bindEventHandlers = function () {
        var lazyLayout = _.debounce(resizeRighContent, 50);

        $('select').selectBoxIt();
        $(window).resize(lazyLayout);

        // Make the click event on the page be the same through the iframe.
        $('html').on('click', function () {
            $('html', $(window.parent.document)).click();
        }).on('hover', function () {
            $('html', $(window.parent.document)).hover();
        });

        fixFilterForm();
    };

    // Show popup inside an iframe.
    $this.Popup = function (title, html, option, isModal) {
        $this.DOMInstance.divDialogPopup.html(html).dialog({
            width: 'auto',
            position: ['center', 160],
            modal: isModal,
            title: title
        });

        if (option) {
            $this.DOMInstance.divDialogPopup.dialog('option', option);
        }
    };
    $this.ClosePopup = function () {
        $this.DOMInstance.divDialogPopup.dialog('close');
    };

    // Cached DOM elements.
    $this.DOMInstance = {
        divMainContent: null,
        divDialogPopup: null
    };

    // Initialize page.
    $this.InitPage = function () {
        cacheDOM();
        bindEventHandlers();
    };
};
var pageLayout = new Apollo.CMS.PageLayoutHelper();

$(document).ready(function () {
    'use strict';
    pageLayout.InitPage();
});