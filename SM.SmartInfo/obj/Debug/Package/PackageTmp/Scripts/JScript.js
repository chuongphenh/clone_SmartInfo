//function ValidateKeypress(numcheck, e) {
//    var keynum, keychar, numcheck;
//    if (window.event) {
//        keynum = e.keyCode;
//    }
//    else if (e.which) {
//        keynum = e.which;
//    }
//    if (keynum == 8 || keynum == 127 || keynum == null || keynum == 9 || keynum == 0 || keynum == 13) return true;
//    keychar = String.fromCharCode(keynum);
//    var result = numcheck.test(keychar);
//    return result;
//}

//function ValidateKeypressIsNumber(evt) {
//    evt = (evt) ? evt : window.event;
//    var charCode = (evt.which) ? evt.which : evt.keyCode;
//    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
//        return false;
//    }
//    return true;
//}

function openInNewTab() {
    window.document.forms[0].target = '_blank';
    setTimeout(function () { window.document.forms[0].target = ''; }, 500);
}

function ShowOrHideObjectByID(id) {
    var obj = document.getElementById(id);
    if (obj.style["display"] === 'none')
        obj.style["display"] = 'block';
    else
        obj.style["display"] = 'none';
}

function getControlClientID(asp_net_id) {
    return document.getElementById($("[id$=" + asp_net_id + "]").attr("id"));
}

function getClientID(asp_net_id) {
    return $("[id$=" + asp_net_id + "]").attr("id");
}

function isDigit(evt, txt) {
    var charCode = evt.which ? evt.which : event.keyCode;

    var c = String.fromCharCode(charCode);

    if (txt.indexOf(c) > 0 && charCode == 46) {
        return false;
    }
    else if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }

    return true;
}

function isNumber(evt) {
    evt = evt ? evt : window.event;
    var charCode = evt.which ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
}

function SelectAll(obj, chkID) {
    var frm = document.forms['aspnetForm'];
    for (var i = 0; i < document.forms[0].length; i++) {
        if (document.forms[0].elements[i].id.indexOf('' + chkID + '') != -1) {
            document.forms[0].elements[i].checked = obj.checked;
        }
    }
}

function SelectAllWithoutDisable(obj, chkID) {
    var frm = document.forms['aspnetForm'];
    for (var i = 0; i < document.forms[0].length; i++) {
        if (document.forms[0].elements[i].id.indexOf('' + chkID + '') != -1 && document.forms[0].elements[i].disabled == false) {
            document.forms[0].elements[i].checked = obj.checked;
        }
    }
}

function CheckOneRadioButton(id) {
    var rdBtn = document.getElementById(id);
    var List = document.getElementsByTagName("input");
    for (i = 0; i < List.length; i++) {
        if (List[i].type == "radio" && List[i].id != rdBtn.id) {
            List[i].checked = false;
        }
    }
}

function refreshParent() {
    try {
        window.returnValue = 'closed';
        var objWindOpener = window.opener;

        if (objWindOpener.progressWindow) {
            objWindOpener.progressWindow.close();
        }

        // get current scroll
        var scrollX, scrollY;
        if (objWindOpener.pageYOffset != undefined) {
            scrollX = objWindOpener.pageXOffset;
            scrollY = objWindOpener.pageYOffset;
        }
        else {
            var d = objWindOpener.document;
            var r = d.documentElement;
            var b = d.body;
            scrollX = r.scrollLeft || b.scrollLeft || 0;
            scrollY = r.scrollTop || b.scrollTop || 0;
        }

        // set current position
        var strNewUrl = objWindOpener.location.href;
        if (strNewUrl.indexOf('#') > 0) {
            strNewUrl = strNewUrl.substring(0, strNewUrl.indexOf('#'));
        }

        if (strNewUrl.indexOf('?') > 0) {
            var splitIndex = strNewUrl.indexOf('&sx=');
            if (splitIndex > 0) {
                strNewUrl = strNewUrl.substring(0, splitIndex);
            }

            strNewUrl = strNewUrl + '&';
        }
        else {
            strNewUrl = strNewUrl + '?';
        }
        strNewUrl = strNewUrl + 'sx=' + scrollX + '&sy=' + scrollY;
        objWindOpener.location.href = strNewUrl;

        window.close();
    } catch (ex) {
    }
}

function keepPreviousScroll() {
    var scrollX = getParam("sx");
    var scrollY = getParam("sy");

    if (scrollX != null && scrollY != null) {
        window.scrollTo(scrollX, scrollY);
    }
}

function keepPreviousScrollOnCookie() {
    var objWindOpener = window.opener;
    if (objWindOpener.pageYOffset != undefined) {
        document.cookie = "keepscroll=" + objWindOpener.pageYOffset;

    } else {
        var d = objWindOpener.document;
        var r = d.documentElement;
        var b = d.body;
        document.cookie = "keepscroll=" + (r.scrollTop || b.scrollTop || 0);
    }
}

function getParam(name) {
    var url = window.location.href;
    var arrUrlPath = url.split("?");
    if (arrUrlPath.length > 1) {
        var arrParam = arrUrlPath[1].split("&");
        for (var i = 0; i < arrParam.length; i++) {
            arrParamPath = arrParam[i].split("=");
            if (name == arrParamPath[0]) {
                return arrParamPath[1];
            }
        }
    }

    return null;
}

function redirectParent(linkRedirect) {
    window.opener.location.href = linkRedirect;

    if (window.opener.progressWindow) {
        window.opener.progressWindow.close();
    }
    window.close();
}

function selectAllCheckbox(obj, chkName) {
    $('span[name = "' + chkName + '"]').children(':enabled').attr('checked', obj.checked);
}

function selectOneRadiobox(obj, rdoName) {
    $('span[name = "' + rdoName + '"]').children().attr('checked', false);
    obj.checked = true;
}

function confirmDeleteAll() {
    chkChecks = document.getElementsByTagName('input');
    count = 0;
    for (i = 0; i < chkChecks.length; i++) {
        objElement = chkChecks[i];
        if (objElement.type == 'checkbox' && objElement.checked == 'true') {
            count = 1;
            break;
        }
    }

    if (count == 0) {
        alert("Bạn chưa chọn bản ghi nào để xóa.");
        return false;
    }

    return confirm('Bạn có muốn xóa bản ghi đã chọn đã chọn?');
}

var targetWin;

function PopupCenter(pageURL, title, w, h) {
    var left = screen.width / 2 - w / 2;
    var top = screen.height / 2 - h / 2;

    strFeatures = 'toolbar=no, location=no, directories=no, status=no, menubar=yes, scrollbars=yes, resizable=yes, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left;
    targetWin = window.open(pageURL, title, strFeatures);
    targetWin.focus();

    return targetWin;
}

function GetRadioButtonValueByName(id) {
    var radio = document.getElementsByName(id);
    for (var j = 0; j < radio.length; j++) {
        if (radio[j].checked)
            alert(radio[j].value);
    }
}

// start decorate grid for status
function updateColor(strName, strStatus, strColor) {
    arrTask = document.getElementsByName(strName);
    for (i = 0; i < arrTask.length; i++) {
        spanTask = arrTask[i];
        strValue = spanTask.innerHTML.trim();
        if (strValue == strStatus.toString()) {
            trTask = spanTask.parentNode.parentNode;
            trTask.style.color = strColor.toString();
        }
    }
}
// end

// start check click inside popup
function isInsidePopup(element, strDivId, strImgId) {
    if (element == null || element == 'undefined') {
        return false;
    }

    strId = getElementId(element);
    if (strId == strDivId) {
        return true;
    }
    if (strId == strImgId) {
        return true;
    }

    return isInsidePopup(element.parentNode, strDivId, strImgId);
}

function getElementId(element) {
    try {
        return element.id;
    }
    catch (ex) { }

    return '';
}
// end

// start popup
function popupShowHide(divData, isSetPosition) {
    objDiv = document.getElementById(divData);
    if (objDiv.style.display == 'none' || objDiv.style.display == '') {
        objDiv.style.display = 'block';
        if (isSetPosition) {
            screenSize = popupGetScreenWidth(false);
            popupSize = objDiv.clientWidth;
            paddingSize = screenSize - popupSize;
            paddingSize = paddingSize / 2;
            objDiv.style.left = paddingSize + 'px';

            screenSize = popupGetScreenHeight(false);
            popupSize = objDiv.clientHeight;
            paddingSize = screenSize - popupSize;
            paddingSize = paddingSize / 2;
            objDiv.style.top = paddingSize + 'px';
        }
    }
    else {
        objDiv.style.display = 'none';
    }
}

function popupFitBlanketSize(divBlanket) {
    blanket = document.getElementById(divBlanket);
    blanket.style.height = popupGetScreenHeight(true) + 'px';
    blanket.style.width = popupGetScreenWidth(true) + 'px';
}

function popup(divData, divBlanket) {
    popupFitBlanketSize(divBlanket);
    popupShowHide(divBlanket, false);
    popupShowHide(divData, true);
}

function popupGetScreenHeight(isIncludeScroll) {
    height = 0;
    if (typeof window.innerHeight != 'undefined') {
        height = window.innerHeight;
    } else {
        height = document.documentElement.clientHeight;
    }

    if (isIncludeScroll) {
        if (height <= document.body.parentNode.scrollHeight || height <= document.body.parentNode.clientHeight) {
            if (document.body.parentNode.clientHeight > document.body.parentNode.scrollHeight) {
                height = document.body.parentNode.clientHeight;
            } else {
                height = document.body.parentNode.scrollHeight;
            }
        }
    }
    return height;
}

function popupGetScreenWidth(isIncludeScroll) {
    width = 0;
    // window
    if (typeof window.innerWidth != 'undefined') {
        width = window.innerWidth;
    } else {
        width = document.documentElement.clientWidth;
    }
    // parent
    if (width < document.body.parentNode.clientWidth) {
        width = document.body.parentNode.clientWidth;
    }
    // scroll
    if (isIncludeScroll) {
        if (width < document.body.parentNode.scrollWidth) {
            width = document.body.parentNode.scrollWidth;
        }
    }
    return width;
}
// end popup

function allowClick() {
    return true;
}

function notAllowClick() {
    return false;
}

// start fix OnItemsRequested event of RadCombobox
function RadHanleOnClientKeyPressing(comboBox, eventArgs) {
    intKeyCode = eventArgs.get_domEvent().keyCode;
    if (isPressNormalChar(intKeyCode)) {
        strChar = String.fromCharCode(96 <= intKeyCode && intKeyCode <= 105 ? intKeyCode - 48 : intKeyCode);
        strText = comboBox.get_text() + strChar;
        comboBox.requestItems(strText, false);
    }
}

function isPressNormalChar(keyCode) {
    switch (keyCode) {
        case 9:
        case 13:
        case 16:
        case 17:
        case 18:
        case 19:
        case 20:
        case 27:
        case 33:
        case 34:
        case 35:
        case 36:
        case 37:
        case 38:
        case 39:
        case 40:
        case 45:
        case 91:
        case 92:
        case 112:
        case 113:
        case 114:
        case 115:
        case 116:
        case 117:
        case 118:
        case 119:
        case 120:
        case 121:
        case 122:
        case 123:
        case 144:
        case 145:
            return false;
    }
    return true;
}
// end fix OnItemsRequested event of RadCombobox

// start show model dialog
function showPagePopup(url) {
    screenWidth = popupGetScreenWidth(false);
    screenHeight = popupGetScreenHeight(false);

    popupTop = 50;
    popupLeft = 50;

    popupWidth = screenWidth - 2 * popupLeft;
    popupHeight = screenHeight - 2 * popupTop;

    popupName = Math.random();
    popupName = popupName + "";
    popupName = popupName.substring(2);

    try {
        showOption = 'scrollbars=1,menubar=0,resizable=1,status=0,width=' + screenWidth + ',height=' + popupHeight + ',left=' + popupLeft + ',top=' + popupTop;
        popupForm = window.open(url, popupName, showOption);
        popupForm.focus();
    } catch (ex) {
    }
}

function showPageDialog(url) {
    screenWidth = popupGetScreenWidth(false);
    screenHeight = popupGetScreenHeight(false);

    popupTop = 50;
    popupLeft = 50;

    popupWidth = screenWidth - 2 * popupLeft;
    popupHeight = screenHeight - 2 * popupTop;

    popupName = Math.random();
    popupName = popupName + "";
    popupName = popupName.substring(2);

    try {
        window.ShowModalDialog(url, 1200, 1000, '', '');
    } catch (ex) {
    }
}
// end show model dialog

//Check maxlength for textarea
function checkMaxLength(textbox, event, long) {
    var maxlength = new Number(long); // Change number to your max length.
    if (!checkSpecialKeys(event)) {
        if (textbox.value.length > maxlength - 1) {
            event.preventDefault();
        }
    }
}

function checkSpecialKeys(e) {
    if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
        return false;
    else
        return true;
}

function CollapseExpandItem(e) {
    var content = e.nextElementSibling;
    content.classList.toggle('toggleExpand');
}

function GetDecimalValue(num) {
    return parseFloat(num.value.toString().replace(new RegExp(',', 'g'), '')).toFixed(2);
}

function GetDecimalDisplay(decimal, numberRound) {
    if (!isNaN(decimal)) {
        if (numberRound > 0)
            return (Math.round(parseFloat(decimal) / Math.pow(10, numberRound)) * Math.pow(10, numberRound)).toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
        else
            return decimal.toFixed(0 - numberRound).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    }
    return '';
}

function isEmptyOrSpaces(str) {
    return str === null || str.match(/^ *$/) !== null;
}

function rdbRadioButton_SelectedIndexChanged(e, lstID) {
    var selected = $(e).find(":checked").val();
    var arr = lstID.split(';');
    for (i = 0; i < arr.length; i++) {
        var ctlValue = arr[i].split('_');
        var cellID = ctlValue[0];
        var controlParent = $(e).closest('table.table-compare');
        var control = controlParent.find('.class' + cellID + '')[0];
        if (control)
            if (parseInt(ctlValue[1]) == selected) {
                if ($(control).hasClass('tdControlHidden')) $(control).removeClass('tdControlHidden');
            }
            else {
                $(control).addClass('tdControlHidden');
            }
    }
}

jQuery('.widget .widget-toolbar .fa-chevron-down').click(function () {
    var el = jQuery(this).parents(".widget").children(".widget-body");
    if (jQuery(this).hasClass("fa-chevron-down")) {
        jQuery(this).removeClass("fa-chevron-down").addClass("fa-chevron-up");
        el.slideUp(200);
    } else {
        jQuery(this).removeClass("fa-chevron-up").addClass("fa-chevron-down");
        el.slideDown(200);
    }
});

// Khoa link button khi thuc hien double click
function lockButton(objButton, waitingMilisecond) {
    var buttonEvent = objButton.getAttribute('onclick');
    objButton.setAttribute('onclick', 'alert("Vui lòng chờ hệ thống thực hiện xong"); return false;');
    setTimeout(function () { unlockButton(objButton, buttonEvent); }, waitingMilisecond);

    return true;
}

function unlockButton(objButton, buttonEvent) {
    objButton.setAttribute('onclick', buttonEvent);
}

//Customize confirm dialog 
var _confirm = false;

function confirmSM(button, confirmText, confirmTitle) {
    if (_confirm == false) {
        jQuery('<div>')
            .html("<p>" + confirmText + "</p>")
            .dialog({
                autoOpen: true,
                modal: true,
                title: confirmTitle,
                buttons: {
                    'Đồng ý': function () {
                        jQuery(this).dialog("close");
                        _confirm = true;
                        button.click();
                    },
                    'Không đồng ý': function () {
                        jQuery(this).dialog("close");
                    }
                },
                close: function () {
                    jQuery(this).remove();
                }
            });
    }
    return _confirm;
}

function setStyleProperty(element, attribute, value, priority) {
    if (typeof priority === 'undefined') {
        priority = null;
    }

    if (typeof element.style.setProperty !== 'undefined') {
        if (priority == null) {
            element.style.setProperty(attribute, value)
        } else {
            if (priority == '!important') {
                priority = 'important';
            }
            element.style.setProperty(attribute, value, priority);
        }
    } else if (typeof element['style'][attribute] !== 'undefined') {
        element['style'][attribute] = value;
    } else {
        element.style = attribute + ': ' + value + priority;
    }
}

function clickParentButton(buttonID) {
    if (buttonID == "") {
        return;
    }

    var objWindOpener = window.opener;

    if (objWindOpener) {
        var parentButton = objWindOpener.document.getElementById(buttonID);
        if (parentButton) {
            parentButton.click();
        }
    }
}