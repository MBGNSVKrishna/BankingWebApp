using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingWebApp.Migrations
{
    public partial class fds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FD",
                columns: table => new
                {
                    FdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FdInvMon = table.Column<double>(type: "float", nullable: false),
                    Month = table.Column<double>(type: "float", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AccountUserUserId = table.Column<int>(type: "int", nullable: true),
                    FdMAmount = table.Column<double>(type: "float", nullable: false),
                    FdInMoney = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FD", x => x.FdId);
                    table.ForeignKey(
                        name: "FK_FD_AccountUser_AccountUserUserId",
                        column: x => x.AccountUserUserId,
                        principalTable: "AccountUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FD_AccountUserUserId",
                table: "FD",
                column: "AccountUserUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FD");
        }
    }
}
