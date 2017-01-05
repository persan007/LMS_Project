(function () {

    var NavigationController = function ($scope, Request, $window, Popup) {

        var SearchFiler = function () {
            // TODO: Hitta alla kurser, lärare mm  som matchar sökresultatet. (Akronym?)
            //console.log("Search term: " + $scope.SearchText);
        }

        var LogOut = function () {
            Request.Make("/Account/GetAntiForgeryToken/", "get").then(function (token) {
                Request.Make("/Account/LogOff/", "post", null, null, { 'RequestVerificationToken': token.data }).then(function (res) {
                    var data = JSON.parse(String(res.data).toLowerCase());

                    if (data) {
                        $window.location.reload();
                    }
                });
            });
        }

        // Test
        Popup.Upload("My title", "Upload new file in this module", null, { enableCancel: true }).then(function (info) {
            console.log(info);
        });

        Request.Make("/Data/GetUserInformation/", "get").then(function (res) {
            $scope.User = res.data[0];
            $scope.User.Role = ((res.data[0].Role === "Teacher") ? true : false);
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
        "Popup",
        NavigationController
    ]);

}());