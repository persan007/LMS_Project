(function () {
    var ScheduleProvider = function ($interval) {
        var titles = ["Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"];

        var canvas = document.getElementById("ScheduleCanvas");
        var ctx = canvas.getContext("2d");

        var numberOfColumns,
            dayStartsAtSunday,
            dayStartsAt,
            dayEndsAt;

        // Used for clearing the canvas //
        var clear = function () {
            ctx.clearRect(0, 0, canvas.width, canvas.height);
        }

        var setSize = function () {
            ctx.canvas.width = canvas.clientWidth;
            ctx.canvas.height = canvas.clientHeight;

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

                var columnWidth = canvas.width / numberOfColumns;

                for (var i = 0; i < numberOfColumns; i++) {
                    ctx.moveTo(columnWidth * i, 0);
                    ctx.lineTo(columnWidth * i, canvas.height);
                }

                ctx.stroke();
            })();

            // Draw headers //
            (function Headers() {
                ctx.lineWidth = 1;
                ctx.strokeStyle = "#000000";

                var height = canvas.height / 16;

                ctx.moveTo(0, height);
                ctx.lineTo(canvas.width, height);

                ctx.stroke();
            }());
        }

        // Initialize the canvas settings //
        var init = function (boardColumns, startAtSunday, dayStarts, dayEnds) {
            numberOfColumns = boardColumns;
            dayStartsAtSunday = ((startAtSunday === true) ? true : false);
            dayStartsAt = dayStarts;
            dayEndsAt = dayEnds;

            // Add eventlistener for canvas resize //
            $interval(setSize, 500);
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