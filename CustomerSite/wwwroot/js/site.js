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
});

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
