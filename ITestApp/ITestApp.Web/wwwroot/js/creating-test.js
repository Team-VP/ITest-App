$(function () {
    $('#add-question-btn').on("click", () => {
        $.ajax({
            url: '/Test/AddQuestion/',
            type: 'GET',
            contentType: 'application/html',
            //data: JSON.stringify(model),
            success: function (content) {
                //var questionContainer = $('div');
                //var text = $('textarea');

                //questionContainer.append(text);

                $('#question-container').html(content);
            },
            error: function (e)
            {
                console.log(e);
            }
        });
    });
});