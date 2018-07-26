if ($(window).width() < 960) {
    $("#resHeader").show();
    $("#mainContainer").hide();
    $("#navResponsive").hide();
    $("#body").addClass(".responsiveBody")
}
else {
    $("#resHeader").hide();
}
var menuFlag = 0;
$("#navBtn").click(function () {
    if (menuFlag == 1) {
        $("#navResponsive").hide();
        menuFlag = 0;
    }
    else if (menuFlag == 0) {
        $("#navResponsive").show();
        menuFlag = 1;
    }
});