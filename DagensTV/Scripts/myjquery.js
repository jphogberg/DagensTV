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
    $.ajax({

        type: "GET",
        url: "/Home/FilterSchedule?Filter=" + filter,
        contentType: "html",
        success: function (response) {
            $("#schedules").html(response);
        }
    });
});


/* Göm placeholder vid focus */
$(function () {
    $('.searchbox').data('holder', $('.searchbox').attr('placeholder'));

    $('.searchbox').focusin(function () {
        $(this).attr('placeholder', '');
    });
    $('.searchbox').focusout(function () {
        $(this).attr('placeholder', $(this).data('holder'));
    });
});

/* Stäng overlay med escape */
$(document).bind('keydown', function (e) {
    if (e.which === 27) {
        hideOverlay();
    }
});

/* Visa meddelande om programmet */
$('.searchbox').keypress(function (e) {    
    if (e.keyCode === 13) {
        showSearchResult();
        e.preventDefault();
    }    
});

/* Ändra placeholder i sökrutan för responsiva designen */
$(function () {
    $(window).on("resize", function (e) {
        var w = e.target.innerWidth;
        $("input[type=search]").attr("placeholder", function () {
            return (w > 1 && w < 1279) ? "Sök" : "";
        });
    }).resize();
});

/* Toggla dropdown menyerna i SubNav för responsiva designen */
$('.category-filter').click(function () {
    var windowsize = $(window).width();
    if (windowsize < 1280) {
        $(".nav-second").toggle();
    }    
})

$('.calendar-filter').click(function () {
    var windowsize = $(window).width();
    if (windowsize < 1280) {
        $("#nav-second-calendar").toggle();
    }    
})

$('.show-genre').click(function () {
    $("#nav-second").toggle();
    
})
$('.show-calendar').click(function () {
    $("#nav-second-calendar").toggle();
})

/* Save checkbox values */
var checkboxValues = function() {
    var arrChannels = [];
    var strChannels = "";
    debugger
    $("#ItemList li input[type=checkbox]").each(function (index, val) {
        var id = $(val).attr("Id");

        arrChannels.push(id);
    })

    if (arrChannels.length != 0) {

        strChannels = arrChannels.toString();

        $.ajax({
            url: "/User/SaveMyChannels",
            type: "POST",
            data: { ItemList: strChannels },
            success: function (response) {

            }

        })
    }



   

};