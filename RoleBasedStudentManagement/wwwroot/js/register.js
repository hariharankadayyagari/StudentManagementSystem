$(document).ready(function () {

    $("#registerForm").submit(function (event) {

        event.preventDefault();

        var name = $("#name").val();
        var email = $("#email").val();
        var password = $("#password").val();
        var course = $("#course").val();
        var year = $("#year").val();

        $.ajax({
            url: "/api/Auth/register",
            type: "POST",
            contentType: "application/json",

            data: JSON.stringify({
                name: name,
                email: email,
                password: password,
                course: course,
                year: Number(year)
            }),

            success: function (response) {

                console.log("Registration successful");

                // Hide registration form
                $("#registerForm").hide();

                // Hide old login button
                $("a[href='/index.html']").hide();

                // Show success message
                $("#message")
                    .removeClass("text-danger")
                    .addClass("text-success fw-bold")
                    .text("Registration completed successfully!");

                // Show Back to Login button
                $("#message").after(
                    "<div class='text-center mt-3'>" +
                    "<a href='/index.html' class='btn btn-primary'>" +
                    "Back to Login" +
                    "</a>" +
                    "</div>"
                );
            },

            error: function (response) {

                console.log("Registration failed");

                console.log(response.status);
                console.log(response.responseText);

                $("#message")
                    .removeClass("text-success")
                    .addClass("text-danger")
                    .text("Registration failed. Please try again.");
            }
        });
    });
});