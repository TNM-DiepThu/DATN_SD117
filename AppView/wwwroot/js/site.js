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


$(document).ready(function () {
    $("#myDropdown").select2();
});
$(function () {
    //Initialize Select2 Elements
    $('.select2').select2()

    //Initialize Select2 Elements
    $('.select2bs4').select2({
        theme: 'bootstrap4'
    })
});
$(document).ready(function () {
    $("#colorNameTextBox").change(function () {
        var idcolor = $('#colorNameTextBox').val(selectedColorName);;
        $.ajax({
            url: "QuanTri/IDmausac",
            method: "POST",
            contentType: "application/json; charset=utf-8",
            data: { idcolor: idcolor },
            success: function (data) {
                if (data == "success") {
                    alert("Lấy ID thành công: " + idcolor);
                } else {
                    alert("Lấy ID thất bại");
                }
            },
            error: function () {
                alert("Có lỗi xảy ra");
            }
        })
    });
});


// Write your JavaScript code.
