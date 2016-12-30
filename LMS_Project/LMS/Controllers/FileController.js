(function () {

    var FileController = function ($scope, Request) {

        var displayImage = function (filename)
        {
            Request.Make("/Home/GetUrlByFilename?fileName=" + filename).then(function (data) {
                $scope.url = data;
                console.log(data);
                //var canvas = document.createElement("canvas")
                //var context = canvas.getContext("2d")
                //context.drawImage(img, 0, 0) // i assume that img.src is your blob url
                //var dataurl = canvas.toDataURL("image/png", 1)
            });

            $scope.buttonClicked = true;
        }

        var uploadFile = function(files)
        {
            var fd = new FormData();

            for (var i = 0; i < files.length; i++)
            {
                console.log(files[i]);
                fd.append(files[i].name, files[i]);
            }

            Request.MakeFile("/Home/UploadFiles/", fd).then(function (data) {
                console.log(data);
            });
        }

        var allFileNames = function ()
        {
            Request.Make("/Home/GetAllFilenames/").then(function (data) {
                console.log(data);
            });
        }

        $scope.AllFileNames     = allFileNames;
        $scope.DisplayImage     = displayImage;
        $scope.UploadFile       = uploadFile;
        $scope.FilesToUpload    = [];
        $scope.buttonClicked    = false;
        $scope.url              = "";
    }

    LMSApp.controller('FileController', [
        '$scope',
        'Request',
        FileController
    ]);

}());
