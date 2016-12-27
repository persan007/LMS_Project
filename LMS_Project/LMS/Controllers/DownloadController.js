(function () {
    var DownloadController = function ($scope, Request) {

        var blobToFile = function (theBlob, fileName){
            theBlob.lastModifiedDate = new Date();
            theBlob.name = fileName;

            $scope.content = theBlob.url;

            return theBlob;
        }

        var file = blobToFile(
            $.ajax({
                type: "POST",
                url: '/Home/DownloadFiles',
                fileName: 'ppap.png',
                success: function (result) {
                    console.log(result);
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] == "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                }
            }),
            'ppap.png'
        );


        $scope.DownloadFile = file;
    }

    LMSApp.controller('DownloadController', [
        '$scope',
        'Request',
        DownloadController
    ]);

}());