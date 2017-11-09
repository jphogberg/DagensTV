/* Funktion för att toggla menyn i responsivt läge */
function toggleMenu(x) {
    x.classList.toggle("change");
    x = document.getElementById("nav-menu");
    if (x.className === "menu") {
        x.className += " responsive";
    } else {
        x.className = "menu";
    }
}

/* Visa & göm Overlay */
var modal = document.getElementById('search-overlay');

function showOverlay(show) {
    modal.display = document.getElementById("overlay-body").innerHTML = show;
    modal.style.display = "block";
}

function hideOverlay() {
    modal.style.display = "none";
}

window.onclick = function (event) {
    if (event.target === modal) {
        modal.style.display = "none";
    }
};

/* Progressbar */

//$("[class*=track-time]").get(function (index, val) {

//    var arrValues = [ "76", "83" ];
//    var startStr = $(val).data('start');
//    var endStr = $(val).data('end');

//    now = moment();
//    var startTime = moment(startStr);
//    var endTime = moment(endStr);
//    var percentage = (now - startTime) / (endTime - startTime) * 100;

//    arrValues.push(percentage);        

//    console.log(arrValues)

//    $("#progressbar").each(function () {
//        $(".active-programme #progressbar").progressbar({
//  .active-programme [class*=track-time]          value: arrValues[0]
//    })

//    });
//})

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

    //var perStr = percentage.toString();




    console.log(startStr)
    $(".active-programme [class*=track-time]").progressbar({
        value: parseInt(percentage)
    })

})

//for (var k = 0; k < channelsAmount.length; k++) {    

//    for (var j = 0; j < pbarsAmount.length; j++) {        
//        $(".active-programme [class*=track-time]").progressbar({
//            value: parseInt(percentage)
//        })
//    }

//}





    //$("#progressbar").each(function () {
    //    $(".active-programme #progressbar").progressbar({
    //        value: 63
    //    })
    //})

