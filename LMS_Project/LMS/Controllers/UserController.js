(function () {
    var UserController = function ($scope, $http, Request) {
        
        var getAntiForgeryToken = function () {
            Request.Make("/Account/GetAntiForgeryToken/").then(function (data) {
                $scope.token = data;
            });
        }

        var getAllRoles = function () {
            Request.Make("/Home/GetAllRoleNames").then(function (data) {
                console.log(data);
                $scope.roles = data;
                $scope.user.selectedName = data[0];
            });
        }

        $scope.user = {};
        console.log($scope.user);
        $scope.sendForm = function () {
            $http({
                method: 'POST',
                url: '/Account/Register',
                data: $scope.user,
                headers: {
                    'RequestVerificationToken': $scope.antiForgeryToken
                }
            }).success(function (data, status, headers, config) {
                $scope.message = '';
                if (data.success == false) {
                    var str = '';
                    for (var error in data.errors) {
                        str += data.errors[error] + '\n';
                    }
                    $scope.message = str;
                }
                else {
                    $scope.message = 'Saved Successfully';
                    $scope.person = {};
                }
            }).error(function (data, status, headers, config) {
                $scope.message = 'Unexpected Error';
            });
        };

        //formSubmit.run(['$http', function ($http) {
        //    $http.defaults.headers.common['RequestVerificationToken'] = angular.element("body").attr('ncg-request-verification-token');
        //}]);

        getAllRoles();
        //$scope.GetAllRoles = getAllRoles;
        //$scope.GetAntiForgeryToken = getAntiForgeryToken;
        getAntiForgeryToken();
    }
    LMSApp.controller("UserController", [
        "$scope",
        "$http",
        "Request",
        UserController
    ]);
}());