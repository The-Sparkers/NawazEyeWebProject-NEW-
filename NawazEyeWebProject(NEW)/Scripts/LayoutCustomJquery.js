$(document).ready(function (e) {
    //login Btn
    $('#loginBtn').hover(function (e) {
        $('#loginBtn #loginBtnItem').show();
    });
    //fixed header hide
    $('.fixedHeader').hide();
    //Header Scroll
    $(document).scroll(function (e) {
        if ($(window).scrollTop() > 800) {
            $('.fixedHeader').show();
            $('.fixedHeader').addClass('w3-flat-clouds');
        } else {
            $('.fixedHeader').hide();
            $('.fixedHeader').removeClass('w3-flat-clouds');
        }
    });
    //Carousel
    var myIndex = 0;
    carousel();
    function carousel() {
        var i;
        var x = document.getElementsByClassName("mySlides");
        for (i = 0; i < x.length; i++) {
            x[i].style.display = "none";
        }
        myIndex++;
        if (myIndex > x.length) { myIndex = 1 }
        x[myIndex - 1].style.display = "block";
        setTimeout(carousel, 9000);
    }
});