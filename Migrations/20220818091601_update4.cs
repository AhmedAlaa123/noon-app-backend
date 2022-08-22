using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace noone.Migrations
{
    public partial class update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliverCompany_DeliverCompany_Id",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "SubCategory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliverCompany_Id",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "DeliverCompany",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliverCompany_DeliverCompany_Id",
                table: "Order",
                column: "DeliverCompany_Id",
                principalTable: "DeliverCompany",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_DeliverCompany_DeliverCompany_Id",
                table: "Order");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "SubCategory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<Guid>(
                name: "DeliverCompany_Id",
                table: "Order",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ContactNumber",
                table: "DeliverCompany",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_DeliverCompany_DeliverCompany_Id",
                table: "Order",
                column: "DeliverCompany_Id",
                principalTable: "DeliverCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
