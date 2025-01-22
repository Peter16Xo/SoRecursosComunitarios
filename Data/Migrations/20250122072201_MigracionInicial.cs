using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Herramienta",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Disponibilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Herramienta", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Instalacion",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacidad = table.Column<int>(type: "int", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraFin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponibilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instalacion", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RecursoAfectado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cedula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReservasHerramientas",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario_ID = table.Column<int>(type: "int", nullable: false),
                    Herramienta_ID = table.Column<int>(type: "int", nullable: false),
                    Dia = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    HoraInicio = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraFin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Disponibilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservasHerramientas", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReservasHerramientas_Herramienta_Herramienta_ID",
                        column: x => x.Herramienta_ID,
                        principalTable: "Herramienta",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservasHerramientas_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReservasInstalaciones",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Usuario_ID = table.Column<int>(type: "int", nullable: false),
                    Instalacion_ID = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: true),
                    Disponibilidad = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservasInstalaciones", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReservasInstalaciones_Instalacion_Instalacion_ID",
                        column: x => x.Instalacion_ID,
                        principalTable: "Instalacion",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservasInstalaciones_Usuarios_Usuario_ID",
                        column: x => x.Usuario_ID,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReservasHerramientas_Herramienta_ID",
                table: "ReservasHerramientas",
                column: "Herramienta_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasHerramientas_Usuario_ID",
                table: "ReservasHerramientas",
                column: "Usuario_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasInstalaciones_Instalacion_ID",
                table: "ReservasInstalaciones",
                column: "Instalacion_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReservasInstalaciones_Usuario_ID",
                table: "ReservasInstalaciones",
                column: "Usuario_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "ReservasHerramientas");

            migrationBuilder.DropTable(
                name: "ReservasInstalaciones");

            migrationBuilder.DropTable(
                name: "Herramienta");

            migrationBuilder.DropTable(
                name: "Instalacion");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
