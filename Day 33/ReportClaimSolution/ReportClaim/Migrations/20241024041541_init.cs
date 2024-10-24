using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ReportClaim.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Claims",
                columns: table => new
                {
                    ClaimId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Claims", x => x.ClaimId);
                });

            migrationBuilder.CreateTable(
                name: "Policies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PolicyNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Policies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClaimaintName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimaintPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimaintEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SettlementForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeathCertificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PolicyCertificate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhotoId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressProof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CancelledCheck = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Others = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClaimId = table.Column<int>(type: "int", nullable: false),
                    PolicyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClaimType",
                        column: x => x.ClaimId,
                        principalTable: "Claims",
                        principalColumn: "ClaimId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PolicyNumber",
                        column: x => x.PolicyId,
                        principalTable: "Policies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ClaimId",
                table: "Reports",
                column: "ClaimId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_PolicyId",
                table: "Reports",
                column: "PolicyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "Claims");

            migrationBuilder.DropTable(
                name: "Policies");
        }
    }
}
