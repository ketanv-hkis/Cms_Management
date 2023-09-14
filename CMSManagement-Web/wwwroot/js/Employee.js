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
                    window.location.href = "/Employee/EmployeeAdd";
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


function SelectedChange() {
    var selectedRole = document.getElementById('ddRole').value;
    if (selectedRole == -1) {
        document.getElementById('error-message').style.display = "block";
    }
    else {
        document.getElementById('error-message').style.display = "none";
    }
}