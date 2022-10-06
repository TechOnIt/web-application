using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iot.Infrastructure.Migrations
{
    public partial class Add_data_Protection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AesKeys",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    { new Guid("10297d17-b232-441f-86f6-53454c464d75"), "mqSsdge5MVYeo0qZGhVlKB71gFQjGAVwyQwogq9VHkA=", "UserKey" },
                    { new Guid("781c3310-c550-4baa-9cba-2bb9b4d4d622"), "h9Nw0cuUsJRKQpc5YPFK/5zB8f9lwpanvofle1w/iFE=", "SesnsorKey" },
                    { new Guid("c3a8f274-3ebe-4374-81a3-9a108a67dd79"), "HSQg0lPLwR+/zNpb6tjJYh7xEuTl4X6dLqo8ddzZekM=", "ReportKey" },
                    { new Guid("dbc38109-1bc0-4995-8ca2-66b038747326"), "rdGKbxanYm933qo81LRtE7xRyz6A2m/5ub0j42y9sFA=", "DeviceKey" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AesKeys");
        }
    }
}
