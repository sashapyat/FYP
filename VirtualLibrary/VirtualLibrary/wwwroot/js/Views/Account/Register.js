
//
// source for toast messages: https://cdnjs.com/libraries/toastr.js/latest
//

$("#RegisterForm").on("submit", function (e) {
    e.stopImmediatePropagation(); //prevents double form submission in IE
    e.preventDefault(); //STOP default form action


    // validate form entry
    if (false === $(this).parsley().validate()) {
        return;
    }

    toastr.options = {
        "positionClass": "toast-center-center"
    };

    // get the Data
    var postData = {
        FirstName: $("#FirstName").val(),
        LastName: $("#LastName").val(),
        Email: $("#Email").val(),
        Username: $("#Username").val(),
        password: $("#password").val()
    }

    //submit config values
    $.ajax({
        url: window.location.pathname,
        dataType: "json",
        data: postData,
        type: "POST",
        success: function (result) {

            $("#FirstName").val(null);
            $("#LastName").val(null);
            $("#Email").val(null);
            $("#Username").val(null);
            $("#password").val(null);
            $("#confirmPassword").val(null);

            $("#RegisterForm").parsley().reset();

            toastr.options.onHidden = function () { window.location.href = "/Account/Login"; }
            toastr.success("You have successfully registered! Please check your email for a verification link.");
        },
        error: function (data) {
            toastr.error(data.responseJSON.message);
        }
    });

});