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
            return (w > 1 && w < 1149) ? "Sök" : "";
        });
    }).resize();
});

/* Validera kontaktformuläret */
var emailPattern = /^[a-zA-Z0-9.!#$%&’*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;

$('#submit').on('click', function() {
    var valid = true,
        message = '';
       
    $('#message-form input').each(function() {
        var $this = $(this),
            inputName = $this.attr("name");
        if(!$this.val()) {
            valid = false;
            message += 'Vänligen ange ' + inputName + '\n';
        }
        
        if(inputName === "epost" && $this.val() !== ""){
            if(!emailPattern.test($this.val())){
                message += "Vänligen ange en korrekt e-postadress.\n";                
                valid  = false;
            }
        }
    });

    $('#message-form textarea').each(function() {
        var comment = $('#textarea').val();
        if (comment === '') {            
            message += "Vänligen lämna ett meddelande.\n";
            valid = false;
        }
    });
    
    if(!valid) {
        alert(message);
    }
    else {
        alert("Tack för att du kontaktat oss.");
    }
});