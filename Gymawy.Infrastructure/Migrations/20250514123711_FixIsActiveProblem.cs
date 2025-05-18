using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixIsActiveProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "trainers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "subscriptions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "sessions");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "rooms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "payments");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "participants");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "gyms");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "gym_trainer");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "bookings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Admin");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "trainers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "subscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "sessions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "payments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "participants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "gyms",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "gym_trainer",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Admin",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
