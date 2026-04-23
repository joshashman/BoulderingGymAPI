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
    migrationBuilder.Sql(@"
        DROP INDEX IF EXISTS IX_Routes_SetByStaffId;
    ");

    migrationBuilder.Sql(@"
        CREATE TABLE Routes_new (
            Id INTEGER NOT NULL CONSTRAINT PK_Routes PRIMARY KEY AUTOINCREMENT,
            Location TEXT NOT NULL,
            Difficulty TEXT NOT NULL,
            DateSet TEXT NOT NULL,
            StripDate TEXT NULL
        );
        INSERT INTO Routes_new (Id, Location, Difficulty, DateSet, StripDate)
        SELECT Id, Location, Difficulty, DateSet, StripDate FROM Routes;
        DROP TABLE Routes;
        ALTER TABLE Routes_new RENAME TO Routes;
    ");
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
