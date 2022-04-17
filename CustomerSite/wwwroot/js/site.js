$(document).ready(function () {
    const menu = document.querySelector(".__menu");
    const navOpen = document.querySelector(".__hamburger");
    const navClose = document.querySelector(".__close");

    navOpen.addEventListener("click", () => {
        menu.classList.add("__show");
        document.body.classList.add("__show");
    });

    navClose.addEventListener("click", () => {
        menu.classList.remove("__show");
        document.body.classList.remove("__show");
        $(".__sub-menu").removeAttr("style");
    });

    // Scroll To
    const links = [...document.querySelectorAll(".__scroll-link")];
    links.map((link) => {
        if (!link) return;
        link.addEventListener("click", (e) => {
            e.preventDefault();

            const id = e.target.getAttribute("href").slice(1);

            const element = document.getElementById(id);
            const fixNav = navBar.classList.contains("__fix-nav");
            let position = element.offsetTop - navHeight;

            window.scrollTo({
                top: position,
                left: 0,
            });

            navBar.classList.remove("__show");
            menu.classList.remove("__show");
            document.body.classList.remove("__show");
        });
    });

    $("#hoverable-el").click(function () {
        const submenu = $(".__nav-list .__nav-item ul");
        const isEnable = $(document).width() <= 850;

        if (!isEnable) {
            $(".__sub-menu").removeAttr("style");
            return;
        }
        const isShow = submenu.css("display") == "block";
        submenu.css({
            visibility: isShow ? "hidden" : "visible",
            opacity: isShow ? "0" : "1",
            display: isShow ? "none" : "block",
        });
    });

    $("#sortBy").on("change", function (e) {
        //get selected value
        var optionSelected = $(this).find("option:selected");
        var valueSelected = optionSelected.val();

        redirectUrl = `${location.protocol}//${
            location.host + location.pathname
        }?filter=${valueSelected}`;
        window.location.href = redirectUrl;
    });

    /*      image click     */
    $(".__thumbnails .__thumbnail img").on("click", function () {
        const selectedImage = $(this).attr("src");
        $(".__main img").attr("src", selectedImage);
    });

    const registerErrMsg = $("#error-msg-reg").html();
    if (registerErrMsg != "") {
        $("#_RegForm").css("transform", "translateX(-300px)");
        $("#_LoginForm").css("transform", "translateX(-300px)");
        $("#_Indicator").css("transform", "translateX(110px)");
    }

    $("#signin_span").click(() => {
        $("#_RegForm").css("transform", "translateX(0px)");
        $("#_LoginForm").css("transform", "translateX(0px)");
        $("#_Indicator").css("transform", "translateX(0px)");
    });

    $("#signup_span").click(() => {
        $("#_RegForm").css("transform", "translateX(-300px)");
        $("#_LoginForm").css("transform", "translateX(-300px)");
        $("#_Indicator").css("transform", "translateX(110px)");
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

    // add product to cart
    $("#addToCartForm").submit(function (e) {
        e.preventDefault();
        e.stopImmediatePropagation();

        var form = $(this);
        var url = form.attr("action");
        var data = $(this).find(":input").serialize();

        $.ajax({
            type: "POST",
            headers: {
                "X-CSRF-TOKEN": $('meta[name="csrf-token"]').attr("content"),
            },
            url: url,
            data: data,
            success: function (data) {
                if (data.status == 200) {
                    showToast("success", "Sản phẩm đã được thêm vào giỏ hàng");
                } else {
                    showToast(
                        "fail",
                        "Rất tiếc đã xảy ra lỗi. Xin vui lòng thử lại sau."
                    );
                }
            },
            error: function () {
                showToast(
                    "fail",
                    "Rất tiếc đã xảy ra lỗi. Xin vui lòng thử lại sau."
                );
            },
        });
    });

    $(".__reply-button").click(function () {
        var isShow =
            $(this).closest("li").find("ul form").css("display") == "none"
                ? false
                : true;
        if (isShow) {
            $(this).find("small").html("Trả lời");
            $(this).closest("li").find("ul form").hide();
        } else {
            $(this).find("small").html("Ẩn trả lời");
            $(this).closest("li").find("ul form").show();
        }
    });

    $(".replyCommentSubmit").click(function () {
        var content = $(this).closest(".row").find("div input").val();
        console.log(content);
        if (content) {
            $(this).closest("form").submit();
        } else {
            $(this)
                .closest(".row")
                .find(".replyCommentError")
                .text("Yêu cầu nhập nội dung bình luận");
        }
    });

    /*      Trigger enter event     */
    $(".enter-event").keyup(function (e) {
        if (e.keyCode == 13) {
            $(this).closest(".row").find("div button").click();
        }
    });

    $(".remove-item-from-cart").click(function () {
        const trElement = $(this).closest(".items_tr");
        var id = trElement.attr("data-id");
        var url = $(this).attr("data-href");
        var data = { id };

        $.ajax({
            type: "POST",
            headers: {
                "X-CSRF-TOKEN": $('meta[name="csrf-token"]').attr("content"),
            },
            url,
            data,
            success: function (data) {
                if (data.status == 200) {
                    var newTotalPrice = 0;
                    const isEmpty = $("tbody tr").length == 2;

                    if (isEmpty) {
                        trElement.remove();
                        $("tbody").append(
                            '<tr><td colspan="6" class="text-center border h4">Giỏ hàng rỗng, bạn chưa chọn mua sản phẩm nào.</td></tr>'
                        );
                        $('#cart-submit').prop('disabled', true);
                        $('#cart-submit').addClass('__disabled-btn');
                    } else {
                        const quantity = parseInt(
                            trElement.find("input[type='number']").val()
                        );
                        const unitPrice = parseInt(
                            trElement.find("input[type='number']").attr("data-unit-price")
                        );
                        const preTotal = quantity * unitPrice;

                        const preTotalPrice = parseInt(
                            $("#total-price").attr("data-total")
                        );
                        newTotalPrice = preTotalPrice - preTotal;
                        trElement.remove();
                    }
                    $("#total-price").attr("data-total", newTotalPrice);
                    $("#total-price").text(
                        newTotalPrice.toLocaleString("it-IT", {
                            currency: "VND",
                        }) + "đ"
                    );
                    showToast("success", "Sản phẩm đã được xoá khỏi giỏ hàng");
                } else {
                    showToast(
                        "fail",
                        "Rất tiếc đã xảy ra lỗi. Xin vui lòng thử lại sau."
                    );
                }
            },
            error: function () {
                showToast(
                    "fail",
                    "Rất tiếc đã xảy ra lỗi. Xin vui lòng thử lại sau."
                );
            },
        });
    });

    $('#cart-submit').click(function() {
        const _this = $(this);
        const checkQuantityURL = _this.attr('data-href-check');
        const redirectURL = _this.attr('data-href-redirect');
    
        $.ajax({
            type: "POST",
            headers: {
                "X-CSRF-TOKEN": $('meta[name="csrf-token"]').attr("content"),
            },
            url: checkQuantityURL,
            success: function (data) {
                if (data.isEnough == 1) {
                    window.location.href = redirectURL;
                } else if (data.isEnough == 0) {
                    showToast(
                        "fail", 
                        data.remaining == 0 
                        ? data.product + " hiện tại đã hết hàng"
                        : data.product + " chỉ còn " + data.remaining + ' ' + data.unit,
                        "Không đủ số lượng"
                    );
                }
            },
            error: function() {
                console.log("failed")
            }
        });
    });

    /*          PAYMENT            */
    const currentChecked = $("#payment-method").val();
    if (currentChecked == "COD") {
        $('#CreditCard-info').hide();
    }
    else if (currentChecked == "CreditCard") {
        $('#COD-info').hide();
    }
   
    $("input[name$='paymentType']").click(function () {
        var value = $(this).val();
        $("#info_Method").val(value);
        $('#COD-info').hide();
        $('#CreditCard-info').hide();
        $("#" + value + "-info").show();
    });

    $('#voucher-btn').click(function() {
        const _this = $(this);
        var url = $(this).attr('data-href');
        const voucherCode = $('#voucher-input').val();
        if (!voucherCode) return;
    
        var data = {
            voucher: voucherCode
        }
    
        $.ajax({
            type: "POST",
            headers: {
                "X-CSRF-TOKEN": $('meta[name="csrf-token"]').attr("content"),
            },
            url,
            data,
            success: function (data) {
                if (data.status == 200) {
                    console.log("voucher valid")
    
                    const voucher = data.voucher;
                    $('#voucher-amount').text(
                        voucher.discount.toLocaleString("it-IT", {currency: "VND",}) + "đ"
                    );

                    $('#voucher-amount').attr('data-amount', voucher.discount)

                    $('#voucher-input').attr('readonly', true);
                    _this.text("Đã áp dụng")
                    _this.addClass("bg-success");
                    _this.attr('disabled', true);
                    _this.css("cursor", "not-allowed")
                    
                    updateTotalPayAmount();

                    showToast(
                        "success", 
                        "Quý khách đã được giảm " + voucher.discount.toLocaleString("it-IT", {currency: "VND",}) + "đ"
                    );
                } else {
                    showToast(
                        "fail", 
                        "Voucher không hợp lệ hoặc đã hết hạn"
                    );
                }
            },
            error: function() {
                console.log("failed")
            }
        });
    })

    $('#voucher-input').keypress(function (e) {
        if (e.which == '13') {
            e.preventDefault();
            $('#voucher-btn').click()
        }
    });

    $('#payment-btn').click(function(e) {
        e.preventDefault();
        $('#payment-loading-btn').show();
        $(this).hide();

        $(this).closest('form').submit();
    });

    /*          BILLS            */
    $('#bill-table tr').click(function () {
        const _this = $(this);
        const href = _this.attr('data-href');

        if (href) {
            window.location.href = href;
        }
    });
});

function updateTotalPayAmount() {
    let totalPrice = parseInt($('#total-price-amount').attr('data-amount'))
    let totalDiscount = parseInt($('#total-discount-amount').attr('data-amount'))
    let totalVoucher = parseInt($('#voucher-amount').attr('data-amount'))
    let totalShipping = parseInt($('#shipping-cost-amount').attr('data-amount'))
    let newTotalPay = totalPrice + totalShipping - totalDiscount - totalVoucher;

    if (newTotalPay < 0) {newTotalPay = 0;}
    
    const _el = $('#total-pay-amount')
    _el.attr('data-amount', newTotalPay)
    _el.text(newTotalPay.toLocaleString("it-IT", {currency: "VND",}) + "đ")
}

gsap.from(".__logo", { opacity: 0, duration: 1, delay: 0.6, x: -20 });
gsap.from(".__hamburger", { opacity: 0, duration: 1, delay: 0.6, x: 20 });
gsap.from(".__cart-icon", { opacity: 0, duration: 1, delay: 0.6, y: -10 });
gsap.from(".__logout-icon", { opacity: 0, duration: 1, delay: 0.6, y: -10 });
gsap.from(".__hero-img", { opacity: 0, duration: 1, delay: 1.5, x: -200 });
gsap.from(".__hero-content h2", { opacity: 0, duration: 1, delay: 2, y: -50 });
gsap.from(".__hero-content h1", {
    opacity: 0,
    duration: 1,
    delay: 2.5,
    y: -45,
});
gsap.from(".__hero-content a", { opacity: 0, duration: 1, delay: 3.5, y: 50 });
