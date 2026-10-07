$(document).ready(function () {
    console.log("it is working");

    $("#loginform").submit(function (event) {
        event.preventDefault();
        var email = $("#email").val();
        var password = $("#password").val();

        $.ajax({
            url: "api/auth/login",
            type: "POST",
            contentType: "application/json",

            data: JSON.stringify({
                email: email,
                password: password
            }),

            success: function (response) {
                console.log("login successfull");
                console.log("JWT :", response);
                localStorage.setItem("token", response);
                var payload = response.split(".")[1];
                var decodePayload = atob(payload);
                var userData = JSON.parse(decodePayload);
                var roleClaim = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
                var role = userData[roleClaim];
                console.log("role :", role);

                if (role === "Admin") {
                    window.location.href = "/admin.html";
                }
                else if (role === "Student") {
                    window.location.href = "/student.html";
                }
                else if (role === "Teacher") {
                    window.location.href = "/teacher.html";
                }

                $("#message").text("Login Successfull");
            },


            error: function (res) {
                console.log("Login failed");
                console.log(res);
                $("#message").text("Invalid email or password");
            }
        });
        $("#loginform")[0].reset();
    })
})