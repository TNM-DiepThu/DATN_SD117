
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
    // lấy id màu bên Sản phẩm chi tiết Detail

    //function changeBackgroundColor(colorName, colorId) {
    //    // Thay đổi nền thành màu xám
    //    document.body.style.backgroundColor = 'gray';

    //    // Lấy thẻ input và cập nhật giá trị ID của màu được chọn
    //    var colorIdTextBox = document.getElementById("colorIdTextBox");
    //    colorIdTextBox.value = colorId;
    //};


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


 


