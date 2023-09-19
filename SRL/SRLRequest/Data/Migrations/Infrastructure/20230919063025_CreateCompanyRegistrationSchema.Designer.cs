﻿// <auto-generated />
using System;
using IT.DigitalCompany.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IT.DigitalCompany.Data.Migrations.Infrastructure
{
    [DbContext(typeof(CompanyRegistrationDbContext))]
    [Migration("20230919063025_CreateCompanyRegistrationSchema")]
    partial class CreateCompanyRegistrationSchema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CompanyLocationOwners", b =>
                {
                    b.Property<Guid>("CompanyLocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompanyLocationId", "OwnerId");

                    b.HasIndex("OwnerId");

                    b.ToTable("CompanyLocationOwners");
                });

            modelBuilder.Entity("CompanyRequestAssociates", b =>
                {
                    b.Property<Guid>("CompanyRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AssociateId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CompanyRequestId", "AssociateId");

                    b.HasIndex("AssociateId");

                    b.ToTable("CompanyRequestAssociates");
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.CompanyLocation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CompanyRegistrationRequestId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CompanyRegistrationRequestId");

                    b.ToTable("CompanyLocation");
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.CompanyRegistrationRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("CompanyRegistrationRequests");
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CRN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Person");
                });

            modelBuilder.Entity("CompanyLocationOwners", b =>
                {
                    b.HasOne("IT.DigitalCompany.Models.CompanyLocation", null)
                        .WithMany()
                        .HasForeignKey("CompanyLocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IT.DigitalCompany.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CompanyRequestAssociates", b =>
                {
                    b.HasOne("IT.DigitalCompany.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("AssociateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("IT.DigitalCompany.Models.CompanyRegistrationRequest", null)
                        .WithMany()
                        .HasForeignKey("CompanyRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.CompanyLocation", b =>
                {
                    b.HasOne("IT.DigitalCompany.Models.CompanyRegistrationRequest", null)
                        .WithMany("Locations")
                        .HasForeignKey("CompanyRegistrationRequestId");

                    b.OwnsOne("IT.DigitalCompany.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("CompanyLocationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Apartment")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Apartment");

                            b1.Property<string>("Block")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Block");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Country");

                            b1.Property<string>("Floor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Floor");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Number");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_PostalCode");

                            b1.Property<string>("Stair")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Stair");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Street");

                            b1.HasKey("CompanyLocationId");

                            b1.ToTable("CompanyLocation");

                            b1.WithOwner()
                                .HasForeignKey("CompanyLocationId");
                        });

                    b.OwnsOne("IT.DigitalCompany.Models.CompanyLocationContract", "Contract", b1 =>
                        {
                            b1.Property<Guid>("CompanyLocationId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("DurationInYears")
                                .HasColumnType("int")
                                .HasColumnName("CompanyLocationContract_DurationInYears");

                            b1.Property<decimal?>("MonthlyRental")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("CompanyLocationContract_MonthlyRental");

                            b1.Property<decimal?>("RentalDeposit")
                                .HasColumnType("decimal(18,2)")
                                .HasColumnName("CompanyLocationContract_RentalDeposit");

                            b1.HasKey("CompanyLocationId");

                            b1.ToTable("CompanyLocation");

                            b1.WithOwner()
                                .HasForeignKey("CompanyLocationId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Contract")
                        .IsRequired();
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.CompanyRegistrationRequest", b =>
                {
                    b.OwnsOne("IT.DigitalCompany.Models.Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("CompanyRegistrationRequestId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_Email");

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_LastName");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_PhoneNumber");

                            b1.HasKey("CompanyRegistrationRequestId");

                            b1.ToTable("CompanyRegistrationRequests");

                            b1.WithOwner()
                                .HasForeignKey("CompanyRegistrationRequestId");
                        });

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.Person", b =>
                {
                    b.OwnsOne("IT.DigitalCompany.Models.IdentityDocument", "IdentityDocument", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("BirthCity")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_BirthCity");

                            b1.Property<string>("BirthCountry")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_BirthCountry");

                            b1.Property<DateTimeOffset?>("BirthDate")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("ID_BirthDate");

                            b1.Property<string>("BirthState")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_BirthState");

                            b1.Property<string>("CNP")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_CNP");

                            b1.Property<string>("Citizenship")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_Citizenship");

                            b1.Property<int>("DocumentType")
                                .HasColumnType("int")
                                .HasColumnName("ID_DocumentType");

                            b1.Property<DateTimeOffset?>("ExpirationDate")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("ID_ExpirationDate");

                            b1.Property<string>("IssueBy")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_IssueBy");

                            b1.Property<DateTimeOffset?>("IssueDate")
                                .HasColumnType("datetimeoffset")
                                .HasColumnName("ID_IssueDate");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_Number");

                            b1.Property<string>("Serial")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("ID_Serial");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("IT.DigitalCompany.Models.Address", "Address", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Apartment")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Apartment");

                            b1.Property<string>("Block")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Block");

                            b1.Property<string>("City")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_City");

                            b1.Property<string>("Country")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Country");

                            b1.Property<string>("Floor")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Floor");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Number");

                            b1.Property<string>("PostalCode")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_PostalCode");

                            b1.Property<string>("Stair")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Stair");

                            b1.Property<string>("State")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_State");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Address_Street");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.OwnsOne("IT.DigitalCompany.Models.Contact", "Contact", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_Email");

                            b1.Property<string>("FirstName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_LastName");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Contact_PhoneNumber");

                            b1.HasKey("PersonId");

                            b1.ToTable("Person");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Contact")
                        .IsRequired();

                    b.Navigation("IdentityDocument")
                        .IsRequired();
                });

            modelBuilder.Entity("IT.DigitalCompany.Models.CompanyRegistrationRequest", b =>
                {
                    b.Navigation("Locations");
                });
#pragma warning restore 612, 618
        }
    }
}
