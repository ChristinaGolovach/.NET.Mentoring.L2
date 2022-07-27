using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catalog.DataLayer.Entities;
using Catalog.DataLayer.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace Catalog.DataLayer
{
	public class CatalogDbContext : DbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Item> Items { get; set; }


		public CatalogDbContext(DbContextOptions<CatalogDbContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			// TODO move outside
			optionsBuilder.UseSqlite("Data Source=Catalog.db");

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new CategoryConfig());
			modelBuilder.ApplyConfiguration(new ItemConfig());
		}
	}
}
