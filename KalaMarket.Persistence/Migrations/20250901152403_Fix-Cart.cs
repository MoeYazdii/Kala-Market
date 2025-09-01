using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KalaMarket.Persistence.Migrations
{
    public partial class FixCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 10, 9, 4, 789, DateTimeKind.Local).AddTicks(8196));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 10, 9, 4, 791, DateTimeKind.Local).AddTicks(6740));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3L,
                column: "InsertTime",
                value: new DateTime(2025, 9, 1, 10, 9, 4, 791, DateTimeKind.Local).AddTicks(6842));
        }
    }
}
