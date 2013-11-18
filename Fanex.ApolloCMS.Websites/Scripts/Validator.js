/* File Created: November 14, 2013 */
var Validator = function ($) {
    return {
        showTooltip: function ($obj, message) {
            if ($obj.is(':hidden')) {
                $obj = $obj.parent();
            }

            $obj.attr('title', message);
            $obj.tipsy(
            {
                trigger: 'manual'
            });
            $obj.tipsy('show');
            $obj.focus();
        },
        clearErrorMessages: function (form) {
            form.find('[original-title]').each(function () {
                $(this).tipsy('hide');
            });
        },
        validateRequiredFields: function (form, errorCounter) {
            $('data-val-required', form).each(function () {
                var eleObj = $(this);
                var errMsg = eleObj.attr('data-val-required');
                var value = $.trim(eleObj.val());

                if (errMsg && !value) {
                    Validator.showTooltip(eleObj, errMsg);
                    errorCounter++;
                }
            });
        },
        validateNumberFields: function (form, errorCounter) {
            $('data-val-number', form).each(function () {
                var eleObj = $(this);
                if (!eleObj.attr('original-title')) {
                    var errMsg = eleObj.attr('data-val-number');
                    var value = $.trim(eleObj.val().replace(/,/g, ''));

                    if (errMsg && (!$.isNumeric(value))) {
                        Validator.showTooltip(eleObj, errMsg);
                        errorCounter++;
                    }
                }
            });
        },
        validateDateFields: function (form, errorCounter) {
            $('data-val-date', form).each(function () {
                var eleObj = $(this);
                if (!eleObj.attr('original-title')) {
                    var errMsg = eleObj.attr('data-val-date');
                    var value = $.trim(eleObj.val());

                    if (errMsg && (value != '') && !moment(value, CMS.commonConstants.MomentDateFormat).isValid()) {
                        Validator.showTooltip(eleObj, errMsg);
                        errorCounter++;
                    }
                }
            });
        },
        validateDateRange: function (form, errorCounter) {
            var dateFrom = $('data-val-date-from', form);
            var dateTo = $('data-val-date-to', form);

            if (dateFrom && !dateFrom.attr('original-title') &&
                dateTo && !dateTo.attr('original-title')) {

                var dFromVal = $.trim(dateFrom.val());
                var dToVal = $.trim(dateTo.val());
                var errorMsg = dateFrom.attr('data-val-date-from');
                var displayObj = dateFrom;

                if (errorMsg == '') {
                    errorMsg = dateTo.attr('data-val-date-from');
                    displayObj = dateTo;
                }

                if ((dFromVal != '') && !moment(dFromVal, CMS.commonConstants.MomentDateFormat).isValid() &&
                    (dToVal != '') && !moment(dToVal, CMS.commonConstants.MomentDateFormat).isValid()) {

                    var fDate = new Date(moment(dFromVal));
                    var tDate = new Date(moment(dToVal));

                    if (fDate.getTime() > tDate.getTime()) {
                        showTooltip(displayObj, errorMsg);
                        errorCounter++;
                    }
                }
            }
        },
        validateForm: function (form) {
            var errorCounter = 0;

            Validator.validateRequiredFields(form, errorCounter);
            Validator.validateNumberFields(form, errorCounter);
            Validator.validateDateFields(form, errorCounter);
            Validator.validateDateRange(form, errorCounter);

            return (errorCounter > 0);
        }
    };
} (jQuery);