$(function () {
    $("#album-list img").on("mouseover", function () {
        $(this).animate({ height: '+=25', width: '+=25' });
    }).on("mouseout", function () {
        $(this).animate({ height: '-=25', width: '-=25' });
    });



   // alert('doc is ready');

});