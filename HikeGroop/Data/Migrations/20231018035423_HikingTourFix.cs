using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HikeGroop.Data.Migrations
{
    /// <inheritdoc />
    public partial class HikingTourFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HikingTourType",
                table: "Destinations",
                newName: "HikingTour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HikingTour",
                table: "Destinations",
                newName: "HikingTourType");
        }
    }
}
