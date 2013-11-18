Namespace.Register("Fanex.ApolloCMS.Message.MessageList");

Fanex.ApolloCMS.Message.MessageList = function () {
    var self = this;

    var _tables = {
        gridMessageContainer: null,
        gridMessage: null,
        txtContentEn: null,
        txtContentCh: null,
        txtContentCs: null,
        txtContentJp: null,
        txtContentKo: null,
        txtContentTh: null,
        txtContentVn: null
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
        },
        updateMessageParam: {
            MsgId: null,
            ContentEn: null,
            ContentCh: null,
            ContentCs: null,
            ContentJp: null,
            ContentKo: null,
            ContentTh: null,
            ContentVn: null
        },
        removeMessageParam: {
            MsgId: null,
            TargetId: null
        }
    };

    var cacheDOMElements = function () {
        _tables.gridMessageContainer = $("#gridMessageContainer");
        _tables.gridMessage = $("#gridMessage");
    };

    var cacheDOMContentElements = function () {
        _tables.txtContentEn = $("#txtContentEn");
        _tables.txtContentCh = $("#txtContentCh");
        _tables.txtContentCs = $("#txtContentCs");
        _tables.txtContentJp = $("#txtContentJp");
        _tables.txtContentKo = $("#txtContentKo");
        _tables.txtContentTh = $("#txtContentTh");
        _tables.txtContentVn = $("#txtContentVn");
    }

    self.InitPage = function () {
        cacheDOMElements();
        self.renderDatePicker();
        self.bindingEvents();
    };

    self.renderDatePicker = function () {
        var dateFrom = null;

        _params.getListMessageParam.FromDate = today;
        _params.getListMessageParam.ToDate = today;

        $("#dateFrom").datepicker({
            showOn: "button",
            buttonImage: "../../../../content/site/img/icon-cal-orange.jpg",
            buttonImageOnly: true,
            changeMonth: true,
            onSelect: function (selectedDate) {

            },
            onClose: function (selectedDate) {
                var maxDate = new Date(selectedDate);
                maxDate.setMonth(maxDate.getMonth() + 3);
                $("#dateTo").datepicker("option", "minDate", selectedDate);
                $("#dateTo").datepicker("option", "maxDate", maxDate);
                _params.getListMessageParam.FromDate = selectedDate;
            }
        }).datepicker('setDate', 'today');

        $("#dateTo").datepicker({
            showOn: "button",
            buttonImage: "../../../../content/site/img/icon-cal-orange.jpg",
            buttonImageOnly: true,
            changeMonth: true,
            onClose: function (selectedDate) {
                _params.getListMessageParam.ToDate = selectedDate;
                $("#dateFrom").datepicker("option", "maxDate", selectedDate);
            }
        }).datepicker('setDate', 'today');
    };

    var getParamValues = function () {
        _params.updateMessageParam.MsgId = _params.getMessageByIdParam.messageId;
        _params.updateMessageParam.ContentEn = $.trim(_tables.txtContentEn.val());
        _params.updateMessageParam.ContentCh = $.trim(_tables.txtContentCh.val());
        _params.updateMessageParam.ContentCs = $.trim(_tables.txtContentCs.val());
        _params.updateMessageParam.ContentJp = $.trim(_tables.txtContentJp.val());
        _params.updateMessageParam.ContentKo = $.trim(_tables.txtContentKo.val());
        _params.updateMessageParam.ContentTh = $.trim(_tables.txtContentTh.val());
        _params.updateMessageParam.ContentVn = $.trim(_tables.txtContentVn.val());
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
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            }
        });
    };

    self.renderGrid = function (rs) {
        _tables.gridMessageContainer.removeClass("display-none");
        var bodyData = [];
        var columns = [
                    { HeaderText: "Posted Date", Width: 200, Name: "PostedDate", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Message English", Width: 300, Name: "MessageContent", HeaderAlign: "center", CellAlign: "left", CustomCellRender: function (currentCell) {
                        return '<div class="link editBtn glyphicon glyphicon-pencil" onclick="editMessage(' + currentCell.Value.MessageId + ')"></div><div class="messageContent">' + currentCell.Value.ContentEn + '</div><div class="clear"></div>';
                    }
                    },
                    { HeaderText: "Type", Width: 80, Name: "MessageType", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Is Specical", Width: 80, Name: "Special", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Creator", Width: 150, Name: "Creator", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Recipient", Width: 80, Name: "Recipient", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: "Remove", Width: 150, Name: "Remove", HeaderAlign: "center", CellAlign: "center", CustomCellRender: function (currentCell) {
                        return '<div class="link" onclick="removeMessage(' + currentCell.Value.MessageId + ',' + currentCell.Value.TargetId + ' )">Remove</div>';
                    }
                    }
                ];

        if (rs) {
            var messageLength = rs.length;
            var currentGroupId = 0;
            var currentItem = null;
            var totalMergeRow = 1;

            $.each(rs, function (index, item) {
                if (currentGroupId != item.MessageId) {
                    currentGroupId = item.MessageId;
                    currentItem = item;
                    currentItem.NumOfElems = 1;
                } else {
                    currentItem.NumOfElems++;
                }
            });

            for (var messageIndex = 0; messageIndex < messageLength; messageIndex++) {
                var currentMessage = rs[messageIndex];
                var row = [];

                if (currentGroupId != currentMessage.MessageId) {
                    currentGroupId = currentMessage.MessageId;
                    row.push(
                        { Name: "PostedDate", Value: currentMessage.PostDate, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "MessageContent", Value: currentMessage, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "MessageType", Value: currentMessage.MessageType, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "Special", Value: currentMessage.MessageIsSpecial, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "Creator", Value: currentMessage.Creator, MergeRowNumber: currentMessage.NumOfElems }
                    );
                }
                row.push(
                    { Name: "Recipient", Value: currentMessage.Recipient },
                    { Name: "Remove", Value: currentMessage }
                );

                bodyData.push({
                    Columns: row
                });
            }
        }

        var gridOptions = {
            columns: columns,
            bodyRows: bodyData,
            showDistinctRow: false
        };

        _tables.gridMessage.fanexGrid(gridOptions);
    };

    self.getMessageById = function (id) {
        _params.getMessageByIdParam.messageId = id;

        $.ajax({
            type: "POST",
            url: "Message/MessageList/GetMessageById",
            contentType: "application/json; charset=utf-8",
            dataType: "html",
            data: JSON.stringify({
                MessageId: _params.getMessageByIdParam.messageId
            }),
            success: function (html) {
                CMS.modal("Edit Message", html);
                cacheDOMContentElements();
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            }
        });
    };

    editMessage = function (id) {
        self.getMessageById(id);
    };

    self.updateMessage = function () {
        $.ajax({
            type: "POST",
            url: "Message/MessageList/UpdateMessage",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(_params.updateMessageParam),
            success: function (rs) {
                CMS.closePopup();
                CMS.showInfoMessage("The message have been updated successfully!");
                self.getListMessage();
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            }
        });
    };

    removeMessage = function (messageId, targetId) {
        self.removeMessage(messageId, targetId);
    };

    self.removeMessage = function (messageId, targetId) {
        _params.removeMessageParam.MsgId = messageId;
        _params.removeMessageParam.TargetId = targetId;

        CMS.headlineMsgBox.confirm("Are you sure you want to remove the message?", function () {
            $.ajax({
                type: "POST",
                url: "Message/MessageList/DeleteMessage",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                data: JSON.stringify(_params.removeMessageParam),
                success: function (rs) {
                    self.getListMessage();
                },
                error: function (xhr, status, err) {
                    CMS.handleAjaxError(xhr, status, err);
                }
            });
        });
    };

    self.bindingEvents = function () {
        $("#btnGetListMessage").click(function () {
            self.getListMessage();
        });
        $("#updateMessageBtn").live("click", function () {
            getParamValues();
            self.updateMessage();
        });
    };
};

var messageList = new Fanex.ApolloCMS.Message.MessageList();

$(document).ready(function () {
    messageList.InitPage();
});