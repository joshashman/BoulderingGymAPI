using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BoulderingGymAPI.Migrations
{
    /// <inheritdoc />
    public partial class RemoveSetByStaffNavigation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Routes_AspNetUsers_SetByStaffId",
                table: "Routes");

            migrationBuilder.DropIndex(
                name: "IX_Routes_SetByStaffId",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "SetByStaffId",
                table: "Routes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SetByStaffId",
                table: "Routes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_SetByStaffId",
                table: "Routes",
                column: "SetByStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Routes_AspNetUsers_SetByStaffId",
                table: "Routes",
                column: "SetByStaffId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
