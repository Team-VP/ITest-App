$(function () {
    $(':radio').change(function () {
        var names = {};
        $(':radio').each(function () {
            names[$(this).attr('myatribute')] = true;
        });
        var count = 0;
        $.each(names, function () {
            count++;
        });
        if ($(':radio:checked').length === count) {
            $('#submit-test-button').prop("disabled", false);
        }
    }).change();

    var takeTest = function (modelTime) {
        var timer = new Timer();

        timer.start({
            countdown: true, startValues: {
                seconds: modelTime
            }
        });

        $('#countdownExample .values').html(timer.getTimeValues().toString());
        timer.addEventListener('secondsUpdated', function (e) {
            $('#countdownExample .values').html(timer.getTimeValues().toString());
        });
        timer.addEventListener('targetAchieved', function (e) {
            $('#countdownExample .values').html('Time Expired');
            e.preventDefault();
            $('#test-form')
                .ajaxSubmit({
                    url: '/TakeTest/Index/',
                    type: 'POST',
                    success: function (response) {
                        $.confirm({
                            title: 'Time is over. :-(',
                            content: 'Your test submitted!',
                            buttons: {
                                Ok: function () {
                                    window.location.href = response;
                                },
                            }
                        });
                    }
                });
        });
    }

    $('#test-form').on('submit', function (e) {
        e.preventDefault();
        var url = this.action;
        var data = $(this).serialize();
        $.confirm({
            title: 'Confirm!',
            content: 'Your test is about to be submitted!',
            buttons: {
                confirm: function () {
                    $.post(url, data, function (r) {
                        console.log(r);
                        window.location.href = r;
                    });
                },
                cancel: function () {
                },
            }
        });
    });
})






