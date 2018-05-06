$(function () {
    summernoteInit();
    const data = initDataObj();
    const $errorPanel = $(".error-panel ul");
    const $accordion = $("#question-container");

    const finishTestCreation = function (shouldPublish, url) {
        $errorPanel.children().remove();
        $accordion.accordion({ header: "h3", active: false });
        let shouldPost = true;

        if ($("#test-form").valid()) {
            let validStringContent = true;

            data.Title = $("#test-name").val();
            data.RequiredTime = $("#test-time").val();
            data.Category = $("#test-category").val();

            if (shouldPublish) {
                data.Status = "Published";
            }

            const allQuestionHolders = $(".question-holder");

            if (shouldPublish && allQuestionHolders.length === 0) {
                toastr.options.positionClass = "toast-top-center";
                toastr.error("Cannot publish a test with no questions!");
                return;
            }

            $.each(allQuestionHolders, (i, q) => {
                const $q = $(q);
                const qSummernote = $q.find(".question-content .summernote");
                const qContent = qSummernote.summernote("code");
                const qId = +qSummernote.attr("data-question-id");

                validStringContent = validator.validateStringContent("Question", qContent, $q, $q);

                if (!validStringContent) {
                    shouldPost = false;
                }

                let question;
                let shouldAddQuestion = true;
                if (!isNaN(qId)) {
                    question = data.Questions.filter(q => q.Id === qId)[0];
                    shouldAddQuestion = false;
                } else {
                    question = {};
                    question.Answers = [];
                }

                question.Content = qContent;
                question.ContentWithoutTags = qContent.replace(/<\/?[^>]+(>|$)/g, "");

                const qAnswers = $q.find(".answer-holder .answer-content");
                let correctAnswers = 0;

                $.each(qAnswers, (i, a) => {
                    const $a = $(a);
                    const aSummernote = $a.find(".summernote");
                    const aContent = aSummernote.summernote("code");
                    const aId = +aSummernote.attr("data-answer-id");
                    validStringContent = validator.validateStringContent("Answer", aContent, $a, $q);

                    if (!validStringContent) {
                        shouldPost = false;
                    }

                    let answer;
                    let shouldAddAnswer = true;
                    if (!isNaN(aId)) {
                        answer = question.Answers.filter(a => a.Id === aId)[0];
                        shouldAddAnswer = false;
                    } else {
                        answer = {};
                    }

                    answer.Content = aContent;
                    answer.ContentWithoutTags = aContent.replace(/<\/?[^>]+(>|$)/g, "");

                    if ($a.find(".correct-answer-cb").is(":checked")) {
                        answer.IsCorrect = true;
                        correctAnswers++;
                    }

                    if (correctAnswers > 1) {
                        shouldPost = false;
                        toastr.options.positionClass = "toast-top-center";
                        const questionNumber = $q.prev().find(".question-number").html()
                        toastr.error(`Question ${questionNumber} must have exactly 1 correct answer!`);
                        return false;
                    }

                    if (shouldAddAnswer) {
                        question.Answers.push(answer);
                    }
                });

                if (correctAnswers < 1) {
                    shouldPost = false;
                    toastr.options.positionClass = "toast-top-center";
                    const questionNumber = $q.prev().find(".question-number").html()
                    toastr.error(`Question ${questionNumber} must have exactly 1 correct answer!`);
                    return false;
                }

                if (!validStringContent) {
                    shouldPost = false;
                }
                
                if (shouldAddQuestion) {
                    data.Questions.push(question);
                }
            });

            if (!shouldPost) {
                return;
            }

            let tokenHeader = $("input[name=__RequestVerificationToken]").val();

            $.ajax({
                url: url,
                type: "POST",
                contentType: "application/json; text/html; charset=utf-8",
                headers: { "__RequestVerificationToken": tokenHeader },
                data: JSON.stringify(data),
                success: (response) => {
                    window.location.href = response;
                },
                error: (err) => {
                    console.log(err)
                }
            })
        }
    };

    // Initialize data object to hold the initial state of the test. It will have empty values upon creation or filled ones upon editting
    function initDataObj() {
        const data = {};
        data.Title = $("#test-name").val();
        data.RequiredTime = $("#test-time").val();
        data.Category = $("#test-category").val();
        data.Status = "Draft";
        data.Questions = [];

        const allQuestionHolders = $(".question-holder");

        $.each(allQuestionHolders, (i, q) => {
            const question = {};
            const $q = $(q);
            const qSummernote = $q.find(".question-content .summernote");
            const qContent = qSummernote.summernote("code");
            const qId = qSummernote.attr("data-question-id");

            question.Content = qContent;
            question.Id = +qId;
            question.Answers = [];

            const qAnswers = $q.find(".answer-holder .answer-content");

            $.each(qAnswers, (i, a) => {
                const $a = $(a);
                const answer = {};
                const aSummernote = $a.find(".summernote");
                const aContent = aSummernote.summernote("code");
                const aId = aSummernote.attr("data-answer-id");

                answer.Content = aContent;
                answer.Id = +aId;

                if ($a.find(".correct-answer-cb").is(":checked")) {
                    answer.IsCorrect = true;
                }

                question.Answers.push(answer);
            });

            data.Questions.push(question);
        });

        return data;
    }

    // Button click events
    $("#publish-btn").on("click", () => {
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure you want to publish this test?',
            buttons: {
                confirm: function () {
                    finishTestCreation(true, "/administration/create/new");
                },
                cancel: function () {
                },
            }
        });
    });

    $("#draft-btn").on("click", () => {
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure you want to save this test as draft?',
            buttons: {
                confirm: function () {
                    finishTestCreation(false, "/administration/create/new");
                },
                cancel: function () {
                },
            }
        });
    });

    $("#edit-save-btn").on("click", (e) => {
        $.confirm({
            title: 'Confirm!',
            content: 'Save the test?',
            buttons: {
                confirm: function () {
                    const elId = $(e.target).attr("data-id");
                    finishTestCreation(false, `/administration/edit/${elId}`);
                },
                cancel: function () {
                },
            }
        });
    });

    $("#edit-publish-btn").on("click", (e) => {
        $.confirm({
            title: 'Confirm!',
            content: 'Save the test?',
            buttons: {
                confirm: function () {
                    const elId = $(e.target).attr("data-id");
                    finishTestCreation(true, `/administration/edit/${elId}`);
                },
                cancel: function () {
                },
            }
        });
    });

    // Add and delete questions


    $('#add-question-btn').on("click", () => {
        $.ajax({
            url: '/Administration/ManageTest/AddQuestion/',
            type: 'GET',
            contentType: 'application/html',
            success: function (html) {
                $accordion.append(html);
                incrementator.incrementAnswers();
                incrementator.incrementQuestions();
                $accordion.accordion("refresh")
                $accordion.accordion("option", "active", ($accordion.children("div").length - 1))
                summernoteInit();
            },
            error: function (err) {
                $accordion.append("<p>Something went wrong... Status: " + err.status + "</p>");
            }
        });
    });

    $accordion.on("click", '.delete-question-btn', (e) => {
        const buttonClicked = $(e.target);
        const questionHolder = buttonClicked.closest(".question-holder");
        const questionHolderTitleTab = questionHolder.prev();
        questionHolder.remove();
        questionHolderTitleTab.remove();
        incrementator.incrementQuestions();

        const qId = +questionHolder.find(".summernote").attr("data-question-id");
        if (!isNaN(qId)) {
            data.Questions.filter(q => q.Id === qId)[0].IsDeleted = true;
        }
    });

    // Add and delete answers
    $accordion.on("click", '.add-answer-btn', (e) => {
        const buttonClicked = $(e.target);
        const extraAnswersContainer = buttonClicked.parent().find(".extra-answer-container");
        $.ajax({
            url: '/Administration/ManageTest/AddAnswer/',
            type: 'GET',
            contentType: 'application/html',
            success: function (html) {
                extraAnswersContainer.append(html);
                incrementator.incrementAnswers();
                summernoteInit();
            },
            error: function (err) {
                extraAnswersContainer.append("<p>Something went wrong... Status: " + err.status + "</p>");
            }
        });
    });

    $accordion.on("click", '.delete-answer-btn', (e) => {
        const $buttonClicked = $(e.target);
        const isValid = validator.validateAnswerCount($buttonClicked);

        if (isValid) {
            const answerContent = $buttonClicked.closest(".answer-content");
            const aId = +answerContent.find(".summernote").attr("data-answer-id");
            const qId = +$buttonClicked.closest(".answer-holder").prev().find(".summernote").attr("data-question-id");

            answerContent.remove();
            incrementator.incrementAnswers();

            if (!isNaN(aId)) {
                data.Questions.filter(q => q.Id === qId)[0].Answers.filter(a => a.Id === aId)[0].IsDeleted = true;
            }
        }
    });

    // JQuery accordion init
    $accordion.accordion({
        heightStyle: "content",
        collapsible: true
    });

    // Summernote.js init
    function summernoteInit() {
        $(".summernote").summernote({
            height: 150,
            disableResizeEditor: true,
            toolbar: [
                ['style', ['bold', 'italic', 'underline', 'clear']],
                ['fontsize', ['fontname', 'fontsize']],
                ['font', ['strikethrough', 'superscript', 'subscript']],
                ['para', ['ul', 'ol', 'paragraph', 'table']],
                ['misc', ['fullscreen', 'codeview', 'help']]
            ]
        });
    }

    // Answers and questions number incrementation
    const incrementator = (function () {
        const incrementAnswers = function () {

            const $answerHolders = $(".answer-holder")

            $.each($answerHolders, (i, el) => {
                const $answers = $(el).find(".answer-number");

                $.each($answers, (i, el) => {
                    $(el).html(i + 1);
                });
            });
        }

        const incrementQuestions = function () {
            const $questionHolders = $(".question-holder")

            $.each($questionHolders, (i, el) => {
                const $el = $(el);
                $el.prev("h3").find(".question-number").html(i + 1);
            });
        }

        return {
            incrementAnswers,
            incrementQuestions
        }
    })();


    // Validations for empty or too long answer and question contents
    const validator = (function () {

        const validateStringContent = function (answerOrQuestionStr, content, $element, $question) {
            let msg;
            const questionNumber = $question.prev().find(".question-number").html();
            const contentWithNoTags = content.replace(/<\/?[^>]+(>|$)/g, "");

            if (!contentWithNoTags) {
                if (answerOrQuestionStr === "Question") {
                    msg = `<li>${answerOrQuestionStr} ${questionNumber} text is empty!</li>`;
                }
                else {
                    const answerNumber = $element.find(".answer-number").html();
                    msg = `<li>${answerOrQuestionStr} ${answerNumber} text for Question ${questionNumber} is empty!</li>`;
                }

                const liEl = $(msg);
                $errorPanel.append(liEl);
                return false;
            }
            else if (content.length > 500) {
                if (answerOrQuestionStr === "Question") {
                    msg = `<li>${answerOrQuestionStr} ${questionNumber} text length is invalid! It must be max 500 characters!</li>`;
                }
                else {
                    const answerNumber = $element.find(".answer-number").html();
                    msg = `<li>${answerOrQuestionStr} ${answerNumber} text length for Question ${questionNumber} is invalid! It must be max 500 characters!</li>`;
                }

                const liEl = $(msg);
                $errorPanel.append(liEl);
                return false;
            }

            return true;
        }

        const validateAnswerCount = function ($answerToBeDeleted) {
            const minNumOfAnswers = 2;
            const actualAnswers = $answerToBeDeleted.closest(".question-holder").find(".answer-content").length;

            if (actualAnswers === minNumOfAnswers) {
                toastr.options.positionClass = "toast-bottom-left";
                toastr.error('A question must have at least 2 answers!')
                return false;
            }

            return true;
        }

        return {
            validateStringContent,
            validateAnswerCount
        }
    })();
});