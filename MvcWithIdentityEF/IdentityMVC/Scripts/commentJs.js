﻿$(document).ready(function () {
    $(".submitButton").click(function () {

        //var fileID = $(this).attr("value");
        //var carID = $(this).attr("data-value");
        var id = $(this).attr("value");
        var text = $("textarea#newcomment").val();

        $.ajax({
            type: "post",
            url: "/BlogPosts/CreateComment",
            data: { "postId": id , "commentText" : text },
            success: function () {
                window.location.hash = '#commentsection';
                window.location.reload(true);
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
                alert("You couldn't post this comment");
            }
        });
    });

    $(".deleteComment").click(function () {

        //var fileID = $(this).attr("value");
        //var carID = $(this).attr("data-value");
        var id = $(this).attr("value");
        var div = $(this).parent().parent().parent().parent();

        $.ajax({
            type: "post",
            url: "/BlogPosts/DeleteComment",
            data: { "id": id },
            success: function () {
                div.fadeOut()
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
                alert("You couldn't delete this comment");
            }
        });
    });
});