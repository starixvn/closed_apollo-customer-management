/* File Created: November 17, 2013 */
$(document).ready(function () {
    var privateMessage = new Fanex.ApolloCMS.Message.PrivateMessage();
    
    privateMessage.InitPage();
});

Namespace.Register("Fanex.ApolloCMS.Message.PrivateMessage");
Fanex.ApolloCMS.Message.PrivateMessage = function () {
    var $this = this;
    var urls = {
        AddNewMessage: CMS.resolveRootUrl('PrivateMessage/AddNewMessage'),
        GetRecipientInfo: CMS.resolveRootUrl('PrivateMessage/GetRecipientInfo'),
        ViewMessageList: CMS.resolveRootUrl('Message/MessageList')
    };
    var params = {
        ContentEn: null,
        ContentCh: null,
        ContentCs: null,
        ContentJp: null,
        ContentKo: null,
        ContentTh: null,
        ContentVn: null,
        RecipientList: []
    };
    var cacheDOM = function () {
        $this.DOMInstance.txtContentEn = $('#ContentEn');
        $this.DOMInstance.txtContentCh = $('#ContentCh');
        $this.DOMInstance.txtContentCs = $('#ContentCs');
        $this.DOMInstance.txtContentJp = $('#ContentJp');
        $this.DOMInstance.txtContentKo = $('#ContentKo');
        $this.DOMInstance.txtContentTh = $('#ContentTh');
        $this.DOMInstance.txtContentVn = $('#ContentVn');
        $this.DOMInstance.ddlMemberType = $('#ddlMemberType');
        $this.DOMInstance.txtUserName = $('#txtUserName');
        $this.DOMInstance.chkInformUpline = $('#chkInformUpline');
        $this.DOMInstance.btnAdd = $('#btnAdd');
        $this.DOMInstance.btnSubmit = $('#btnSubmit');
        $this.DOMInstance.divEnglish = $('#divEnglish');
        $this.DOMInstance.spnOtherLanguage = $('.other-lag');
        $this.DOMInstance.divOtherLanguage = $('#divOtherLanguage');
        $this.DOMInstance.tblMemberListBody = $('#tblMemberList > tbody');
    };
    var initFields = function () {
        $this.DOMInstance.spnOtherLanguage.bind('click', function () {
            $this.DOMInstance.divOtherLanguage.slideToggle('slow');
        });

        $this.DOMInstance.btnAdd.bind('click', function () {
            var isAgent = $this.DOMInstance.ddlMemberType.val() == 'true';
            var userName = $.trim($this.DOMInstance.txtUserName.val());
            var informUpline = $this.DOMInstance.chkInformUpline.is(':checked');

            $this.GetRecipientInfo(isAgent, userName, informUpline);
        });

        $this.DOMInstance.btnSubmit.bind('click', function () {
            if (validateMsgForm()) {
                getParamValues();
                $this.AddNewMessage();
            }
        });
    };
    var validateMsgForm = function () {
        var contentEnValue = $.trim($this.DOMInstance.txtContentEn.val());
        var label = $this.DOMInstance.txtContentEn.prev();
        var isValid = true;

        label.tipsy('hide');
        if (!contentEnValue) {
            var errorMsg = $this.DOMInstance.txtContentEn.attr('data-val-required');

            Validator.showTooltip(label, errorMsg, 'sw');
            isValid = false;
        }

        $this.DOMInstance.txtUserName.tipsy('hide');
        if (params.RecipientList.length == 0) {
            var requiredErrorMsg = $this.DOMInstance.txtUserName.attr('data-val-required');
            Validator.showTooltip($this.DOMInstance.txtUserName, requiredErrorMsg, 's');
            isValid = false;
        }

        return isValid;
    };
    var validateCustForm = function (member, isAgent) {
        var isValid = true;

        $this.DOMInstance.txtUserName.tipsy('hide');
        if (member) {
            var isDuplicated = false;
            $.each(params.RecipientList, function (index, obj) {
                if ((member.UserName == obj.TargetName) && (isAgent == obj.IsAgent)) {
                    isDuplicated = true;
                }
            });

            if (isDuplicated) {
                var errorMsg = $this.DOMInstance.txtUserName.attr('data-val-existing');

                Validator.showTooltip($this.DOMInstance.txtUserName, errorMsg, 's');
                isValid = false;
            } else {

            }
        } else {
            var errorMsg = $this.DOMInstance.txtUserName.attr('data-val-invalid-username');

            Validator.showTooltip($this.DOMInstance.txtUserName, errorMsg, 's');
            isValid = false;
        }

        return isValid;
    };
    var getParamValues = function () {
        params.ContentEn = $.trim($this.DOMInstance.txtContentEn.val());
        params.ContentCh = $.trim($this.DOMInstance.txtContentCh.val());
        params.ContentCs = $.trim($this.DOMInstance.txtContentCs.val());
        params.ContentJp = $.trim($this.DOMInstance.txtContentJp.val());
        params.ContentKo = $.trim($this.DOMInstance.txtContentKo.val());
        params.ContentTh = $.trim($this.DOMInstance.txtContentTh.val());
        params.ContentVn = $.trim($this.DOMInstance.txtContentVn.val());
    };
    var addRow = function (member, isAgent, informUpline) {
        var userId = member.CustId;
        var rowFormat = '<tr><td class="text-center">{0}</td><td>{1}</td><td class="text-center">{2}</td><td class="text-center">{3}</td><td class="text-center">{4}</td></tr>';
        var no = params.RecipientList.length + 1;
        var level = isAgent ? labels.Agent : labels.Member;
        var upline = informUpline ? '<span class="glyphicon glyphicon-ok" />' : '';
        var remove = '<a href="#" onclick="removeRow({0})" class="glyphicon glyphicon-minus-sign" title="Remove"></a>'.format(no - 1);

        if (isAgent) {
            userId = member.AgentId;
        }

        if (params.RecipientList.length == 0) {
            $this.DOMInstance.tblMemberListBody.html('');
        }

        $this.DOMInstance.tblMemberListBody.append(rowFormat.format(no, member.UserName, level, upline, remove));
        params.RecipientList.push({
            TargetId: userId,
            TargetName: member.UserName,
            IsAgent: isAgent,
            InformUpline: informUpline
        });

        if (params.RecipientList.length >= 20) {
            $this.DOMInstance.btnAdd.attr('disabled', 'true');
        }
    };
    removeRow = function (index) {
        var numOfEles = params.RecipientList.length;

        if (numOfEles > 0) {
            $this.DOMInstance.tblMemberListBody.children().eq(index).remove();
            $this.DOMInstance.tblMemberListBody.children().each(function (eleIndex, eleValue) {
                $('td:first-child', eleValue).text(eleIndex + 1);
                $('td:last-child > a', eleValue).click(function () {
                    removeRow(eleIndex);
                });
            });

            params.RecipientList.splice(index, 1);

            if (params.RecipientList.length == 0) {
                $this.DOMInstance.tblMemberListBody.append('<tr><td colspan="5" class="text-center">{0}</td></tr>'.format(labels.NoMember));
            } else if (params.RecipientList.length == 19) {
                $this.DOMInstance.btnAdd.removeAttr('disabled');
            }
        }
    };

    $this.DOMInstance = {
        txtContentEn: null,
        txtContentCh: null,
        txtContentCs: null,
        txtContentJp: null,
        txtContentKo: null,
        txtContentTh: null,
        txtContentVn: null,
        ddlMemberType: null,
        txtUserName: null,
        chkInformUpline: null,
        btnAdd: null,
        btnSubmit: null,
        divEnglish: null,
        spnOtherLanguage: null,
        divOtherLanguage: null,
        tblMemberListBody: null
    };
    $this.InitPage = function () {
        cacheDOM();
        initFields();
    };
    $this.GetRecipientInfo = function (isAgent, userName, informUpline) {
        var data = {
            IsAgent: isAgent,
            UserName: userName
        };

        $.ajax({
            type: "POST",
            url: urls.GetRecipientInfo,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify(data),
            beforeSend: function () {
                CMS.loadingBox.show();
            },
            success: function (result) {
                if (validateCustForm(result, isAgent)) {
                    addRow(result, isAgent, informUpline);
                    $this.DOMInstance.txtUserName.val('');
                }
            },
            error: function (xhr, status, err) {
                CMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                CMS.loadingBox.close();
            }
        });
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