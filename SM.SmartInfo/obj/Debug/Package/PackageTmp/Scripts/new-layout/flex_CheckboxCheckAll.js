$(function () {
    $('.flex_combo-checkbox').each(function () {
        var cell = document.createElement('td');
        cell.innerHTML = "<input type='checkbox' class='chkSelectedAll' onclick='chkCheckAll(this)' /><label>Chọn tất cả/ Bỏ chọn tất cả</label>";
        var row = $(document.createElement('tr')).append(cell);
        $(row).css({ 'background': '#F8D2A5' });
        var header = $(document.createElement('thead')).append(row);
        $(this).append(header);

        flex_ComboCheckBox_GetCheckedItems(this);
    });
});

//check All
function chkCheckAll(e) {
    var $chkCheck = $(e);
    var $table = $chkCheck
        .closest('table.flex_combo-checkbox')
        .find('tbody td').each(function (i) {
            $(this).find('input[type="checkbox"]')[0].checked = $chkCheck.is(":checked");
        })

    flex_ComboCheckBox_GetCheckedItems(e);
};

//overide javascript
function flex_ComboCheckBox_GetCheckedItems(trigger) {
    var popup = $(trigger).closest("div[flex_type='popup']");
    var txtText = flex_GetTriggerOfPopup(popup);

    var count = 0;
    var item = 0;
    popup.find("table.flex_combo-checkbox tbody input[type='checkbox']").each(function (i, ck) {
        item++;
        if (ck.checked == true) {
            count++;
        }
    });

    var chkAll = popup.find("table.flex_combo-checkbox thead input.chkSelectedAll")[0];
    if (count == 0) {
        txtText.val('');
        chkAll.checked = false;
    }
    else {
        txtText.val(count + ' bản ghi được chọn');
        chkAll.checked = item == count;
    }
}