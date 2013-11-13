Namespace.Register("Fanex.ApolloCMS.Message.MessageList");

Fanex.ApolloCMS.Message.MessageList = function () {
    var self = this;

    self.InitPage = function () {
        self.renderDatePicker();
    };

    self.renderDatePicker = function () {
        var dateFrom = null;
        $("#dateFrom").datepicker({
            showOn: "button",
            buttonImage: "../../../../Content/themes/img/smoothness/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            onClose: function (selectedDate) {
                dateFrom = selectedDate;
                $("#dateTo").datepicker("option", "minDate", selectedDate);
            }
        });
        $("#dateTo").datepicker({
            showOn: "button",
            buttonImage: "../../../../Content/themes/img/smoothness/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            maxDate: dateFrom + "3M",
            onClose: function (selectedDate) {
                $("#dateFrom").datepicker("option", "maxDate", selectedDate);
            }
        });
    };
};

var messageList = new Fanex.ApolloCMS.Message.MessageList();

$(document).ready(function () {
    messageList.InitPage();
});