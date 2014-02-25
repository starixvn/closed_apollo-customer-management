/* File Created: February 21, 2014 */
/* global Apollo, Namespace, pageLayout */

$(document).ready(function () {
    'use strict';
    var setting = new Apollo.CMS.CustomerSetting.BetAndCommSetting();

    setting.InitPage();
});

Namespace.Register('Apollo.CMS.CustomerSetting.BetAndCommSetting');
Apollo.CMS.CustomerSetting.BetAndCommSetting = function () {
    'use strict';
    var $this = this;
//    var urls = {
//        UpdateBetAndComm: CMS.resolveRootUrl('CustomerSetting/BetAndCommSetting/UpdateBetAndComm')
//    };
//    var parameters = {
//    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divCommission = $('#divCommission');
        $this.DOMInstance.spnCommissionInfo = $('#spnCommissionInfo');
        $this.DOMInstance.divBetLimit = $('#divBetLimit');
        $this.DOMInstance.spnBetLimitInfo = $('#spnBetLimitInfo');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        $this.DOMInstance.divCommission.find('input.text-field').on('focus', function () {
            $(this).select();
        }).on('keypress', { decimals: 2 }, pageLayout.MarkAsNumberField).on('keyup', pageLayout.FormatNumberField);

        $this.DOMInstance.divBetLimit.find('input.text-field').on('focus', function () {
            $(this).select();
        }).on('keypress', { decimals: 0 }, pageLayout.MarkAsNumberField).on('keyup', pageLayout.FormatNumberField);
    };

    $this.DOMInstance = {
        divCommission: null,
        spnCommissionInfo: null,
        divBetLimit: null,
        spnBetLimitInfo: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};