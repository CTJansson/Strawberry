function closediv() {
    var div = document.getElementById("popup");
    var divClass = div.className;

    if (divClass == "popupcontainer fadein") {
        div.className = "popupcontainer fadeout";
        
        setTimeout(function () {
            div.className = "";
            $("popup").hide();

        }, 800);
    }
}

function opendiv(text) {
    $("#popup-message").text(text); 
    $("#popup").addClass("popupcontainer fadein");
}