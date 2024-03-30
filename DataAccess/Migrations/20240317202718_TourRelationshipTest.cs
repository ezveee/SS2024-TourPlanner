using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class TourRelationshipTest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "tourid",
                table: "tourlogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tourlogs_tourid",
                table: "tourlogs",
                column: "tourid");

            migrationBuilder.AddForeignKey(
                name: "FK_tourlogs_tours_tourid",
                table: "tourlogs",
                column: "tourid",
                principalTable: "tours",
                principalColumn: "tourid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tourlogs_tours_tourid",
                table: "tourlogs");

            migrationBuilder.DropIndex(
                name: "IX_tourlogs_tourid",
                table: "tourlogs");

            migrationBuilder.DropColumn(
                name: "tourid",
                table: "tourlogs");
        }
    }
}
