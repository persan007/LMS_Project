(function () {

    var ScheduleController = function ($scope, Request, Schedule) {
        var canvas = document.getElementById("ScheduleCanvas");
        var spinner = document.getElementById("canvasLoading");

        canvas.style.opacity = 0;

        Request.Make("/Account/GetAntiForgeryToken/", "get").then(function (token) {
            Request.Make("/Home/GetSchedule/", "get", null, null, { 'RequestVerificationToken': token }).then(function (data) {
                Schedule.Initialize(canvas, 5, false, "08:00", "17:00", data);

                spinner.style.opacity = 0;
                canvas.style.opacity = 1;
            });
        });
    }

    LMSApp.controller('ScheduleController', [
        '$scope',
        'Request',
        'Schedule',
        ScheduleController
    ]);

}());