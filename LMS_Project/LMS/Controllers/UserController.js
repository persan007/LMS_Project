(function () {
    var UserController = function ($scope, Request) {

        var sendForm = function () {
            console.log($scope.user);
            Request.Make("/Account/GetAntiForgeryToken/", "post").then(function (data) {
                Request.Make("/Account/Register/", "post", JSON.stringify($scope.user), null, { 'RequestVerificationToken': data });
            });
        }

        Request.Make("/Home/GetAllRoleNames", "get").then(function (data) {
            $scope.user.UserRole = data[0].Id.toString();
            $scope.roles = data;
        });

        

        $scope.user = {
            Firstname: "Klas",
            Lastname: "Claywuald",
            SSN: "456456564",
            Phonenumber: "564654564",
            Email: "test2@test.com",
            Password: "Test@123",
            ConfirmPassword: "Test@123",
            UserRole: null
        };

        
        $scope.SendForm = sendForm;
    }
    LMSApp.controller("UserController", [
        "$scope",
        "Request",
        UserController
    ]);
}());