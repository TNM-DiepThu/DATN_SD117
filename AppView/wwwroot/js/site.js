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

document.getElementById("statusSelect").addEventListener("change", function () {
    var selectedValue = this.value; // Lấy giá trị được chọn trong select
    document.getElementById("selectedStatus").value = selectedValue; // Đặt giá trị vào TextBoxFor
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



// Write your JavaScript code.
