(function () {

    var RequestProvider = function ($http) {
        var status = null;

        // If an error occurs display it in the console
        var OnError = function (response) {
            console.error("Error: " + response);
            status = response.status;
            return response;
        }

        // Returns the data obj from server
        var make = function (TO, DATA) {
            TO = TO || null;

            //console.log("TO: " + TO);
            //console.log("DATA: " + DATA);

            if (!TO) {
                return TO;
            }

            return $http.post(TO, DATA).then(function (response) {
                status = response.status;
                console.log("status: " + status);
                return response.data;
            }, OnError);
        }

        return {
            Make: make,
            Status: status
        };
    }

    LMSApp.factory("Request", [
        "$http",
        RequestProvider
    ]);

}());