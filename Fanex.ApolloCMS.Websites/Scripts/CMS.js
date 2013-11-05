
var CMS = function ($) {

	return {
		ClientUrl: null,
		RootUrl: null,
		dialogMsg: null,
		dialogPopup: null,
		headlineMsgBox: null,
		loadingBox: null,
		alertCallback: null,
		confirmCallback: null,
		msieVersion: null,
		agentIsOldIe: null,
		agentIsApple: null,
		agentIsMobile: null,

		commonConstants: {
			DateFormat: 'mm-dd-yy',
			MomentDateFormat: 'MM-DD-YYYY',
			MomentDateTimeFormat: 'MM-DD-YYYY hh:mm:ss A',
			DecimalFormat: '0,0.00',
			NumberFormat: '0,0'
		},

		initCoreValues: function () {
			CMS.ClientUrl = $('#ClientURL').val();
			CMS.RootUrl = $('#RootURL').val();
			CMS.dialogPopup = $('<div id="dialog-popup"></div>').appendTo('body');

			CMS.msieVersion = CMS.getIeVersion();
			CMS.agentIsOldIe = CMS.msieVersion != -1 && CMS.msieVersion < 9.0;
			CMS.agentIsApple = CMS.agentIs('apple') != null;
			CMS.agentIsMobile = CMS.agentIs('mobile') != null;
		},
		resolveClientUrl: function (relativeUrl) {
			return CMS.ClientUrl + relativeUrl;
		},
		resolveRootUrl: function (relativeUrl) {
			return CMS.RootUrl + relativeUrl;
		},
		// Handles common error when making ajax request
		handleAjaxError: function (xhr, status, err) {
			if (xhr.status == 403) {
				CMS.headlineMsgBox.showErr('Unauthorized request rejected!', { sticky: true });
			} else if (xhr.status == 404) {
				CMS.headlineMsgBox.showErr('Resources not found!', { sticky: true });
			} else {
				CMS.headlineMsgBox.showErr('An unexpected error has occurred!', { sticky: true });
			}
		},
		showInfoMessage: function (message) {
			CMS.infoMsgBox.showInfo(message, { duration: 3000 });
		},
		getIeVersion: function () {
			var version = -1; // Return value assumes failure.

			if (window.navigator.userAgent.indexOf("MSIE") > 0) {
				var usrAgent = navigator.userAgent;
				var reg = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");

				if (reg.exec(usrAgent) != null) version = parseFloat(RegExp.$1);
			}

			return version;
		},
		agentIs: function (name) {
			switch (name.toLowerCase()) {
				case 'ipad':
					return navigator.userAgent.match(/iPad/i);
				case 'iphone':
					return navigator.userAgent.match(/iPhone/i);
				case 'ipod':
					return navigator.userAgent.match(/iPod/i);
				case 'apple':
					return navigator.userAgent.match(/iPad/i) ||
						navigator.userAgent.match(/iPhone/i) ||
						navigator.userAgent.match(/iPod/i);
				case 'mobile':
					return
					// Apple
					navigator.userAgent.match(/iPad/i) ||
						navigator.userAgent.match(/iPhone/i) ||
						navigator.userAgent.match(/iPod/i) ||
					// RIM
						navigator.userAgent.match(/BlackBerry/i) ||
						navigator.userAgent.match(/PlayBook/i)
					// Windows Phone
					navigator.userAgent.match(/Windows Phone/i);
					// Android
					navigator.userAgent.match(/Android/i);
					// General
					navigator.userAgent.match(/Touch/i);
				default: return null;
			}
		},
		alert: function (title, msg, callback) {
		},
		confirm: function (title, msg, callback) {
		},
		modal: function (title, html) {
			CMS.dialogPopup.html(html).dialog({
				width: 'auto',
				modal: true,
				position: ['center', 160]
			});

			CMS.dialogPopup.dialog('option', { title: title });
		},
		popup: function (title, html) {
			CMS.dialogPopup.html(html).dialog({
				width: 'auto',
				modal: false,
				position: ['center', 160]
			});

			CMS.dialogPopup.dialog('option', { title: title });
		},
		closePopup: function () {
			CMS.dialogPopup.dialog('close');
		},
		downloadFile: function (url, data, method) {
			//url and data options required
			if (url && data) {
				//data can be string of parameters or array/object
				data = typeof data == 'string' ? data : $.param(data);
				var downloadUrl = url + "?" + data;
				var iframe = $('body iframe');
				if (iframe.length == 0) {
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
			window.location.replace(CMS.resolveClientUrl(url));
		},
		navigateToRoot: function (url) {
			window.location.replace(CMS.resolveRootUrl(url));
		},
		openWindow: function (url) {
			var popH = $(window).height(), popW = $(window).width();
			var formatter = new Array(
				"resizable=no",
				"scrollbars=yes",
				"toolbar=no",
				"menubar=no",
				"location=no",
				"addressbar=no",
				"directories=no",
				"status=yes"
			).join(',');

			window.open(url, '_blank', '' + ',' + formatter);
		},
		openNewTab: function (url) {
			window.open(url, '_blank');
		},
		htmlEncode: function (value) {
			return $('<div/>').text(value).html();
		},
		htmlDecode: function (value) {
			return $('<div/>').html(value).text();
		},
		showSuccessMsg: function (msg, url, msec) {
			var msgWrp = $('.validation-summary-valid');

			msgWrp.addClass('success')
				.find('li:first').html(CMS.htmlDecode(msg)).show();

			CMS.showSuccessMsgCountDown(url, msec / 1000);
		},
		showSuccessMsgCountDown: function (url, msec) {
			if (msec-- > 0) {
				setTimeout('CMS.showSuccessMsgCountDown("' + url + '", ' + msec + ')', 1000);
				$('.validation-summary-valid .msec').html(msec);
			} else {
				window.location.replace(NAP.resolveClientUrl(url));
			}
		},

		encodeURI: function (str) {
			// ref: https://developer.mozilla.org/en-US/docs/JavaScript/Reference/Global_Objects/encodeURI
			return encodeURI(str).replace(/%5B/g, '[').replace(/%5D/g, ']');
		}
	};

} (jQuery);

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

// -- Utilities -- //

// String Format
String.prototype.format = function () {
	var args = arguments;
	return this.replace(/{(\d+)}/g, function (match, number) {
		return typeof args[number] != 'undefined' ? args[number]: match;
	});
};

// Array Remove - By John Resig (MIT Licensed)
Array.prototype.remove = function (from, to) {
	var rest = this.slice((to || from) + 1 || this.length);
	this.length = from < 0 ? this.length + from : from;
	return this.push.apply(this, rest);
};

// Array Remove All elements matched the criteria
Array.prototype.removeAll = function (criteria) {
	var length = this.length;

	for (var i = 0; i < length; i++) {
		var element = this[i];

		if (criteria(element)) {
			this.splice(i--, 1);
			length--;
		}
	}
};