$(function () {
    var sec = 5;

    setInterval(function () {
        if (sec > 0) {
            sec = sec - 1;
            if (sec == 0) {
                $(".modal").css({
                    "visibility": "visible",
                    "transform": "scale(1)",
                    "opacity": 1
                })
            }
        }
    }, 1000);

    $(".close").click(function () {
        $(".modal").css({
            "visibility": "hidden",
            "opacity": 0
        })
    })
})