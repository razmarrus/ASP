using Microsoft.EntityFrameworkCore.Migrations;

namespace PointSystem.Data.Migrations
{
    public partial class FeastAdded3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFeast_AspNetUsers_AspNetUserId",
                table: "RegistrationFeast");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFeast_Feast_Feastid",
                table: "RegistrationFeast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationFeast",
                table: "RegistrationFeast");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feast",
                table: "Feast");

            migrationBuilder.DropColumn(
                name: "FeastSuperId",
                table: "RegistrationFeast");

            migrationBuilder.RenameTable(
                name: "RegistrationFeast",
                newName: "RegistrationFeasts");

            migrationBuilder.RenameTable(
                name: "Feast",
                newName: "Feasts");

            migrationBuilder.RenameColumn(
                name: "Feastid",
                table: "RegistrationFeasts",
                newName: "FeastId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrationFeast_Feastid",
                table: "RegistrationFeasts",
                newName: "IX_RegistrationFeasts_FeastId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrationFeast_AspNetUserId",
                table: "RegistrationFeasts",
                newName: "IX_RegistrationFeasts_AspNetUserId");

            migrationBuilder.AlterColumn<int>(
                name: "FeastId",
                table: "RegistrationFeasts",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationFeasts",
                table: "RegistrationFeasts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feasts",
                table: "Feasts",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFeasts_AspNetUsers_AspNetUserId",
                table: "RegistrationFeasts",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFeasts_Feasts_FeastId",
                table: "RegistrationFeasts",
                column: "FeastId",
                principalTable: "Feasts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFeasts_AspNetUsers_AspNetUserId",
                table: "RegistrationFeasts");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistrationFeasts_Feasts_FeastId",
                table: "RegistrationFeasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistrationFeasts",
                table: "RegistrationFeasts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feasts",
                table: "Feasts");

            migrationBuilder.RenameTable(
                name: "RegistrationFeasts",
                newName: "RegistrationFeast");

            migrationBuilder.RenameTable(
                name: "Feasts",
                newName: "Feast");

            migrationBuilder.RenameColumn(
                name: "FeastId",
                table: "RegistrationFeast",
                newName: "Feastid");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrationFeasts_FeastId",
                table: "RegistrationFeast",
                newName: "IX_RegistrationFeast_Feastid");

            migrationBuilder.RenameIndex(
                name: "IX_RegistrationFeasts_AspNetUserId",
                table: "RegistrationFeast",
                newName: "IX_RegistrationFeast_AspNetUserId");

            migrationBuilder.AlterColumn<int>(
                name: "Feastid",
                table: "RegistrationFeast",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "FeastSuperId",
                table: "RegistrationFeast",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistrationFeast",
                table: "RegistrationFeast",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feast",
                table: "Feast",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFeast_AspNetUsers_AspNetUserId",
                table: "RegistrationFeast",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistrationFeast_Feast_Feastid",
                table: "RegistrationFeast",
                column: "Feastid",
                principalTable: "Feast",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
