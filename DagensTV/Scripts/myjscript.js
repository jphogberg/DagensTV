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