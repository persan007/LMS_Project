
// Init angular application (global)
var LMSApp = angular.module("LMS-app", ['ngRoute']);

// Init angular routing
LMSApp.config(["$routeProvider", function ($routeProvider) {
    var basePath = "/Resources/Templates/";

    $routeProvider
        .when("/", {
            templateUrl: basePath + "_main.html"
        })
        .when("/User/Add/", {
            templateUrl: basePath + "_newUser.html"
        })
        .when("/Schedule/", {
            templateUrl: basePath + "_schedule.html"
        })
        .otherwise({
            templateUrl: basePath + "_404.html"
        });
}]);


// Animate navigation menu
window.onload = function () {
    (function ($) {
        var openState = true;

        function checkState() {
            var w = $(window).width();

            if (w <= 770 && openState)
                toggleViewState();
            else if (w > 770 && !openState)
                toggleViewState();
        }

        function toggleViewState() {
            openState = !openState;

            $(".toggle-nav.hidden-btn").toggleClass("hide").promise().done(function () {
                $("#document-nav").toggleClass("col-md-3").toggleClass("col-sm-4").toggleClass("col-xs-9").toggleClass("col-0").promise().done(function () {

                    $("#document-content").toggleClass("col-md-9").toggleClass("col-sm-8").toggleClass("col-xs-12").toggleClass("container-fluid");

                    if ($(".toggle-nav.hidden-btn").hasClass("hide"))
                        $("#document-content-wrapper").toggleClass("container");
                    else
                        $("#document-content-wrapper").delay(1000).toggleClass("container");

                });
            });
        }

        $(".toggle-nav").click(function () { toggleViewState(); });
        $(window).resize(function () { checkState(); });

        checkState();
    })(jQuery);
}