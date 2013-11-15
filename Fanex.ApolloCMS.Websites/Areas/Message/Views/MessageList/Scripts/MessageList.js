Namespace.Register("Fanex.ApolloCMS.Message.MessageList");

Fanex.ApolloCMS.Message.MessageList = function () {
    var self = this;

    var _tables = {
        gridMessageContainer: null,
        gridMessage: null
    };

    var _params = {
        getListMessageParam: {
            TopNewMsg: null,
            MsgType: null,
            Special: null,
            FromDate: null,
            ToDate: null
        },
        getMessageByIdParam: {
            messageId: null
        }
    };

    var cacheDOMElements = function () {
        _tables.gridMessageContainer = $("#gridMessageContainer");
        _tables.gridMessage = $("#gridMessage");
    };

    self.InitPage = function () {
        cacheDOMElements();
        self.renderDatePicker();
        self.bindingEvents();
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
                _params.getListMessageParam.FromDate = selectedDate;
            }
        }).datepicker('setDate', 'today');

        $("#dateTo").datepicker({
            showOn: "button",
            buttonImage: "../../../../Content/themes/img/smoothness/calendar.gif",
            buttonImageOnly: true,
            changeMonth: true,
            maxDate: dateFrom + "3M",
            onClose: function (selectedDate) {
                _params.getListMessageParam.ToDate = selectedDate;
                $("#dateFrom").datepicker("option", "maxDate", selectedDate);
            }
        }).datepicker('setDate', 'today');
    };

    self.getListMessage = function () {
        _params.getListMessageParam.TopNewMsg = $("#ddlTopNewMessageSelectBoxItText").attr("data-val");
        _params.getListMessageParam.MsgType = $("#ddlMessageTypeSelectBoxItText").attr("data-val");
        _params.getListMessageParam.Special = $("#ddlSpecialSelectBoxItText").attr("data-val");

        if (_params.getListMessageParam.Special == 1) {
            _params.getListMessageParam.Special = true;
        }
        else if (_params.getListMessageParam.Special == 0) {
            _params.getListMessageParam.Special = false;
        }

        $.ajax({
            type: "POST",
            url: "Message/MessageList/GetMessageList",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                TopNewMsg: _params.getListMessageParam.TopNewMsg,
                MsgType: _params.getListMessageParam.MsgType,
                Special: _params.getListMessageParam.Special,
                FromDate: _params.getListMessageParam.FromDate,
                ToDate: _params.getListMessageParam.ToDate
            }),
            success: function (rs) {
                self.renderGrid(rs);
            }
        });
    }

    self.renderGrid = function (rs) {
        _tables.gridMessageContainer.removeClass("display-none");
        var bodyData = [];
        var columns = [
                    { HeaderText: "Posted Date", Width: 200, Name: "PostedDate", HeaderAlign: "center", CellAlign: "center", FormatDateTime: "MMMM Do YYYY" },
                    { HeaderText: "Message English", Width: 300, Name: "MessageContent", HeaderAlign: "center", CellAlign: "left", CustomCellRender: function (currentCell) {
                        return '<div class="link editBtn" onclick="editMessage(' + currentCell.Value.MessageId + ')">Edit</div><div class="messageContent">' + currentCell.Value.Content_EN + '</div><div class="clear"></div>';
                    }
                    },
                    { HeaderText: "Type", Width: 80, Name: "MessageType", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Is Specical", Width: 80, Name: "Special", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Creator", Width: 150, Name: "Creator", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Recipient", Width: 80, Name: "Recipient", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Remove", Width: 150, Name: "Remove", HeaderAlign: "center", CellAlign: "center", CustomCellRender: function (currentCell) {
                        return '<div class="link" onclick="removeMessage(' + currentCell.Value + ')">Remove</div>';
                    }
                    }
                ];

        if (rs) {
            var messageLength = rs.length;

            for (var messageIndex = 0; messageIndex < messageLength; messageIndex++) {
                var currentMessage = rs[messageIndex];

                bodyData.push({
                    Columns: [
                                { Name: "PostedDate", Value: currentMessage.PostDate },
                                { Name: "MessageContent", Value: currentMessage },
                                { Name: "MessageType", Value: currentMessage.MessageType },
                                { Name: "Special", Value: currentMessage.MessageIsSpecial },
                                { Name: "Creator", Value: currentMessage.Creator },
                                { Name: "Recipient", Value: currentMessage.Recipient },
                                { Name: "Remove", Value: currentMessage.TargetId }
                            ]
                });
            }
        }

        var gridOptions = {
            columns: columns,
            bodyRows: bodyData
        };

        _tables.gridMessage.fanexGrid(gridOptions);
    };

    self.getMessageById = function (id) {
        _params.getMessageByIdParam.messageId = id;

        $.ajax({
            type: "POST",
            url: "Message/MessageList/GetMessageById",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                MessageId:_params.getMessageByIdParam.messageId
            }),
            success: function (rs) {
                console.log(rs);
            }
        });
    }

    editMessage = function (id) {
        self.getMessageById(id);
    }

    self.bindingEvents = function () {
        $("#btnGetListMessage").click(function () {
            self.getListMessage();
        });
    };
};

var messageList = new Fanex.ApolloCMS.Message.MessageList();

$(document).ready(function () {
    messageList.InitPage();
});