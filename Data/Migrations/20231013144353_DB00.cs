using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AppData.Migrations
{
    public partial class DB00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voucherDetail_AspNetUsers_IdVoucher",
                table: "voucherDetail");

            migrationBuilder.RenameColumn(
                name: "IdNhuoiDung",
                table: "voucherDetail",
                newName: "IdNguoiDung");

            migrationBuilder.RenameColumn(
                name: "GiaTriVouchet",
                table: "Voucher",
                newName: "GiaTriVoucher");

            migrationBuilder.AlterColumn<int>(
                name: "SoLuong",
                table: "hoaDonChiTiets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)");

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "hoaDonChiTiets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_voucherDetail_IdNguoiDung",
                table: "voucherDetail",
                column: "IdNguoiDung");

            migrationBuilder.AddForeignKey(
                name: "FK_voucherDetail_AspNetUsers_IdNguoiDung",
                table: "voucherDetail",
                column: "IdNguoiDung",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_voucherDetail_AspNetUsers_IdNguoiDung",
                table: "voucherDetail");

            migrationBuilder.DropIndex(
                name: "IX_voucherDetail_IdNguoiDung",
                table: "voucherDetail");

            migrationBuilder.DropColumn(
                name: "status",
                table: "hoaDonChiTiets");

            migrationBuilder.RenameColumn(
                name: "IdNguoiDung",
                table: "voucherDetail",
                newName: "IdNhuoiDung");

            migrationBuilder.RenameColumn(
                name: "GiaTriVoucher",
                table: "Voucher",
                newName: "GiaTriVouchet");

            migrationBuilder.AlterColumn<string>(
                name: "SoLuong",
                table: "hoaDonChiTiets",
                type: "nvarchar(100)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_voucherDetail_AspNetUsers_IdVoucher",
                table: "voucherDetail",
                column: "IdVoucher",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
