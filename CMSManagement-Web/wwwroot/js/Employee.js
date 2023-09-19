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
                    Swal.fire({
                        title: 'Success!',
                        text: "Login SuccessFully!",
                        icon: 'success',
                    }).then((result) => {
                        if (result.isConfirmed) {
                            window.location.href = "/Employee/EmployeeList";
                        }
                    });
                } else {
                    Swal.fire(
                        'Opps',
                        'Something went wrong!',
                        'error'
                    )
                }
            }
        });
    }
    else {
        Swal.fire(
            'Failed!',
            'Please Enter Email and Password.',
            'error'
        );
    }
}


function DeleteEmployee(Id) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/Employee/EmployeeDelete',
                type: 'POST',
                data: { Id: Id },
                success: function (data) {
                    if (data == "success") {
                        Swal.fire(
                            'Deleted!',
                            'Your employee has been deleted.',
                            'success'
                        );
                        window.location.reload();
                    }
                }
            })
        }
    });
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