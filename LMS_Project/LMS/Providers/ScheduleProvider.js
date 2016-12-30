(function () {
    var ScheduleProvider = function ($http) {
        var status = null;

        // If an error occurs display it in the console
        var OnError = function (response) {
            console.error("Error: " + response);
            status = response.status;
            return response;
        }

        // Returns all lessons in the database
        //var allLessons = function () {
        //    return $http.get('/Home/GetAllLessons')
        //        .then(function (response) {
        //            $scope.lessons = respone.data;
        //        }, OnError);
        //}

        var make = function (TO, DATA) {
            TO = TO || null;

            console.log("TO: " + TO);
            console.log("DATA: " + DATA);

            if (!TO) {
                return TO;
            }

            return $http.post(TO, DATA).then(function (response) {
                status = response.status;
                return response.data;
            }, OnError);
        }

        return {
            //AllLessons: allLessons,
            Make: make,
            Status: status
        };
    }

    LMSApp.factory("ScheduleProvider", [
        "$http",
        ScheduleProvider
    ]);

}());