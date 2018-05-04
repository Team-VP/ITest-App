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

    //$('a[name=edit]').on('click', function (data) {
    //    data.preventDefault();
    //    let url = $(this).attr('href');

    //    $(".hidden-container").toggleClass("hide");
    //    window.location.href = url
    //    //Promise.resolve(
    //    //    $.ajax({
    //    //        url: url,
    //    //        type: "GET"
    //    //    })).then((result) => {
    //    //        $(".hidden-container").toggleClass("hide");
    //    //        console.log(result);
    //    //        $("body").html(result);
    //    //        //window.location.href = url;
    //    //    }).catch((err) => {
    //    //        console.log(err)
    //    //    })

    //    //Promise.resolve()
    //    //    .then((result) => {
    //    //        console.log(result);
    //    //        //$(".hidden-container").toggleClass("hide");
    //    //    }).catch((err) => {
    //    //        console.log(err)
    //    //    })
    //});
});
