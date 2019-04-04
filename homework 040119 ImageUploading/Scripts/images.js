$(() => {
    if ($("#beenHere").val() === "beenHere") {
        $("#img").attr('src', $("#fileName").val())
        $("#views").append(`Views: ${$("#num").val()}`)
        $("#error").remove();
    }

    $("#submit").on('click', function () {
        const pw = $("#password").val();
        const realPw = $("#realpw").val();
        if (pw === realPw) {
            $("#img").attr('src', $("#fileName").val())
            $("#views").append(`Views: ${$("#num").val()}`)
            $("#error").remove();
        }
        else if (pw !== realPw) {
            $("#error").append('<h3>Sorry, the password is incorrect. Please try again</h3>');
        }
    });

})