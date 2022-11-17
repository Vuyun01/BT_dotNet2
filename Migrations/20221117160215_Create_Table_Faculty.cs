using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TranBaVuBTH2.Migrations
{
    public partial class Create_Table_Faculty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FacultyID",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Faculties",
                columns: table => new
                {
                    FacultyID = table.Column<string>(type: "TEXT", nullable: false),
                    FacultyName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculties", x => x.FacultyID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_FacultyID",
                table: "Students",
                column: "FacultyID");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students",
                column: "FacultyID",
                principalTable: "Faculties",
                principalColumn: "FacultyID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Faculties_FacultyID",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Faculties");

            migrationBuilder.DropIndex(
                name: "IX_Students_FacultyID",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyID",
                table: "Students");
        }
    }
}
