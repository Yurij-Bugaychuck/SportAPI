using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SportAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatsCategories",
                columns: table => new
                {
                    StatsCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatsCategories", x => x.StatsCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsExerciseCategory",
                columns: table => new
                {
                    WorkoutExerciseCategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExerciseCategory", x => x.WorkoutExerciseCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "UsersOptions",
                columns: table => new
                {
                    UserOptionsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                    UserStatsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StatsCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
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
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    About = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsPublished = table.Column<bool>(type: "boolean", nullable: false)
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
                name: "WorkoutsExercises",
                columns: table => new
                {
                    WorkoutExerciseId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: true),
                    IsSet = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: true),
                    Repeats = table.Column<int>(type: "integer", nullable: true),
                    Calories = table.Column<int>(type: "integer", nullable: true),
                    Sets = table.Column<int>(type: "integer", nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: true),
                    Weight = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    About = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkoutExerciseCategoryId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExercises", x => x.WorkoutExerciseId);
                    table.ForeignKey(
                        name: "FK_WorkoutsExercises_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "WorkoutId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutsExercises_WorkoutsExerciseCategory_WorkoutExerciseC~",
                        column: x => x.WorkoutExerciseCategoryId,
                        principalTable: "WorkoutsExerciseCategory",
                        principalColumn: "WorkoutExerciseCategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutsOptions",
                columns: table => new
                {
                    WorkoutOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutId = table.Column<Guid>(type: "uuid", nullable: false),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
                name: "WorkoutsExercisesOptions",
                columns: table => new
                {
                    WorkoutExerciseOptionId = table.Column<Guid>(type: "uuid", nullable: false),
                    WorkoutExerciseId = table.Column<Guid>(type: "uuid", nullable: true),
                    Key = table.Column<string>(type: "text", nullable: true),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WorkoutExerciseId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutsExercisesOptions", x => x.WorkoutExerciseOptionId);
                    table.ForeignKey(
                        name: "FK_WorkoutsExercisesOptions_WorkoutsExercises_WorkoutExerciseI~",
                        column: x => x.WorkoutExerciseId1,
                        principalTable: "WorkoutsExercises",
                        principalColumn: "WorkoutExerciseId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkoutsExercisesOptions_WorkoutsExercises_WorkoutExerciseId",
                        column: x => x.WorkoutExerciseId,
                        principalTable: "WorkoutsExercises",
                        principalColumn: "WorkoutExerciseId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Phone",
                table: "Users",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);

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
                name: "IX_WorkoutsExercises_WorkoutExerciseCategoryId",
                table: "WorkoutsExercises",
                column: "WorkoutExerciseCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExercises_WorkoutId",
                table: "WorkoutsExercises",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExercisesOptions_WorkoutExerciseId",
                table: "WorkoutsExercisesOptions",
                column: "WorkoutExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutsExercisesOptions_WorkoutExerciseId1",
                table: "WorkoutsExercisesOptions",
                column: "WorkoutExerciseId1");

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
                name: "WorkoutsExercisesOptions");

            migrationBuilder.DropTable(
                name: "WorkoutsOptions");

            migrationBuilder.DropTable(
                name: "StatsCategories");

            migrationBuilder.DropTable(
                name: "WorkoutsExercises");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "WorkoutsExerciseCategory");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
