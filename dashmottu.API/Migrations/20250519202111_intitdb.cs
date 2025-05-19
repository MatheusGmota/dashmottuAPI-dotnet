using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dashmottu.API.Migrations
{
    /// <inheritdoc />
    public partial class intitdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    ESTADO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ENDERECO", x => x.ID_ENDERECO);
                });

            migrationBuilder.CreateTable(
                name: "LOGIN",
                columns: table => new
                {
                    ID_LOGIN = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    USUARIO = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    SENHA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOGIN", x => x.ID_LOGIN);
                });

            migrationBuilder.CreateTable(
                name: "PATIO",
                columns: table => new
                {
                    ID_PATIO = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    ID_LOGIN = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ID_ENDERECO = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    URL_IMG_PLANTA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIO", x => x.ID_PATIO);
                });
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
