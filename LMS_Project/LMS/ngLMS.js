
// Init angular application (global)
var LMSApp = angular.module("LMS-app", []);


// Animate navigation menu
window.onload = function () {
    (function ($) {

        $(".toggle-nav").click(function () {
            $(".toggle-nav.hidden-btn").toggleClass("hide");

            $("#document-nav").toggleClass("col-md-3")
                .toggleClass("col-sm-4")
                .toggleClass("col-xs-9")
                .toggleClass("col-0");

            $("#document-content").toggleClass("col-md-9")
                .toggleClass("col-sm-8")
                .toggleClass("col-xs-12")
                .toggleClass("container");
        });

    })(jQuery);
}