$(document).ready(function () {
    /*       RATING-STAR    */
    /* 1. Visualizing things on Hover - See next part for action on click */
    $('#stars li').on('mouseover', function () {
      var onStar = parseInt($(this).data('value'), 10); // The star currently mouse on

        // Now highlight all the stars that's not after the current hovered star
        $(this).parent().children('li.__star').each(function (e) {
            if (e < onStar) {
                $(this).addClass('__hover');
            }
            else {
                $(this).removeClass('__hover');
            }
        });
    }).on('mouseout', function () {
        $(this).parent().children('li.__star').each(function (e) {
            $(this).removeClass('__hover');
        });
    });

    var ratingValue = 0;
    /* 2. Action to perform on click */
    $('#stars li').on('click', function () {
        var onStar = parseInt($(this).data('value'), 10); // The star currently selected
        var stars = $(this).parent().children('li.__star');

        for (i = 0; i < stars.length; i++) {
            $(stars[i]).removeClass('__selected');
        }

        for (i = 0; i < onStar; i++) {
            $(stars[i]).addClass('__selected');
        }

        // JUST RESPONSE (Not needed)
        ratingValue = parseInt($('#stars li.__selected').last().data('value'), 10);
    });

    //Show rating modal
    $('#rating-product').on('click', function () {
        $('.__popup-rating').show();
    });

    $('._submit-rating').click(function (e) {
        e.stopImmediatePropagation();
        $('.__popup-rating').hide();

        const productId = parseInt($('.__popup-rating').attr('data-product'));
        const href = $('.__popup-rating').attr('data-href');
        const url = `${window.location.protocol}//${window.location.host}/${href}`;

        if (ratingValue == 0) {
            showToast("fail", "Cần chọn số sao đánh giá.")
            return;
        }

        var formData = {
            productId,
            stars: ratingValue
        };

        $.ajax({
            type: "POST",
            url: url,
            data: formData,
            success: function () {
                showToast("success", "Đã lưu đánh giá.");

                //reset rating stars
                var stars = $("#stars").children('li.__star');
                for (i = 0; i < stars.length; i++) {
                    $(stars[i]).removeClass('__selected');
                    $(stars[i]).removeClass('__hover');
                }
            },
            error: function() {
                showToast("fail", "Rất tiếc đã xảy ra lỗi. Xin vui lòng thử lại sau.");
            }
        });
    });

    $('.__popup-rating-close').click(function () {
        $('.__popup-rating').hide();
    });

    $('.__popup-rated-close').click(function () {
        $('.__popup-rated').hide();
    });

    let x;
    function showToast(mess, toastMess, toastTitle = null) {
        clearTimeout(x);

        if (mess == "success") {
            $("#toast").removeClass("fail");
        } else {
            $("#toast").removeClass("success");
        }

        $("#toast").addClass(mess);
        $("#toast").css("transform", "translateX(0px)");
        x = setTimeout(() => {
            $("#toast").css("transform", "translateX(400px)");
        }, 7000);

        if (mess == "success") {
            $(".toast-sta").text("Thành công");
            $(".toast-msg").text(toastMess);
        } else {
            $(".toast-sta").text("Thất bại");
            $(".toast-msg").text(toastMess);
        }

        if (toastTitle) {
            $(".toast-sta").text(toastTitle);
        }
        window.scrollTo({ top: 0, behavior: "smooth" });
    }

    $("#close").on("click", () => {
        $("#toast").css("transform", "translateX(400px)");
    });
});