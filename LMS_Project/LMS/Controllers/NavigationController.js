(function () {

    var NavigationController = function ($scope, Request, $window) {

        var SearchFiler = function () {
            // TODO: Hitta alla kurser, lärare mm  som matchar sökresultatet. (Akronym?)
            //console.log("Search term: " + $scope.SearchText);
        }

        var LogOut = function () {
            Request.Make("/Account/GetAntiForgeryToken/", "get").then(function (token) {
                Request.Make("/Account/LogOff/", "post", null, null, { 'RequestVerificationToken': token }).then(function (data) {
                    data = JSON.parse(String(data).toLowerCase());

                    if (data) {
                        $window.location.reload();
                    }
                });
            });
        }

        Request.Make("/Home/GetUserInformation/", "get").then(function (data) {
            $scope.User = data[0];
            $scope.User.Role = ((data[0].Role === "Teacher") ? true : false);
        });
        
        $scope.$watch("SearchText", SearchFiler);
        $scope.SearchText = "";
        $scope.LogOut = LogOut;
        $scope.User = null;
    }

    LMSApp.controller("NavigationController", [
        "$scope",
        "Request",
        "$window",
        NavigationController
    ]);

}());