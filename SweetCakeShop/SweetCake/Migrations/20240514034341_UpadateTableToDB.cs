using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetCake.Migrations
{
    /// <inheritdoc />
    public partial class UpadateTableToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SAN_PHAM_THONGTIN_NHANHANG_ThongTin_NhanHangId",
                table: "SAN_PHAM");

            migrationBuilder.DropIndex(
                name: "IX_SAN_PHAM_ThongTin_NhanHangId",
                table: "SAN_PHAM");

            migrationBuilder.DropColumn(
                name: "ThongTin_NhanHangId",
                table: "SAN_PHAM");

            migrationBuilder.AddColumn<int>(
                name: "SanPhamId",
                table: "THONGTIN_NHANHANG",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SanPhamId",
                table: "THONGTIN_NHANHANG");

            migrationBuilder.AddColumn<int>(
                name: "ThongTin_NhanHangId",
                table: "SAN_PHAM",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_ThongTin_NhanHangId",
                table: "SAN_PHAM",
                column: "ThongTin_NhanHangId");

            migrationBuilder.AddForeignKey(
                name: "FK_SAN_PHAM_THONGTIN_NHANHANG_ThongTin_NhanHangId",
                table: "SAN_PHAM",
                column: "ThongTin_NhanHangId",
                principalTable: "THONGTIN_NHANHANG",
                principalColumn: "Id");
        }
    }
}
