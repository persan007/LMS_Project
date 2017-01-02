(function () {
    var ScheduleProvider = function ($interval) {
        var canvas = document.getElementById("ScheduleCanvas");
        var ctx = canvas.getContext("2d");
        var textHeight = ctx.measureText("_").width;

        var canvasCurrentWidthCheck = 0;

        var numberOfColumns,
            dayStartsAtSunday,
            dayStartsAt,
            dayEndsAt,
            allLessons,
            titles;

        var canvasContentWidth,
            canvasContentHeight,
            canvasContentOffset,
            canvasHeadlineHeight;

        var headlineFont,
            timelineFont;

        // Used for clearing the canvas //
        var clear = function () {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
        }

        var setSize = function () {
            if (canvasCurrentWidthCheck == canvas.clientWidth)
                return;

            canvasCurrentWidthCheck = canvas.clientWidth
            ctx.canvas.width = canvas.clientWidth;
            ctx.canvas.height = canvas.clientHeight;

            canvasContentOffset = (canvas.clientWidth / 10);
            canvasContentWidth = canvas.clientWidth - canvasContentOffset;
            canvasContentHeight = canvas.clientHeight;
            canvasHeadlineHeight = canvas.clientHeight / 16;

            if (canvas.clientWidth <= 480)
                textHeight = ctx.measureText("_").width / 2;
            else
                textHeight = ctx.measureText("_").width;
            
            // Set text font size relative canvas width //
            if (canvas.clientWidth >= 780) {
                headlineFont = (canvasContentWidth / numberOfColumns) / 9;
                timelineFont = canvasContentOffset / 10;
            }
            if (canvas.clientWidth < 780) {
                headlineFont = (canvasContentWidth / numberOfColumns) / 8;
                timelineFont = canvasContentOffset / 6;
            }
            if (canvas.clientWidth < 480) {
                headlineFont = (canvasContentWidth / numberOfColumns) / 6;
                timelineFont = canvasContentOffset / 4;
            }

            // Clear the canvas //
            clear();

            // Call draw method to sync //
            draw();
        }

        // Draw all the data to the canvas //
        var draw = function () {

            // Draw board //
            (function Board() {
                ctx.lineWidth = 1;
                ctx.strokeStyle = "#000000";

                var columnWidth = canvasContentWidth / numberOfColumns;

                for (var i = 0; i < numberOfColumns; i++) {
                    ctx.moveTo(columnWidth * i + canvasContentOffset, 0);
                    ctx.lineTo(columnWidth * i + canvasContentOffset, canvas.height);
                }

                ctx.stroke();
            })();

            // Draw headers //
            (function Headers() {
                ctx.lineWidth = 1;
                ctx.strokeStyle = "#000000";

                var columnWidth = canvasContentWidth / numberOfColumns;

                ctx.moveTo(canvasContentOffset, canvasHeadlineHeight);
                ctx.lineTo(canvas.width + canvasContentOffset, canvasHeadlineHeight);

                ctx.font = headlineFont + "px Georgia";

                for (var i = 0; i < titles.length; i++) {
                    var textWidth = ctx.measureText(titles[i]).width;

                    var w = (columnWidth * i + (columnWidth / 2) + canvasContentOffset) - (textWidth / 2);
                    var h = (canvasHeadlineHeight / 2) + textHeight;

                    ctx.fillText(titles[i], w, h);
                }

                ctx.stroke();
            }());

            // Draw Timeline //
            (function Timeline() {
                // Show every 30min //
                var interval = 60;

                var index = 0;
                var dayLength = GetTimespanInMinutes(dayStartsAt, dayEndsAt) + 60;
                var height = Math.ceil((canvasContentHeight - canvasHeadlineHeight) / (dayLength / interval));

                ctx.font = timelineFont + "px Arial";

                for (var i = 0; i <= dayLength; i += interval) {

                    var time = GetTimeInHoursMinutes(i);
                    var w = canvasContentOffset - ctx.measureText(time).width - 10;
                    var h = height * index + canvasHeadlineHeight + textHeight;

                    ctx.fillText(time, w, h);
                    index += 1;
                }

                ctx.stroke();
            }());

            // Draw the lessons //
            (function Lessons() {

            }());
        }

        // Initialize the canvas settings //
        var init = function (boardColumns, startAtSunday, dayStarts, dayEnds, lessons) {
            if (boardColumns > 0 && boardColumns <= 7)
                numberOfColumns = boardColumns;
            else
                numberOfColumns = 7;

            dayStartsAtSunday = ((startAtSunday === true) ? true : false);
            dayStartsAt = dayStarts;
            dayEndsAt = dayEnds;
            allLessons = lessons || Array(0);

            titles = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];

            if (dayStartsAtSunday) {
                var day = titles.splice(-1, 1);
                titles.unshift(day);
            }

            // Add eventlistener for canvas resize //
            $interval(setSize, 500);
        }

        // Get number of minutes between two times //
        function GetTimespanInMinutes(from, to) {
            var dayStartsAtMinutes = ConvertHoursToMinutes(from);
            var dayEndssAtArrMinutes = ConvertHoursToMinutes(to);

            return Math.abs(Math.floor(dayEndssAtArrMinutes - dayStartsAtMinutes));
        }

        // Convert time of type string to minutes type int //
        function ConvertHoursToMinutes(hoursToConvert) {
            var tmp = (hoursToConvert != null) ? String(hoursToConvert).split(":") : String("00:00").split(":");

            if ((+tmp[0]) > 0) {
                tmp[0] *= 60;
            }

            return (+tmp[0]) + (+tmp[1]);
        }

        // Convert minutes type int to time type string //
        function GetTimeInHoursMinutes(minutesToConvert) {
            minutesToConvert += ConvertHoursToMinutes(dayStartsAt);

            var realMin = minutesToConvert % 60;
            var hours = Math.floor(minutesToConvert / 60);

            return ((String(hours).length == 2) ? hours : "0" + hours) + ":" + ((String(realMin).length == 2) ? realMin : realMin + "0");
        }

        return {
            Initialize: init,
            UpdateCanvasSize: setSize,
            Draw: draw,
            Clear: clear,
            element: canvas
        };
    }

    LMSApp.factory("Schedule", [
        "$interval",
        ScheduleProvider
    ]);

}());