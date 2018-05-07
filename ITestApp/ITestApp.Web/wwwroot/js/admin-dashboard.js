$(function () {
    $('#results-tabse').DataTable();
    $('#admins-tests').DataTable();

    $('a[name=disable]').on('click', function (data) {
        data.preventDefault();
        let url = $(this).attr('href');
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure you want to disable this test?',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: url,
                        success: function (r) {
                            window.location.href = r;
                        }
                    });
                },
                cancel: function () {
                },
            }
        });
    });

    $('a[name=publish]').on('click', function (data) {
        data.preventDefault();
        let url = $(this).attr('href');
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure you want to publish this test?',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: url,
                        success: function (r) {
                            window.location.href = r;
                        }
                    });
                },
                cancel: function () {
                },
            }
        });
    });

    $('a[name=delete]').on('click', function (data) {
        data.preventDefault();
        let url = $(this).attr('href');
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure you want to delete this test?',
            buttons: {
                confirm: function () {
                    $.ajax({
                        url: url,
                        success: function (r) {
                            window.location.href = r;
                        }
                    });
                },
                cancel: function (err) {
                    console.log(err);
                },
            }
        });
    });
});
