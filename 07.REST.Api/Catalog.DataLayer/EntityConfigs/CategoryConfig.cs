using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Catalog.DataLayer.Entities;

namespace Catalog.DataLayer.EntityConfigs
{
	class CategoryConfig : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.ToTable("Category");
			builder.HasKey(category => category.Id);
			builder.Property(category => category.Name)
				.IsRequired()
				.HasMaxLength(100);

			builder.HasMany(category => category.Items)
				.WithOne(item => item.Category)
				.HasForeignKey(item => item.CategoryId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
