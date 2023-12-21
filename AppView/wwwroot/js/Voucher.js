function LayNguoiDungChoVoucher() {
    $.ajax({
        type: 'GET',
        url: '/NguoiDung/NguoiDungTangVoucher',
        success: function (data) {
            var items = data.response;
            console.log(NguoiDung);
            var content = $("#tableNguoiDung").val();
            content += '<table>';
            content += '<thead>';
            content += '<tr>';
            content += '<th><input type="checkbox" id="CheckAll" /></th>';
            content += '<th>ID</th>';
            content += '<th>Tên người dùng</th>';
            content += '<th>Email</th>';
            content += '<th>Ngày sinh</th>';
            content += '<th>Trạng thái</th>';
            content += '</tr>';
            content += '</thead>';
            content += '<tbody>';
            if (data != null) {
                jQuery.each(items, function (idx, item) {
                    content += '<td><input type="checkbox" data-id="' + item.id + '"/></td>';
                    content += '<td>' + item.id + '</td>';
                    content += '<td>' + item.tenNguoiDung + '</td>';
                    content += '<td>' + item.email + '</td>';
                    content += '<td>' + item.ngaySinh + '</td>';
                    if (item.status == 0) {
                        content += '<td>Không hoạt động</td>';
                    } else if (item.status == 1) {
                        content += '<td>Hoạt động</td>';
                    }
                });
            }
            content += '</tbody>';
            content += '</table>';
            jQuery('body').append(content);
        },
        error: function (error) {
            console.error('Lỗi khi lấy dữ liệu từ Session:', error);
        }
    });
}