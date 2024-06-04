var totalColumn, totalRow;

/*Bỏ context menu mặc định*/
$(document).on('contextmenu', function (e) {
    if ($(e.target).is(".flex-input") || $(e.target).is(".flex-toolbox"))
        return false;
});

$(document).on('click', '.color-item', function (e) {
    var picker = $('#flex-toolbox');
    var tdIndex = picker.attr('data-index');
    let color = $(this).attr('data-hex');
    let tbl = $(this).closest('#designsheet').find('#flex-table');
    tbl.find('td[data-index="' + tdIndex + '"]').css({
        'background-color': color,
        'opacity': (color === '') ? '' : '1'
    });

    tbl.find('td.selected-item').each(function (i) {
        $(this).css({
            'background-color': color,
            'opacity': (color === '') ? '' : '1'
        });
    });

    saveImage($(e.target));
});

/*Thêm event chuột phải*/
$(document).on('mousedown', '#flex-table tr td', function (e) {
    let isedit = $(this).closest('.flex-canvas').attr('mode').toLowerCase();
    if (isedit !== 'true') return false;
    var picker = $('#flex-toolbox');

    let tbl = $(this).closest('#designsheet').find('#flex-table');
    tbl.find('tr td').removeClass('selected-item');
    picker.hide();
    picker.attr('data-index', '');

    //Nếu là chuột phải -> show toolbox
    if (e.button === 2) {
        var $control = $(e.target);

        if ($control.hasClass('flex-input')) {
            isMouseDown = true;
            $control.closest('td').addClass('selected-item');
            $('.input-note').val('');
            picker.css({
                'display': 'block',
                'position': 'absolute',
                'top': e.pageY - picker.height(),
                'left': e.pageX
            });

            //Lưu index td đang chọn
            let index = $control.attr('data-index');
            picker.attr('data-index', index);
        }
        return false;
    }
    return true;
});

var isMouseDown = false;
var startIndex;
var endIndex;
$(document).on('mousedown', '#flex-table tr td', function (e) {
    let isedit = $(this).closest('.flex-canvas').attr('mode').toLowerCase();
    if (isedit !== 'true') return false;

    startIndex = null;
    endIndex = null;

    if (e.button === 0) {
        isMouseDown = true;
        startIndex = $(e.target).attr('data-index');
    }
});

$(document).on('mouseup', '#flex-table tr td', function (e) {
    let isedit = $(this).closest('.flex-canvas').attr('mode').toLowerCase();
    if (isedit !== 'true') return false;
    var picker = $('#flex-toolbox');

    if (isMouseDown === true) {
        if (startIndex != null && endIndex != null && startIndex !== undefined && endIndex !== undefined) {
            if (startIndex !== endIndex) {
                picker.css({
                    'display': 'block',
                    'position': 'absolute',
                    'top': e.pageY - picker.height(),
                    'left': e.pageX
                });
            }
        }

        isMouseDown = false;
    }
});

$(document).on('mouseover', '#flex-table tr td', function (e) {
    let isedit = $(this).closest('.flex-canvas').attr('mode').toLowerCase();
    if (isedit !== 'true') return false;

    if ($(e.target).hasClass('flex-input')) {
        if (isMouseDown === true) {
            let curentEnd = endIndex;
            endIndex = $(e.target).attr('data-index');
            if (endIndex !== curentEnd) {

                let tbl = $(this).closest('#designsheet').find('#flex-table');
                //TÍNH TOÁN BÔI MÀU
                $(e.target).closest('td').removeClass('selected-item');
                let tdStartIndex = tbl.find('.flex-input[data-index="' + startIndex + '"]').closest('td').index();
                let tdEndIndex = tbl.find('.flex-input[data-index="' + endIndex + '"]').closest('td').index();
                let colCount = Math.abs(tdStartIndex - tdEndIndex) + 1;

                let rowStart = parseInt(startIndex / totalColumn);
                let rowEnd = parseInt(endIndex / totalColumn);
                let rowCount = Math.abs(rowStart - rowEnd) + 1;
                console.log(tbl);
                addSelectedBackground(tbl, colCount, rowCount);
            }
        }
    }
});

function clearText(e) {
    var picker = $('#flex-toolbox');
    var inputIndex = picker.attr('data-index');
    let tbl = e.closest('#designsheet').find('#flex-table');
    tbl.find('td .flex-input[data-index="' + inputIndex + '"]').text('');

    saveImage(e);
}

function addSelectedBackground(tbl, colCount, rowCount) {
    for (let j = 0; j < rowCount; j++) {
        let start = startIndex * 1 + totalColumn * j;
        for (let i = 0; i < colCount; i++) {
            let newIndex = start + i;
            tbl.find('.flex-input[data-index="' + newIndex + '"]').closest('td').addClass('selected-item');
        }
    }
}

function addBorder(e) {
    let tbl = e.closest('#designsheet').find('#flex-table');
    let len = tbl.find('td.selected-item').length;
    let firstTD = tbl.find('td.selected-item')[0];
    let lastTD = tbl.find('td.selected-item')[len - 1];
    let startTDIndex = parseInt($(firstTD).attr('data-index'));
    let lastTDIndex = parseInt($(lastTD).attr('data-index'));

    //TÍNH TOÁN BÔI MÀU
    let tdStartIndex = $(firstTD).index();
    let tdEndIndex = $(lastTD).index();
    let colCount = Math.abs(tdStartIndex - tdEndIndex) + 1;

    let rowStart = parseInt(startTDIndex / totalColumn);
    let rowEnd = parseInt(lastTDIndex / totalColumn);
    let rowCount = Math.abs(rowStart - rowEnd) + 1;

    //Border trên
    for (let i = 0; i < colCount; i++) {
        let newIndex = startTDIndex * 1 + i;
        let $td = tbl.find('td[data-index="' + newIndex + '"]');
        $td.addClass('border-top');
    }

    //Border trái
    for (let i = 0; i < rowCount; i++) {
        let newIndex = startTDIndex + totalColumn * i;
        let $td = tbl.find('td[data-index="' + newIndex + '"]');
        $td.addClass('border-left');
    }

    //Border phải .
    for (let i = 0; i < rowCount; i++) {
        let newIndex = startTDIndex * 1 + (colCount - 1) + totalColumn * i;
        let $td = tbl.find('td[data-index="' + newIndex + '"]');
        $td.addClass('border-right');
    }

    //Border bottom
    for (let i = 0; i < colCount; i++) {
        let newIndex = startTDIndex * 1 + totalColumn * (rowCount - 1) + i;
        let $td = tbl.find('td[data-index="' + newIndex + '"]');
        $td.addClass('border-bottom');
    }

    saveImage(e);
}

function addDiagonalBorder(e) {
    let tbl = e.closest('#designsheet').find('#flex-table');
    let len = tbl.find('td.selected-item').length;
    let firstTD = tbl.find('td.selected-item')[0];
    let lastTD = tbl.find('td.selected-item')[len - 1];
    let startTDIndex = parseInt($(firstTD).attr('data-index'));
    let lastTDIndex = parseInt($(lastTD).attr('data-index'));

    //TÍNH TOÁN BÔI MÀU
    let tdStartIndex = $(firstTD).index();
    let tdEndIndex = $(lastTD).index();
    let colCount = Math.abs(tdStartIndex - tdEndIndex) + 1;

    let rowStart = parseInt(startTDIndex / totalColumn);
    let rowEnd = parseInt(lastTDIndex / totalColumn);
    let rowCount = Math.abs(rowStart - rowEnd) + 1;

    for (let j = 0; j < rowCount; j++) {
        let start = startTDIndex * 1 + totalColumn * j;
        for (let i = 0; i < colCount; i++) {
            let newIndex = start + i;
            tbl.find('td[data-index="' + newIndex + '"]').addClass('border-diagonal');
        }
    }

    saveImage(e);
}

function clearBorder(e) {
    let tbl = e.closest('#designsheet').find('#flex-table');
    tbl.find('td.selected-item').removeClass('border-top border-left border-right border-bottom border-diagonal');

    saveImage(e);
}

function changeText(e, text) {
    let tbl = e.closest('#designsheet').find('#flex-table');
    let selectitem = tbl.find('td.selected-item .flex-input').first();
    selectitem.css({ 'position': 'absolute' });
    selectitem.text(text);
}

function saveImage(e) {
    try {
        var element = e.closest('#designsheet').find('#flex-table').get(0);
        console.log(e.closest('#designsheet').find('.save-image'));
        e.closest('#designsheet').find('.save-image').attr('enable', false);
        
        html2canvas(element).then(function (canvas) {
            var canvasWidth = canvas.width;
            var canvasHeight = canvas.height;
            var baseString = Canvas2Image.getImageString(canvas, canvasWidth, canvasHeight, 'png');

            let idImg = e.closest('#designsheet').find('#flexcanvas').attr('id-img');
            let idHtml = e.closest('#designsheet').find('#flexcanvas').attr('id-html');

            e.closest('#designsheet').find('input[key="' + idImg + '"]').val(baseString);
            e.closest('#designsheet').find('input[key="' + idHtml + '"]').val($('<div>').text(element.outerHTML).html());

            e.closest('#designsheet').find('.save-image').attr('enable', true);
        });
    } catch (e) {
        console.log(e);
    }
}

function downloadImage(e) {
    try {
        var element = e.closest('#designsheet').find('#flex-table').get(0);
        html2canvas(element).then(function (canvas) {
            var canvasWidth = canvas.width;
            var canvasHeight = canvas.height;
            var baseString = Canvas2Image.getImageString(canvas, canvasWidth, canvasHeight, 'png');
            let idImg = e.closest('#designsheet').find('#flexcanvas').attr('id-img');
            let idHtml = e.closest('#designsheet').find('#flexcanvas').attr('id-html');

            e.closest('#designsheet').find('input[key="' + idImg + '"]').val(baseString);
            e.closest('#designsheet').find('input[key="' + idHtml + '"]').val($('<div>').text(element.outerHTML).html());

            let type;
            let f;
            Canvas2Image.saveAsImage(canvas, canvasWidth, canvasHeight, type, f);
        });
        
    } catch (e) {
        console.log(e);
    }
}