(function () {
    var ScheduleController = function ($scope, ScheduleProvider) {
        var canvas = document.getElementById("ScheduleCanvas");
        var context = canvas.getContext("2d");
        var hour = 60;
        var s = new Schedule();

        fitToContainer(canvas);
        
        var ColumnWidth = canvas.width / 5;
        s.drawBoard();
        
        function MinutesFromStartEnd(start, end)
        {
            start = start.split(":");
            end = end.split(":");
            var minutes = (parseInt(end[0]) - parseInt(start[0])) * 60 + (parseInt(end[1]) - parseInt(start[1]));
            return minutes;
        }

        function Schedule()
        {
            this.width;
            this.height;
            this.breakColor = "#999999";
            this.columnSpace = 0;
            this.dayLength = MinutesFromStartEnd("08:00", "17:00");
            this.draw = function (day, color, startPoint, endPoint) {
                context.fillStyle = color;
                context.fillRect(ColumnWidth * day, startPoint, ColumnWidth, endPoint);
            }
            this.drawColumn = function (i) {
                context.moveTo(ColumnWidth + ColumnWidth * i, 0);
                context.lineTo(ColumnWidth + ColumnWidth * i, canvas.height);
            }
            this.drawLesson = function (day, color, start, end) {
                var startPoint = 0;
                var endPoint = 0;

                start = start.split(":");
                var startHours = parseInt(start[0]) - 8;
                var startMinutes = parseInt(start[1]);
                startPoint += ((startHours * 60) * renderMinute) + (startMinutes * renderMinute);


                end = end.split(":");
                var endHours = (parseInt(end[0]) - startHours) - 8;
                var endMinutes = parseInt(end[1]);
                endPoint += ((endHours * 60) * renderMinute) + (endMinutes * renderMinute);

                s.draw(day, color, startPoint, endPoint);
            }
            this.drawBoard = function () {
                for (var i = 0; i < 4; i++) {
                    s.drawColumn(i);
                }
                context.lineWidth = 1;
                context.strokeStyle = "black";

                //s.drawLesson(0, "#FF0000", "08:00", "12:00");
                //s.drawLesson(0, "#FF0000", "13:00", "16:30");

                var lessons = ScheduleProvider.Make("/Home/GetAllLessons/").then(function (data) { console.log(data); })

                for (var l = 0; l < lessons.length; l++)
                {
                    s.drawLesson(lessons[l].Day, "#FF0000", lessons[l].StartTime, lessons[l].EndTime);
                }

                context.stroke();
            }
        }

        function fitToContainer(canvas) {
            canvas.style.width = '100%';
            canvas.style.height = '100%';
            canvas.width = canvas.offsetWidth;
            canvas.height = canvas.offsetHeight;
            renderMinute = canvas.height / s.dayLength;
        }
    }

    LMSApp.controller('ScheduleController', [
        '$scope',
        'ScheduleProvider',
        ScheduleController
    ]);

}());
