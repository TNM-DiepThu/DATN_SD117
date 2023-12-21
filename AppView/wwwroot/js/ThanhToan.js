$(document).ready(function () {
    var isInputVisible = false;
    $("#searchButton").click(function () {
        if (isInputVisible) {
            $("#searchContainer").hide();
        } else {
            $("#searchContainer").show();
        }
        isInputVisible = !isInputVisible;
    });
    $("#searchButton1").click(function () {
        if (isInputVisible) {
            $("#searchContainer1").hide();
        } else {
            $("#searchContainer1").show();
        }
        isInputVisible = !isInputVisible;
    });
    $("#searchBox").on("input", function () {
        var searchValue = $(this).val().toLowerCase();
        $("#provinceDropdown option").each(function () {
            if ($(this).text().toLowerCase().indexOf(searchValue) === -1) {
                $(this).hide();
            } else {
                $(this).show();
            }
        });
    });
    $("#searchBox1").on("input", function () {
        var searchValue = $(this).val().toLowerCase();
        $("#districtDropdown option").each(function () {
            if ($(this).text().toLowerCase().indexOf(searchValue) === -1) {
                $(this).hide();
            } else {
                $(this).show();
            }
        });
    });
    $("#provinceDropdown").change(function () {
        var selectedId = $(this).val();
        var textofSelect = document.getElementById("provinceDropdown");
        var selectedOption = textofSelect.options[textofSelect.selectedIndex];;

        var selectedOption = $(this).find("option:selected");
        var url = selectedOption.data("url");
        if (selectedId && url) {
            $.ajax({
                url: url,
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ IdThanhPho: selectedId }),
                success: function (data) {
                    //alert("Lấy ID thành công: " + selectedId);
                    //document.getElementById("searchBox").value = $(this).val();
                    updateDistrictDropdown(data);
                    layDuLieuTuQuanHuyen();
                },
                error: function () {
                    alert("Có lỗi xảy ra");
                }
            });
        }
    });
    $("#districtDropdown").change(function () {
        var selectedId = $(this).val();
        var selectedOption = $(this).find("option:selected");
        var url = selectedOption.data("url");
        if (selectedId && url) {
            $.ajax({
                url: url,
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ IdQuanHuyen: selectedId }),
                success: function (data) {
                    //alert("Lấy ID thành công: " + selectedId);
                    CapNhatXaPhuong(data)
                    layDuLieuTuXaPhuong();
                },
                error: function () {
                    alert("Có lỗi xảy ra");
                }
            });
        }
    });
    $("#WardDropdown").change(function () {
        var selectedId = $(this).val();
        var selectedOption = $(this).find("option:selected");
        var url = selectedOption.data("url");
        if (selectedId && url) {
            $.ajax({
                url: url,
                method: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ IdQuanHuyen: selectedId }),
                success: function (data) {
                    //alert("Lấy ID thành công: " + selectedId);
                    layDuLieuTuXaPhuong();
                    hienthitienship();
                },
                error: function () {
                    alert("Có lỗi xảy ra");
                }
            });
        }
    });
    //$("#voucherCodeInput").on("change", function () {
    //    sendDataToServer();
    //});
});
function hienthitienship() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/HienThiTienShip', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            // Dữ liệu được trả về từ controller
            var tienship = result.tienship;
            if (tienship == null) {
                jQuery("#btnChon").attr("style", "pointer-events: none;");
            } else {
                var hienthitiienship = document.getElementById("tienship");

                var formattedNumber = tienship.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });

                hienthitiienship.innerText = formattedNumber;

                // tổng tiền phải trả
                var tongtien = document.getElementById("tongtien").innerText;
                var number = parseFloat(tongtien.replace(/[^\d]/g, ''));
                var tiemgiamgia = document.getElementById("tiemgiamgia").innerText;
                var tiengiam = parseFloat(tiemgiamgia.replace(/[^\d]/g, ''));
                var number1 = parseFloat(number);
                var number2 = parseFloat(tienship);
                var tiemgiamgia1 = parseFloat(tiengiam);
                var sum = number1 + number2 - tiemgiamgia1;
                var formattedNumber1 = sum.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
                document.getElementById("thanhtien").innerText = formattedNumber1;
                document.getElementById("tientra").value = sum;
                //document.getElementById("tientra").value = formattedNumber1;
                console.log(formattedNumber);
                console.log(formattedNumber1);
                jQuery("#btnChon").attr("style", "pointer-events: auto;");
            }
            
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
}
function hienthiGiaTriVoucher() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/NhanMaVoucher', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            // Dữ liệu được trả về từ controller
            console.log(result);
            var GiaTriVoucher = result.giatrivoucher;
            var hienthiGiaTriVoucher = document.getElementById("GiaTriVoucher");
            hienthiGiaTriVoucher.innerText = GiaTriVoucher + " %";
            var thanhtien = document.getElementById("tientra").value;
            var TongTien = thanhtien - (thanhtien * GiaTriVoucher) / 100; 
            thanhtien = TongTien; 
            var formattedNumber1 = TongTien.toLocaleString('vi-VN', { style: 'currency', currency: 'VND' });
            document.getElementById("thanhtien").innerText = formattedNumber1;
            console.log(thanhtien);
            
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
}

function updateDistrictDropdown(data) {
    // Xóa tất cả các option hiện tại trong dropdown "district"

    $("#districtDropdown").empty();
    $("#districtDropdown").append('<option value="">Chọn Quận huyện: </option>');
    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            $("#districtDropdown").append('<option value="' + data[i].id + '" data-url="' + '/GioHang/NhanIDHuyen?IdQuanHuyen=' + data[i].id + '">' + data[i].name + '</option>');
        }
    }
}
function layDuLieuTuQuanHuyen() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/LayQuanHuyen', // Điều chỉnh đường dẫn tùy thuộc vào định tuyến của bạn
        success: function (result) {
            // Dữ liệu được trả về từ controller
            var thongTinQuanHuyen = result.thongTinQuanHuyen;
            //updateDistrictDropdown(thongTinQuanHuyen)
            console.log(thongTinQuanHuyen);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
}

function layDuLieuTuXaPhuong() {
    $.ajax({
        type: 'GET',
        url: '/GioHang/GetAllXaPhuong', 
        success: function (result) {
            var ThongTinXaPhuong = result.thongTinXaPhuong;
            console.log(ThongTinXaPhuong);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
}
function CapNhatXaPhuong(data) {
    // Xóa tất cả các option hiện tại trong dropdown "district"

    $("#WardDropdown").empty();
    $("#WardDropdown").append('<option value="">Chọn xã phường: </option>');
    if (data != null) {
        for (var i = 0; i < data.length; i++) {
            $("#WardDropdown").append('<option value="' + data[i].wardCode + '" data-url="' + '/GioHang/TinhTienShip?WardCode=' + data[i].wardCode + '">' + data[i].wardName + '</option>');
        }
    }
}
function useVoucher(voucherCode) {
    $('#voucherCodeInput').val(voucherCode);
}
$(document).ready(function () {
    // Bắt sự kiện khi giá trị của input thay đổi

});
function sendDataToServer() {
    // Lấy giá trị từ input
    var voucherCode = $("#voucherCodeInput").val();
    $.ajax({
        type: "POST",
        url: "/GioHang/NhanMaVoucher",
        data: JSON.stringify(voucherCode),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            console.log(voucherCode);
            hienthiGiaTriVoucher();
        },
        error: function (err) {
            // Xử lý lỗi nếu có
        }
    });
}