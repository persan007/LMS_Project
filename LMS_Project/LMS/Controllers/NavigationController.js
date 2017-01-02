(function () {

    var NavigationController = function ($scope, Request, $route) {

        var SearchFiler = function () {
            // TODO: Hitta all kurser, lärare mm  som matchar sökresultatet. (Akronym?)
            //console.log("Search term: " + $scope.SearchText);
        }

        var LogOut = function () {
            Request.Make("/Account/LogOff/", "post").then(function (data) {
                $route.reload();
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
        "$route",
        NavigationController
    ]);

}());