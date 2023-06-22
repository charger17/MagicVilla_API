using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioMigracion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 22, 10, 26, 53, 724, DateTimeKind.Local).AddTicks(7573), new DateTime(2023, 6, 22, 10, 26, 53, 724, DateTimeKind.Local).AddTicks(7560) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 22, 10, 26, 53, 724, DateTimeKind.Local).AddTicks(7576), new DateTime(2023, 6, 22, 10, 26, 53, 724, DateTimeKind.Local).AddTicks(7575) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5863), new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5851) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5865), new DateTime(2023, 6, 21, 19, 48, 58, 218, DateTimeKind.Local).AddTicks(5865) });
        }
    }
}
