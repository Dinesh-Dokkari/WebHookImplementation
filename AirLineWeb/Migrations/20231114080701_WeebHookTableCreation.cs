using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirLineWeb.Migrations
{
    /// <inheritdoc />
    public partial class WeebHookTableCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebHookSubscriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WebHookURi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Secrect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebHookType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebHookPublisher = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebHookSubscriptions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebHookSubscriptions");
        }
    }
}
