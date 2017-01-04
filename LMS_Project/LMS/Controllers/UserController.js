(function () {
    var UserController = function ($scope, Request) {

        var sendForm = function (isValid) {
            if (isValid) {
                alert('our form is amazing');
                console.log($scope.user);
                Request.Make("/Account/GetAntiForgeryToken/", "post").then(function (token) {
                    Request.Make("/Account/Register/", "post", JSON.stringify($scope.user), null, { 'RequestVerificationToken': token }).then(function (info) {
                        if (Request.Status != 200) {
                            console.log("Error: " + info);
                        }
                        else {
                            console.log("All good");
                        }
                    });
                });
            }
            else
            {
                alert('The form is missing some stuff');
            }

             //DummyUser
             //Request.Make("/Account/GetAntiForgeryToken/", "post").then(function (data) {
             //    Request.Make("/Account/Register/", "post", JSON.stringify(DummyUser), null, { 'RequestVerificationToken': data }).then(function (data) {
             //        console.log(data);
             //    });
             //});
        }

        Request.Make("/Home/GetAllRoleNames/", "get").then(function (data) {
            $scope.user.UserRole = data[0];
            $scope.roles = data;
        });

        

        $scope.user = {};

        //var DummyUser = {
        //    Firstname: "Klas",
        //    Lastname: "Claywuald",
        //    SSN: "456456564",
        //    Phonenumber: "564654564",
        //    Email: "test2@test.com",
        //    Password: "Test@123",
        //    ConfirmPassword: "Test@123",
        //    UserRole: "Teacher"
        //};

        
        $scope.SendForm = sendForm;
    }
    LMSApp.controller("UserController", [
        "$scope",
        "Request",
        UserController
    ]);
}());