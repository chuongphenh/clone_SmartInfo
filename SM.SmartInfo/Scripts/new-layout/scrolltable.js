//TABLE muốn fixed header thêm class 'table-scrolling'
//Header muốn fixed thêm class 'table-scrolling-header'
//option: Paging thêm class 'table-scrolling-footer'

var listTable = [];
var marginTop = 80;//top?? tùy theo project chọn cách xác định

window.onload = function () {
    if ($('.table-scrolling').length) {
        configTable();
        window.addEventListener("resize", function () { resizeWindow(); }, false);
        window.addEventListener("scroll", function () { showhideTable(); }, false);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(configTable);
    }
};

var configTable = function () {
    listTable = [];

    $('.table-scrolling').each(function () {
        console.log($(this).find('.table-scrolling-footer').height());
        listTable.push({
            top: $(this).offset().top - marginTop,
            bottom: $(this).offset().top - marginTop + $(this).height() - $(this).find('.table-scrolling-footer').height(),
            margintop: marginTop
        });

        //Thêm thead với table dạng không có thead -> đồng bộ style
        if (!$(this).find('thead').lenght) {
            $(this).prepend(document.createElement('thead'));
            var $thread = $(this).find('thead')
                    .append($(this).find('tbody tr.table-scrolling-header'));
        }

        //Tạo thead giả + tách riêng từng khối table
        $(this).wrap('<div class="scrolling-wrap" />')
               .before('<table class="' + this.className.replace('table-scrolling', '') + ' scrolling-thead" />');
        var $scrollingHead = $(this).siblings('.scrolling-thead')
            .append($(this).find('thead').clone());

        //Set width cho header giả
        $thread.find('td').each(function (i) {
            var w = $(this).outerWidth();
            $scrollingHead.find('td').eq(i)
                .attr('style', 'width: ' + w + 'px !important');            
        });

        //Ẩn header giả
        $scrollingHead.attr('style', 'display: none');
    });
};

var resizeWindow = function () {
    listTable = [];

    $('.table-scrolling').each(function () {
        listTable.push({
            top: $(this).offset().top - marginTop,
            bottom: $(this).offset().top - marginTop + $(this).height() - $(this).find('.table-scrolling-footer').height(),
            margintop: marginTop
        });

        // Set lại width cho thead giả khi resize
        var $scrollingHead = $(this).siblings('.scrolling-thead');
        $(this).find('thead').find('td').each(function (i) {
            var w = $(this).outerWidth();
            $scrollingHead.find('td').eq(i)
                .attr('style', 'width: ' + w + 'px !important');
        });
        $scrollingHead.attr('style', 'width: ' + $(this).outerWidth() + 'px !important');
    });

    showhideTable();
}

var showhideTable = function () {
    setTimeout(function () {
        if (listTable == undefined || listTable.length == 0)
            return;

        for (var i = 0; i < listTable.length ; i++) {
            if ($(window).scrollTop() > listTable[i].top &&
                $(window).scrollTop() < listTable[i].bottom) {
                $('.scrolling-thead').eq(i).css({
                    'display': 'block',
                    'position': 'fixed',
                    'top': listTable[i].margintop + 'px'
                });
            } else {
                $('.scrolling-thead').eq(i).css({
                    'display': 'none'
                });
            }
        }
    }, 200);
}

//// VERSION 0
////table muốn fixed header thêm class 'table-scrolling'
////Header muốn fixed thêm class 'table-scrolling-header'
//window.onload = function () {
//    if ($('.table-scrolling').length) {
//        configTable();
//        window.addEventListener("resize", function () { resizeWindow(); }, true);
//        window.addEventListener("scroll", function () {
//            $('.table-scrolling').each(function () {
//                showhideTable(this);
//            });
//        }, true);
//        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(configTable);
//    }
//};

//var resizeWindow = function () {
//    $('.table-scrolling').each(function () {
//        // Set lại width cho thead giả khi resize
//        var $scrollingHead = $(this).siblings('.scrolling-thead');
//        $(this).find('thead').find('td').each(function (i) {
//            var w = $(this).outerWidth();
//            $scrollingHead.find('td').eq(i)
//                .attr('style', 'width: ' + w + 'px !important');
//        });
//        $scrollingHead.attr('style', 'width: ' + $(this).outerWidth() + 'px !important');

//        showhideTable(this);
//    });
//}

//var configTable = function () {
//    $('.table-scrolling').each(function () {

//        //Thêm thead với table dạng không có thead -> đồng bộ style
//        if (!$(this).find('thead').lenght) {
//            $(this).prepend(document.createElement('thead'));
//            var $thread = $(this).find('thead')
//                    .append($(this).find('tbody tr.table-scrolling-header'));
//        }

//        //Tạo thead giả + tách riêng từng khối table
//        $(this).wrap('<div class="scrolling-wrap" />')
//               .before('<table class="' + this.className.replace('table-scrolling', '') + ' scrolling-thead" />');
//        var $scrollingHead = $(this).siblings('.scrolling-thead')
//            .append($(this).find('thead').clone());

//        //Set width cho header giả
//        $thread.find('td').each(function (i) {
//            var w = $(this).outerWidth();
//            $scrollingHead.find('td').eq(i)
//                .attr('style', 'width: ' + w + 'px !important');
//        });

//        //Ẩn header giả
//        $scrollingHead.attr('style', 'display: none');
//    });
//};

//var showhideTable = function (table) {
//    var $thead = $(table).siblings('.scrolling-thead');
//    var tblTop = $(table).offset().top;
//    var tblHeight = $(table).height();
//    var ftHeight = $(table).find('.grid-pager').height();

//    //Xác định vị trí header dừng (xác định theo class) [position() lấy vị trí absolute, offset() vị trí đã gồm padding, margin...]
//    //MB
//    //var $wrap = $(table).closest('.fixed-wrap');
//    //var marginTop = $wrap.position().top + $wrap.siblings('.toolbar').height();
//    var marginTop = 80; //todo: tạm fix test

//    if (
//        $(window).scrollTop() > tblTop - marginTop &&
//        $(window).scrollTop() < tblTop - marginTop + tblHeight - ftHeight) {
//        $thead.css({
//            'display': 'block',
//            'position': 'fixed',
//            'top': marginTop + 'px'
//        });
//    } else {
//        $thead.css({
//            'display': 'none'
//        });
//    }
//}