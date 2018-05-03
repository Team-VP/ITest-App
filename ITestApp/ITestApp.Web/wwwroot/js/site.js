// Write your JavaScript code.
$(".ttt").on("click", () => {
    $("#loading-container").show();

    Promise.resolve
    $.ajax({
        url: "/administration/create/new",
        type: "POST",
        contentType: "application/json",
        headers: { "__RequestVerificationToken": tokenHeader },
        data: JSON.stringify(data),
        success: (response) => {
            $("#loading-container").hide();
            window.location.href = response;
        },
        error: (err) => {
            console.log(err)
        }
    })
});