(function () {

    var FileController = function ($scope, Request) {

        var DoUpload = function (f) {
            var fd = new FormData();
            console.log(f);
            for (var i = 0; i < f.length; i++) {
                console.log("Appending to FormData");
                console.log("File amount: " + f.length);
                fd.append('[' + i + '].File', f[i]);
            }

            $.ajax({
                type: "POST",
                url: '/Home/UploadFiles',
                contentType: false,
                processData: false,
                data: fd,
                enctype: 'multipart/form-data',
                success: function (result) {
                    console.log(result);
                },
                error: function (xhr, status, p3, p4) {
                    var err = "Error " + " " + status + " " + p3 + " " + p4;
                    if (xhr.responseText && xhr.responseText[0] == "{")
                        err = JSON.parse(xhr.responseText).Message;
                    console.log(err);
                }
            });
        }

        var tmpUpload = function (f) {
            var formData = new FormData();
            var totalFiles = f.length;
            for (var i = 0; i < totalFiles; i++) {
                var file = f[i];
                formData.append("FileUpload", file);
            }
            $.ajax({
                type: "POST",
                url: '/Home/UploadFiles',
                data: formData,
                dataType: 'json',
                contentType: false,
                processData: false,
                success: function (response) {
                    alert('succes!!');
                },
                error: function (error) {
                    alert("errror");
                }
            });
        }

        var file = function ()
        { }

        var displayImage = function (filename, element)
        {
            var urlCreator = window.URL || window.webkitURL;
            Request.Make("/Home/GetImageBlobByFileName?fileName=" + filename).then(function (data) {
                var binaryData = [];
                binaryData.push(data);
                console.log(data);
                $scope.url = urlCreator.createObjectURL(new Blob([binaryData], { type: "image/png" }));
            });
        }

        var uploadFile = function(files)
        {
            for (var i = 0; i < files.length; i++)
            {
                var fd = new FormData();
                var blob = new Blob([files[i]], { type: files[i].type });
                fd.append("blob", blob, files[i].filename);
                Request.Make("/Home/UploadFiles/").then(function (data) {
                    console.log(data);
                    console.log(fd);
                })
            }
        }

        $scope.DownloadFile     = file;
        $scope.DisplayImage     = displayImage;
        $scope.UploadFile       = uploadFile;
        $scope.FilesToUpload    = [];
    }

    LMSApp.controller('FileController', [
        '$scope',
        'Request',
        FileController
    ]);

}());
