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
            new RoleGroup { Id = 2, Name = "User" }
        };

        modelBuilder.Entity<RoleGroup>().HasData(roleGroups);

        // Define users and their role groups
        var userLogins = new List<UserLogin>
        {
            new UserLogin { Id = 1, Username = "admin", Password = "admin123", RoleGroupId = 1, IsDeleted = false },
            new UserLogin { Id = 2, Username = "user1", Password = "user123", RoleGroupId = 2, IsDeleted = false }
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
				new { RoleGroupsId = 2, RolesId = 8 }
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
            }
        };

        modelBuilder.Entity<Customer>().HasData(customers);

        #endregion


    }
}
