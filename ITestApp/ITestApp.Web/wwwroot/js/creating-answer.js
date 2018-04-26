$(function () {
    $('#add-answer-btn').on("click", () => {
        $.ajax({
            url: '/Test/АddAnswer/',
            type: 'GET',
            contentType: 'application/html',
            //data: JSON.stringify(model),
            success: function (content) {
                //var questionContainer = $('div');
                //var text = $('textarea');

                //questionContainer.append(text);

                $('#answer-container').append(content);
            },
            error: function (e) {
                console.log(e);
            }
        });
    });
});