$(function () {
    $('#add-question-btn').on("click", () => {
        $.ajax({
            url: '/Test/AddQuestion/',
            type: 'GET',
            contentType: 'application/html',
            success: function (html) {
                $('#question-container').append(html);
            },
            error: function (e) {
                console.log(e);
            }
        });
    });

    $('#question-container').on("click", '.add-answer-btn', (e) => {
        let buttonClicked = $(e.target);
        let extraAnswersContainer = buttonClicked.parent().find(".extra-answer-container");
        $.ajax({
            url: '/Test/AddAnswer/',
            type: 'GET',
            contentType: 'application/html',
            success: function (html) {
                extraAnswersContainer.append(html);
            },
            error: function (e) {
                console.log(e);
            }
        });
    });

    $('#question-container').on("click", '.delete-answer-btn', (e) => {
        let buttonClicked = $(e.target);
        let answerHolder = buttonClicked.closest(".answer-holder");
        answerHolder.remove();
    });

    $('#question-container').on("click", '.delete-question-btn', (e) => {
        let buttonClicked = $(e.target);
        let questionHolder = buttonClicked.closest(".question-holder");
        questionHolder.remove();
    });
});