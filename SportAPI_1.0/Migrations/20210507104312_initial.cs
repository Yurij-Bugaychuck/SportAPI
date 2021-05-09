using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportAPI.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatsCategories",
                columns: table => new
                {
                    StatsCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsCategories", x => x.StatsCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsExcercisesCategories",
                columns: table => new
                {
                    Workout_excercise_categories_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExcercisesCategories", x => x.Workout_excercise_categories_id);
                });

            migrationBuilder.CreateTable(
                name: "UsersOptions",
                columns: table => new
                {
                    UserOptionsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersOptions", x => x.UserOptionsId);
                    table.ForeignKey(
                        name: "FK_UsersOptions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UsersStats",
                columns: table => new
                {
                    UserStatsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatsCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersStats", x => x.UserStatsId);
                    table.ForeignKey(
                        name: "FK_UsersStats_StatsCategories_StatsCategoryId",
                        column: x => x.StatsCategoryId,
                        principalTable: "StatsCategories",
                        principalColumn: "StatsCategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UsersStats_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    About = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.WorkoutId);
                    table.ForeignKey(
                        name: "FK_Workouts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsExcercises",
                columns: table => new
                {
                    WorkoutExcerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsSet = table.Column<bool>(type: "bit", nullable: false),
                    Order = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    About = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExcercises", x => x.WorkoutExcerciseId);
                    table.ForeignKey(
                        name: "FK_WorkoutsExcercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsOptions",
                columns: table => new
                {
                    WorkoutOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsOptions", x => x.WorkoutOptionId);
                    table.ForeignKey(
                        name: "FK_WorkoutsOptions_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsExcercisesCategory",
                columns: table => new
                {
                    WorkoutExcerciseEategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutExcerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutExcerciseCategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExcercisesCategory", x => x.WorkoutExcerciseEategoryId);
                    table.ForeignKey(
                        name: "FK_WorkoutsExcercisesCategory_WorkoutsExcercises_WorkoutExcerciseId",
                        column: x => x.WorkoutExcerciseId,
                        principalTable: "WorkoutsExcercises",
                        principalColumn: "WorkoutExcerciseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkoutsExcercisesCategory_WorkoutsExcercisesCategories_WorkoutExcerciseCategoriesId",
                        column: x => x.WorkoutExcerciseCategoriesId,
                        principalTable: "WorkoutsExcercisesCategories",
                        principalColumn: "Workout_excercise_categories_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsExcercisesOptions",
                columns: table => new
                {
                    WorkoutExcerciseOptionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkoutExcerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkoutExcerciseId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExcercisesOptions", x => x.WorkoutExcerciseOptionId);
                    table.ForeignKey(
                        name: "FK_WorkoutsExcercisesOptions_WorkoutsExcercises_WorkoutExcerciseId",
                        column: x => x.WorkoutExcerciseId,
                        principalTable: "WorkoutsExcercises",
                        principalColumn: "WorkoutExcerciseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutsExcercisesOptions_WorkoutsExcercises_WorkoutExcerciseId1",
                        column: x => x.WorkoutExcerciseId1,
                        principalTable: "WorkoutsExcercises",
                        principalColumn: "WorkoutExcerciseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true,
                filter: "[Phone] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true,
                filter: "[Username] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsersOptions_UserId",
                table: "UsersOptions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStats_StatsCategoryId",
                table: "UsersStats",
                column: "StatsCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersStats_UserId",
                table: "UsersStats",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExcercises_WorkoutId",
                table: "WorkoutsExcercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExcercisesCategory_WorkoutExcerciseCategoriesId",
                table: "WorkoutsExcercisesCategory",
                column: "WorkoutExcerciseCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExcercisesCategory_WorkoutExcerciseId",
                table: "WorkoutsExcercisesCategory",
                column: "WorkoutExcerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExcercisesOptions_WorkoutExcerciseId",
                table: "WorkoutsExcercisesOptions",
                column: "WorkoutExcerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExcercisesOptions_WorkoutExcerciseId1",
                table: "WorkoutsExcercisesOptions",
                column: "WorkoutExcerciseId1");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsOptions_WorkoutId",
                table: "WorkoutsOptions",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersOptions");

            migrationBuilder.DropTable(
                name: "UsersStats");

            migrationBuilder.DropTable(
                name: "WorkoutsExcercisesCategory");

            migrationBuilder.DropTable(
                name: "WorkoutsExcercisesOptions");

            migrationBuilder.DropTable(
                name: "WorkoutsOptions");

            migrationBuilder.DropTable(
                name: "StatsCategories");

            migrationBuilder.DropTable(
                name: "WorkoutsExcercisesCategories");

            migrationBuilder.DropTable(
                name: "WorkoutsExcercises");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
