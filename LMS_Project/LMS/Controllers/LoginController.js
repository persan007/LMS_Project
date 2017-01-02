(function () {

    var LoginController = function ($scope, Request) {

        var Authenticate = function () {
            //Request.Make("TO", $scope.Credentials).then(function (data) {
            //     TODO: när användaren trycker logga in
            //});
        }

        $scope.Authenticate = Authenticate;
        $scope.Credentials = {
            Username: "",
            Password: ""
        };
    }

    LMSApp.controller("LoginController", [
        "$scope",
        "Request",
        LoginController
    ]);

}());