using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT.DigitalCompany.Data.Migrations.Infrastructure
{
    /// <inheritdoc />
    public partial class CreateCompanyRegistrationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CompanyRegistrationRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contact_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRegistrationRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CRN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Contact_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_DocumentType = table.Column<int>(type: "int", nullable: false),
                    ID_Serial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_CNP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_BirthCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_BirthState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_BirthCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_IssueDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ID_ExpirationDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    ID_IssueBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ID_BirthDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Stair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Floor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Apartment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLocation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Block = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Stair = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Floor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address_Apartment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompanyLocationContract_DurationInYears = table.Column<int>(type: "int", nullable: false),
                    CompanyLocationContract_MonthlyRental = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyLocationContract_RentalDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CompanyRegistrationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyLocation_CompanyRegistrationRequests_CompanyRegistrationRequestId",
                        column: x => x.CompanyRegistrationRequestId,
                        principalTable: "CompanyRegistrationRequests",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CompanyRequestAssociates",
                columns: table => new
                {
                    CompanyRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRequestAssociates", x => new { x.CompanyRequestId, x.AssociateId });
                    table.ForeignKey(
                        name: "FK_CompanyRequestAssociates_CompanyRegistrationRequests_CompanyRequestId",
                        column: x => x.CompanyRequestId,
                        principalTable: "CompanyRegistrationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyRequestAssociates_Person_AssociateId",
                        column: x => x.AssociateId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyLocationOwners",
                columns: table => new
                {
                    CompanyLocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocationOwners", x => new { x.CompanyLocationId, x.OwnerId });
                    table.ForeignKey(
                        name: "FK_CompanyLocationOwners_CompanyLocation_CompanyLocationId",
                        column: x => x.CompanyLocationId,
                        principalTable: "CompanyLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyLocationOwners_Person_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLocation_CompanyRegistrationRequestId",
                table: "CompanyLocation",
                column: "CompanyRegistrationRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLocationOwners_OwnerId",
                table: "CompanyLocationOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRequestAssociates_AssociateId",
                table: "CompanyRequestAssociates",
                column: "AssociateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLocationOwners");

            migrationBuilder.DropTable(
                name: "CompanyRequestAssociates");

            migrationBuilder.DropTable(
                name: "CompanyLocation");

            migrationBuilder.DropTable(
                name: "Person");

            migrationBuilder.DropTable(
                name: "CompanyRegistrationRequests");
        }
    }
}
