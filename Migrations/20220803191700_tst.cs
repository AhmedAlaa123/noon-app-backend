using Microsoft.EntityFrameworkCore.Migrations;
using noone.Contstants;

#nullable disable

namespace noone.Migrations
{
    public partial class tst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(11)",
                maxLength: 11,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // inserT roles into database
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }
                , values: new object[] { Guid.NewGuid().ToString(), Roles.USER_ROLE, Roles.USER_ROLE.ToLower(), Guid.NewGuid().ToString()});
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }
                , values: new object[] { Guid.NewGuid().ToString(), Roles.EMPLOYEE_ROLE, Roles.EMPLOYEE_ROLE.ToLower(), Guid.NewGuid().ToString() });
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new string[] { "Id", "Name", "NormalizedName", "ConcurrencyStamp" }
                , values: new object[] { Guid.NewGuid().ToString(), Roles.ADMIN_ROLE, Roles.ADMIN_ROLE.ToLower(), Guid.NewGuid().ToString() });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(11)",
                oldMaxLength: 11);
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
