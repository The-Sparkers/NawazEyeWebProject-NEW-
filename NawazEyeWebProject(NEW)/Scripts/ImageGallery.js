/* Photography by Hilary Spencer Creative Photography */
/* ************************************************** */

/* https://www.facebook.com/pages/Hilary-Spencer-Creative-Photography/107123415116 */

// Hovering an image gives a smooth enlarge effect
// Clicking an image will enlarge it even more, and add an overlay
// Clicking the overlay will exit the overlay
// You can also hit "esc" to exit the full view

// Added an on click event to make it more like a production environment, though the primary function is the hover effect
// When the page has loaded
$(function () {
    // View the selected photo at full size
    $(".photo-image").click(function () {
        $(this).addClass("photo-selected");
        $(this).parent().addClass("photo-x");
        $("#overlay").show();
    });

    // Close the full size view when #overlay is clicked
    $("#overlay").click(function () {
        $(".photo-image").removeClass("photo-selected");
        $(".photo-x").removeClass("photo-x");
        $("#overlay").hide();
    });
});

// Close full size view if "esc"
$(document).keyup(function (e) {
    if (e.keyCode == 27) {
        $(".photo-image").removeClass("photo-selected");
        $("#overlay").hide();
    }
});