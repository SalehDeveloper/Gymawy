using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixMaxGymType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "MaxGyms",
                table: "subscriptions",
                type: "INT",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "DECIMAL(10,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "MaxGyms",
                table: "subscriptions",
                type: "DECIMAL(10,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INT");
        }
    }
}
