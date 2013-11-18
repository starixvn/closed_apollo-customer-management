/* File Created: November 17, 2013 */
$(document).ready(function () {
    var privateMessage = new Fanex.ApolloCMS.Message.PrivateMessage();

    privateMessage.InitPage();
});

Namespace.Register("Fanex.ApolloCMS.Message.PrivateMessage");
Fanex.ApolloCMS.Message.PrivateMessage = function () {
    var $this = this;
    var urls = {
        AddNewMessage: CMS.resolveRootUrl('PrivateMessage/AddNewMessage')
    };
    var params = {
        ContentEn: null,
        ContentCh: null,
        ContentCs: null,
        ContentJp: null,
        ContentKo: null,
        ContentTh: null,
        ContentVn: null,
        RecipientList: null
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
    };
    var initFields = function () {
        $this.DOMInstance.spnOtherLanguage.bind('click', function () {
            $this.DOMInstance.divOtherLanguage.slideToggle('slow');
        });

        $this.DOMInstance.btnAdd.bind('click', function () {
            
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
        params.ContentEn = $.trim($this.DOMInstance.txtContentEn.val());
        params.ContentCh = $.trim($this.DOMInstance.txtContentCh.val());
        params.ContentCs = $.trim($this.DOMInstance.txtContentCs.val());
        params.ContentJp = $.trim($this.DOMInstance.txtContentJp.val());
        params.ContentKo = $.trim($this.DOMInstance.txtContentKo.val());
        params.ContentTh = $.trim($this.DOMInstance.txtContentTh.val());
        params.ContentVn = $.trim($this.DOMInstance.txtContentVn.val());
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
                //@@TODO: Display info message and reload page
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