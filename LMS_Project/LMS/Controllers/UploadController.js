//(function () {

//    LMSApp.controller('UploadCtrl', ['$scope', function ($scope) {
//        $scope.uploadFile = function (files) {
//            console.log("HEJ");
//            file.upload = Upload.upload({
//                url: 'Home/UploadFiles',
//                data: { file: files }
//            })
//        }
//    }]);

//})();



////url: '../../Resources/Img/Logo/Koop-Forum-logo-small.png',
(function () {

    var UploadController = function ($scope, Request) {
        
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

        $scope.UploadFile = tmpUpload;
        $scope.FilesToUpload = [];
    }

    LMSApp.controller('UploadController', [
        '$scope',
        'Request',
        UploadController
    ]);

}());