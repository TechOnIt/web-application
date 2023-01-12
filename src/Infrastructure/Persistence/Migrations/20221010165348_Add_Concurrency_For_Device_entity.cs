using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechOnIt.Infrastructure.Migrations
{
    public partial class Add_Concurrency_For_Device_entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("10297d17-b232-441f-86f6-53454c464d75"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("781c3310-c550-4baa-9cba-2bb9b4d4d622"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("c3a8f274-3ebe-4374-81a3-9a108a67dd79"));

            migrationBuilder.DeleteData(
                table: "AesKeys",
                keyColumn: "Id",
                keyValue: new Guid("dbc38109-1bc0-4995-8ca2-66b038747326"));

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "Devices",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "RowVersion",
                table: "Devices");

            migrationBuilder.InsertData(
                table: "AesKeys",
                columns: new[] { "Id", "Key", "Title" },
                values: new object[,]
                {
                    { new Guid("10297d17-b232-441f-86f6-53454c464d75"), "mqSsdge5MVYeo0qZGhVlKB71gFQjGAVwyQwogq9VHkA=", "UserKey" },
                    { new Guid("781c3310-c550-4baa-9cba-2bb9b4d4d622"), "h9Nw0cuUsJRKQpc5YPFK/5zB8f9lwpanvofle1w/iFE=", "SesnsorKey" },
                    { new Guid("c3a8f274-3ebe-4374-81a3-9a108a67dd79"), "HSQg0lPLwR+/zNpb6tjJYh7xEuTl4X6dLqo8ddzZekM=", "ReportKey" },
                    { new Guid("dbc38109-1bc0-4995-8ca2-66b038747326"), "rdGKbxanYm933qo81LRtE7xRyz6A2m/5ub0j42y9sFA=", "DeviceKey" }
                });
        }
    }
}
