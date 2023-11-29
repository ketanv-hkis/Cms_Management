var checkedEmp = [];
var Id = 0;
var TeamAssignId = 0;

function hideShow() {

    $("#btnplus").hide();
    $("#btnsave").removeClass('hide');
    $("#Team").hide();
    $("#textaddteam").removeClass('hide');
    $("#empData").hide();
    $("#btnSaveEmp").hide();

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
            location.reload(true);
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
    var employeData = '';
    var teamAssignData = '';
    var checked;
    $.ajax({
        type: "GET",
        url: "/Team/getTeamAssign",
        data: { 'TeamAssignId': Id },
        dataType: "json",
        success: function (data) {
            teamAssignData = data;

        }
    });

    $.ajax({
        type: "GET",
        url: "/Employee/Index",
        dataType: "json",
        success: function (data) {
            employeData = data;
            var emp = '';
            for (var i = 0; i < employeData.length; i++) {
                if (teamAssignData.length > 0) {
                    var empId = (teamAssignData[0].empId.split(',')).map(Number);
                    if (empId.includes(employeData[i].id)) {
                        TeamAssignId = teamAssignData[0].teamAssignId;
                        checkedEmp.push(employeData[i].id);
                        emp += '<input type="CheckBox" checked class="emp_Checkbox" id="chk_' + employeData[i].id + '" onclick="selectEmployee(this)"><span class="emp_Name">' + employeData[i].firstname + '</span></br>'
                    }
                    else {
                        emp += '<input type="CheckBox" class="emp_Checkbox" id="chk_' + employeData[i].id + '" onclick="selectEmployee(this)"><span class="emp_Name">' + employeData[i].firstname + '</span></br>'
                    }
                }
                else {
                    emp += '<input type="CheckBox" class="emp_Checkbox" id="chk_' + employeData[i].id + '" onclick="selectEmployee(this)"><span class="emp_Name">' + employeData[i].firstname + '</span></br>'

                }
            }
            $("#empData").append(emp);
            $("#btnSaveEmp").removeClass('hide');
        }
    });

}



//$("#empData").on('change', 'input[type=checkbox]', function (e) {
//    var id = e.currentTarget.id;
//    if ($('#' + id).is(":checked")) {
//        var row = [];
//        var myId = id.split("_");
//        row["id"] = myId[1];
//        row["firstname"] = $('#' + e.currentTarget.id).attr('data-item');
//        empData.push(row);
//    }
//    else {
//        var myId = id.split("_");
//        for (var i = 0; i < empData.length; i++) {
//            if (empData[i].id == myId[1]) {
//                empData.splice(i, 1);

//            }
//        }
//    }

//});



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
    var teamAssign = { "EmpId": employee, "TeamId": parseInt(Id), "TeamAssignId": TeamAssignId }
    $.ajax({
        type: "POST",
        url: "/Team/saveTeamAssign",
        data: { 'teamAssign': teamAssign },
        dataType: "json",
        success: function (data) {
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