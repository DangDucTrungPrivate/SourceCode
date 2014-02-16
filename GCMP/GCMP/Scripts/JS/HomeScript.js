$(function () {
    // Document.ready -> link up remove event handler
    $(".iconshow").click(function () {
        // Get the id from the link
        var lor = $(this).attr("data-nav");
        if (lor == "left") {
            var classname = $("#news").attr('class');
            if (classname == "sidebar left") {
                $("#news").attr('class', 'sidebar left invis');
            } else {
                $("#news").attr('class', 'sidebar left');
            }
        } else {
            var classname = $("#feeds").attr('class');
            if (classname == "sidebar right") {
                $("#feeds").attr('class', 'sidebar right invis');
            } else {
                $("#feeds").attr('class', 'sidebar right');
            }
        }

    });
});

function login() {
    var username = document.getElementById("usernameLayout").value;
    var password = document.getElementById("passwordLayout").value;
    var rememberme = true;
    var url = document.URL;
    $.ajax({
        type: "POST",
        async: false,
        url: "/Account/Login",
        data: { "username": username, "password": password },
        success: function (data) {

            data;
            if (data != "Email đăng nhập hoặc mật khẩu không chính xác.") {
                window.location.reload(false);
            } else {
                toastr.error('Email đăng nhập hoặc mật khẩu không chính xác.', 'Thông báo')

            }
        }
    });
}

function logoffajax() {
    var username = "@User.Identity.Name"
    $.ajax({
        type: "POST",
        async: false,
        url: "/PAccount/ALogOff",
        data: { "username": username },
        success: function (data) {
            data;
            window.location.reload(false);
        }
    });
}


function echeck(str) {

    var at = "@";
    var dot = ".";
    var lat = str.indexOf(at);
    var lstr = str.length;
    var ldot = str.indexOf(dot);
    if (str.indexOf(at) == -1) {
        return false;
    }

    if (str.indexOf(at) == -1 || str.indexOf(at) == 0 || str.indexOf(at) == lstr) {

        return false;
    }

    if (str.indexOf(dot) == -1 || str.indexOf(dot) == 0 || str.indexOf(dot) == lstr) {

        return false;
    }

    if (str.indexOf(at, (lat + 1)) != -1) {

        return false;
    }

    if (str.substring(lat - 1, lat) == dot || str.substring(lat + 1, lat + 2) == dot) {

        return false;
    }

    if (str.indexOf(dot, (lat + 2)) == -1) {

        return false;
    }

    if (str.indexOf(" ") != -1) {

        return false;
    }

    return true;
}

