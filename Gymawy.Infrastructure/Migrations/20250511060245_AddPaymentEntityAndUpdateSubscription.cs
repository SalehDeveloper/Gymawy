using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentEntityAndUpdateSubscription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admin_subscriptions_SubscriptionId",
                table: "Admin");

            migrationBuilder.DropIndex(
                name: "IX_Admin_SubscriptionId",
                table: "Admin");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "subscriptions",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "subscriptions",
                type: "DATETIME",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "subscriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SubscriptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "DECIMAL(10,2)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StripePaymentIntentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiptUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_payments_subscriptions_SubscriptionId",
                        column: x => x.SubscriptionId,
                        principalTable: "subscriptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_subscriptions_AdminId",
                table: "subscriptions",
                column: "AdminId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_payments_SubscriptionId",
                table: "payments",
                column: "SubscriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_subscriptions_Admin_AdminId",
                table: "subscriptions",
                column: "AdminId",
                principalTable: "Admin",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_subscriptions_Admin_AdminId",
                table: "subscriptions");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropIndex(
                name: "IX_subscriptions_AdminId",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "StripeCustomerId",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "StripeSubscriptionId",
                table: "subscriptions");

            migrationBuilder.CreateIndex(
                name: "IX_Admin_SubscriptionId",
                table: "Admin",
                column: "SubscriptionId",
                unique: true,
                filter: "[SubscriptionId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Admin_subscriptions_SubscriptionId",
                table: "Admin",
                column: "SubscriptionId",
                principalTable: "subscriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
