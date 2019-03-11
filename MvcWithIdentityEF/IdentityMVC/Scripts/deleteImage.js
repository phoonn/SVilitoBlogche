$(document).ready(function () {
    $(".deleteButton").click(function () {
        if (!confirm("Are you sure you want to delete this image ?")) {
            return false;
        }

        //var fileID = $(this).attr("value");
        //var carID = $(this).attr("data-value");
        var parent = $(this).parent('div');
        var id = $(this).attr("value");

        var form = $('#__AjaxAntiForgeryForm');
        var token = $('input[name="__RequestVerificationToken"]', form).val();

        $.ajax({
            type: "post",
            url: "/BlogPosts/DeleteImage",
            data: { __RequestVerificationToken: token, id: id },
            success: function () {
                parent.fadeOut();
                $('#uploadnew').css("display", "block");
            },
            error: function () {
                alert("You couldn't delete this");
            }
        });
    });
});