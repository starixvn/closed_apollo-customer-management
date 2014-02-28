/* File Created: February 21, 2014 */
/* global numeral, Apollo, CMS, Namespace, pageLayout, Validator, hasEditSettingRight, filter, messages */

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
        UpdateBetAndComm: CMS.resolveRootUrl('CustomerSetting/BetAndCommSetting/UpdateCommAndBetLimit')
    };
    var parameters = {
        CustId: null,
        UplineId: null,
        IsMember: false,
        Group: null,
        BetLimits: [],
        Discounts: []
    };
    var backup = {
        BetLimits: [],
        Discounts: [],
        Group: null
    };

    // Used for update member's group.
    var commRanges = {
        a: {
            value: '',
            valueFormat: '[0 - {0}]'
        },
        b: {
            value: '',
            valueFormat: '[0 - {0}]'
        },
        c: {
            value: '',
            valueFormat: '[0 - {0}]'
        },
        d: {
            value: '',
            valueFormat: '[{0}]'
        }
    };

    // Cache DOM elements
    var cacheDOM = function () {
        $this.DOMInstance.divCommission = $('#divCommission');
        $this.DOMInstance.spnCommissionInfo = $('#spnCommissionInfo');
        $this.DOMInstance.divCommInstructions = $('#divCommInstructions');
        $this.DOMInstance.ddlLine = $this.DOMInstance.divCommission.find('#ddlLine');
        $this.DOMInstance.divGroupCommRange = $this.DOMInstance.divCommission.find('#divGroupCommRange');
        $this.DOMInstance.divBetLimit = $('#divBetLimit');
        $this.DOMInstance.spnBetLimitInfo = $('#spnBetLimitInfo');
        $this.DOMInstance.divBetLimitInstructions = $('#divBetLimitInstructions');
        $this.DOMInstance.btnSubmit = $('#btnSubmit');
    };

    // Parse a formatted string of a number to float.
    var parseNumber = function (number) {
        var result = 0;

        if (number) {
            var rawValue = $.trim(number.replace(/,/g, ''));

            result = $.isNumeric(rawValue) ? parseFloat(rawValue) : result;
        }

        return result;
    };

    // Backup bet limits.
    var getBetLimits = function () {
        var $rows = $this.DOMInstance.divBetLimit.find('div.bet-setting');
        var $currentRow = null;
        var betLimits = [];
        var sportType;
        var betType;
        var singleGame;
        var singleBet;
        var minSingleBet;

        $.each($rows, function (rowIndex, row) {
            $currentRow = $(row);
            sportType = $currentRow.find('#hdnSportType').val();
            betType = $currentRow.find('#hdnBetType').val();
            singleGame = $currentRow.find('input.single-game').val();
            singleBet = $currentRow.find('input.single-bet').val();
            minSingleBet = $currentRow.find('input.min-single-bet').val();

            betLimits.push({
                SportType: sportType,
                BetType: betType,
                MaxPerMatch: parseNumber(singleGame),
                Max: parseNumber(singleBet),
                Min: parseNumber(minSingleBet)
            });
        });

        return betLimits;
    };

    var getCommissions = function () {
        var result = [];
        var lineA;
        var lineB;
        var lineC;
        var lineD;
        var sportType;
        var betType;

        // Get group commission.
        if (!filter.isMember) {
            lineA = $this.DOMInstance.divCommission.find('#txtLineA').val();
            lineB = $this.DOMInstance.divCommission.find('#txtLineB').val();
            lineC = $this.DOMInstance.divCommission.find('#txtLineC').val();
            lineD = $this.DOMInstance.divCommission.find('#txtLineD').val();
            sportType = $this.DOMInstance.divCommission.find('#hdnSportType').val();
            betType = $this.DOMInstance.divCommission.find('#hdnBetType').val();

            result.push({
                SportType: sportType,
                BetType: betType,
                DiscountA: parseNumber(lineA),
                DiscountB: parseNumber(lineB),
                DiscountC: parseNumber(lineC),
                DiscountD: parseNumber(lineD)
            });
        } else {
            lineA = $this.DOMInstance.divCommission.find('#txtGroupComm').val();
            sportType = $this.DOMInstance.divCommission.find('#hdnSportType').val();
            betType = $this.DOMInstance.divCommission.find('#hdnBetType').val();

            result.push({
                SportType: sportType,
                BetType: betType,
                DiscountA: parseNumber(lineA)
            });
        }

        // Get 1x2 commission.
        lineA = $this.DOMInstance.divCommission.find('#txt1x2Comm').val();
        sportType = $this.DOMInstance.divCommission.find('#hdnSportTyp1x2').val();
        betType = $this.DOMInstance.divCommission.find('#hdnBetType1x2').val();

        result.push({
            SportType: sportType,
            BetType: betType,
            DiscountA: parseNumber(lineA)
        });

        // Get Others commission.
        lineA = $this.DOMInstance.divCommission.find('#txtOthersComm').val();
        sportType = $this.DOMInstance.divCommission.find('#hdnSportTypeOthers').val();
        betType = $this.DOMInstance.divCommission.find('#hdnBetTypeOthers').val();

        result.push({
            SportType: sportType,
            BetType: betType,
            DiscountA: parseNumber(lineA)
        });

        return result;
    };

    var getCommRanges = function () {
        if (filter.isMember) {
            var lineA = $this.DOMInstance.divCommission.find('#hdnMaxDiscountA').val();
            var lineB = $this.DOMInstance.divCommission.find('#hdnMaxDiscountB').val();
            var lineC = $this.DOMInstance.divCommission.find('#hdnMaxDiscountC').val();
            var lineD = $this.DOMInstance.divCommission.find('#hdnMaxDiscountD').val();

            commRanges.a.value = lineA;
            commRanges.b.value = lineB;
            commRanges.c.value = lineC;
            commRanges.d.value = lineD;
        }
    };

    // Backup loaded data.
    var backupData = function () {
        backup.BetLimits = getBetLimits();
        backup.Discounts = getCommissions();
        backup.Group = $this.DOMInstance.ddlLine.val();
    };

    // Get changed limit settings
    var getChangedBetLimits = function () {
        var newBetLimits = getBetLimits();
        var changedBetLimits = [];
        var backupItem = null;

        $.each(newBetLimits, function (index, item) {
            backupItem = backup.BetLimits[index];

            if ((item.Min !== backupItem.Min) ||
                (item.Max !== backupItem.Max) ||
                (item.MaxPerMatch !== backupItem.MaxPerMatch)) {
                changedBetLimits.push(item);
            }
        });

        return changedBetLimits;
    };

    // Get changed commission settings.
    var getChangedCommissions = function () {
        var newCommissions = getCommissions();
        var changedCommissions = [];
        var backupItem = null;

        $.each(newCommissions, function (index, item) {
            backupItem = backup.Discounts[index];

            if (item.DiscountA !== backupItem.DiscountA) {
                changedCommissions.push(item);
            }
        });

        return changedCommissions;
    };

    // Get changed group value.
    var getChangedGroup = function () {
        var newGroup = $this.DOMInstance.ddlLine.val();

        return backup.Group === newGroup ? null : newGroup;
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
    var validateCommField = function ($obj) {
        $obj.tipsy('hide');
        $obj.removeAttr('original-title');

        return Validator.validateNumberRange($obj, 's');
    };

    // Validate bet limit form.
    var validateBetLimitField = function ($obj) {
        var hasError = false;

        $obj.tipsy('hide');
        $obj.removeAttr('original-title');
        hasError = Validator.validateNumberRange($obj, 's');
        hasError = hasError || Validator.validateNumberGreaterThan($obj, 's');

        return hasError;
    };

    // Validate a row of bet limit session.
    var validateBetLimitRow = function ($row) {
        var hasError = false;

        var txtSingleGame = $row.find('input.single-game');
        var txtSingleBet = $row.find('input.single-bet');
        var txtMinSingleBet = $row.find('input.min-single-bet');

        var singleGameError = txtSingleGame.attr('data-greater-error');
        var singleBetError = txtSingleBet.attr('data-greater-error');

        var singleGameValue = txtSingleGame.val();
        var singleBetValue = txtSingleBet.val();
        var minSingleBetValue = txtMinSingleBet.val();

        var rawSingleGameValue = parseNumber(singleGameValue);
        var rawSingleBetValue = parseNumber(singleBetValue);
        var rawMinSingleBetValue = parseNumber(minSingleBetValue);

        if (rawSingleBetValue < rawMinSingleBetValue) {
            Validator.showTooltip(txtSingleBet, singleBetError.format(minSingleBetValue), 's');
            hasError = true;
        }

        if (rawSingleGameValue < rawSingleBetValue) {
            Validator.showTooltip(txtSingleGame, singleGameError.format(singleBetValue), 's');
            hasError = true;
        }

        return hasError;
    };

    // Validate commission form.
    var validateCommForm = function () {
        var hasError = false;

        Validator.clearErrorMessages($this.DOMInstance.divCommission);
        $this.DOMInstance.divCommission.find('input.text-field').each(function () {
            if (!hasError) {
                hasError = validateCommField($(this));
            }
        });

        return hasError;
    };

    // Validate bet limit form.
    var validateBetLimitForm = function () {
        var hasError = false;

        Validator.clearErrorMessages($this.DOMInstance.divBetLimit);
        $this.DOMInstance.divBetLimit.find('input.text-field').each(function () {
            if (!hasError) {
                hasError = validateBetLimitField($(this));
            }
        });

        // Validate data on the same row.
        if (!hasError) {
            $this.DOMInstance.divBetLimit.find('div.bet-setting').each(function () {
                var $row = $(this);

                if (!hasError) {
                    hasError = validateBetLimitRow($row);
                }
            });
        }

        return hasError;
    };

    // Format input value in commission field after loosing focus.
    var formatCommValue = function ($obj) {
        var value = $obj.val();
        var rawValue = value.replace(/,/g, '');

        value = numeral(rawValue).format(CMS.commonConstants.DecimalFormat);
        $obj.val(value);
    };

    // Format input value in commission field after loosing focus.
    var formatBetLimitValue = function ($obj) {
        var value = $obj.val();
        var rawValue = value.replace(/,/g, '');

        value = numeral(rawValue).format(CMS.commonConstants.NumberFormat);
        $obj.val(value);
    };

    // Initialize fields and handle events.
    var initFields = function () {
        $this.DOMInstance.spnCommissionInfo.on('click', function () {
            pageLayout.ClosePopup();
            pageLayout.Popup(messages.commissionSettingInstructions, $this.DOMInstance.divCommInstructions.html(), {}, false);
        });

        $this.DOMInstance.spnBetLimitInfo.on('click', function () {
            console.log($this.DOMInstance.divBetLimitInstructions.html());
            pageLayout.Popup(messages.betLimitInstruction, $this.DOMInstance.divBetLimitInstructions.html(), {}, false);
        });

        if (!hasEditSettingRight) {
            return;
        }

        $this.DOMInstance.ddlLine.on('change', function () {
            var txtGroupComm = $this.DOMInstance.divCommission.find('#txtGroupComm');
            var newGroup = $(this).val();
            var groupRange = commRanges[newGroup];

            $this.DOMInstance.divGroupCommRange.text(groupRange.valueFormat.format(groupRange.value));
            txtGroupComm.attr('data-range-max', groupRange.value);
        });

        $this.DOMInstance.divCommission.find('input.text-field').on('focus', function () {
            $(this).select();
        }).on('blur', function () {
            var $obj = $(this);

            formatCommValue($obj);
            validateCommField($obj);
        }).on('keypress', { decimals: 2 }, pageLayout.MarkAsNumberField).on('keyup', pageLayout.FormatNumberField);

        $this.DOMInstance.divBetLimit.find('input.text-field').on('focus', function () {
            $(this).select();
        }).on('blur', function () {
            var $obj = $(this);

            formatBetLimitValue($obj);
            validateBetLimitField($obj);
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
            validateBetLimitForm();
        });

        $this.DOMInstance.btnSubmit.on('click', function () {
            var isValid = !validateCommForm() && !validateBetLimitForm();

            if (isValid) {
                updateSettings();
            }
        });

        backupData();
        getCommRanges();
    };

    // Update settings.
    var updateSettings = function () {
        parameters.BetLimits = getChangedBetLimits();
        parameters.Discounts = getChangedCommissions();
        parameters.Group = getChangedGroup();
        parameters.CustId = filter.custId;
        parameters.UplineId = filter.uplineId;
        parameters.IsMember = filter.isMember;

        if (parameters.BetLimits.length > 0 ||
            parameters.Discounts.length > 0 ||
            parameters.Group !== null) {
            CMS.headlineMsgBox.confirm(messages.updateSettingConfirmMsg, function () {
                $.ajax({
                    type: 'POST',
                    url: urls.UpdateBetAndComm,
                    contentType: 'application/json; charset=utf-8',
                    dataType: 'json',
                    data: JSON.stringify(parameters),
                    beforeSend: function () {
                        CMS.loadingBox.show();
                    },
                    success: function (result) {
                        if (result.Code === 0) {
                            CMS.showInfoMessage(messages.updateMessage);
                            window.location.href = window.location.href;
                        } else {
                            CMS.headlineMsgBox.showErr(result.Message);
                        }
                    },
                    error: function (xhr, status, err) {
                        CMS.handleAjaxError(xhr, status, err);
                    },
                    complete: function () {
                        CMS.loadingBox.close();
                    }
                });
            });
        } else {
            CMS.showInfoMessage(messages.noChange);
        }
    };

    $this.DOMInstance = {
        divCommission: null,
        spnCommissionInfo: null,
        divCommInstructions: null,
        ddlLine: null,
        divGroupCommRange: null,
        divBetLimit: null,
        spnBetLimitInfo: null,
        divBetLimitInstructions: null,
        btnSubmit: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
};