using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportAPI.Migrations
{
    public partial class AddStartFromToWorkout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutsExercises_Workouts_WorkoutId",
                table: "WorkoutsExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutsOptions_Workouts_WorkoutId",
                table: "WorkoutsOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutsOptions_WorkoutId",
                table: "WorkoutsOptions");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutsExercises_WorkoutId",
                table: "WorkoutsExercises");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Workouts",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AddColumn<DateTime>(
                name: "StartFrom",
                table: "Workouts",
                type: "timestamp without time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartFrom",
                table: "Workouts");

            migrationBuilder.AlterColumn<bool>(
                name: "IsPublished",
                table: "Workouts",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsOptions_WorkoutId",
                table: "WorkoutsOptions",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExercises_WorkoutId",
                table: "WorkoutsExercises",
                column: "WorkoutId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutsExercises_Workouts_WorkoutId",
                table: "WorkoutsExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutsOptions_Workouts_WorkoutId",
                table: "WorkoutsOptions",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "WorkoutId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
