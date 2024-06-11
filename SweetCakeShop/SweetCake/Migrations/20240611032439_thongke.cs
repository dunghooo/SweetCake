using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetCake.Migrations
{
    /// <inheritdoc />
    public partial class thongke : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ThongKeDoanhThu",
                columns: table => new
                {
                    Thang = table.Column<int>(type: "int", nullable: false),
                    Nam = table.Column<int>(type: "int", nullable: false),
                    DoanhThu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "ThongKeDoanhThuTheoNgay",
                columns: table => new
                {
                    Thang = table.Column<int>(type: "int", nullable: false),
                    DoanhThu = table.Column<int>(type: "int", nullable: false),
                    Ngay = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThongKeDoanhThu");

            migrationBuilder.DropTable(
                name: "ThongKeDoanhThuTheoNgay");
        }
    }
}
