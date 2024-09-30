using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Glauber.NotificationSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apps", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActiveChannels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    WebPush = table.Column<bool>(type: "bit", nullable: false),
                    Email = table.Column<bool>(type: "bit", nullable: false),
                    SMS = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActiveChannels", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActiveChannels_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiverEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailTemplateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Received = table.Column<bool>(type: "tinyint", nullable: false),
                    Opened = table.Column<bool>(type: "tinyint", nullable: false),
                    Clicked = table.Column<bool>(type: "tinyint", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailNotifications_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailSettings_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMSNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSNotifications_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMSSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSSettings_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebPushNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AudienceSegments = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IconUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RedirectUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPushNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPushNotifications_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WebPushSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebPushSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebPushSettings_Apps_AppId",
                        column: x => x.AppId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Uri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplate_EmailSettings_EmailSettingsId",
                        column: x => x.EmailSettingsId,
                        principalTable: "EmailSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sender", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sender_EmailSettings_EmailSettingsId",
                        column: x => x.EmailSettingsId,
                        principalTable: "EmailSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Server",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SmtpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SmtpPort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserLogin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Server", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Server_EmailSettings_EmailSettingsId",
                        column: x => x.EmailSettingsId,
                        principalTable: "EmailSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SMSProvider",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMSSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSProvider", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMSProvider_SMSSettings_SMSSettingsId",
                        column: x => x.SMSSettingsId,
                        principalTable: "SMSSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllowNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AllowButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DenyButtonText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPushSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllowNotification_WebPushSettings_WebPushSettingsId",
                        column: x => x.WebPushSettingsId,
                        principalTable: "WebPushSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebPushSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_WebPushSettings_WebPushSettingsId",
                        column: x => x.WebPushSettingsId,
                        principalTable: "WebPushSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WelcomeNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnableUrlRedirect = table.Column<bool>(type: "tinyint", nullable: false),
                    UrlRedirect = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WebPushSettingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WelcomeNotification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WelcomeNotification_WebPushSettings_WebPushSettingsId",
                        column: x => x.WebPushSettingsId,
                        principalTable: "WebPushSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActiveChannels_AppId",
                table: "ActiveChannels",
                column: "AppId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllowNotification_WebPushSettingsId",
                table: "AllowNotification",
                column: "WebPushSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailNotifications_AppId",
                table: "EmailNotifications",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_EmailSettings_AppId",
                table: "EmailSettings",
                column: "AppId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmailTemplate_EmailSettingsId",
                table: "EmailTemplate",
                column: "EmailSettingsId");

            migrationBuilder.CreateIndex(
                name: "IX_Sender_EmailSettingsId",
                table: "Sender",
                column: "EmailSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Server_EmailSettingsId",
                table: "Server",
                column: "EmailSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Site_WebPushSettingsId",
                table: "Site",
                column: "WebPushSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSNotifications_AppId",
                table: "SMSNotifications",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_SMSProvider_SMSSettingsId",
                table: "SMSProvider",
                column: "SMSSettingsId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SMSSettings_AppId",
                table: "SMSSettings",
                column: "AppId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebPushNotifications_AppId",
                table: "WebPushNotifications",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_WebPushSettings_AppId",
                table: "WebPushSettings",
                column: "AppId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WelcomeNotification_WebPushSettingsId",
                table: "WelcomeNotification",
                column: "WebPushSettingsId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActiveChannels");

            migrationBuilder.DropTable(
                name: "AllowNotification");

            migrationBuilder.DropTable(
                name: "EmailNotifications");

            migrationBuilder.DropTable(
                name: "EmailTemplate");

            migrationBuilder.DropTable(
                name: "Sender");

            migrationBuilder.DropTable(
                name: "Server");

            migrationBuilder.DropTable(
                name: "Site");

            migrationBuilder.DropTable(
                name: "SMSNotifications");

            migrationBuilder.DropTable(
                name: "SMSProvider");

            migrationBuilder.DropTable(
                name: "WebPushNotifications");

            migrationBuilder.DropTable(
                name: "WelcomeNotification");

            migrationBuilder.DropTable(
                name: "EmailSettings");

            migrationBuilder.DropTable(
                name: "SMSSettings");

            migrationBuilder.DropTable(
                name: "WebPushSettings");

            migrationBuilder.DropTable(
                name: "Apps");
        }
    }
}
