// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$('#confirmPassword').on('change input keyup', function () {
    if (this.value) {
        $('#password').prop('required', true).parsley().validate();
    } else {
        $('#password').prop('required', false).parsley().validate();
    }
});

$('#ConfirmNewPassword').on('change input keyup', function () {
    if (this.value) {
        $('#NewPassword').prop('required', true).parsley().validate();
    } else {
        $('#NewPassword').prop('required', false).parsley().validate();
    }
});

$('#password, #confirmPassword, #NewPassword, #ConfirmNewPassword, #Email').keydown(function (e) {
    if (e.keyCode == 32) {
        return false;
    }
});

$('#FirstName, #LastName').keydown(function (e) {
    if ((e.keyCode > 47 && e.keyCode < 58) || (e.keyCode > 93 && e.keyCode < 106)) {
        return false;
    }
});

$(function () {
    $(".modalBtn, .modalXBtn").on('click', function () {
        $('#readBookModal').modal('hide');
    });
});
