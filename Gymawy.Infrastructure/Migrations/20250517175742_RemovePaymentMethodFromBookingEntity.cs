using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemovePaymentMethodFromBookingEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
