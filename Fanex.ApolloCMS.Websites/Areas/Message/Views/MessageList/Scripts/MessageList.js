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

    var messageDetailParam = {
        publicMessage: 3,
        privateMessage: 1,
        messageIspecial: true,
        messageNotSpecial: false
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

    var getSubMessageContent = function (content) {
        if (content.length > 100) {
            content = content.substring(0, 100) + '...';
        }
        return content;
    };

    self.renderDatePicker = function () {
        var dateFrom = null;

        _params.getListMessageParam.FromDate = today;
        _params.getListMessageParam.ToDate = today;

        $("#dateFrom").datepicker({
            showOn: "button",
            buttonImage: CMS.datePickerButtonUrl,
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
            buttonImage: CMS.datePickerButtonUrl,
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

    var validateForm = function () {
        var contentEnValue = $.trim(_tables.txtContentEn.val());
        var label = _tables.txtContentEn.prev();
        var isValid = true;

        label.tipsy('hide');
        if (!contentEnValue) {
            var errorMsg = _tables.txtContentEn.attr('data-val-required');

            Validator.showTooltip(label, errorMsg, 'sw');
            isValid = false;
        }
        return isValid;
    };

    self.getMessageType = function (msgType) {
        var messageType = null;
        if (msgType == messageDetailParam.publicMessage) {
            messageType = publicMessage;
        }
        else {
            messageType = privateMessage;
        }
        return messageType;
    };

    self.messageIsSpecial = function (isSpecial) {
        var msgIsSpecial = null;
        if (isSpecial == messageDetailParam.messageIspecial) {
            msgIsSpecial = '<div><span class="glyphicon glyphicon-ok"></span></div>';
        }
        else {
            msgIsSpecial = '<div></div>';
        }
        return msgIsSpecial;
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
            url: "MessageList/GetMessageList",
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
        var columns = [];
        var bodyData = [];

        if (hasHostRight) {
            columns = [
                    { HeaderText: postedDate, Width: 150, Name: "PostedDate", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: messageEnglish, Width: 300, Name: "MessageContent", HeaderAlign: "center", CellAlign: "left", CustomCellRender: function (currentCell) {
                        return '<div class="link editBtn glyphicon glyphicon-edit" title="Edit" onclick="editMessage(' + currentCell.Value.MessageId + ')"></div><div class="messageContent" title="' + currentCell.Value.ContentEn + '">' + getSubMessageContent(currentCell.Value.ContentEn) + '</div><div class="clear"></div>';
                    }
                    },
                    { HeaderText: messageType, Width: 60, Name: "MessageType", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: isSpecial, Width: 80, Name: "Special", HeaderAlign: "center", CellAlign: "center", CustomCellRender: function (currentCell) {
                        return self.messageIsSpecial(currentCell.Value.IsSpecial);
                    }
                    },
                    { HeaderText: creator, Width: 80, Name: "Creator", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: recipient, Width: 80, Name: "Recipient", HeaderAlign: "center", CellAlign: "center" },
                    { HeaderText: remove, Width: 60, Name: "Remove", HeaderAlign: "center", CellAlign: "center", CustomCellRender: function (currentCell) {
                        return '<div title="Remove" class="glyphicon glyphicon-minus-sign" onclick="removeMessage(' + currentCell.Value.MessageId + ',' + currentCell.Value.TargetId + ' )"></div>';
                    }
                    }
                ];
        } else {
            columns = [
                { HeaderText: postedDate, Width: 150, Name: "PostedDate", HeaderAlign: "center", CellAlign: "center" },
                { HeaderText: messageEnglish, Width: 300, Name: "MessageContent", HeaderAlign: "center", CellAlign: "left", CustomCellRender: function (currentCell) {
                    return '<div class="messageContent" title="' + currentCell.Value.ContentEn + '">' + getSubMessageContent(currentCell.Value.ContentEn) + '</div>';
                }
                },
                { HeaderText: messageType, Width: 60, Name: "MessageType", HeaderAlign: "center", CellAlign: "center" },
                { HeaderText: isSpecial, Width: 80, Name: "Special", HeaderAlign: "center", CellAlign: "center", CustomCellRender: function (currentCell) {
                    return self.messageIsSpecial(currentCell.Value.IsSpecial);
                }
                },
                { HeaderText: creator, Width: 80, Name: "Creator", HeaderAlign: "center", CellAlign: "center" },
                { HeaderText: recipient, Width: 80, Name: "Recipient", HeaderAlign: "center", CellAlign: "center" }
            ];
        }

        if (rs) {
            var messageLength = rs.length;
            var currentGroupId = 0;
            var currentItem = null;
            var totalMergeRow = 1;
            var msgType = null;
            var msgSpecial = null;

            $.each(rs, function (index, item) {
                if (currentGroupId != item.MessageId) {
                    currentGroupId = item.MessageId;
                    currentItem = item;
                    currentItem.NumOfElems = 1;
                } else {
                    currentItem.NumOfElems++;
                }
            });

            currentGroupId = 0;

            if (hasHostRight) {
                for (var messageIndex = 0; messageIndex < messageLength; messageIndex++) {
                    var currentMessage = rs[messageIndex];
                    var row = [];

                    msgType = self.getMessageType(currentMessage.MsgType);

                    if (currentGroupId != currentMessage.MessageId) {
                        currentGroupId = currentMessage.MessageId;
                        row.push(
                        { Name: "PostedDate", Value: currentMessage.PostDate, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "MessageContent", Value: currentMessage, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "MessageType", Value: msgType, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "Special", Value: currentMessage, MergeRowNumber: currentMessage.NumOfElems },
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
            } else {
                for (var messageIndex = 0; messageIndex < messageLength; messageIndex++) {
                    var currentMessage = rs[messageIndex];
                    var row = [];

                    msgSpecial = self.messageIsSpecial(currentMessage.IsSpecial);
                    msgType = self.getMessageType(currentMessage.MsgType);

                    if (currentGroupId != currentMessage.MessageId) {
                        currentGroupId = currentMessage.MessageId;
                        row.push(
                        { Name: "PostedDate", Value: currentMessage.PostDate, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "MessageContent", Value: currentMessage, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "MessageType", Value: msgType, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "Special", Value: currentMessage, MergeRowNumber: currentMessage.NumOfElems },
                        { Name: "Creator", Value: currentMessage.Creator, MergeRowNumber: currentMessage.NumOfElems }
                    );
                    }
                    row.push(
                    { Name: "Recipient", Value: currentMessage.Recipient }
                );

                    bodyData.push({
                        Columns: row
                    });
                }
            }

        }

        var gridOptions = {
            columns: columns,
            bodyRows: bodyData,
            gridExpandHeight: 250,
            bodyRowHeight: 30,
            showDistinctRow: false
        };

        _tables.gridMessage.fanexGrid(gridOptions);
    };

    self.getMessageById = function (id) {
        _params.getMessageByIdParam.messageId = id;

        $.ajax({
            type: "POST",
            url: "MessageList/GetMessageById",
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
            url: "MessageList/UpdateMessage",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(_params.updateMessageParam),
            success: function (rs) {
                CMS.closePopup();
                CMS.showInfoMessage(updateSuccessInform);
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

        CMS.headlineMsgBox.confirm(removeMessageConfirm, function () {
            $.ajax({
                type: "POST",
                url: "MessageList/DeleteMessage",
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
            if (Validator.validateForm($('.filter-form'))) {
                self.getListMessage();
            }
        });
        $("#updateMessageBtn").live("click", function () {
            if (validateForm()) {
                getParamValues();
                self.updateMessage();
            }
        });
        $("#collapseExpand").click(function () {
            if (!$('#gridMessageContainer').hasClass('display-none')) {
                setTimeout(function () {
                    $("#gridMessage").fanexGrid("resizeGrid");
                }, 110);
            }
        });

        $(".other-lag").live('click', function () {
            $('#divOtherLanguage').slideToggle('slow');
        });

        if (isRedirected) {
            $("#btnGetListMessage").click();
        }
    };
};

var messageList = new Fanex.ApolloCMS.Message.MessageList();

$(document).ready(function () {
    messageList.InitPage();
});