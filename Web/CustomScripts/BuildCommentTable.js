$(document).ready(function () {

    $.ajax({
        url: '/Comments/BuildCommentTable',
        success: function (result) {
            $('#commentDiv').html(result);
        }
    })
});