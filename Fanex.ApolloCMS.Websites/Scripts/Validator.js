/* File Created: November 14, 2013 */
var Validator = function ($) {
    return {
        showTooltip: function ($obj, message) {
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
        validateForm: function (form) {
            var errorCounter = 0;

            $('data-val-required', form).each(function () {
                var eleObj = $(this);
                var errMsg = eleObj.attr('data-val-required');
                var value = $.trim(eleObj.val());

                if (errMsg && !value) {
                    Validator.showTooltip(eleObj, errMsg);
                    errorCounter++;
                }
            });

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

            $('data-val-date', form).each(function () {
                var eleObj = $(this);
                if (!eleObj.attr('original-title')) {
                    var errMsg = eleObj.attr('data-val-date');
                    var value = $.trim(eleObj.val());

                    if (errMsg && !moment(date, CMS.commonConstants.MomentDateFormat).isValid()) {
                        Validator.showTooltip(eleObj, errMsg);
                        errorCounter++;
                    }
                }
            });
        }
    };
} (jQuery);