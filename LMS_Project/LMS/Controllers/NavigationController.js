(function () {

    var NavigationController = function ($scope, Request) {

        var SearchFiler = function () {
            // TODO: Hitta all kurser, lärare mm  som matchar sökresultatet. (Akronym?)
            console.log("Search term: " + $scope.SearchText);
        }

        var LogOut = function () {
            Request.Make("/Account/LogOff/", null).then(function (data) {
                console.log(data);
            });
        }

        Request.Make("/Home/GetUserInformation/", null).then(function (data) {
            $scope.User = data;
        });
        
        $scope.$watch("SearchText", SearchFiler);
        $scope.SearchText = "";
        $scope.LogOut = LogOut;
        $scope.User = {};
    }

    LMSApp.controller("NavigationController", [
        "$scope",
        "Request",
        NavigationController
    ]);

}());