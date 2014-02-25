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
        validateNumberRange: function (form, gravity, getFirstError) {
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
                                minValue = $.trim(minValue.replace(/,/g, ''));
                                minValue = parseFloat(minValue);
                                maxValue = $.trim(maxValue.replace(/,/g, ''));
                                maxValue = parseFloat(maxValue);
                                value = $.trim(value.replace(/,/g, ''));
                                value = parseFloat(value);

                                if (value < minValue || value > maxValue) {
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
        validateNumberGreaterThan: function (form, gravity, getFirstError) {
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