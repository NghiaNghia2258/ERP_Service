using ERP_Service.DAL.Data;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace ERP_Service.Infrastructure;

public partial class AppDbContext : DbContext
{
	public AppDbContext()
	{
	}

	public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	public virtual DbSet<Role> Roles { get; set; }

	public virtual DbSet<RoleGroup> RoleGroups { get; set; }

	public virtual DbSet<UserLogin> UserLogins { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("Data Source=mssql-189685-0.cloudclusters.net,10046;Initial Catalog=AppDb;User ID=admin;Password=Admin123;Trust Server Certificate=True");



	private static LambdaExpression GenerateQueryFilterLambda(Type entityType)
	{
		var parameter = Expression.Parameter(entityType, "w");

		var propertyAccess = Expression.PropertyOrField(parameter, nameof(ISoftDelete.IsDeleted));

		var equalExpression = Expression.Equal(propertyAccess, Expression.Constant(false));

		return Expression.Lambda(equalExpression, parameter);
	}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		SeedData.Seed(modelBuilder);
		modelBuilder.Entity<RoleGroup>(entity =>
		{
			entity.HasMany(d => d.Roles).WithMany(p => p.RoleGroups)
				.UsingEntity<Dictionary<string, object>>(
					"RoleRoleGroup",
					r => r.HasOne<Role>().WithMany().HasForeignKey("RolesId"),
					l => l.HasOne<RoleGroup>().WithMany().HasForeignKey("RoleGroupsId"),
					j =>
					{
						j.HasKey("RoleGroupsId", "RolesId");
						j.ToTable("RoleRoleGroup");
						j.HasIndex(new[] { "RolesId" }, "IX_RoleRoleGroup_RolesId");
					});
		});

		modelBuilder.Entity<UserLogin>(entity =>
		{
			entity.HasIndex(e => e.RoleGroupId, "IX_UserLogins_RoleGroupId");

			entity.HasOne(d => d.RoleGroup).WithMany(p => p.UserLogins).HasForeignKey(d => d.RoleGroupId);
		});

		var softDeleteEntities = typeof(ISoftDelete).Assembly.GetTypes()
		.Where(type => typeof(ISoftDelete).IsAssignableFrom(type)
			&& type.IsClass
			&& !type.IsAbstract);
		foreach (var softDeleteEntity in softDeleteEntities)
		{
			modelBuilder.Entity(softDeleteEntity).HasQueryFilter(GenerateQueryFilterLambda(softDeleteEntity));
		}
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		OnModelCreatingPartial(modelBuilder);
	}

	partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
