(function () {

    var NavigationController = function ($scope) {

        var SearchFiler = function () {
            // TODO: Hitta all kurser, lärare mm  som matchar sökresultatet. (Akronym?)
            console.log("Search term: " + $scope.SearchText);
        }
        
        $scope.$watch("SearchText", SearchFiler)
        $scope.SearchText = "";
    }

    LMSApp.controller("NavigationController", [
        "$scope",
        NavigationController
    ]);

}());