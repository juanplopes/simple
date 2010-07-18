
$(function() {
    $('.fancy').fancybox({ titleShow: false });
    setTimeout(function() {
        $('.autohide').slideUp();
    }, 3000);
});