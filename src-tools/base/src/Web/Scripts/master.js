
$(function() {
    $('.fancy').fancybox({ titleShow: false, scrolling: 'no' });
    setTimeout(function() {
        $('.autohide').slideUp();
    }, 3000);
});