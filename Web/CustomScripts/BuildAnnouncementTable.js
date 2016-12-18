$(document).ready(function () {

    $.ajax({
        url: '/Announcements/BuildAnnouncementTable',
        success: function (result) {
            $('#tableDiv').html(result);
        }
    })
});