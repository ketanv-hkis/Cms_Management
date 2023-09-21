function LoginUser() {
    var email = $('#txtEmail').val();
    var password = $('#txtPassword').val();

    if (email != "" && password != "") {
        $.ajax({
            url: "Login",
            type: "Post",
            data: { Email: email, Password: password },
            success: function (result) {
                if (result != undefined) {
                    alert("login successfully");
                    window.location.href = "/Home/Index";
                } else {
                    alert("login Failed");
                }
            }
        });
    }
    else {
        alert("Please enter email and password");
    }
}