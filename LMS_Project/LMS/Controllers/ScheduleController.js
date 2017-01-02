(function () {

    var ScheduleController = function ($scope, Schedule) {
        var test = [
            {
                From: "10:30",
                To: "12:00",
                Day: "Tuesday",
                LessonType: "English",
                Color: "lightblue",
                Teacher: "TLUG"
            },
            {
                From: "10:30",
                To: "15:20",
                Day: "Friday",
                LessonType: "Programming",
                Color: "pink",
                Teacher: "ELÖV"
            },
            {
                From: "08:30",
                To: "09:45",
                Day: "Monday",
                LessonType: "Math",
                Color: "lightgreen",
                Teacher: "POLV"
            }
        ];

        Schedule.element.style.opacity = 0;
        Schedule.Initialize(5, false, "08:00", "17:00", test);
        Schedule.element.style.opacity = 1;
    }

    LMSApp.controller('ScheduleController', [
        '$scope',
        'Schedule',
        ScheduleController
    ]);

}());