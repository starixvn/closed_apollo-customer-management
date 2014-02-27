/* File Created: November 14, 2013 */

/* global CMS, moment */

var Validator = (function ($) {
    'use strict';
    return {
        showTooltip: function ($obj, message, gravity) {
            if ($obj.is(':hidden')) {
                $obj = $obj.parent();
            }

            $obj.attr('title', message);
            $obj.tipsy(
            {
                trigger: 'manual',
                gravity: gravity ? gravity : 's',
                opacity: 0.97
            });
            $obj.tipsy('show');
            $obj.focus();
        },
        clearErrorMessages: function (form) {
            form.find('[original-title]').each(function () {
                $(this).tipsy('hide');
                $(this).removeAttr('original-title');
            });
        },
        validateRequiredFields: function (form, gravity, getFirstError) {
            var errorCounter = 0;

            $('[data-val-required]', form).each(function () {
                if (!getFirstError || (getFirstError && errorCounter === 0)) {
                    var eleObj = $(this);
                    var errMsg = eleObj.attr('data-val-required');
                    var value = $.trim(eleObj.val());

                    if (errMsg && !value) {
                        Validator.showTooltip(eleObj, errMsg, gravity);
                        errorCounter++;
                    }
                }
            });

            return errorCounter;
        },
        validateNumberFields: function (form, gravity, getFirstError) {
            var errorCounter = 0;

            $('[data-val-number]', form).each(function () {
                if (!getFirstError || (getFirstError && errorCounter === 0)) {
                    var eleObj = $(this);

                    if (!eleObj.attr('original-title')) {
                        var errMsg = eleObj.attr('data-val-number');
                        var value = $.trim(eleObj.val().replace(/,/g, ''));

                        if (errMsg && (!$.isNumeric(value))) {
                            Validator.showTooltip(eleObj, errMsg, gravity);
                            errorCounter++;
                        }
                    }
                }
            });

            return errorCounter;
        },
        validateNumber: function ($obj, gravity) {
            var errMsg = $obj.attr('data-val-number');
            var isError = false;

            if (errMsg) {
                var value = $.trim($obj.val().replace(/,/g, ''));

                if (!$.isNumeric(value)) {
                    Validator.showTooltip($obj, errMsg, gravity);
                    isError = true;
                }
            }

            return isError;
        },
        validateNumberRangeFields: function (form, gravity, getFirstError) {
            var errorCounter = 0;

            if (Validator.validateNumberFields(form, gravity) === 0) {
                $('[data-val-range]', form).each(function () {
                    if (!getFirstError || (getFirstError && errorCounter === 0)) {
                        var eleObj = $(this);

                        if (!eleObj.attr('original-title')) {
                            var errMsg = eleObj.attr('data-val-range');
                            var minValue = eleObj.attr('data-range-min');
                            var maxValue = eleObj.attr('data-range-max');
                            var value = eleObj.val();

                            if (errMsg && minValue && maxValue && value) {
                                var rawMinValue = $.trim(minValue.replace(/,/g, ''));
                                var rawMaxValue = $.trim(maxValue.replace(/,/g, ''));
                                var rawValue = $.trim(value.replace(/,/g, ''));

                                rawMinValue = parseFloat(rawMinValue);
                                rawMaxValue = parseFloat(rawMaxValue);
                                rawValue = parseFloat(rawValue);

                                if (rawValue < rawMinValue || rawValue > rawMaxValue) {
                                    Validator.showTooltip(eleObj, errMsg.format(minValue, maxValue), gravity);
                                    errorCounter++;
                                }
                            }
                        }
                    }
                });
            }

            return errorCounter;
        },
        validateNumberRange: function ($obj, gravity) {
            var errMsg = $obj.attr('data-val-range');
            var isError = Validator.validateNumber($obj, gravity);

            if (errMsg && !isError) {
                var minValue = $obj.attr('data-range-min');
                var maxValue = $obj.attr('data-range-max');
                var value = $obj.val();

                if (minValue && maxValue && value) {
                    var rawMinValue = $.trim(minValue.replace(/,/g, ''));
                    var rawMaxValue = $.trim(maxValue.replace(/,/g, ''));
                    var rawValue = $.trim(value.replace(/,/g, ''));

                    rawMinValue = parseFloat(rawMinValue);
                    rawMaxValue = parseFloat(rawMaxValue);
                    rawValue = parseFloat(rawValue);

                    if (rawValue < rawMinValue || rawValue > rawMaxValue) {
                        Validator.showTooltip($obj, errMsg.format(minValue, maxValue), gravity);
                        isError = true;
                    }
                }
            }

            return isError;
        },
        validateNumberGreaterThanFields: function (form, gravity, getFirstError) {
            var errorCounter = 0;

            if (Validator.validateNumberFields(form, gravity) === 0) {
                $('[data-val-greater]', form).each(function () {
                    if (!getFirstError || (getFirstError && errorCounter === 0)) {
                        var eleObj = $(this);

                        if (!eleObj.attr('original-title')) {
                            var errMsg = eleObj.attr('data-val-greater');
                            var minValue = eleObj.attr('data-greater-value');
                            var value = eleObj.val();

                            if (errMsg && minValue && value) {
                                minValue = $.trim(minValue.replace(/,/g, ''));
                                minValue = parseFloat(minValue);
                                value = $.trim(value.replace(/,/g, ''));
                                value = parseFloat(value);

                                if (value < minValue) {
                                    Validator.showTooltip(eleObj, errMsg.format(minValue), gravity);
                                    errorCounter++;
                                }
                            }
                        }
                    }
                });
            }

            return errorCounter;
        },
        validateNumberGreaterThan: function ($obj, gravity) {
            var errMsg = $obj.attr('data-val-greater');
            var isError = Validator.validateNumber($obj, gravity);

            if (errMsg && !isError) {
                var minValue = $obj.attr('data-greater-value');
                var value = $obj.val();

                if (minValue && value) {
                    var rawMinValue = $.trim(minValue.replace(/,/g, ''));
                    var rawValue = $.trim(value.replace(/,/g, ''));

                    rawMinValue = parseFloat(rawMinValue);
                    rawValue = parseFloat(rawValue);

                    if (rawValue < rawMinValue) {
                        Validator.showTooltip($obj, errMsg.format(minValue), gravity);
                        isError = true;
                    }
                }
            }

            return isError;
        },
        validateDateFields: function (form, gravity, getFirstError) {
            var errorCounter = 0;

            $('[data-val-date]', form).each(function () {
                if (!getFirstError || (getFirstError && errorCounter === 0)) {
                    var eleObj = $(this);

                    if (!eleObj.attr('original-title')) {
                        var errMsg = eleObj.attr('data-val-date');
                        var value = $.trim(eleObj.val());

                        if (errMsg && (value !== '') && !moment(value, CMS.commonConstants.MomentDateFormat).isValid()) {
                            Validator.showTooltip(eleObj, errMsg, gravity);
                            errorCounter++;
                        }
                    }
                }
            });

            return errorCounter;
        },
        validateDateRange: function (form, gravity) {
            var errorCounter = 0;
            var dateFrom = $('[data-val-date-from]', form);
            var dateTo = $('[data-val-date-to]', form);

            if (dateFrom && !dateFrom.attr('original-title') &&
                dateTo && !dateTo.attr('original-title')) {

                var dFromVal = $.trim(dateFrom.val());
                var dToVal = $.trim(dateTo.val());
                var errorMsg = dateFrom.attr('data-val-date-from');
                var displayObj = dateFrom;

                if (errorMsg === '') {
                    errorMsg = dateTo.attr('data-val-date-to');
                    displayObj = dateTo;
                }

                if ((dFromVal !== '') && moment(dFromVal, CMS.commonConstants.MomentDateFormat).isValid() &&
                    (dToVal !== '') && moment(dToVal, CMS.commonConstants.MomentDateFormat).isValid()) {

                    var fDate = new Date(moment(dFromVal));
                    var tDate = new Date(moment(dToVal));

                    if (fDate.getTime() > tDate.getTime()) {
                        Validator.showTooltip(displayObj, errorMsg, gravity);
                        errorCounter++;
                    }
                }
            }

            return errorCounter;
        },
        validateForm: function (form, gravity) {
            var errorCounter = 0;

            Validator.clearErrorMessages(form, gravity);
            errorCounter += Validator.validateRequiredFields(form, gravity);
            errorCounter += Validator.validateNumberFields(form, gravity);
            errorCounter += Validator.validateDateFields(form, gravity);
            errorCounter += Validator.validateDateRange(form, gravity);

            return (errorCounter === 0);
        }
    };
} (jQuery));