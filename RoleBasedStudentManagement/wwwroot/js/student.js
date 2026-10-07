$(document).ready(function () {

    // View all students
    $("#viewStudents").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/Student",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("Students received");
                console.log(response);
                $("#studentsList").html(
                    "<h3 class='mb-3' >All Students </h3>"
                )

                // Hide other sections
                $("#myProfile").html("");
                $("#editProfile").html("");

                // Clear old students
                $("#studentsList").html("");

                $("#studentsList").html(
                    "<h3 class='mb-3'>All students</h3>" +
                    "<div class='table-responsive'>" + 
                    "<table class= 'table table-bordered table-hover'>" +
                    "<thead class = 'table-dark'>" +
                    "<tr>" +
                    "<th>Name</th>" +
                    "<th>Email</th>" +
                    "<th>Course</th>" +
                    "<th>Year</th>" +
                    "</tr>" +
                    "</thead>" +
                    "<tbody id='studentTableBody'>" + "</tbody >" +
                    "</table>" +
                    "</div>"
                )

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

            }
        });

    });


    // My Profile
    $("#myProfileButton").click(function () {

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/Student/me",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                console.log("My profile received");
                console.log(response);

                // Hide other sections
                $("#studentsList").html("");
                $("#editProfile").html("");

                $("#myProfile").html(
                    "<h3 class='mb-4'>My Profile</h3>" +
                    "<p><strong>Name: </strong> " + response.name + "</p>" +
                    "<p><strong>Email: </strong>" + response.email + "</p>" +
                    "<p><strong>Course: </strong>" + response.course + "</p>" +
                    "<p><strong>Year: </strong>" + response.year + "</p>"
                );

            },

            error: function (response) {

                console.log("Failed to get profile");
                console.log(response.status);
                console.log(response.responseText);

            }
        });

    });


    // Edit Profile
    $("#editProfileButton").click(function () {

        // Hide other sections
        $("#studentsList").html("");
        $("#myProfile").html("");

        var token = localStorage.getItem("token");

        $.ajax({
            url: "/api/Student/me",
            type: "GET",

            headers: {
                "Authorization": "Bearer " + token
            },

            success: function (response) {

                $("#editProfile").html(
                    "<h3 class='mb-4'>Edit Profile</h3>" +

                    "<label>Name</label>" +
                    "<input type='text' id='editName' class='form-control' value='" + response.name + "'>" +
                 

                    "<label>Email</label>" +
                    "<input type='email' id='editEmail' class='form-control' value='" + response.email + "'>" +
                    

                    "<label>Course</label>" +
                    "<input type='text' id='editCourse' class='form-control' value='" + response.course + "'>" +
                   

                    "<label>Year</label>" +
                    "<input type='number' id='editYear' class='form-control' value='" + response.year + "'>" +
                    

                    "<button id='updateProfileButton'class = 'btn btn-primary mt-3'>Update</button>"
                );


                // Update Profile
                $("#updateProfileButton").click(function () {

                    var name = $("#editName").val();
                    var email = $("#editEmail").val();
                    var course = $("#editCourse").val();
                    var year = $("#editYear").val();

                    $.ajax({
                        url: "/api/Student/" + response.id,
                        type: "PUT",

                        headers: {
                            "Authorization": "Bearer " + token
                        },

                        contentType: "application/json",

                        data: JSON.stringify({
                            name: name,
                            email: email,
                            course: course,
                            year: Number(year)
                        }),

                        success: function (updatedStudent) {

                            console.log("Profile updated");
                            console.log(updatedStudent);

                            $("#editProfile").html("");

                            $("#myProfile").html(
                                "<h3>My Profile</h3>" +
                                "<p>Name: " + updatedStudent.name + "</p>" +
                                "<p>Email: " + updatedStudent.email + "</p>" +
                                "<p>Course: " + updatedStudent.course + "</p>" +
                                "<p>Year: " + updatedStudent.year + "</p>"
                            );

                        },

                        error: function (response) {

                            console.log("Update failed");
                            console.log(response.status);
                            console.log(response.responseText);

                        }
                    });

                });

            },

            error: function (response) {

                console.log("Failed to load profile");
                console.log(response.status);
                console.log(response.responseText);

            }
        });

    });

    $("#logoutButton").click(function () {
        localStorage.removeItem("token");
        window.location.href = "/index.html";
    })

});