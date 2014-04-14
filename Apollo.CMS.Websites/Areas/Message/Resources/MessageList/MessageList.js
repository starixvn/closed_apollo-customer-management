/* global Apollo, Namespace, CMS, Validator, pageLayout, layoutHelper, tableHeaders, message, minDate, today, hasHostRight, isRedirected */

Namespace.Register('Apollo.CMS.Message.MessageList');
Apollo.CMS.Message.MessageList = function () {
    'use strict';
    var $this = this;
    var urls = {
        GetMessageList: CMS.resolveRootUrl('MessageList/GetMessageList'),
        GetMessageById: CMS.resolveRootUrl('MessageList/GetMessageById'),
        UpdateMessage: CMS.resolveRootUrl('MessageList/UpdateMessage'),
        DeleteMessage: CMS.resolveRootUrl('MessageList/DeleteMessage')
    };
    var parameters = {
        TopNewMsg: null,
        FromDate: null,
        ToDate: null,
        MsgType: null,
        Special: null
    };
    var domElements = {
        divMsgFilterForm: null,
        ddlTopNewMessage: null,
        dpkFromDate: null,
        dpkToDate: null,
        ddlMessageTypes: null,
        ddlSpecial: null,
        btnSubmit: null,
        divMessage: null,
        divPopup: null
    };
    var popupElements = {
        divEnglishLabel: null,
        txtContentEn: null,
        txtContentCh: null,
        txtContentCs: null,
        txtContentJp: null,
        txtContentKo: null,
        txtContentTh: null,
        txtContentVn: null,
        divOtherLanguageTitle: null,
        divOtherLanguage: null,
        spanOtherLanguage: null,
        btnUpdateMsg: null
    };

    var cacheDOMElements = function () {
        domElements.divMsgFilterForm = $('#divMsgFilterForm');
        domElements.ddlTopNewMessage = $('#ddlTopNewMessage', domElements.divMsgFilterForm);
        domElements.dpkFromDate = $('#dpkFromDate', domElements.divMsgFilterForm);
        domElements.dpkToDate = $('#dpkToDate', domElements.divMsgFilterForm);
        domElements.ddlMessageType = $('#ddlMessageType', domElements.divMsgFilterForm);
        domElements.ddlSpecial = $('#ddlSpecial', domElements.divMsgFilterForm);
        domElements.btnSubmit = $('#btnSubmit', domElements.divMsgFilterForm);
        domElements.divMessage = $('#divMessage');
        domElements.divPopup = $('#divPopup');
    };

    var cachePopupElements = function () {
        popupElements.divEnglishLabel = $('#divEnglishLabel', domElements.divPopup);
        popupElements.txtContentEn = $('#ContentEn', domElements.divPopup);
        popupElements.txtContentCh = $('#ContentCh', domElements.divPopup);
        popupElements.txtContentCs = $('#ContentCs', domElements.divPopup);
        popupElements.txtContentJp = $('#ContentJp', domElements.divPopup);
        popupElements.txtContentKo = $('#ContentKo', domElements.divPopup);
        popupElements.txtContentTh = $('#ContentTh', domElements.divPopup);
        popupElements.txtContentVn = $('#ContentVn', domElements.divPopup);
        popupElements.divOtherLanguageTitle = $('#divOtherLanguageTitle', domElements.divPopup);
        popupElements.divOtherLanguage = $('#divOtherLanguage', domElements.divPopup);
        popupElements.spanOtherLanguage = $('#spanOtherLanguage', domElements.divPopup);
        popupElements.btnUpdateMsg = $('#btnUpdateMsg', domElements.divPopup);
    };

    var initFields = function () {
        layoutHelper.CreateDatepicker(domElements.dpkFromDate, null, null, function (selectedDate) {
            domElements.dpkToDate.datepicker('option', 'minDate', selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        layoutHelper.CreateDatepicker(domElements.dpkToDate, null, null, function (selectedDate) {
            domElements.dpkFromDate.datepicker('option', 'maxDate', selectedDate);
        }).datepicker('option', 'minDate', minDate).datepicker('option', 'maxDate', today);

        domElements.btnSubmit.click(function () {
            parameters.TopNewMsg = domElements.ddlTopNewMessage.val();
            parameters.FromDate = domElements.dpkFromDate.val();
            parameters.ToDate = domElements.dpkToDate.val();
            parameters.MsgType = domElements.ddlMessageType.val();
            parameters.Special = domElements.ddlSpecial.val();

            getMessageList();
        });

        if (isRedirected) {
            domElements.btnSubmit.click();
        }
    };

    var getMessageList = function () {
        $.ajax({
            type: 'POST',
            url: urls.GetMessageList,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(parameters),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                renderMessageList(result);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };

    var renderMessageList = function (data) {
        var gridOptions = {
            gridExpandHeight: 160,
            bodyRowHeight: 30,
            showDistinctRow: false
        };
        var columns = [
            { HeaderText: tableHeaders.postedDate, Width: 150, Name: 'ColPostedDate', HeaderAlign: 'center', CellAlign: 'center' },
            { HeaderText: tableHeaders.messageEnglish, Width: 300, Name: 'ColMessageEnglish', HeaderAlign: 'center', CellAlign: 'left' },
            { HeaderText: tableHeaders.type, Width: 60, Name: 'ColMessageType', HeaderAlign: 'center', CellAlign: 'center' },
            { HeaderText: tableHeaders.isSpecial, Width: 80, Name: 'ColSpecial', HeaderAlign: 'center', CellAlign: 'center' },
            { HeaderText: tableHeaders.creator, Width: 80, Name: 'ColCreator', HeaderAlign: 'center', CellAlign: 'center' },
            { HeaderText: tableHeaders.recipient, Width: 80, Name: 'ColRecipient', HeaderAlign: 'center', CellAlign: 'center' }
        ];
        var bodyData = [];

        if (hasHostRight) {
            columns.push({ HeaderText: tableHeaders.remove, Width: 60, Name: 'ColRemove', HeaderAlign: 'center', CellAlign: 'center' });
        }

        if (data && data.length > 0) {
            var currentGroupId = 0;
            var currentItem = null;
            var fields = null;
            var editCol = '<div class="editBtn icon-edit" title="{0}" data-message-id="{1}"></div>';
            var messageCol = '<div class="message-content" title="{0}">{1}</div><div class="clear"></div>';
            var specialCol = '<span class="glyphicon glyphicon-ok" />';
            var creatorCol = '<div class="user-content">{0}</div>';
            var recipientCol = '<div class="user-content">{0}</div>';
            var removeCol = '<div title="{0}" class="icon-remove" data-message-id="{1}" data-msg-target-id="{2}"/>';
            var message = null;

            $.each(data, function (index, item) {
                if (currentGroupId !== item.MessageId) {
                    currentGroupId = item.MessageId;
                    currentItem = item;
                    currentItem.NumOfElems = 1;
                } else {
                    currentItem.NumOfElems++;
                }
            });
            currentGroupId = 0;

            $.each(data, function (index, item) {
                fields = [];

                if (currentGroupId !== item.MessageId) {
                    currentGroupId = item.MessageId;
                    message = '';

                    if (hasHostRight) {
                        message = editCol.format(tableHeaders.edit, item.MessageId);
                    }

                    message += messageCol.format(CMS.htmlEncode(item.ContentEn), trimString(item.ContentEn));

                    fields.push(
                        { Name: "ColPostedDate", Value: item.PostTime, MergeRowNumber: item.NumOfElems },
                        { Name: "ColMessageEnglish", Value: message, MergeRowNumber: item.NumOfElems },
                        { Name: "ColMessageType", Value: item.MessageType, MergeRowNumber: item.NumOfElems },
                        { Name: "ColSpecial", Value: item.IsSpecial ? specialCol : '', MergeRowNumber: item.NumOfElems },
                        { Name: "ColCreator", Value: creatorCol.format(item.Creator), MergeRowNumber: item.NumOfElems }
                    );
                }

                fields.push({ Name: "ColRecipient", Value: recipientCol.format(item.Recipient) });

                if (hasHostRight) {
                    fields.push({ Name: "ColRemove", Value: removeCol.format(tableHeaders.remove, item.MessageId, item.MsgTargetId) });
                }

                bodyData.push({ Columns: fields });
            });
        }

        gridOptions.columns = columns;
        gridOptions.bodyRows = bodyData;
        domElements.divMessage.fanexGrid(gridOptions);
        handleMessageListEvents();
    };

    var handleMessageListEvents = function () {
        $('div.icon-remove', domElements.divMessage).click(function () {
            var $obj = $(this);
            var messageId = $obj.attr('data-message-id');
            var targetId = $obj.attr('data-msg-target-id');
            var removeMsgParams = {
                MsgId: messageId,
                MsgTargetId: targetId
            };

            deleteMessage(removeMsgParams);
        });

        $('div.icon-edit', domElements.divMessage).click(function () {
            var $obj = $(this);
            var messageId = $obj.attr('data-message-id');

            getMessageById(messageId);
        });
    };

    var deleteMessage = function (removeMsgParams) {
        CMS.headlineMsgBox.confirm(message.removeMessageConfirm, function () {
            $.ajax({
                type: 'POST',
                url: urls.DeleteMessage,
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                data: JSON.stringify(removeMsgParams),
                success: function () {
                    getMessageList();
                },
                error: function (xhr, status, err) {
                    CMS.handleAjaxError(xhr, status, err);
                }
            });
        }, { modal: true });
    };

    var getMessageById = function (id) {
        var params = {
            MessageId: id
        };

        $.ajax({
            type: 'POST',
            url: urls.GetMessageById,
            contentType: 'application/json; charset=utf-8',
            dataType: 'html',
            data: JSON.stringify(params),
            success: function (html) {
                pageLayout.Popup(message.editMessage, html, { position: ['center', 90] }, true);
                handlePopupEvents(id);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            }
        });
    };

    var validateUpdateForm = function () {
        var contentEnValue = $.trim(popupElements.txtContentEn.val());
        var label = $('label.language-title', popupElements.divEnglishLabel);
        var isValid = true;

        label.tipsy('hide');

        if (!contentEnValue) {
            var errorMsg = popupElements.txtContentEn.attr('data-val-required');

            Validator.showTooltip(label, errorMsg, 'w');
            isValid = false;
        }

        return isValid;
    };

    var updateMessage = function (params) {
        $.ajax({
            type: 'POST',
            url: urls.UpdateMessage,
            contentType: 'application/json; charset=utf-8',
            dataType: "json",
            data: JSON.stringify(params),
            success: function () {
                pageLayout.ClosePopup();
                CMS.showInfoMessage(message.updateSuccessInform);
                getMessageList();
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            }
        });
    };

    var handlePopupEvents = function (id) {
        cachePopupElements();

        popupElements.divOtherLanguageTitle.click(function () {
            if (popupElements.spanOtherLanguage.hasClass('icon-exp-panel')) {
                popupElements.spanOtherLanguage.removeClass('icon-exp-panel').addClass('icon-colp-panel');
            } else {
                popupElements.spanOtherLanguage.removeClass('icon-colp-panel').addClass('icon-exp-panel');
            }

            popupElements.divOtherLanguage.slideToggle('slow');
        });

        popupElements.btnUpdateMsg.click(function () {
            if (validateUpdateForm()) {
                var params = {
                    MsgId: id,
                    ContentEn: $.trim(popupElements.txtContentEn.val()),
                    ContentCh: $.trim(popupElements.txtContentCh.val()),
                    ContentCs: $.trim(popupElements.txtContentCs.val()),
                    ContentJp: $.trim(popupElements.txtContentJp.val()),
                    ContentKo: $.trim(popupElements.txtContentKo.val()),
                    ContentTh: $.trim(popupElements.txtContentTh.val()),
                    ContentVn: $.trim(popupElements.txtContentVn.val())
                };

                updateMessage(params);
            }
        });
    };

    var trimString = function (content) {
        if (content.length > 100) {
            content = content.substring(0, 100) + '...';
        }

        return CMS.htmlEncode(content);
    };

    $this.InitPage = function () {
        cacheDOMElements();
        initFields();
    };
};



$(document).ready(function () {
    'use strict';
    var messageList = new Apollo.CMS.Message.MessageList();

    messageList.InitPage();
});