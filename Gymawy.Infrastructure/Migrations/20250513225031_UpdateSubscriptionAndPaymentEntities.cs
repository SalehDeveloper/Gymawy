using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSubscriptionAndPaymentEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "ReceiptUrl",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "StripePaymentIntentId",
                table: "payments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeCustomerId",
                table: "subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeSubscriptionId",
                table: "subscriptions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptUrl",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StripePaymentIntentId",
                table: "payments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
