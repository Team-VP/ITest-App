$(function () {
    $("#publish-btn").on("click", () => {
        if ($("#test-form").valid()) {
            let data = {};

            data.Title = $("#test-name").val();
            data.RequiredTime = $("#test-time").val();
            data.Category = $("#test-category").val();
            data.Status = "Published";
            data.Questions = [];

            let allQuestionHolders = $(".question-holder");

            $.each(allQuestionHolders, (i, q) => {
                let question = {};

                let qContent = $(q).find(".question-content textarea").val();

                question.Content = qContent;
                question.Answers = [];

                let qAnswers = $(q).find(".answer-holder .answer-content");

                $.each(qAnswers, (i, a) => {
                    let answer = {};
                    answer.Content = $(a).find("textarea").val();
                    question.Answers.push(answer);
                });

                data.Questions.push(question);
            });

            $.ajax({
                url: "/CreateTest/New",
                type: "POST",
                contentType: "application/json",
                data: JSON.stringify(data),
                success: (response) => {
                    window.location.href = response;
                },
                error: function (err) {
                    console.log(err)
                }
            })
        }
    });

    $('#add-question-btn').on("click", () => {
        $.ajax({
            url: '/CreateTest/AddQuestion/',
            type: 'GET',
            contentType: 'application/html',
            success: function (html) {
                $('#question-container').append(html);
            },
            error: function (err) {
                $('#question-container').append("<p>Something went wrong... Status: " + err.status + "</p>");
            }
        });
    });

    $('#question-container').on("click", '.add-answer-btn', (e) => {
        let buttonClicked = $(e.target);
        let extraAnswersContainer = buttonClicked.parent().find(".extra-answer-container");
        $.ajax({
            url: '/CreateTest/AddAnswer/',
            type: 'GET',
            contentType: 'application/html',
            success: function (html) {
                extraAnswersContainer.append(html);
            },
            error: function (err) {
                extraAnswersContainer.append("<p>Something went wrong... Status: " + err.status + "</p>");
            }
        });
    });

    $('#question-container').on("click", '.delete-answer-btn', (e) => {
        let buttonClicked = $(e.target);
        let answerHolder = buttonClicked.closest(".answer-content");
        answerHolder.remove();
    });

    $('#question-container').on("click", '.delete-question-btn', (e) => {
        let buttonClicked = $(e.target);
        let questionHolder = buttonClicked.closest(".question-holder");
        questionHolder.remove();
    });
});