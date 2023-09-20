// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//function dynamicMenu() {
//    var menuList = ""
//}

$(document).ready(function () {
    //$('#dynamicMenu').sessionId();

    $.ajax({
        type: "POST",
        url: "/Home/sessionId",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger;
            if (data == 0) {
                $('#dynamicMenu').append("<li class='nav-item'><a class='nav-link text-dark' href='/Home/Privacy'>New-Task</a></li><li class='nav-item'><a class='nav-link text-dark' href='/Team/Index'>Team</a></li><li class='nav-item'><a class='nav-link text-dark' href='/Home/Privacy'>Privacy</a></li><li class='nav-item'><a class='nav-link text-dark' href='/Home/Privacy'>Assign-Task</a></li>");
            }
            else {
                $('#dynamicMenu').append("<li class='nav-item'><a class='nav-link text-dark' href='/Home/Privacy'>New-Task</a></li><li class='nav-item'><a class='nav-link text-dark' href='/Team/Index'>Team</a></li><li class='nav-item'><a class='nav-link text-dark' href='/Home/Privacy'>Home</a></li><li class='nav-item'><a class='nav-link text-dark' href='/Home/Privacy'>Assign-Task</a></li>");
            }
        }
    });

})
