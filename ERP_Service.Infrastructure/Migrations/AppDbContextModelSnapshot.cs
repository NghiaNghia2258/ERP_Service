﻿// <auto-generated />
using System;
using ERP_Service.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ERP_Service.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ERP_Service.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Debt")
                        .HasColumnType("float");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Point")
                        .HasColumnType("int");

                    b.Property<int?>("UserLoginId")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserLoginId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("CustomerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CustomerPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<double?>("DiscountValue")
                        .HasColumnType("float");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<Guid?>("RecipientsInformationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Tax")
                        .HasColumnType("float");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("VoucherCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("VoucherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("VoucherId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<double?>("DiscountValue")
                        .HasColumnType("float");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductVariantId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<double>("UnitPrice")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductVariantId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.Voucher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("DiscountPercent")
                        .HasColumnType("float");

                    b.Property<double?>("DiscountValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("ExpirationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double?>("MaxDiscountValue")
                        .HasColumnType("float");

                    b.Property<double?>("MinOrderValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UsageLimit")
                        .HasColumnType("int");

                    b.Property<int?>("UsedCount")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.Property<string>("VoucherCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vouchers");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("MainImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("TotalInventory")
                        .HasColumnType("float");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ProductCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mùa đông",
                            Version = 0
                        },
                        new
                        {
                            Id = 3,
                            Name = "Mùa hè",
                            Version = 0
                        },
                        new
                        {
                            Id = 4,
                            Name = "Mùa xuân",
                            Version = 0
                        },
                        new
                        {
                            Id = 5,
                            Name = "Mùa thu",
                            Version = 0
                        },
                        new
                        {
                            Id = 6,
                            Name = "Thời trang nữ",
                            Version = 0
                        },
                        new
                        {
                            Id = 7,
                            Name = "Thời trang trẻ em",
                            Version = 0
                        },
                        new
                        {
                            Id = 8,
                            Name = "Thời trang nam",
                            Version = 0
                        });
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductImage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductRate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductRates");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductVariant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CreatedName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Inventory")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("UpdatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UpdatedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductVariants");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 5,
                            Name = "CREATE_CUSTOMER",
                            Version = 0
                        },
                        new
                        {
                            Id = 6,
                            Name = "DELETE_CUSTOMER",
                            Version = 0
                        },
                        new
                        {
                            Id = 7,
                            Name = "UPDATE_CUSTOMER",
                            Version = 0
                        },
                        new
                        {
                            Id = 8,
                            Name = "SELECT_CUSTOMER",
                            Version = 0
                        });
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.RoleGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RoleGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin",
                            Version = 0
                        },
                        new
                        {
                            Id = 2,
                            Name = "User",
                            Version = 0
                        });
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.UserLogin", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeletedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DeletedName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Version")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "RoleGroupId" }, "IX_UserLogins_RoleGroupId");

                    b.ToTable("UserLogins");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsDeleted = false,
                            Password = "admin123",
                            RoleGroupId = 1,
                            Username = "admin",
                            Version = 0
                        },
                        new
                        {
                            Id = 2,
                            IsDeleted = false,
                            Password = "user123",
                            RoleGroupId = 2,
                            Username = "user1",
                            Version = 0
                        });
                });

            modelBuilder.Entity("RoleRoleGroup", b =>
                {
                    b.Property<int>("RoleGroupsId")
                        .HasColumnType("int");

                    b.Property<int>("RolesId")
                        .HasColumnType("int");

                    b.HasKey("RoleGroupsId", "RolesId");

                    b.HasIndex(new[] { "RolesId" }, "IX_RoleRoleGroup_RolesId");

                    b.ToTable("RoleRoleGroup", (string)null);

                    b.HasData(
                        new
                        {
                            RoleGroupsId = 1,
                            RolesId = 5
                        },
                        new
                        {
                            RoleGroupsId = 1,
                            RolesId = 6
                        },
                        new
                        {
                            RoleGroupsId = 1,
                            RolesId = 7
                        },
                        new
                        {
                            RoleGroupsId = 1,
                            RolesId = 8
                        },
                        new
                        {
                            RoleGroupsId = 2,
                            RolesId = 8
                        });
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Customer", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.UserLogin", "UserLogin")
                        .WithMany("Customers")
                        .HasForeignKey("UserLoginId");

                    b.Navigation("UserLogin");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.Order", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("ERP_Service.Domain.Models.Orders.Voucher", "Voucher")
                        .WithMany("Orders")
                        .HasForeignKey("VoucherId");

                    b.Navigation("Customer");

                    b.Navigation("Voucher");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.OrderItem", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.Orders.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP_Service.Domain.Models.Products.ProductVariant", "ProductVariant")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductVariantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("ProductVariant");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.Product", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.Products.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductImage", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.Products.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductRate", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.Customer", "Customer")
                        .WithMany("ProductRates")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP_Service.Domain.Models.Products.Product", "Product")
                        .WithMany("ProductRates")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductVariant", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.Products.Product", "Product")
                        .WithMany("ProductVariants")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.UserLogin", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.RoleGroup", "RoleGroup")
                        .WithMany("UserLogins")
                        .HasForeignKey("RoleGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleGroup");
                });

            modelBuilder.Entity("RoleRoleGroup", b =>
                {
                    b.HasOne("ERP_Service.Domain.Models.RoleGroup", null)
                        .WithMany()
                        .HasForeignKey("RoleGroupsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ERP_Service.Domain.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RolesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ProductRates");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Orders.Voucher", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.Product", b =>
                {
                    b.Navigation("ProductImages");

                    b.Navigation("ProductRates");

                    b.Navigation("ProductVariants");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.Products.ProductVariant", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.RoleGroup", b =>
                {
                    b.Navigation("UserLogins");
                });

            modelBuilder.Entity("ERP_Service.Domain.Models.UserLogin", b =>
                {
                    b.Navigation("Customers");
                });
#pragma warning restore 612, 618
        }
    }
}
