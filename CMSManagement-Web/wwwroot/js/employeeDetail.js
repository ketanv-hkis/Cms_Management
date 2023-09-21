var checkedEmp = [];
var Id = 0;

function hideShow() {
    $("#btnplus").hide();
    $("#btnsave").show();
    $("#Team").hide();
    $("#textaddteam").show();
    $("#empData").hide();
}
//$("#btnplus").on('click', function () {
//    $(this).hide();
//    $("#btnsave").show();
//});


//$("#btnplus").on('click', function(){
//    $("#Team").hide();
//    $("#textaddteam").show();

//});

function getEmployeeList() {
    var name = $('#textaddteam').val(); //$(this).find("option:selected").text());
    $.ajax({
        type: "POST",
        url: "/Team/create",
        data: { 'name': name },
        dataType: "json",
        success: function (response) {
            if (response != null) {
                $("#textaddteam").hide();
                $("#btnplus").show();
                $("#Team").show();
                $("#btnsave").hide();
            }
        }
    });
    
}

//$("#btnsave").on('click', function () {
//    $("#textaddteam").hide();
//    $("#btnplus").show();
//    $("#Team").show();
//});

function SelectedIndexChanged(event) {
    $("#empData").html('');
    Id = event;
    $.ajax({
        type: "GET",
        url: "/Employee/Index",
        dataType: "json",
        success: function (data) {
            var emp = '';
            for (var i = 0; i < data.length; i++) {
                emp += '<input type="CheckBox" class="emp_Checkbox" id="chk_' + data[i].id + '" onclick="selectEmployee(this)"><span class="emp_Name">' + data[i].firstname + '</span></br>'

            }
            $("#empData").append(emp);
            $("#btnSaveEmp").show();

        }
    });
}

function selectEmployee(event) {
    var id = event.id.split('_')[1];
    if (event.checked) {
        checkedEmp.push(id);
    } else {
        checkedEmp = checkedEmp.filter(X => X != id);
    }
}

$("#Team").on('click', function () {
   $("#btnSaveEmp").show();
});

function saveEmpData() {
    var employee = checkedEmp.join(",");
    var teamAssign = { "EmpId": employee, "TeamId": parseInt(Id) }
    $.ajax({
        type: "POST",
        url: "/Team/saveTeamAssign",
        data: { 'teamAssign': teamAssign },
        dataType: "json",
        success: function (data) {
            console.log(data)
        }
    });
}

//$(document).ready(function () {
//    $('#btnsave').on('click', function () {
function SaveData() {
    var name = $('#textaddteam').val(); //$(this).find("option:selected").text());
    var x = document.getElementById("textaddteam");
    $.ajax({
        type: "POST",
        url: "/Team/create",
        data: { 'name': name.toString() },
        dataType: "json",
        success: function (data) {
            x.style.display = "none";
        }
    });
}


    //$("#btnSaveEmp").on('click', function () {
    //    var employee = checkedEmp.join(",");
    //    var teamAssign = { "EmpId": employee, "TeamId": parseInt(Id) }
    //    $.ajax({
    //        type: "POST",
    //        url: "/Team/saveTeamAssign",
    //        data: { 'teamAssign': teamAssign },
    //        dataType: "json",
    //        success: function (data) {
    //            console.log(data)
    //        }
    //    });

    //});