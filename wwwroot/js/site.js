// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function() {
    // Handle form submissions with AJAX
    $("form").submit(function(event) {
        event.preventDefault();
        var form = $(this);
        $.ajax({
            type: form.attr("method"),
            url: form.attr("action"),
            data: form.serialize(),
            success: function(response) {
                $("#mainContent").html(response);
            }
        });
    });

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