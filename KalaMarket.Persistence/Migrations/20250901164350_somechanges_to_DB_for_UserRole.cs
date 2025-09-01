using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KalaMarket.Persistence.Migrations
{
    public partial class somechanges_to_DB_for_UserRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 20, 13, 49, 928, DateTimeKind.Local).AddTicks(3766));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 20, 13, 49, 929, DateTimeKind.Local).AddTicks(8343));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 20, 13, 49, 929, DateTimeKind.Local).AddTicks(8429));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 18, 54, 3, 71, DateTimeKind.Local).AddTicks(9657));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 18, 54, 3, 73, DateTimeKind.Local).AddTicks(3860));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 18, 54, 3, 73, DateTimeKind.Local).AddTicks(3946));
        }
    }
}
