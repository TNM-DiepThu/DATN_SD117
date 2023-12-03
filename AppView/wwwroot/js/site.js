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

$(document).ready(function () {
    // Assuming districts is a JavaScript array containing your data
    var districts = @Html.Raw(Json.Serialize(ViewBag.thongtinhuyen));

    // Get the select element
    var districtDropdown = $("#districtDropdown");

    // Clear existing options
    districtDropdown.empty();

    // Add default option
    districtDropdown.append("<option value=''>Chọn Quận huyện: </option>");

    // Populate dropdown with data from districts
    $.each(districts, function (index, district) {
        var option = $("<option>")
            .attr("value", district.ID)
            .attr("data-url", "@Url.Action("ThanhToan", "GioHang", new { IdQuanHuyen = "district.ID" })")
            .text(district.Name);
        districtDropdown.append(option);
    });
});
// Write your JavaScript code.
