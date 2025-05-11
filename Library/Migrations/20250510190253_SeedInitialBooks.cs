using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "Title", "Quantity" },
                values: new object[,]
                {
                    { 1, "Carl Sagan", "Cosmos", 6 },
                    { 2, "Stephen Hawking", "A Brief History of Time", 5 },
                    { 3, "Neil deGrasse Tyson", "Astrophysics for People in a Hurry", 7 },
                    { 4, "Brian Greene", "The Fabric of the Cosmos", 4 },
                    { 5, "Galileo Galilei", "Sidereus Nuncius (Starry Messenger)", 3 },
                    { 6, "Phil Plait", "Death from the Skies!", 2 },
                    { 7, "Chris Impey", "How It Ends: From You to the Universe", 3 },
                    { 8, "Michio Kaku", "Parallel Worlds", 4 },
                    { 9, "James B. Kaler", "Stars and Their Spectra", 2 },
                    { 10, "Richard Preston", "First Light: The Search for the Edge of the Universe", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValues: new object[]
                {
                    1, 2, 3, 4, 5, 6, 7, 8, 9, 10
                });

        }
    }
}
