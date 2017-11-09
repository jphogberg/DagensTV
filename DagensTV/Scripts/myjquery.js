/* Toggle för nav */
$(document).ready(function () {
    $('a.icon').click(function () {
        $("#popular-wrapper").toggleClass("open");
    });
});

/* Visa valt program i modal från tablån */
var ShowInfo = function (id) {

    $.ajax({

        type: "GET",
        url: "/Home/ShowInfo",
        data: { Id: id },
        success: function (response) {
            $("#overlay-body").html(response);
            $("#tv-overlay").modal("show");
        }
    });
};

/* Filtera tablån efter vald kategori */
$(".category-filter").click(function () {
    var filter = $(this).attr('title');
    var date = $(".start-time").data("date");    
    $.ajax({

        type: "GET",
        url: "/Home/FilterSchedule",
        data: {
            Filter: filter,
            date: date
        },
        contentType: "html",
        success: function (response) {
            $("#schedules").html(response);
        }
    });
});

/* Stäng overlay med escape */
$(document).bind('keydown', function (e) {
    if (e.which === 27) {
        hideOverlay();
    }
});

/* Göm placeholder vid focus */
/* Utkommenterat då vi inte använder sökfunktionen */
//$(function () {
//    $('.searchbox').data('holder', $('.searchbox').attr('placeholder'));

//    $('.searchbox').focusin(function () {
//        $(this).attr('placeholder', '');
//    });
//    $('.searchbox').focusout(function () {
//        $(this).attr('placeholder', $(this).data('holder'));
//    });
//});

/* Visa meddelande om programmet */
/* Utkommenterat då vi inte använder sökfunktionen */
//$('.searchbox').keypress(function (e) {    
//    if (e.keyCode === 13) {
//        showSearchResult();
//        e.preventDefault();
//    }    
//});

/* Ändra placeholder i sökrutan för responsiva designen */
/* Utkommenterat då vi inte använder sökfunktionen */
//$(function () {
//    $(window).on("resize", function (e) {
//        var w = e.target.innerWidth;
//        $("input[type=search]").attr("placeholder", function () {
//            return (w > 1 && w < 1279) ? "Sök" : "";
//        });
//    }).resize();
//});

/* Toggla dropdown menyerna i SubNav för responsiva designen */
$('.category-filter').click(function () {
    var windowsize = $(window).width();
    if (windowsize < 1280) {
        $(".nav-second").toggle();
    }
});

$('.calendar-filter').click(function () {
    var windowsize = $(window).width();
    if (windowsize < 1280) {
        $("#nav-second-calendar").toggle();
    }
});

$('.show-genre').click(function () {
    $("#nav-second").toggle();

});
$('.show-calendar').click(function () {
    $("#nav-second-calendar").toggle();
});

/* Progressbar */
/* Ej fungerande som den skall */
var now, nowMS, startStr, startMS, endStr, endMS, percentage, pbarsAmount, channelsAmount, percentage;
elements = $(".channel-class .active-programme #progressbar");
elements.each(function (i, obj) {

    pbarsAmount = document.getElementsByClassName("active-programme");
    channelsAmount = document.getElementsByClassName("channel-class");

    now = new Date();
    nowMS = now.getTime();
    startStr = $(this).data('start');
    startMS = Date.parse(startStr);
    endStr = $(this).data('end');
    endMS = Date.parse(endStr);

    percentage = Math.round(((nowMS - startMS) / (endMS - startMS)) * 100);

    console.log(startStr)
    $(".active-programme [class*=track-time]").progressbar({
        value: parseInt(percentage)
    })

})

///* Local storage*/
//$('#save-local').on('click', function () {
//    var chb, mySettings = [];
//    $("[id^=chbx]").each(function () {
//        chb = { id: $(this).attr('id'), value: $(this).prop('checked') };
//        mySettings.push(chb);
//    });
//    localStorage.setItem("mySettings", JSON.stringify(mySettings));
//});

//$(document).ready(function () {
//    var chbox = JSON.parse(localStorage.getItem('mySettings'));
//    if (!chbox.length) { return };
//    console.debug(chbox);

//    for (var i = 0; i < chbox.length; i++) {
//        console.debug(chbox[i].value == 'on');
//        $('#' + chbox[i].id).prop('checked', chbox[i].value);
//    }
//});