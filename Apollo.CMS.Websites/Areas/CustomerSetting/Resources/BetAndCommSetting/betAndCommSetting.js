/* File Created: February 21, 2014 */
/* global Apollo, CMS, Namespace, pageLayout */

$(document).ready(function () {
    'use strict';
    var setting = new Apollo.CMS.CustomerSetting.BetAndCommSetting();

    setting.InitPage();
});

Namespace.Register('Apollo.CMS.CustomerSetting.BetAndCommSetting');
Apollo.CMS.CustomerSetting.BetAndCommSetting = function () {
    'use strict';
    var $this = this;
    var urls = {
        UpdateBetAndComm: CMS.resolveRootUrl('CustomerSetting/BetAndCommSetting/UpdateBetAndComm')
    };
    var parameters = {
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divCommission = $('#divCommission');
        $this.DOMInstance.spnCommissionInfo = $('#spnCommissionInfo');
        $this.DOMInstance.divBetLimit = $('#divBetLimit');
        $this.DOMInstance.spnBetLimitInfo = $('#spnBetLimitInfo');
    };

    // Copy a setting set to other sports.
    var copyBetLimitSetting = function (rows, settings) {
        $.each(rows, function (index, row) {
            var $row = $(row);

            if (settings) {
                $row.find('input.single-game').val(settings.singleGame);
                $row.find('input.single-bet').val(settings.singleBet);
                $row.find('input.min-single-bet').val(settings.minSingleBet);
            }
        });
    };

    // Validate commission form.
    var validateCommForm = function () {
        Validator.validateNumberRange($this.DOMInstance.divCommission, 's');
    };

    // Validate bet limit form.
    var validateBetLimitForm = function () {
        Validator.validateNumberRange($this.DOMInstance.divBetLimit, 's');
        Validator.validateNumberGreaterThan($this.DOMInstance.divBetLimit, 's');
    };

    // Initialize fields and handle events.
    var initFields = function () {
        $this.DOMInstance.divCommission.find('input.text-field').on('focus', function () {
            $(this).select();
        }).on('blur', function () {
            validateCommForm();
        }).on('keypress', { decimals: 2 }, pageLayout.MarkAsNumberField).on('keyup', pageLayout.FormatNumberField);

        $this.DOMInstance.divBetLimit.find('input.text-field').on('focus', function () {
            $(this).select();
        }).on('blur', function () {
            validateBetLimitForm();
        }).on('keypress', { decimals: 0 }, pageLayout.MarkAsNumberField).on('keyup', pageLayout.FormatNumberField);

        $this.DOMInstance.divBetLimit.find('span.icon-copy').on('click', function () {
            var currentRow = $(this).closest('div.bet-setting');
            var nextRows = currentRow.nextAll();
            var settings = {
                singleGame: currentRow.find('input.single-game').val(),
                singleBet: currentRow.find('input.single-bet').val(),
                minSingleBet: currentRow.find('input.min-single-bet').val()
            };

            copyBetLimitSetting(nextRows, settings);
            // @@TODO: Validate bet limit.
        });
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