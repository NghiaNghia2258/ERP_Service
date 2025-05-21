using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Products;
using ERP_Service.Domain.Models.Stores;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.DAL.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
		#region seeding data for Authen/Authozi
		// Define roles with specific permissions
		var roles = new List<Role>
        {
			new Role { Id = 5, Name = "CREATE_CUSTOMER" },
			new Role { Id = 6, Name = "DELETE_CUSTOMER" },
			new Role { Id = 7, Name = "UPDATE_CUSTOMER" },
			new Role { Id = 8, Name = "SELECT_CUSTOMER" }
		};

        modelBuilder.Entity<Role>().HasData(roles);

        // Define role groups
        var roleGroups = new List<RoleGroup>
        {
            new RoleGroup { Id = 1, Name = "Admin" },
            new RoleGroup { Id = 2, Name = "User" },
            new RoleGroup { Id = 3, Name = "OwnerStore" },
        };

        modelBuilder.Entity<RoleGroup>().HasData(roleGroups);

        // Define users and their role groups
        var userLogins = new List<UserLogin>
        {
            new UserLogin { Id = 1, Username = "admin", Password = "admin123", RoleGroupId = 1, IsDeleted = false },
            new UserLogin { Id = 2, Username = "user1", Password = "user123", RoleGroupId = 2, IsDeleted = false },
            new UserLogin { Id = 3, Username = "user2", Password = "user123", RoleGroupId = 2, IsDeleted = false },
            new UserLogin { Id = 4, Username = "user3", Password = "user123", RoleGroupId = 2, IsDeleted = false },
            new UserLogin { Id = 5, Username = "user4", Password = "user123", RoleGroupId = 2, IsDeleted = false },
            new UserLogin { Id = 6, Username = "user5", Password = "user123", RoleGroupId = 2, IsDeleted = false },
            new UserLogin { Id = 7, Username = "store1", Password = "store123", RoleGroupId = 3, IsDeleted = false },
        };

        modelBuilder.Entity<UserLogin>().HasData(userLogins);

        // Map role groups to roles
        modelBuilder.Entity<RoleGroup>()
            .HasMany(rg => rg.Roles)
            .WithMany(r => r.RoleGroups)
            .UsingEntity(j => j.HasData(
               
				new { RoleGroupsId = 1, RolesId = 5 },
				new { RoleGroupsId = 1, RolesId = 6 },
				new { RoleGroupsId = 1, RolesId = 7 },
				new { RoleGroupsId = 1, RolesId = 8 },
				new { RoleGroupsId = 2, RolesId = 8 },
                new { RoleGroupsId = 3, RolesId = 5 },
                new { RoleGroupsId = 3, RolesId = 6 },
                new { RoleGroupsId = 3, RolesId = 7 },
                new { RoleGroupsId = 3, RolesId = 8 }

            ));
        #endregion
        #region Seeding data for Store

        var storeId = new Guid("BDBAD2AE-0FFA-4420-ACB9-275E5476013B");
        var store = new Store
        {
            Id = storeId,
            Name = "Cửa hàng Thời Trang Dao",
            Description = "Chuyên cung cấp quần áo nam nữ cao cấp, phong cách hiện đại.",
            Logo = "https://example.com/logo.png",
            CoverImage = "https://example.com/cover.jpg",
            Rating = 4.8,
            ReviewCount = 135,
            Followers = 1200,
            JoinDate = new DateTime(2024, 1, 15),
            Verified = true,
            Location = "Hà Nội, Việt Nam",
            ContactPhone = "0909123456",
            ContactEmail = "store@example.com",
            Policies = "[{\"title\":\"Đổi trả trong 7 ngày\",\"description\":\"Hỗ trợ đổi trả trong 7 ngày nếu có lỗi sản phẩm.\"}]",
            Facebook = "https://facebook.com/store",
            Instagram = "https://instagram.com/store",
            Twitter = "https://twitter.com/store",
            IsDeleted = false,
            DeletedAt = null,
            DeletedBy = null,
            DeletedName = null,
            UserLoginId = 1 // Gắn với user admin đã seed ở trên
        };

        modelBuilder.Entity<Store>().HasData(store);
        var stores = new List<Store> {
            new Store
            {
                Id = new Guid("ADBAD2AE-1FFA-4420-ACB9-275E5376013B"),
                Name = "Cửa hàng Phụ Kiện Nữ Trendy",
                Description = "Cung cấp phụ kiện thời trang hiện đại và cá tính cho phái nữ.",
                Logo = "https://example.com/logo2.png",
                CoverImage = "https://example.com/cover2.jpg",
                Rating = 4.6,
                ReviewCount = 89,
                Followers = 850,
                JoinDate = new DateTime(2024, 3, 20),
                Verified = true,
                Location = "TP. Hồ Chí Minh, Việt Nam",
                ContactPhone = "0911222333",
                ContactEmail = "trendy@example.com",
                Policies = "[{\"title\":\"Miễn phí vận chuyển\",\"description\":\"Miễn phí vận chuyển cho đơn hàng từ 300.000đ trở lên.\"}]",
                Facebook = "https://facebook.com/trendyshop",
                Instagram = "https://instagram.com/trendyshop",
                Twitter = "https://twitter.com/trendyshop",
                IsDeleted = false,
                DeletedAt = null,
                DeletedBy = null,
                DeletedName = null,
                UserLoginId = 7
            },
        };


        #endregion
        #region Seeding data for ProductCategory
        // Define product categories
        var productCategories = new List<ProductCategory>
        {
            new ProductCategory { Id = 1, Name = "Mùa đông", StoreId = storeId },
            new ProductCategory { Id = 3, Name = "Mùa hè", StoreId = storeId  },
            new ProductCategory { Id = 4, Name = "Mùa xuân" , StoreId = storeId},
            new ProductCategory { Id = 5, Name = "Mùa thu" , StoreId = storeId},
            new ProductCategory { Id = 6, Name = "Thời trang nữ" , StoreId = storeId},
            new ProductCategory { Id = 7, Name = "Thời trang trẻ em" , StoreId = storeId},
            new ProductCategory { Id = 8, Name = "Thời trang nam" , StoreId = storeId}
        };
        modelBuilder.Entity<ProductCategory>().HasData(productCategories);
        #endregion
        #region Seeding data for Customers

        var customerId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        var customers = new List<Customer>
        {
            new Customer
            {
                Id = customerId,
                Name = "Nguyễn Văn A",
                Code = "CUST001",
                Phone = "0909123456",
                Gender = "Nam",
                Point = 120,
                Debt = 0,
                UserLoginId = 2, // user1
		        CreatedAt = new DateTime(2025,5,15),
                CreatedBy = "admin",
                CreatedName = "Admin",
                IsDeleted = false,
                StoreId = storeId
            },
            new Customer
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Trần Thị B",
                Code = "CUST002",
                Phone = "0909234567",
                Gender = "Nữ",
                Point = 100,
                Debt = 0,
                UserLoginId = 3, // user2
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "Admin",
                IsDeleted = false,
                StoreId = storeId
            },
            new Customer
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Lê Văn C",
                Code = "CUST003",
                Phone = "0909345678",
                Gender = "Nam",
                Point = 80,
                Debt = 0,
                UserLoginId = 4, // user3
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "Admin",
                IsDeleted = false,
                StoreId = storeId
            },
            new Customer
            {
                Id = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                Name = "Phạm Thị D",
                Code = "CUST004",
                Phone = "0909456789",
                Gender = "Nữ",
                Point = 90,
                Debt = 0,
                UserLoginId = 5, // user4
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "Admin",
                IsDeleted = false,
                StoreId = storeId
            },
            new Customer
            {
                Id = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                Name = "Đặng Văn E",
                Code = "CUST005",
                Phone = "0909567890",
                Gender = "Nam",
                Point = 70,
                Debt = 0,
                UserLoginId = 6, // user5
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "Admin",
                IsDeleted = false,
                StoreId = storeId
            }
        };

        modelBuilder.Entity<Customer>().HasData(customers);

        #endregion

        var products = new List<Product>
        {
            new Product
            {
                Id = 2,
                Name = "Test",
                PropertyName1 = "Màu sắc",
                PropertyName2 = "Dung lượng",
                PropertyValue1 = "Đen,Trắng",
                PropertyValue2 = "128GB",
                OriginalPrice = 100,
                Price = 85,
                CategoryId = 1,
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "admin",
                ImageUrls = "[\"https://placehold.co/100x100\",\"https://placehold.co/100x100\"]",
                IsDeleted = false,
                StoreId = storeId
            },
            new Product
            {
                Id = 3,
                Name = "Test - Đen 128GB",
                PropertyName1 = "Màu sắc",
                PropertyName2 = "Dung lượng",
                PropertyValue1 = "Đen",
                PropertyValue2 = "128GB",
                OriginalPrice = 100,
                Price = 85,
                CategoryId = 1,
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "admin",
                ImageUrls = "[\"https://placehold.co/100x100\"]",
                IsDeleted = false,
                StoreId = storeId
            },
            new Product
            {
                Id = 4,
                Name = "Test - Trắng 128GB",
                PropertyName1 = "Màu sắc",
                PropertyName2 = "Dung lượng",
                PropertyValue1 = "Trắng",
                PropertyValue2 = "128GB",
                OriginalPrice = 100,
                Price = 85,
                CategoryId = 1,
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "admin",
                ImageUrls = "[\"https://placehold.co/100x100\"]",
                IsDeleted = false,
                StoreId = storeId
            },
            new Product
            {
                Id = 5,
                Name = "Test - Đen 256GB",
                PropertyName1 = "Màu sắc",
                PropertyName2 = "Dung lượng",
                PropertyValue1 = "Đen",
                PropertyValue2 = "256GB",
                OriginalPrice = 120,
                Price = 100,
                CategoryId = 1,
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "admin",
                ImageUrls = "[\"https://placehold.co/100x100\"]",
                IsDeleted = false,
                StoreId = storeId
            },
            new Product
            {
                Id = 6,
                Name = "Test - Trắng 256GB",
                PropertyName1 = "Màu sắc",
                PropertyName2 = "Dung lượng",
                PropertyValue1 = "Trắng",
                PropertyValue2 = "256GB",
                OriginalPrice = 120,
                Price = 100,
                CategoryId = 1,
                CreatedAt = new DateTime(2025, 5, 15),
                CreatedBy = "admin",
                CreatedName = "admin",
                ImageUrls = "[\"https://placehold.co/100x100\"]",
                IsDeleted = false,
                StoreId = storeId
            }
        };
        modelBuilder.Entity<Product>().HasData(products);

        var variants = new List<ProductVariant>
                {
                    new ProductVariant
                    {
                        Id = 5,
                        PropertyValue1 = "Đen",
                        PropertyValue2 = "128GB",
                        Price = 85,
                        ImageUrl = "https://placehold.co/100x100",
                        Inventory = 10,
                        ProductId = 2,
                        IsActivate = true,
                        CreatedBy = "admin",
                        CreatedName = "admin"
                    },
                     new ProductVariant
                    {
                        Id = 6,
                        PropertyValue1 = "Trắng",
                        PropertyValue2 = "128GB",
                        Price = 85,
                        ImageUrl = "https://placehold.co/100x100",
                        Inventory = 10,
                        ProductId = 2,
                        IsActivate = true,
                          CreatedBy = "admin",
                        CreatedName = "admin"
                    },
                     new ProductVariant
                    {
                        Id = 10,
                        PropertyValue1 = "Đen",
                        PropertyValue2 = "128GB",
                        Price = 85,
                        ImageUrl = "https://placehold.co/100x100",
                        Inventory = 10,
                        ProductId = 3,
                        IsActivate = true,
                        CreatedBy = "admin",
                        CreatedName = "admin"
                    },
                    new ProductVariant
                    {
                        Id = 7,
                        PropertyValue1 = "Trắng",
                        PropertyValue2 = "128GB",
                        Price = 85,
                        ImageUrl = "https://placehold.co/100x100",
                        Inventory = 10,
                        ProductId = 4,
                        IsActivate = true,
                        CreatedBy = "admin",
                        CreatedName = "admin"
                    },
                    new ProductVariant
                    {
                        Id = 8,
                        PropertyValue1 = "Đen",
                        PropertyValue2 = "256GB",
                        Price = 100,
                        ImageUrl = "https://placehold.co/100x100",
                        Inventory = 10,
                        ProductId = 5,
                        IsActivate = true,
                        CreatedBy = "admin",
                        CreatedName = "admin"
                    },
                    new ProductVariant
                    {
                        Id = 9,
                        PropertyValue1 = "Trắng",
                        PropertyValue2 = "256GB",
                        Price = 100,
                        ImageUrl = "https://placehold.co/100x100",
                        Inventory = 10,
                        ProductId = 6,
                        IsActivate = true,
                        CreatedBy = "admin",
                        CreatedName = "admin"
                    }
                };
        modelBuilder.Entity<ProductVariant>().HasData(variants);
    }
}
