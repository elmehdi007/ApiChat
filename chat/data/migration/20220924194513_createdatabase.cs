using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chat.data.migration
{
    public partial class createdatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_cnx_status = table.Column<bool>(type: "bit", nullable: false),
                    user_mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_adress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    user_phone = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    user_status = table.Column<int>(type: "int", nullable: false),
                    password_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image_user = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    is_connected = table.Column<bool>(type: "bit", nullable: false),
                    user_birth_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    token = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_users_user_mail",
                table: "users",
                column: "user_mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_user_phone",
                table: "users",
                column: "user_phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
