using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechOnIt.Infrastructure.Migrations
{
    public partial class refactor_data_types : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PerformanceReports_SensorId",
                table: "PerformanceReports");

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("1f90640a-3818-44e4-bd4b-2d75b542e693"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("a9a98a57-cc9e-4495-9d48-8129521d9a8c"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("d0ab21d2-b8be-4dd1-8684-86ed567c8b70"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("fe64f141-2dde-4443-a70e-bfa5238dbc75"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MaxFailCount",
                table: "Users",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Structures",
                type: "nvarchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Structures",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                type: "nvarchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Places",
                type: "nvarchar(150)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Value",
                table: "PerformanceReports",
                type: "numeric",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Pin",
                table: "Devices",
                type: "numeric",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AesKeys",
                columns: new[] { "Id", "Key", "Title" },
                values: new object[,]
                {
                    { new Guid("042ca73b-6f09-4b44-bf91-7d106c7e8907"), "MqvCUqzqTV7L2LqPImEh6I+ul2KkbNs74G2063+CaEc=", "SesnsorKey" },
                    { new Guid("2b809feb-711a-4e94-972e-ab10df749557"), "KHtlhN6/rLtL0BVP0XHyhNToPj02R/v+/QLPhg79M34=", "UserKey" },
                    { new Guid("cda0c500-3394-4684-83e1-0fc051a687a2"), "lAbYlJIyAljjXC4gM6mxj8Ql25KYFsectIcR4VOq70k=", "ReportKey" },
                    { new Guid("d2634df0-ad14-4ef6-befe-e74c128c0bbf"), "0+p+D9fiRvicNPLQ2ulxtzPm2TtH0MWKEjQJpcCILrg=", "DeviceKey" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReports_SensorId",
                table: "PerformanceReports",
                column: "SensorId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_PhoneNumber",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PerformanceReports_SensorId",
                table: "PerformanceReports");

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("042ca73b-6f09-4b44-bf91-7d106c7e8907"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("2b809feb-711a-4e94-972e-ab10df749557"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("cda0c500-3394-4684-83e1-0fc051a687a2"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("d2634df0-ad14-4ef6-befe-e74c128c0bbf"));

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<short>(
                name: "MaxFailCount",
                table: "Users",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Structures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Structures",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedName",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Roles",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Places",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Places",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "PerformanceReports",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<int>(
                name: "Pin",
                table: "Devices",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldMaxLength: 10);

            migrationBuilder.InsertData(
                table: "AesKeys",
                columns: new[] { "Id", "Key", "Title" },
                values: new object[,]
                {
                    { new Guid("1f90640a-3818-44e4-bd4b-2d75b542e693"), "CqVaYN6qPhxbtyst8JA42IGi07MJG0Hdu3JiF1NePtU=", "ReportKey" },
                    { new Guid("a9a98a57-cc9e-4495-9d48-8129521d9a8c"), "LY9W9wYzlkrBC1H0sq7jsh8O4HgfqV/hgfpS9v2Ed24=", "UserKey" },
                    { new Guid("d0ab21d2-b8be-4dd1-8684-86ed567c8b70"), "2NJYL5crAi/RyTj8l1mVASyr1PP+SXtBLaKXOH1TzYE=", "DeviceKey" },
                    { new Guid("fe64f141-2dde-4443-a70e-bfa5238dbc75"), "t+qFAjYNDO74v3W8VtwEq+fQCLu7wgAcvRdiAqr8Qwc=", "SesnsorKey" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReports_SensorId",
                table: "PerformanceReports",
                column: "SensorId");
        }
    }
}
