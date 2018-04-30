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
                error: (err) => {
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
                //var active = $("#question-container").accordion("option", "active");
                
                let $accordion = $("#question-container");
                $accordion.append(html);
                $accordion.accordion("refresh")
                $accordion.accordion("option", "active", ($accordion.children("div").length - 1))
                $(".summernote").summernote({
                    height: 100,
                    disableResizeEditor: true
                });
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
                $(".summernote").summernote({
                    height: 100,
                    disableResizeEditor: true
                });
            },
            error: function (err) {
                extraAnswersContainer.append("<p>Something went wrong... Status: " + err.status + "</p>");
            }
        });
    });

    $('#question-container').on("click", '.delete-answer-btn', (e) => {
        let buttonClicked = $(e.target);
        let answerContent = buttonClicked.closest(".answer-content");
        answerContent.remove();
    });

    $('#question-container').on("click", '.delete-question-btn', (e) => {        
        let buttonClicked = $(e.target);
        let questionHolder = buttonClicked.closest(".question-holder");
        let questionHolderTitleTab = questionHolder.prev();
        questionHolder.remove();
        questionHolderTitleTab.remove();
    });

    $("#question-container").accordion({
        heightStyle: "content",
        collapsible: true
    });
});