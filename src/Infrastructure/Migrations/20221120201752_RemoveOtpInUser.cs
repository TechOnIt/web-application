using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechOnIt.Infrastructure.Migrations
{
    public partial class RemoveOtpInUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("394d408f-6bf9-4bae-b999-fea1aab75497"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("a3c49b51-a91c-4b30-93af-21abfa9bb45b"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("dfcfd309-083c-4ad2-b02c-a6c5adf25dde"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("efefe7e5-3b29-40fe-8f00-05d45d7910d1"));

            migrationBuilder.DropColumn(
                name: "OtpCode",
                table: "Users");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "OtpCode",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AesKeys",
                columns: new[] { "Id", "Key", "Title" },
                values: new object[,]
                {
                    { new Guid("394d408f-6bf9-4bae-b999-fea1aab75497"), "fQIQ8pUM1M3W8U4oXBXKg2GEOoZLC4Qqt8bG3M+WZj8=", "ReportKey" },
                    { new Guid("a3c49b51-a91c-4b30-93af-21abfa9bb45b"), "reFhQ2I3uCCvFOjwzvi4BKLHCr8eVKReZCizLYdkwxo=", "SesnsorKey" },
                    { new Guid("dfcfd309-083c-4ad2-b02c-a6c5adf25dde"), "oqUjQtVFQXCo6++wevRRhoE/1lcgr79PpN3QNXRtN1Y=", "DeviceKey" },
                    { new Guid("efefe7e5-3b29-40fe-8f00-05d45d7910d1"), "MYPDd36BtZMKFGDSldFPi7Y7tWEPNQlqqAE7QtWPWhQ=", "UserKey" }
                });
        }
    }
}
