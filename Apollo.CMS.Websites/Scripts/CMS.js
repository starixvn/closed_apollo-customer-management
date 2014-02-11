
var CMS = (function ($) {

    return {
        rootUrl: null,
        unauthorizedRequestError: null,
        resourceNotFound: null,
        internalError: null,
        dialogMsg: null,
        dialogPopup: null,
        headlineMsgBox: null,
        loadingBox: null,
        datePickerButtonUrl: null,
        serverTimezone: null,
        pageRanges: [10, 50, 100, 200, 500],

        commonConstants: {
            DateFormat: 'mm/dd/yy',
            MomentDateFormat: 'MM/DD/YYYY',
            MomentDateTimeFormat: 'MM/DD/YYYY hh:mm:ss A',
            DecimalFormat: '0,0.00',
            NumberFormat: '0,0'
        },

        initCoreValues: function () {
            CMS.rootUrl = $('#RootURL').val();
            CMS.unauthorizedRequestError = $('#UnAuthorizedRequestError').val();
            CMS.resourceNotFound = $('#ResourceNotFoundError').val();
            CMS.internalError = $('#InternalError').val();
            CMS.dialogPopup = $('<div id="dialog-popup"></div>').appendTo('body');
            CMS.datePickerButtonUrl = $('#DatePickerButtonUrl').val();
            CMS.serverTimezone = $('#ServerTimezone').val();
        },
        resolveRootUrl: function (relativeUrl) {
            return CMS.rootUrl + relativeUrl;
        },
        // Handles common error when making ajax request
        handleAjaxError: function (xhr, status, err) {
            if (xhr.status == 403) {
                CMS.headlineMsgBox.showErr(CMS.unauthorizedRequestError, { sticky: true });
            } else if (xhr.status == 404) {
                CMS.headlineMsgBox.showErr(CMS.resourceNotFound, { sticky: true });
            } else {
                CMS.headlineMsgBox.showErr(CMS.internalError, { sticky: true });
            }
        },
        showInfoMessage: function (message) {
            CMS.infoMsgBox.showInfo(message, { duration: 3000 });
        },
        showErrorMessage: function (message) {
            CMS.headlineMsgBox.showErr(message, { duration: 3000 });
        },
        showWarningMessage: function (message) {
            CMS.headlineMsgBox.showWarn(message, { duration: 3000 });
        },
        modal: function (title, html, option) {
            CMS.dialogPopup.html(html).dialog({
                width: 'auto',
                modal: true,
                position: ['center', 160],
                title: title
            });

            if (option) {
                CMS.dialogPopup.dialog('option', option);
            }
        },
        popup: function (title, html, option) {
            CMS.dialogPopup.html(html).dialog({
                width: 'auto',
                modal: false,
                position: ['center', 160],
                title: title
            });

            if (option) {
                CMS.dialogPopup.dialog('option', option);
            }
        },
        closePopup: function () {
            CMS.dialogPopup.dialog('close');
        },
        downloadFile: function (url, data, method) {
            //url and data options required
            if (url && data) {
                //data can be string of parameters or array/object
                data = typeof data == 'string' ? data : $.param(data);
                var downloadUrl = url + '?' + data;
                var iframe = $('body iframe');
                if (iframe.length === 0) {
                    iframe = $('<iframe width="0" height="0" class="display-none"></iframe>');
                    iframe.appendTo('body');
                }

                iframe.attr('src', downloadUrl);
            }
        },
        redirect: function (url, msec) {
            setTimeout('CMS.navigateTo("' + url + '")', msec);
        },
        navigateTo: function (url) {
            window.location.replace(url);
        },
        navigateToRoot: function (url) {
            window.location.replace(CMS.resolveRootUrl(url));
        },
        openWindow: function (url) {
            var popH = $(window).height(), popW = $(window).width();
            var formatter = new Array(
                'resizable=no',
                'scrollbars=yes',
                'toolbar=no',
                'menubar=no',
                'location=no',
                'addressbar=no',
                'directories=no',
                'status=yes'
            ).join(',');

            window.open(url, '_blank', '' + ',' + formatter);
        },
        openNewTab: function (url) {
            window.open(url, '_blank');
        },
        htmlEncode: function (value) {
            return $('<div/>').text(value).html().replace(/"/g, "&quot;");
        },
        htmlDecode: function (value) {
            return $('<div/>').html(value).text();
        },

        encodeURI: function (str) {
            // ref: https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/encodeURI
            return encodeURI(str).replace(/%5B/g, '[').replace(/%5D/g, ']');
        }
    };

} (jQuery));

$(document).ready(function () {
    CMS.initCoreValues();

    CMS.headlineMsgBox = $.msgbox({
        message: '',
        //containerId: 'msg-bar',
        type: 'err',
        modal: false,
        position: ['top:130', 'center'],
        theme: '',
        zIndex: 9999,
        useAnimation: false
    });

    CMS.infoMsgBox = $.msgbox({
        message: '',
        type: 'info',
        modal: false,
        position: ['top:130', 'center'],
        theme: '',
        zIndex: 9999,
        useAnimation: false
    });

    CMS.loadingBox = $.msgbox({
        message: 'Loading...',
        type: 'warn',
        modal: true,
        position: ['top:5', 'center'],
        closeBtn: false,
        theme: '',
        zIndex: 9999,
        sticky: true,
        useAnimation: false
    });
});