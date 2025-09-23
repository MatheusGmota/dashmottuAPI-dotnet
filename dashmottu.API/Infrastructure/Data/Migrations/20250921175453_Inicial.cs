using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashmottu.API.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PATIO",
                columns: table => new
                {
                    ID_PATIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    URL_IMAGEM = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIO", x => x.ID_PATIO);
                });

            migrationBuilder.CreateTable(
                name: "ENDERECO",
                columns: table => new
                {
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    CEP = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    LOGRADOURO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NUMERO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BAIRRO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    CIDADE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ESTADO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_PATIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO", x => x.ID_ENDERECO);
                    table.ForeignKey(
                        name: "FK_ENDERECO_PATIO_ID_PATIO",
                        column: x => x.ID_PATIO,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOGIN",
                columns: table => new
                {
                    ID_LOGIN = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USUARIO = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    ID_PATIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGIN", x => x.ID_LOGIN);
                    table.ForeignKey(
                        name: "FK_LOGIN_PATIO_ID_PATIO",
                        column: x => x.ID_PATIO,
                        principalTable: "PATIO",
                        principalColumn: "ID_PATIO",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ENDERECO_ID_PATIO",
                table: "ENDERECO",
                column: "ID_PATIO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LOGIN_ID_PATIO",
                table: "LOGIN",
                column: "ID_PATIO",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LOGIN_USUARIO",
                table: "LOGIN",
                column: "USUARIO",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ENDERECO");

            migrationBuilder.DropTable(
                name: "LOGIN");

            migrationBuilder.DropTable(
                name: "PATIO");
        }
    }
}
