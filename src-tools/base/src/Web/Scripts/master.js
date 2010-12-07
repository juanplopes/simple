$(function() {
    $('.fancy').fancybox({ titleShow: false, ajax: { cache: false} });

    $(document).ajaxStart(function() { $.fancybox.showActivity(); });
    $(document).ajaxSuccess(function() {
        $.fancybox.hideActivity();
        $('.fancy').fancybox({ titleShow: false, ajax: { cache: false} });
    });

    setTimeout(function() {
        $('.autohide').slideUp();
    }, 3000);
});