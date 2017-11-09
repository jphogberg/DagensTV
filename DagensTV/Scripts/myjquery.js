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

/* Testar local storage*/
$(document).ready(function () {

    $("#save-local").click(function (e) {
        e.preventDefault();

        localStorage.clear();

        var listContents = [];
        $("#myList").each(function () {
            listContents.push(this.innerHTML);
        })
        localStorage.setItem('myChannelList', JSON.stringify(listContents));
    });

    //$("#clearAll").click(function (e) {
    //    e.preventDefault();
    //    localStorage.clear();
    //    location.reload();
    //});

    loadMyChannelList();

    function loadToDo() {
        if (localStorage.getItem('todoList')) {
            var listContents = JSON.parse(localStorage.getItem('todoList'));
            $("ul").each(function (i) {
                this.innerHTML = listContents[i];
            })
        }
    }
});
/*Testar om det finns något sparat*/
if (!localStorage.getItem('bgcolor')) {
    populateStorage();
} else {
    Storage.clear();
    populateStorage();
}