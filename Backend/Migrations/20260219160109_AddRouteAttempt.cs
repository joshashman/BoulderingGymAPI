using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulderingGymAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRouteAttempt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RouteAttempts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClimbingRouteId = table.Column<int>(type: "INTEGER", nullable: false),
                    AttemptDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RouteAttempts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RouteAttempts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RouteAttempts_Routes_ClimbingRouteId",
                        column: x => x.ClimbingRouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RouteAttempts_ClimbingRouteId",
                table: "RouteAttempts",
                column: "ClimbingRouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RouteAttempts_UserId",
                table: "RouteAttempts",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RouteAttempts");
        }
    }
}
