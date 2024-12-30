using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Products;
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
		#region Seeding data for ProductCategory
		// Define product categories
		var productCategories = new List<ProductCategory>
		{
			new ProductCategory { Id = 1, Name = "Mùa đông" },
			new ProductCategory { Id = 3, Name = "Mùa hè" },
			new ProductCategory { Id = 4, Name = "Mùa xuân" },
			new ProductCategory { Id = 5, Name = "Mùa thu" },
			new ProductCategory { Id = 6, Name = "Thời trang nữ" },
			new ProductCategory { Id = 7, Name = "Thời trang trẻ em" },
			new ProductCategory { Id = 8, Name = "Thời trang nam" }
		};
		modelBuilder.Entity<ProductCategory>().HasData(productCategories);
		#endregion
	}
}
