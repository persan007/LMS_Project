/*
** PopupProvider made by Kristoffer Lindström
** 
** Version 0.2.0
**
** Copyright 2017
**
** --------------- Usage --------------------
** PopupProvider has one method that takes two required parameters and two optional.
** 
** TITLE        The title inside the modal window                                               Default: null
** MESSAGE      The message that goes inside the modal                                          Default: null
** TYPE         The modal window type, see modal types for mor information                      Default: null (blank/empty)
** OPTIONS      Specify extra options for the modal, see modal options for more information     Default: OPTION object values
**
**
** Modal window types :
**  * Ok        - Displays a green checkmark
**  * Error     - Displays a red cross
**  * Warning   - Displays a orange exclamation mark
**  * Info      - Displays a blue information symbol (i)
**  * Question  - Displays a gray question mark
**
**
** Modal window options :
**  * timer         - Starts a countdown timer witch will close after x milliseconds            Default: null
**  * enableCancel  - Shows of hides cancel button                                              Default: false
**  * confirmText   - Confirm button text value                                                 Default: "Confirm"
**  * inputType     - Displays textbox of a certain type, see input types for mor information   Default: null
**  * classes       - Adds extra classes to the modal window                                    Default: "" (no classes)
**
**
** Modal input types
**  * text          - Displays a textbox
**  * email         - Displays a textbox with email validation
**  * password      - Displays a textbox with hidden characters (*)
**  * number        - Displays a number selection box
**  * tel           - Displays a textbox with telephonenumber validation
**  * range         - Displays a slider
**  * textarea      - Displays a textarea
**  * select        - Displays a dropdown list
**  * radio         - Displays a radio button
**  * checkbox      - Displays a checkbox button
**  * file          - Displays a file handler
*/

(function () {

    // The global app name //
    var app = LMSApp;
    
    var PopupProvider = function () {
        var ti, m, ty, opt;

        var types = {
            Ok: "success",
            Error: "Error",
            Warning: "warning",
            Info: "info",
            Question: "question"
        }

        // Message setup //
        var msg = function (TITLE, MESSAGE, TYPE, OPTIONS) {
            ti  = TITLE || null;
            m   = MESSAGE || null;
            ty  = TYPE || null;

            opt = Object.assign({
                timer: null,
                enableCancel: false,
                confirmText: "Confirm",
                inputType: null,
                classes: ""
            }, OPTIONS);

            // Make message popup //
            return DisplayMessage();
        }

        // Display the message //
        function DisplayMessage() {
            return swal({
                title: ti,
                text: m,
                type: ty,
                timer: opt["timer"],
                input: opt["inputType"],
                showCancelButton: opt["enableCancel"],
                confirmButtonText: opt["confirmText"],
                customClass: opt["classes"]
            }).then(function (result) {

                return result;

            }, function (dismiss) {

                return "Closed by " + dismiss;

            }).catch(swal.noop);
        }

        // Provider access values //
        return {
            Message: msg,
            Types: types
        };
    }

    app.factory("Popup", [
        "$http",
        PopupProvider
    ]);

}());