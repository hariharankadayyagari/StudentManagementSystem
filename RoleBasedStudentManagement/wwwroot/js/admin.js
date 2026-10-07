$(document).ready(function () {

    // View all students
    $("#viewStudentsButton").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/student",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("Students received");
                console.log(response);

                $("#usersList").html("");
                $("#viewTeachers").html("");
                $("#message").html("");

                $("#viewStudents").html(
                    "<h3 class='mb-3'>All Students</h3>" +

                    "<div class='table-responsive'>" +

                    "<table class='table table-bordered table-hover'>" +

                    "<thead class='table-dark'>" +

                    "<tr>" +
                    "<th>Name</th>" +
                    "<th>Email</th>" +
                    "<th>Course</th>" +
                    "<th>Year</th>" +
                    "</tr>" +

                    "</thead>" +

                    "<tbody id='studentTableBody'>" +

                    "</tbody>" +

                    "</table>" +

                    "</div>"
                );

                response.forEach(function (student) {

                    $("#studentTableBody").append(

                        "<tr>" +

                        "<td>" +
                        student.name +
                        "</td>" +

                        "<td>" +
                        student.email +
                        "</td>" +

                        "<td>" +
                        student.course +
                        "</td>" +

                        "<td>" +
                        student.year +
                        "</td>" +

                        "</tr>"
                    );
                });
            },

            error: function (res) {

                console.log("Failed to get students");

                console.log(res.status);
                console.log(res.responseText);

                $("#message").text(
                    "Error: " + res.status
                );
            }
        });
    });


    // View all teachers
    $("#viewTeachersButton").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/teacher",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("Teachers received");
                console.log(response);

                $("#viewStudents").html("");
                $("#usersList").html("");
                $("#message").html("");

                $("#viewTeachers").html(
                    "<h3 class='mb-3'>All Teachers</h3>" +

                    "<div class='table-responsive'>" +

                    "<table class='table table-bordered table-hover'>" +

                    "<thead class='table-dark'>" +

                    "<tr>" +
                    "<th>Name</th>" +
                    "<th>Email</th>" +
                    "<th>Subject</th>" +
                    "<th>Department</th>" +
                    "</tr>" +

                    "</thead>" +

                    "<tbody id='teacherTableBody'>" +

                    "</tbody>" +

                    "</table>" +

                    "</div>"
                );

                response.forEach(function (teacher) {

                    $("#teacherTableBody").append(

                        "<tr>" +

                        "<td>" +
                        teacher.name +
                        "</td>" +

                        "<td>" +
                        teacher.email +
                        "</td>" +

                        "<td>" +
                        teacher.subject +
                        "</td>" +

                        "<td>" +
                        teacher.department +
                        "</td>" +

                        "</tr>"
                    );
                });
            },

            error: function (res) {

                console.log("Failed to get teachers");

                console.log(res.status);
                console.log(res.responseText);

                $("#message").text(
                    "Error: " + res.status
                );
            }
        });
    });


    // Manage Users
    $("#manageUsersButton").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/Admin/users",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("Users received");
                console.log(response);

                $("#viewStudents").html("");
                $("#viewTeachers").html("");
                $("#message").html("");

                $("#usersList").html(
                    "<h3 class='mb-3'>Manage Users</h3>" +

                    "<div class='table-responsive'>" +

                    "<table class='table table-bordered table-hover'>" +

                    "<thead class='table-dark'>" +

                    "<tr>" +
                    "<th>Name</th>" +
                    "<th>Email</th>" +
                    "<th>Role</th>" +
                    "<th>Action</th>" +
                    "</tr>" +

                    "</thead>" +

                    "<tbody id='userTableBody'>" +

                    "</tbody>" +

                    "</table>" +

                    "</div>"
                );

                response.forEach(function (user) {

                    $("#userTableBody").append(

                        "<tr>" +

                        "<td>" +
                        user.name +
                        "</td>" +

                        "<td>" +
                        user.email +
                        "</td>" +

                        "<td>" +
                        user.role +
                        "</td>" +

                        "<td>" +

                        "<button class='changeRoleButton btn btn-warning btn-sm' " +
                        "data-user-id='" + user.id + "'>" +
                        "Change Role" +
                        "</button>" +

                        "</td>" +

                        "</tr>"
                    );
                });
            },

            error: function (response) {

                console.log("Failed to get users");

                console.log(response.status);
                console.log(response.responseText);

                $("#message").text(
                    "Error: " + response.status
                );
            }
        });
    });


    // Change Role
    $(document).on("click", ".changeRoleButton", function () {

        var userId = $(this).data("user-id");

        var token = localStorage.getItem("token");


        var role = prompt(
            "Enter new role: Admin, Teacher or Student"
        );


        if (role == null) {
            return;
        }


        if (
            role !== "Student" &&
            role !== "Teacher" &&
            role !== "Admin"
        ) {

            alert("Invalid role");

            return;
        }


        var subject = "";
        var department = "";


        // If changing to Teacher
        if (role === "Teacher") {

            subject = prompt("Enter subject:");

            if (subject == null) {
                return;
            }

            department = prompt("Enter department:");

            if (department == null) {
                return;
            }
        }


        var course = "";
        var year = 0;


        // If changing to Student
        if (role === "Student") {

            course = prompt("Enter course:");

            if (course == null) {
                return;
            }

            year = prompt("Enter year:");

            if (year == null) {
                return;
            }
        }


        var data = {

            role: role,

            subject: subject,

            department: department,

            course: course,

            year: Number(year)
        };


        $.ajax({

            url: "/api/admin/users/" + userId + "/role",

            type: "PUT",

            headers: {
                "Authorization": "Bearer " + token
            },

            contentType: "application/json",

            data: JSON.stringify(data),


            success: function (response) {

                console.log("Role changed successfully");

                alert("Role changed successfully");

                // Refresh users list
                $("#manageUsersButton").click();
            },


            error: function (response) {

                console.log("Role change failed");

                console.log(response.status);
                console.log(response.responseText);

                alert(
                    "Role change failed: " +
                    response.status
                );
            }
        });
    });


    // Logout
    $("#logout").click(function () {

        localStorage.removeItem("token");

        window.location.href = "/index.html";
    });

});