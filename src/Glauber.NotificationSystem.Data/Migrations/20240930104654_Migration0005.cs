using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glauber.NotificationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Migration0005 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "EnableUrlRedirect",
                table: "WelcomeNotification",
                type: "int",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "EnableUrlRedirect",
                table: "WelcomeNotification",
                type: "bit",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }
    }
}
