using ERP_Service.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.DAL.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Define roles with specific permissions
        var roles = new List<Role>
        {
            new Role { Id = 1, Name = "CREATE_QUIZ" },
            new Role { Id = 2, Name = "DELETE_QUIZ" },
            new Role { Id = 3, Name = "UPDATE_QUIZ" },
            new Role { Id = 4, Name = "SELECT_QUIZ" }
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
                new { RoleGroupsId = 1, RolesId = 1 },
                new { RoleGroupsId = 1, RolesId = 2 },
                new { RoleGroupsId = 1, RolesId = 3 },
                new { RoleGroupsId = 1, RolesId = 4 }, // Admin has full permissions
                new { RoleGroupsId = 2, RolesId = 4 } // User only has SELECT permission
            ));
    }
}
