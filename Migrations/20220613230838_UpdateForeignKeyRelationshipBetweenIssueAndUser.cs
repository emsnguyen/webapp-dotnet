using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
    public partial class UpdateForeignKeyRelationshipBetweenIssueAndUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_CreatedBy",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Issues",
                newName: "CreatedById");

            migrationBuilder.RenameColumn(
                name: "AssignedTo",
                table: "Issues",
                newName: "AssignedToId");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_CreatedBy",
                table: "Issues",
                newName: "IX_Issues_CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issues",
                column: "AssignedToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_AssignedToId",
                table: "Issues",
                column: "AssignedToId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_CreatedById",
                table: "Issues",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_AssignedToId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_CreatedById",
                table: "Issues");

            migrationBuilder.DropIndex(
                name: "IX_Issues_AssignedToId",
                table: "Issues");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Issues",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "AssignedToId",
                table: "Issues",
                newName: "AssignedTo");

            migrationBuilder.RenameIndex(
                name: "IX_Issues_CreatedById",
                table: "Issues",
                newName: "IX_Issues_CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_CreatedBy",
                table: "Issues",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
