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
                name: "CompanyLocations",
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
                    CompanyLocationContract_RentalDeposit = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyLocations", x => x.Id);
                });

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
                name: "Persons",
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
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRegistrationRequestLocations",
                columns: table => new
                {
                    CompanyRegistationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LocationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRegistrationRequestLocations", x => new { x.CompanyRegistationRequestId, x.LocationId });
                    table.ForeignKey(
                        name: "FK_CompanyRegistrationRequestLocations_CompanyLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "CompanyLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyRegistrationRequestLocations_CompanyRegistrationRequests_CompanyRegistationRequestId",
                        column: x => x.CompanyRegistationRequestId,
                        principalTable: "CompanyRegistrationRequests",
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
                        name: "FK_CompanyLocationOwners_CompanyLocations_CompanyLocationId",
                        column: x => x.CompanyLocationId,
                        principalTable: "CompanyLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyLocationOwners_Persons_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyRegistrationRequestAssociates",
                columns: table => new
                {
                    CompanyRegistationRequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AssociateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyRegistrationRequestAssociates", x => new { x.CompanyRegistationRequestId, x.AssociateId });
                    table.ForeignKey(
                        name: "FK_CompanyRegistrationRequestAssociates_CompanyRegistrationRequests_CompanyRegistationRequestId",
                        column: x => x.CompanyRegistationRequestId,
                        principalTable: "CompanyRegistrationRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyRegistrationRequestAssociates_Persons_AssociateId",
                        column: x => x.AssociateId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyLocationOwners_OwnerId",
                table: "CompanyLocationOwners",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegistrationRequestAssociates_AssociateId",
                table: "CompanyRegistrationRequestAssociates",
                column: "AssociateId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyRegistrationRequestLocations_LocationId",
                table: "CompanyRegistrationRequestLocations",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyLocationOwners");

            migrationBuilder.DropTable(
                name: "CompanyRegistrationRequestAssociates");

            migrationBuilder.DropTable(
                name: "CompanyRegistrationRequestLocations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "CompanyLocations");

            migrationBuilder.DropTable(
                name: "CompanyRegistrationRequests");
        }
    }
}
