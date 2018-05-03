$(function () {
    $('#results-tabse').DataTable();
    $('#admins-tests').DataTable();

    $('a[name=disable]').on('click', function (data) {
        data.preventDefault()
        let url = $(this).attr('href');
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure that you want to disable this tests?',
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
        data.preventDefault()
        let url = $(this).attr('href');
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure that you want to publish this tests?',
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
        data.preventDefault()
        let url = $(this).attr('href');
        $.confirm({
            title: 'Confirm!',
            content: 'Are you sure that you want to delete this tests?',
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

    $('a[name=edit]').on('click', function (data) {
        data.preventDefault()
        let url = $(this).attr('href');
        $(".hidden-container").toggleClass("hide");

        Promise.resolve($.ajax({
            url: url,
            type: "GET"
        })).then((result) => {
            $(".hidden-container").toggleClass("hide");
            $("body").html(result);
            window.location.href = url;
        }).catch((err) => {
            console.log(err)
        })

        //$.confirm({
        //    title: 'Confirm!',
        //    content: 'Are you sure that you want to publish this tests?',
        //    buttons: {
        //        confirm: function () {
        //            $.ajax({
        //                url: url,
        //                success: function (r) {
        //                    window.location.href = r;
        //                }
        //            });
        //        },
        //        cancel: function () {
        //        },
        //    }
        //});
    });
});
