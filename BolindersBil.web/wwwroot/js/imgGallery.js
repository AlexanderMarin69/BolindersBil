$(function () {

    /* GLOBAL VARIABLES */

    var carouselImgElem = $(".carousel-item");
    //carouselImgElem.first().addClass("active");
    var thumbnailImg = $(".thumbnails").find("img");

    thumbnailImg.on("click", function () {
        var yyy = $(this).parent().index();
        console.log(yyy);
        $(".carousel-item").eq(yyy).addClass("active");
        //carouselImgElem.index(yyy).addClass("active");
        //var index = $(this).parent().index();

        //var test = $('.thumbnails img').eq(index);
    });

    $('#exampleModal').on('hidden.bs.modal', function (e) {
        // do something...
        carouselImgElem.removeClass("active");
        //carouselImgElem.first().addClass("active");

        console.log("close modal");
    })
    $('#exampleModal').on('show.bs.modal', function (e) {
        // do something...

        console.log(e);
    })
});