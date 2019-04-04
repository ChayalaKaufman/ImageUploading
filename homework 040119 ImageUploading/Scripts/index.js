$(() => {
    $("#password").on('keyup', function () {
        const password = $("#password").val();
        $("#upload").prop('disabled', password == '')

    });
})