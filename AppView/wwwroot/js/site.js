// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

    var selectedImageId = null;
    var selectedImageFilename = null;

    var selectableImages = document.querySelectorAll('.selectable-image');
    var selectImageBtn = document.getElementById('selectImageBtn');

    selectableImages.forEach(function (img) {
        img.addEventListener('click', function () {
            selectedImageId = this.getAttribute('data-image-id');
            selectedImageFilename = this.getAttribute('data-image-filename');
        });
    });

    selectImageBtn.addEventListener('click', function () {
        if (selectedImageId != null && selectedImageFilename != null) {
        document.getElementById('imageId').value = selectedImageId;
    document.getElementById('imageFilename').value = selectedImageFilename;
        }
    });



// Write your JavaScript code.
