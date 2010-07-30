
$(function() {
    $('.fancy').fancybox({ titleShow: false, scrolling: 'no' });
    $('.jsClickable').clickable();
    setTimeout(function() {
        $('.autohide').slideUp();
    }, 3000);
});