(function () {

    var AddToClassController = function ($scope) {

        var addToClass = function (student) {
            if ($scope.SelectedClass) {
                var index = $scope.Students.indexOf(student);

                if (index != -1)
                    $scope.Students.splice(index, 1);

                $scope.SelectedClass.Students.push(student)
            }
            else {
                alert("Please select a class first!");
            }
        }

        var removeFromClass = function (student) {
            var index = $scope.SelectedClass.Students.indexOf(student);
            if (index != -1)
                $scope.SelectedClass.Students.splice(index, 1);

            $scope.Students.push(student);
        }

        var selectClass = function (cls) {
            $scope.SelectedClass = cls;
        }

        $scope.Add = addToClass;
        $scope.Remove = removeFromClass;
        $scope.SelectCLass = selectClass;

        $scope.SelectedClass;
        $scope.Classes = [
            {
                ID: "sddfgdgfdgdf",
                Name: "TE6B",
                Students: []
            },
            {
                ID: "sddfgsdhfggfdgdf",
                Name: "NA3E",
                Students: []
            },
            {
                ID: "dfhgth5y34ertre",
                Name: "BF1A",
                Students: []
            },
            {
                ID: "uer7e8yueryhr87he",
                Name: "BF1B",
                Students: []
            }
        ];
        $scope.Students = [
            {
                Firstname: "Lars",
                Lastname: "Gunnarsson",
                SSN: "943793834895"
            },
            {
                Firstname: "Jomi",
                Lastname: "Gunnarsson",
                SSN: "89546875487"
            },
            {
                Firstname: "Linnéa",
                Lastname: "Andersson",
                SSN: "2134213443"
            },
            {
                Firstname: "Fia",
                Lastname: "Nilsson",
                SSN: "883488478"
            },
        ];
    }

    LMSApp.controller("AddToClassController", [
        "$scope",
        AddToClassController
    ]);

})();