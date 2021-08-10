using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BankingWebApp.Migrations
{
    public partial class asp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tr",
                columns: table => new
                {
                    TrId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    amount = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    AccountUserUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tr", x => x.TrId);
                    table.ForeignKey(
                        name: "FK_Tr_AccountUser_AccountUserUserId",
                        column: x => x.AccountUserUserId,
                        principalTable: "AccountUser",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tr_AccountUserUserId",
                table: "Tr",
                column: "AccountUserUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tr");
        }
    }
}
