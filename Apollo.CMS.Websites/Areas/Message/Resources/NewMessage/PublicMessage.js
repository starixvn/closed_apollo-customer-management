/* File Created: November 13, 2013 */
$(document).ready(function () {
    var publicMessage = new Apollo.CMS.Message.PublicMessage();

    publicMessage.InitPage();
});

Namespace.Register("Apollo.CMS.Message.PublicMessage");
Apollo.CMS.Message.PublicMessage = function () {
    var $this = this;
    var urls = {
        AddNewMessage: CMS.resolveRootUrl('PublicMessage/AddNewMessage'),
        ViewMessageList: CMS.resolveRootUrl('Message/MessageList?isRedirected=true')
    };
    var params = {
        ContentEn: null,
        ContentCh: null,
        ContentCs: null,
        ContentJp: null,
        ContentKo: null,
        ContentTh: null,
        ContentVn: null,
        RecipientList: null,
        IsSpecialMsg: null
    };
    var cacheDOM = function () {
        $this.DOMInstance.txtContentEn = $('#ContentEn');
        $this.DOMInstance.txtContentCh = $('#ContentCh');
        $this.DOMInstance.txtContentCs = $('#ContentCs');
        $this.DOMInstance.txtContentJp = $('#ContentJp');
        $this.DOMInstance.txtContentKo = $('#ContentKo');
        $this.DOMInstance.txtContentTh = $('#ContentTh');
        $this.DOMInstance.txtContentVn = $('#ContentVn');
        $this.DOMInstance.ddlRecipient = $('#Recipient');
        $this.DOMInstance.chkIsSpecialMsg = $('#IsSpecialMsg');
        $this.DOMInstance.btnSubmit = $('#btnSubmit');
        $this.DOMInstance.divEnglishLabel = $('#divEnglishLabel');
        $this.DOMInstance.spanOtherLanguage = $('#spanOtherLanguage');
        $this.DOMInstance.divOtherLanguageTitle = $('#divOtherLanguageTitle');
        $this.DOMInstance.divOtherLanguage = $('#divOtherLanguage');
        $this.DOMInstance.divAgentLevels = $('#divAgentLevels');
        $this.DOMInstance.chkSuper = $('#chkSuper');
    };
    var initFields = function () {
        $this.DOMInstance.divOtherLanguageTitle.bind('click', function () {
            if ($this.DOMInstance.spanOtherLanguage.hasClass('icon-exp-panel')) {
                $this.DOMInstance.spanOtherLanguage.removeClass('icon-exp-panel').addClass('icon-colp-panel');
            } else {
                $this.DOMInstance.spanOtherLanguage.removeClass('icon-colp-panel').addClass('icon-exp-panel');
            }

            $this.DOMInstance.divOtherLanguage.slideToggle('slow');
        });

        $this.DOMInstance.ddlRecipient.on('change', function () {
            var selectedIndex = $('option:selected', $this.DOMInstance.ddlRecipient).index();

            if (selectedIndex === 1) {
                var defaultRadio = $('input:radio[value="HighLevelAgents"]', $this.DOMInstance.divAgentLevels);

                $this.DOMInstance.divAgentLevels.slideDown('slow');
                defaultRadio.prop('checked', true);
                defaultRadio.change();
            } else {
                $this.DOMInstance.divAgentLevels.slideUp('slow');
            }
        });
        $this.DOMInstance.ddlRecipient.change();

        $('input:radio', $this.DOMInstance.divAgentLevels).on('change', function () {
            $('input:checkbox', $this.DOMInstance.divAgentLevels).prop('checked', true);

            if ($(this).val() === 'HighLevelAgents') {
                $('input:checkbox', $this.DOMInstance.divAgentLevels).removeAttr('disabled');
            } else {
                $('input:checkbox', $this.DOMInstance.divAgentLevels).attr('disabled', 'true');
            }
        });

        $this.DOMInstance.btnSubmit.on('click', function () {
            if (validateForm()) {
                getParamValues();
                $this.AddNewMessage();
            }
        });
    };
    var validateForm = function () {
        var contentEnValue = $.trim($this.DOMInstance.txtContentEn.val());
        var label = $this.DOMInstance.divEnglishLabel;
        var levels = $('input:checkbox:checked', $this.DOMInstance.divAgentLevels);
        var selectedRadio = $('input:radio:checked', $this.DOMInstance.divAgentLevels);
        var isValid = true;

        label.tipsy('hide');
        if (!contentEnValue) {
            var errorMsg = $this.DOMInstance.txtContentEn.attr('data-val-required');

            Validator.showTooltip(label, errorMsg, 'sw');
            $this.DOMInstance.txtContentEn.focus();
            isValid = false;
        }

        $this.DOMInstance.chkSuper.tipsy('hide');
        if (($this.DOMInstance.divAgentLevels.is(':visible')) && (levels.length === 0) && (selectedRadio.val() === 'HighLevelAgents')) {
            var errorMsg = $this.DOMInstance.divAgentLevels.attr('data-val-required');

            Validator.showTooltip($this.DOMInstance.chkSuper, errorMsg, 'nw');
            isValid = false;
        }

        return isValid;
    };
    var getParamValues = function () {
        var selectedRecipientId = $this.DOMInstance.ddlRecipient.val();
        var selectedRecipientName = $('option:selected', $this.DOMInstance.ddlRecipient).text();
        var selectedHighLevels = $('input[name=AgentLevel]:checked', $this.DOMInstance.divAgentLevels).val();
        var recipients = [];

        if (selectedRecipientId == 'null') {
            var childItems = $this.DOMInstance.ddlRecipient.children();
            var numOfChilds = childItems.length;

            for (var index = 1; index < numOfChilds; index++) {
                var item = childItems.eq(index);
                var recipient = {
                    TargetId: item.val(),
                    TargetName: item.text()
                };

                recipients.push(recipient);
            }
        } else if ((selectedHighLevels === 'HighLevelAgents') && ($this.DOMInstance.divAgentLevels.is(':visible'))) {
            var levels = $('input:checkbox:checked', $this.DOMInstance.divAgentLevels);

            for (var index = 0; index < levels.length; index++) {
                var recipient = {
                    TargetId: selectedRecipientId,
                    TargetName: selectedRecipientName,
                    AgentLevel: $(levels[index]).attr('data-msg-target-id')
                };

                recipients.push(recipient);
            }
        } else {
            var recipient = {
                TargetId: selectedRecipientId,
                TargetName: selectedRecipientName
            };

            recipients.push(recipient);
        }

        params.ContentEn = $.trim($this.DOMInstance.txtContentEn.val());
        params.ContentCh = $.trim($this.DOMInstance.txtContentCh.val());
        params.ContentCs = $.trim($this.DOMInstance.txtContentCs.val());
        params.ContentJp = $.trim($this.DOMInstance.txtContentJp.val());
        params.ContentKo = $.trim($this.DOMInstance.txtContentKo.val());
        params.ContentTh = $.trim($this.DOMInstance.txtContentTh.val());
        params.ContentVn = $.trim($this.DOMInstance.txtContentVn.val());
        params.IsSpecialMsg = $this.DOMInstance.chkIsSpecialMsg.is(':checked');
        params.RecipientList = recipients;
    };
    var disableForm = function () {
        //Attention: this will not reset the form to enable when refreshing the page by pressing F5.

        $this.DOMInstance.txtContentEn.attr('disabled', 'true');
        $this.DOMInstance.txtContentCh.attr('disabled', 'true');
        $this.DOMInstance.txtContentCs.attr('disabled', 'true');
        $this.DOMInstance.txtContentJp.attr('disabled', 'true');
        $this.DOMInstance.txtContentKo.attr('disabled', 'true');
        $this.DOMInstance.txtContentTh.attr('disabled', 'true');
        $this.DOMInstance.txtContentVn.attr('disabled', 'true');
        $this.DOMInstance.ddlRecipient.attr('disabled', 'true');
        $this.DOMInstance.chkIsSpecialMsg.attr('disabled', 'true');
        $this.DOMInstance.btnSubmit.attr('disabled', 'true');
        $('input:checkbox', $this.DOMInstance.divAgentLevels).attr('disabled', 'true');
        $('input:radio', $this.DOMInstance.divAgentLevels).attr('disabled', 'true');
    };

    $this.DOMInstance = {
        txtContentEn: null,
        txtContentCh: null,
        txtContentCs: null,
        txtContentJp: null,
        txtContentKo: null,
        txtContentTh: null,
        txtContentVn: null,
        ddlRecipient: null,
        chkIsSpecialMsg: null,
        btnSubmit: null,
        divEnglishLabel: null,
        divOtherLanguageTitle: null,
        divOtherLanguage: null,
        divAgentLevels: null,
        chkSuper: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
    $this.AddNewMessage = function () {
        disableForm();

        $.ajax({
            type: 'POST',
            url: urls.AddNewMessage,
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            data: JSON.stringify(params),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                CMS.showInfoMessage(messages.PostMessageSuccessfully);
                setTimeout(function () {
                    if (hasViewMessageRight) {
                        window.location.replace(urls.ViewMessageList);
                    } else {
                        window.location.reload(true);
                    }
                }, 3000);
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
    };
}