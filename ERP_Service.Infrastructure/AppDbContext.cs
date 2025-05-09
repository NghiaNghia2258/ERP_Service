using ERP_Service.DAL.Data;
using ERP_Service.Domain.Abstractions.Model;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.Models.Products;
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
	public virtual DbSet<Customer> Customers { get; set; }

	#region DbSet module Product
	public virtual DbSet<Product> Products { get; set; }
	public virtual DbSet<ProductVariant> ProductVariants { get; set; }
	public virtual DbSet<ProductCategory> ProductCategories { get; set; }
	public virtual DbSet<ProductBrand> ProductBrands { get; set; }
    public virtual DbSet<ProductRate> ProductRates { get; set; }

	#endregion

	#region DbSet module Order
	//public virtual DbSet<Order> Orders { get; set; }
	//public virtual DbSet<OrderItem> OrderItems { get; set; }
	//public virtual DbSet<Voucher> Vouchers { get; set; }
	#endregion

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		=> optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=AppDb2;Integrated Security=True;Trust Server Certificate=True");



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
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var foreignKey in entityType.GetForeignKeys())
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
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
