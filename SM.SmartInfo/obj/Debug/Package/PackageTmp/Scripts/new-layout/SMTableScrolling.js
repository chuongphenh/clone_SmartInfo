$(function () {
    $('.table-scrolling').each(function () {

        //issue: issue với jquery 1.x (tốt trên jquery 3.x)

        //xac dinh class chinh cua table (TH sử dụng reporting)
        var defaultClass = 'grid-main';
        var defaultHeaderClass = 'grid-header';
        if ($(this).hasClass('flex_grid-main'))
            defaultClass = 'flex_grid-main';

        // feature1: config số lượng cột cố định bởi css class (cancel)
        //var keepColumnNumber = 1;
        //var arrClasses = $(this).attr('class').split(' ');
        //for (var index in arrClasses) {
        //    if (arrClasses[index].match(/^table-scrolling-fixed-\d+$/)) {
        //        var keepNumber = parseInt(arrClasses[index].replace(/table-scrolling-fixed-/g, ''));
        //        if (!isNaN(keepNumber)) keepColumnNumber = keepNumber;
        //        break;
        //    }
        //}

        // feature2: config số lượng cột cố định bởi css class của từng cột (final)
        //class css config: fixed-item

        // feature3: thêm checkedbox vào grid (final)
        //class checkbox: .chkCheckbox

        //Chuyen DataGrid sang table chua thead va tbody
        $(this).prepend(document.createElement('thead'));
        $(this).find('thead').append($(this).find('tbody tr:eq(0)'));

        if ($(this).find('thead').length > 0 && ($(this).hasClass('table-scrolling-row') || $(this).hasClass('table-scrolling-col'))) {
            // Clone <thead>
            var $w = $(window),
				$t = $(this),
				$thead = $t.find('thead').clone(),
				$col = $t.find('thead, tbody').clone();

            // Tạo khung chứa table cuộn
            $t
			.addClass('scrolling-enabled')
			.css({
			    margin: 0,
			    width: '100%'
			}).wrap('<div class="scrolling-wrap" />');

            //Active cuộn dọc
            if ($t.hasClass('table-scrolling-row')) $t.removeClass('table-scrolling-row').parent().addClass('table-scrolling-row');
            //Active cuộn ngang
            if ($t.hasClass('table-scrolling-col')) $t.removeClass('table-scrolling-col').parent().addClass('table-scrolling-col');

            //Tạo header cuộn dọc
            if ($t.parent().hasClass('table-scrolling-row')) {
                $t.after('<table class="scrolling-thead" />');
            }

            //Tạo header cuộn ngang
            if ($t.parent().hasClass('table-scrolling-col')) {
                $t.after('<table class="scrolling-col" /><table class="scrolling-intersect" />');
            }

            // Sao chép header cuộn từ table gốc
            var $scrollingHead = $(this).siblings('.scrolling-thead'),
				$scrollingCol = $(this).siblings('.scrolling-col'),
				$scrollingInsct = $(this).siblings('.scrolling-intersect'),
				$scrollingWrap = $(this).parent('.scrolling-wrap');

            //Sao chép header cuộn dọc
            $scrollingHead.append($thead).addClass(defaultClass);

            //Sao chép header cuộn ngang
            $scrollingCol
			    .append($col)
                .addClass(defaultClass)
				.find('thead td:not(.fixed-item)').remove()
				.end()
                .find('tbody td:not(.fixed-item)').remove();

            //Sao chép cell góc trái	
            $scrollingInsct
                .addClass(defaultClass)
                .html($scrollingCol.find('thead')[0].innerHTML);

            // Set width cho header giả
            var setWidths = function () {
                $t
                .find('thead td').each(function (i) {
                    $scrollingHead.find('td').eq(i).width($(this).width());
                    //var w = (this).getBoundingClientRect();
                    //if (w.width) {
                    //    $scrollingHead.find('td').eq(i).width(w.width);
                    //} else {
                    //    $scrollingHead.find('td').eq(i).width(w.right - w.left);
                    //}
                })
                .end()
                .find('tr').each(function (i) {
                    $scrollingCol.find('tr').eq(i).height($(this).height());
                    //var h = (this).getBoundingClientRect();
                    //if (h.height) {
                    //    $scrollingCol.find('tr').eq(i).height(h.height);
                    //} else {
                    //    $scrollingCol.find('tr').eq(i).height(h.top - h.bottom);
                    //}
                });

                // set width header cuộn dọc
                $scrollingHead.width($t.find('thead').width());

                // set width header cuộn ngang
                $scrollingInsct.find('tr.' + defaultHeaderClass + ':first-child td').each(function (i) {
                    var cell = $scrollingCol.find('thead td').eq(i);
                    $(this).width(cell.width());
                    $(this).height(cell.height());
                    //var w = cell.getBoundingClientRect();
                    //if (w.width) {
                    //    $(this).width(w.width);
                    //} else {
                    //    $(this).width(w.right - w.left);
                    //}
                });
            },
            //Set position khi cuộn dọc
				repositionscrollingHead = function () {
				    var allowance = calcAllowance();
				    if ($t.height() > $scrollingWrap.height()) {
				        if ($scrollingWrap.scrollTop() > 0) {
				            $scrollingHead.add($scrollingInsct).css({
				                opacity: 1,
				                top: $scrollingWrap.scrollTop()
				            });
				        } else {
				            $scrollingHead.add($scrollingInsct).css({
				                opacity: 0,
				                top: 0
				            });
				        }
				    } else {
				        if ($w.scrollTop() > $t.offset().top && $w.scrollTop() < $t.offset().top + $t.outerHeight() - allowance) {
				            $scrollingHead.add($scrollingInsct).css({
				                opacity: 1,
				                top: $w.scrollTop() - $t.offset().top
				            });
				        } else {
				            $scrollingHead.add($scrollingInsct).css({
				                opacity: 0,
				                top: 0
				            });
				        }
				    }
				},
				//Set position khi Cuon ngang
				repositionscrollingCol = function () {
				    if ($scrollingWrap.scrollLeft() > 0) {
				        $scrollingCol.add($scrollingInsct).css({
				            opacity: 1,
				            left: $scrollingWrap.scrollLeft()
				        });
				    } else {
				        $scrollingCol
						.css({ opacity: 0 })
						.add($scrollingInsct).css({ left: 0 });
				    }
				},
				calcAllowance = function () {
				    var a = 0;
				    $t.find('tbody tr:lt(3)').each(function () {
				        a += $(this).height();
				    });

				    if (a > $w.height() * 0.25) {
				        a = $w.height() * 0.25;
				    }

				    a += $scrollingHead.height();
				    return a;
				};

            setWidths();

            // insert checkbox function nếu có
            $scrollingCol.find('tbody td span.chkCheckbox input[type="checkbox"]').each(function (i) {
                $(this).attr('onclick', 'CheckedScrollingTable(this);');
            });

            $t.parent().scroll($.throttle(250, function () {
                repositionscrollingHead();
                repositionscrollingCol();
                setWidths();//todo: Có thể không cần
            }));

            $w
			.load(setWidths)
			.resize($.debounce(250, function () {
			    repositionscrollingHead();
			    repositionscrollingCol();
			    setWidths();
			}))
			.scroll($.throttle(250, repositionscrollingHead));
        }
    });
});

// checkbox cho grid
function CheckedScrollingTable(e) {
    var $t = $(e).closest('div.scrolling-wrap');
    $t.find('.scrolling-col tbody td span.chkCheckbox input[type="checkbox"]').each(function (i) {
        $t.find('.table-scrolling tbody td span.chkCheckbox input[type="checkbox"]')[i].checked = $(this).is(":checked");;
    });
};