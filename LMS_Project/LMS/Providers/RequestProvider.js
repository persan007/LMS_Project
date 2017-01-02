(function () {
    var RequestProvider = function ($http) {
        var status = null;

        // If an error occurs display it in the console
        var OnError = function (response) {
            console.error("Error: " + response.statusText);
            status = response.status;
            return response;
        }

        // Returns the data obj from server
        var make = function (TO, TYPE, DATA, ENCTYPE, HEADERS) {
            TO      = TO || null;
            TYPE    = TYPE || null;
            DATA    = DATA || null;
            HEADERS = HEADERS || null;
            ENCTYPE = ENCTYPE || "application/x-www-form-urlencoded";
            
            if (!TO) {
                OnError({
                    status: 500,
                    statusText: "Invalid Action string {" + TO + "}"
                });
            }

            if ( !((String(TYPE).toLowerCase() != "get") || (String(TYPE).toLowerCase() != "post")) ) {
                OnError({
                    status: 500,
                    statusText: "Invalid type {" + TYPE + "}"
                });
            }

            return $http({
                url: TO,
                method: TYPE,
                enctype: ENCTYPE,
                data: DATA,
                headers: HEADERS,
                transformRequest: angular.identity
            }).then(function (response) {
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