using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noone.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_Category_Id",
                table: "SubCategory");

            migrationBuilder.AlterColumn<Guid>(
                name: "Category_Id",
                table: "SubCategory",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_Category_Id",
                table: "SubCategory",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubCategory_Category_Category_Id",
                table: "SubCategory");

            migrationBuilder.AlterColumn<Guid>(
                name: "Category_Id",
                table: "SubCategory",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_SubCategory_Category_Category_Id",
                table: "SubCategory",
                column: "Category_Id",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }
    }
}
