$(function () {
    //Initialize Select2 Elements
    $('.select2bs4').select2({
        theme: 'bootstrap4'
    });
});
// Lắng nghe sự kiện khi giá trị thay đổi trong select
var selectElement = document.getElementById("SelectedStatus");

// Lắng nghe sự kiện khi giá trị thay đổi
selectElement.addEventListener("change", function () {
    // Lấy giá trị đã chọn và chuyển đổi thành số (int)
    var selectedValue = parseInt(selectElement.value);

    // In giá trị đã chuyển đổi vào console để kiểm tra
document.getElementById("selectedStatus").value = selectedValue;