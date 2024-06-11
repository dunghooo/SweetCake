using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SweetCake.Migrations
{
	/// <inheritdoc />
	public partial class AddPaymentMethodToDonHang : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentMethod",
                table: "DON_HANG",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "DON_HANG");
        }
    }
}
