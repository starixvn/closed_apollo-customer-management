/* File Created: November 13, 2013 */
$(document).ready(function () {
    var publicMessage = new Fanex.ApolloCMS.Message.PublicMessage();

    publicMessage.InitPage(labels);
});

Namespace.Register("Fanex.ApolloCMS.Message.PublicMessage");
Fanex.ApolloCMS.Message.PublicMessage = function () {
    var $this = this;
    var urls = {
        AddNewMessage: ParlayRMS.resolveRootUrl('PublicMessage/AddNewMessage')
    };
    var params = {
        Content_En: null,
        Content_Ch: null,
        Content_Cs: null,
        Content_Jp: null,
        Content_Ko: null,
        Content_Th: null,
        Content_Vn: null,
        Recipient: null,
        IsSpecialMsg: null
    };
    var cacheDOM = function () {
        $this.DOMInstance.txtContentEn = $('#txtContentEn');
        $this.DOMInstance.txtContentCh = $('#txtContentCh');
        $this.DOMInstance.txtContentCs = $('#txtContentCs');
        $this.DOMInstance.txtContentJp = $('#txtContentJp');
        $this.DOMInstance.txtContentKo = $('#txtContentKo');
        $this.DOMInstance.txtContentTh = $('#txtContentTh');
        $this.DOMInstance.txtContentVn = $('#txtContentVn');
        $this.DOMInstance.ddlRecipient = $('#ddlRecipient');
        $this.DOMInstance.chkIsSpecialMsg = $('#chkIsSpecialMsg');
        $this.DOMInstance.btnSubmit = $('#btnSubmit');
        $this.DOMInstance.divEnglish = $('#divEnglish');
        $this.DOMInstance.spnOtherLanguage = $('#spnOtherLanguage');
        $this.DOMInstance.divOtherLanguage = $('#divOtherLanguage');
    };
    var initFields = function () {
        $this.DOMInstance.spnOtherLanguage.bind('click', function () {
            $this.DOMInstance.divOtherLanguage.toggle();
        });

        $this.DOMInstance.btnSearch.bind('click', function () {

        });
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
    $this.InitPage = function (labels) {
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
                ParlayRMS.loadingBox.show();
            },
            success: function (result) {
                //@@TODO: Display info message and reload page
            },
            error: function (xhr, status, err) {
                ParlayRMS.handleAjaxError(xhr, status, err);
            },
            complete: function () {
                ParlayRMS.loadingBox.close();
            }
        });
    };
}