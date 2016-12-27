(function () {

    var FR = function () {

        var load = function (scope, element, attributes) {
            element.bind("change", function (changeEvent) {
                //var reader = new FileReader();

                //reader.onload = function (loadEvent) {
                //    scope.$apply(function () {
                //        scope.filereader = loadEvent.target.result;
                //    });
                //}

                //reader.readAsDataURL(changeEvent.target.files[0]);

                scope.filereader = changeEvent.target.files;
            });
        }

        return {
            scope: {
                filereader: "="
            },
            link: load
        };
    }

    LMSApp.directive("filereader", [FR]);

}());