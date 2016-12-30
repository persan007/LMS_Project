(function () {

    var image = function () {

        var bind = function (scope, element, attributes) {
            scope.$watch("bgImage", function (url) {
                element.css({
                    'background-image': 'url(' + url + ')',
                    'background-size': 'cover'
                });
            });
        };

        return {
            scope: {
                bgImage: "="
            },
            link: bind
        };

    }

    LMSApp.directive("bgImage", [image]);

}());