/* File Created: November 13, 2013 */
$(document).ready(function () {
    var publicMessage = new Fanex.ApolloCMS.Message.PublicMessage();

    publicMessage.InitPage();
});

Namespace.Register("Fanex.ApolloCMS.Message.PublicMessage");
Fanex.ApolloCMS.Message.PublicMessage = function () {
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
        $this.DOMInstance.divEnglish = $('#divEnglish');
        $this.DOMInstance.spnOtherLanguage = $('.other-lag');
        $this.DOMInstance.divOtherLanguage = $('#divOtherLanguage');
    };
    var initFields = function () {
        $this.DOMInstance.spnOtherLanguage.bind('click', function () {
            $this.DOMInstance.divOtherLanguage.slideToggle('slow');
        });

        $this.DOMInstance.btnSubmit.bind('click', function () {
            if (validateForm()) {
                getParamValues();
                $this.AddNewMessage();
            }
        });
    };
    var validateForm = function () {
        var contentEnValue = $.trim($this.DOMInstance.txtContentEn.val());
        var label = $this.DOMInstance.txtContentEn.prev();

        label.tipsy('hide');
        if (!contentEnValue) {
            var errorMsg = $this.DOMInstance.txtContentEn.attr('data-val-required');

            label.attr('title', errorMsg);
            label.tipsy({ trigger: 'manual', gravity: 'sw' });
            label.tipsy('show');
            $this.DOMInstance.txtContentEn.focus();

            return false;
        } else {
            return true;
        }
    };
    var getParamValues = function () {
        var selectedRecipientId = $this.DOMInstance.ddlRecipient.val();
        var selectedRecipientName = $('option:selected', $this.DOMInstance.ddlRecipient).text();
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
        divEnglish: null,
        spnOtherLanguage: null,
        divOtherLanguage: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
    $this.AddNewMessage = function () {
        $.ajax({
            type: "POST",
            url: urls.AddNewMessage,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
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