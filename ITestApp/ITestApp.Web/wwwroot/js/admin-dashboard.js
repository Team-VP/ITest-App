$(function () {
    $('#results-tabse').DataTable();
    $('#admins-tests').DataTable();

    $('#admins-tests').on('click', '.dashboard-disable', function (data) {
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
                            console.log(r);
                            window.location.href = r;
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                },
                cancel: function () {
                },
            }
        });
    });

    $('#admins-tests').on('click', '.dashboard-publish', function (data) {
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

    $('#admins-tests').on('click','.dashboard-delete', function (data) {
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
