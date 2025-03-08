$(document).ready(function() {
    // Handle main menu link click
    $("#mainMenuLink").click(function(event) {
        event.preventDefault();
        $.ajax({
            url: "/Employee/AdminDashboard",
            success: function(response) {
                $("#mainContent").html(response);
            }
        });
    });

    // Toggle password visibility
    $(".toggle-password").click(function() {
        $(this).toggleClass("fa-eye fa-eye-slash");
        var input = $($(this).attr("toggle"));
        if (input.attr("type") == "password") {
            input.attr("type", "text");
        } else {
            input.attr("type", "password");
        }
    });
});