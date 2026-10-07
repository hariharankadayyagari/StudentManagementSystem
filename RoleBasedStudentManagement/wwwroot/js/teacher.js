$(document).ready(function () {

    // View all students
    $("#viewStudents").click(function () {

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

                // Hide other sections
                $("#teachersList").html("");
                $("#myProfile").html("");
                $("#editProfile").html("");
                $("#message").html("");

                $("#studentsList").html(
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
                        "<td>" + student.name + "</td>" +
                        "<td>" + student.email + "</td>" +
                        "<td>" + student.course + "</td>" +
                        "<td>" + student.year + "</td>" +
                        "</tr>"
                    );

                });

            },

            error: function (response) {

                console.log("Failed to get students");
                console.log(response.status);
                console.log(response.responseText);

                $("#message").text("Error: " + response.status);
            }
        });

    });


    // View all teachers
    $("#viewTeachers").click(function () {

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

                // Hide other sections
                $("#studentsList").html("");
                $("#myProfile").html("");
                $("#editProfile").html("");
                $("#message").html("");

                $("#teachersList").html(
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
                        "<td>" + teacher.name + "</td>" +
                        "<td>" + teacher.email + "</td>" +
                        "<td>" + teacher.subject + "</td>" +
                        "<td>" + teacher.department + "</td>" +
                        "</tr>"
                    );

                });

            },

            error: function (response) {

                console.log("Failed to get teachers");
                console.log(response.status);
                console.log(response.responseText);

                $("#message").text("Error: " + response.status);
            }
        });

    });


    // My Profile
    $("#myProfileButton").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/teacher/me",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("Teacher profile received");
                console.log(response);

                // Hide other sections
                $("#studentsList").html("");
                $("#teachersList").html("");
                $("#editProfile").html("");
                $("#message").html("");

                $("#myProfile").html(
                    "<h3 class='mb-4'>My Profile</h3>" +
                    "<p><strong>Name:</strong> " + response.name + "</p>" +
                    "<p><strong>Email:</strong> " + response.email + "</p>" +
                    "<p><strong>Subject:</strong> " + response.subject + "</p>" +
                    "<p><strong>Department:</strong> " + response.department + "</p>"
                );

            },

            error: function (response) {

                console.log("Failed to get teacher profile");
                console.log(response.status);
                console.log(response.responseText);

                $("#message").text("Error: " + response.status);
            }
        });

    });


    // Edit Profile
    $("#editProfileButton").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/teacher/me",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("Editing teacher profile");
                console.log(response);

                // Hide other sections
                $("#studentsList").html("");
                $("#teachersList").html("");
                $("#myProfile").html("");
                $("#message").html("");

                $("#editProfile").html(
                    "<h3 class='mb-4'>Edit Profile</h3>" +

                    "<div class='mb-3'>" +
                    "<label class='form-label'>Name</label>" +
                    "<input type='text' id='editName' class='form-control' value='" + response.name + "'>" +
                    "</div>" +

                    "<div class='mb-3'>" +
                    "<label class='form-label'>Email</label>" +
                    "<input type='email' id='editEmail' class='form-control' value='" + response.email + "'>" +
                    "</div>" +

                    "<div class='mb-3'>" +
                    "<label class='form-label'>Subject</label>" +
                    "<input type='text' id='editSubject' class='form-control' value='" + response.subject + "'>" +
                    "</div>" +

                    "<div class='mb-3'>" +
                    "<label class='form-label'>Department</label>" +
                    "<input type='text' id='editDepartment' class='form-control' value='" + response.department + "'>" +
                    "</div>" +

                    "<button id='UpdateTeacherButton' class='btn btn-primary mt-2'>Update</button>"
                );


                // Update Teacher Profile
                $("#UpdateTeacherButton").click(function () {

                    var name = $("#editName").val();
                    var email = $("#editEmail").val();
                    var subject = $("#editSubject").val();
                    var department = $("#editDepartment").val();

                    $.ajax({
                        url: "/api/teacher/" + response.id,
                        type: "PUT",

                        headers: {
                            "Authorization": "Bearer " + token
                        },

                        contentType: "application/json",

                        data: JSON.stringify({
                            name: name,
                            email: email,
                            subject: subject,
                            department: department
                        }),

                        success: function (updatedTeacher) {

                            console.log("Teacher profile updated");
                            console.log(updatedTeacher);

                            $("#editProfile").html("");

                            $("#myProfile").html(
                                "<h3 class='mb-4'>My Profile</h3>" +
                                "<p><strong>Name:</strong> " + updatedTeacher.name + "</p>" +
                                "<p><strong>Email:</strong> " + updatedTeacher.email + "</p>" +
                                "<p><strong>Subject:</strong> " + updatedTeacher.subject + "</p>" +
                                "<p><strong>Department:</strong> " + updatedTeacher.department + "</p>"
                            );

                        },

                        error: function (response) {

                            console.log("Teacher update failed");
                            console.log(response.status);
                            console.log(response.responseText);

                            $("#message").text("Error: " + response.status);
                        }
                    });

                });

            },

            error: function (response) {

                console.log("Failed to load teacher profile");
                console.log(response.status);
                console.log(response.responseText);

                $("#message").text("Error: " + response.status);
            }
        });

    });


    // Logout
    $("#logoutButton").click(function () {

        localStorage.removeItem("token");

        window.location.href = "/index.html";

    });

});