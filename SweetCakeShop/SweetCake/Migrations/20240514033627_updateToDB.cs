using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetCake.Migrations
{
    /// <inheritdoc />
    public partial class updateToDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LOAI_SP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOAI_SP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "THONGTIN_NHANHANG",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SDT = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GhiChu = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_THONGTIN_NHANHANG", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SAN_PHAM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mota = table.Column<string>(type: "ntext", nullable: true),
                    TrangThai = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    LoaiSPId = table.Column<int>(type: "int", nullable: false),
                    ThongTin_NhanHangId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SAN_PHAM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SAN_PHAM_LOAI_SP_LoaiSPId",
                        column: x => x.LoaiSPId,
                        principalTable: "LOAI_SP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SAN_PHAM_THONGTIN_NHANHANG_ThongTin_NhanHangId",
                        column: x => x.ThongTin_NhanHangId,
                        principalTable: "THONGTIN_NHANHANG",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ANH",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SanphamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ANH", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ANH_SAN_PHAM_SanphamId",
                        column: x => x.SanphamId,
                        principalTable: "SAN_PHAM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CHI_TIET_SP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Gia = table.Column<int>(type: "int", nullable: true),
                    SoLuong = table.Column<int>(type: "int", nullable: true),
                    NgaySanXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HanSuDung = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SanPhamId = table.Column<int>(type: "int", nullable: false),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CHI_TIET_SP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CHI_TIET_SP_SAN_PHAM_SanPhamId",
                        column: x => x.SanPhamId,
                        principalTable: "SAN_PHAM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ANH_SanphamId",
                table: "ANH",
                column: "SanphamId");

            migrationBuilder.CreateIndex(
                name: "IX_CHI_TIET_SP_SanPhamId",
                table: "CHI_TIET_SP",
                column: "SanPhamId");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_LoaiSPId",
                table: "SAN_PHAM",
                column: "LoaiSPId");

            migrationBuilder.CreateIndex(
                name: "IX_SAN_PHAM_ThongTin_NhanHangId",
                table: "SAN_PHAM",
                column: "ThongTin_NhanHangId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ANH");

            migrationBuilder.DropTable(
                name: "CHI_TIET_SP");

            migrationBuilder.DropTable(
                name: "SAN_PHAM");

            migrationBuilder.DropTable(
                name: "LOAI_SP");

            migrationBuilder.DropTable(
                name: "THONGTIN_NHANHANG");
        }
    }
}
