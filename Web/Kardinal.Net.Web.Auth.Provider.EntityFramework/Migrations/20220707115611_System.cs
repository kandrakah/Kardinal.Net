using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kardinal.Net.Web.Auth.Provider.EntityFramework.Migrations
{
    public partial class System : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "stm");

            migrationBuilder.CreateTable(
                name: "PermissionsGroups",
                schema: "stm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsRestricted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSION_GROUP_ID", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProtectionKeys",
                schema: "stm",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendlyName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Xml = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROTECTION_KEY_ID", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                schema: "stm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERMISSION_ID", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PERMISSION_GROUP_PERMISSIONS",
                        column: x => x.GroupId,
                        principalSchema: "stm",
                        principalTable: "PermissionsGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_PERMISSION_GROUP_ID",
                schema: "stm",
                table: "Permissions",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IDX_PERMISSION_KEY",
                schema: "stm",
                table: "Permissions",
                column: "Key",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions",
                schema: "stm");

            migrationBuilder.DropTable(
                name: "ProtectionKeys",
                schema: "stm");

            migrationBuilder.DropTable(
                name: "PermissionsGroups",
                schema: "stm");
        }
    }
}
