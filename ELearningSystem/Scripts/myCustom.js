$(document).on("click", ".question input[type='radio']", function () {
    var clickedIndex = $(this).data("index");
    $(this).closest(".question").find("input[type='hidden']").val(clickedIndex);

});

$(document).on("click", ".course-info-js", function () {
    var courseId = $(this).closest("li").find("input[type='hidden']").val();
    $.ajax({
        url: "/ELearningSystem/Courses/Details?courseId=" + courseId
    })
        .done(function (data) {
            $("#course-info-container").html(data)
        });
});

$(document).on("change", "select[name='CoursesDropdown']", function () {
    var chosenCourseId = $("select[name='CoursesDropdown']").val();
    $.ajax({
        url: "/ELearningSystem/Lecture/AddLecture?isPartial=true&courseId=" + chosenCourseId
    })
        .done(function (data) {
            $("#lecture-form-container").html(data)
        });
});