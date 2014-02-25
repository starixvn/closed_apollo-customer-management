/* File Created: January 6, 2014 */

/* global _, numeral, Apollo, CMS, Namespace */

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

    // Mark an input as number field. This function needs to be used for keypress event.
    $this.MarkAsNumberField = function (event) {
        var value = $.trim($(this).val());
        var decimalPos = value.indexOf('.');
        var minusPos = value.indexOf('-');

        // Allow decimal character if there is no decimal character has been input yet
        // and some digits have already been input before the decimal character.
        if ((event.data.decimals > 0) && (event.keyCode === 46) && (decimalPos < 0) && (value.length > 0)) {
            return;
        }

        // Allow only one minus character at the begining of the value.
        if (event.data.allowNegative && (event.keyCode === 45) && (minusPos < 0) && (value.length === 0)) {
            return;
        }

        // Allow digits.
        if ((48 <= event.keyCode) && (event.keyCode <= 57)) {
            // Check whether the number of digits after the decimal meets the requrement or not.
            if ((decimalPos < 0) ||
                ((decimalPos >= 0) && (value.length - decimalPos <= event.data.decimals))) {
                return;
            }
        }

        return false;
    };

    // Format number fields. This function is used with keyUp event.
    $this.FormatNumberField = function (event) {
        var value = $.trim($(this).val());
        var rawValue = value.replace(/,/g, '');
        var decimalPos = rawValue.indexOf('.');

        rawValue = rawValue === '' ? '0' : rawValue;
        console.log(event.ctrlKey); console.log(event.keyCode);

        if (decimalPos < 0) {
            value = numeral(rawValue).format(CMS.commonConstants.NumberFormat);
        }

        if ((48 <= event.keyCode && event.keyCode <= 57) ||
            (96 <= event.keyCode && event.keyCode <= 105) ||
            (event.keyCode === 45 || event.keyCode === 46) ||
            (event.keyCode === 109 || event.keyCode === 110)) {
            $(this).val(value);
        }
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