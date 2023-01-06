using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechOnIt.Infrastructure.Migrations
{
    public partial class DropAesKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AesKeys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AesKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AesKeys", x => x.Id);
                });

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
        }
    }
}
