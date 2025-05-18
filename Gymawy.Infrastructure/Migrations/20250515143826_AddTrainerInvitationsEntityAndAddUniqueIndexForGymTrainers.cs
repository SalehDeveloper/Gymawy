using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gymawy.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTrainerInvitationsEntityAndAddUniqueIndexForGymTrainers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_gym_trainer_GymId",
                table: "gym_trainer");

            migrationBuilder.CreateTable(
                name: "TrainerInvitations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GymId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ExpirationDateUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainerInvitations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainerInvitations_gyms_GymId",
                        column: x => x.GymId,
                        principalTable: "gyms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TrainerInvitations_trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_gym_trainer_GymId_TrainerId",
                table: "gym_trainer",
                columns: new[] { "GymId", "TrainerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TrainerInvitations_GymId",
                table: "TrainerInvitations",
                column: "GymId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainerInvitations_TrainerId",
                table: "TrainerInvitations",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainerInvitations");

            migrationBuilder.DropIndex(
                name: "IX_gym_trainer_GymId_TrainerId",
                table: "gym_trainer");

            migrationBuilder.CreateIndex(
                name: "IX_gym_trainer_GymId",
                table: "gym_trainer",
                column: "GymId");
        }
    }
}
