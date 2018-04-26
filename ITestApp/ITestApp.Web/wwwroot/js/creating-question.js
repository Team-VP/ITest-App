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

                $('#question-container').append(content);
            },
            error: function (e) {
                console.log(e);
            }
        });
    });

    $('#question-container').on("click", '.add-answer-btn', () => {
        $.ajax({
            url: '/Test/AddAnswer/',
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