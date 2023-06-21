using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AgregarNumeroVillaTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NumeroVillas",
                columns: table => new
                {
                    VillaNo = table.Column<int>(type: "int", nullable: false),
                    VillaId = table.Column<int>(type: "int", nullable: false),
                    DetallesEspeciales = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumeroVillas", x => x.VillaNo);
                    table.ForeignKey(
                        name: "FK_NumeroVillas_Villas_VillaId",
                        column: x => x.VillaId,
                        principalTable: "Villas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4835), new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4825) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4837), new DateTime(2023, 6, 21, 17, 18, 9, 86, DateTimeKind.Local).AddTicks(4837) });

            migrationBuilder.CreateIndex(
                name: "IX_NumeroVillas_VillaId",
                table: "NumeroVillas",
                column: "VillaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NumeroVillas");

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 20, 16, 40, 8, 156, DateTimeKind.Local).AddTicks(3618), new DateTime(2023, 6, 20, 16, 40, 8, 156, DateTimeKind.Local).AddTicks(3608) });

            migrationBuilder.UpdateData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaActualiazcion", "FechaCreacion" },
                values: new object[] { new DateTime(2023, 6, 20, 16, 40, 8, 156, DateTimeKind.Local).AddTicks(3621), new DateTime(2023, 6, 20, 16, 40, 8, 156, DateTimeKind.Local).AddTicks(3621) });
        }
    }
}
