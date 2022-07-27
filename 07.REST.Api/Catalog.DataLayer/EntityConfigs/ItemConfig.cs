using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Catalog.DataLayer.Entities;

namespace Catalog.DataLayer.EntityConfigs
{
	public class ItemConfig : IEntityTypeConfiguration<Item>
	{
		public void Configure(EntityTypeBuilder<Item> builder)
		{
			builder.ToTable("Item");
			builder.HasKey(category => category.Id);
			builder.Property(category => category.Name)
				.IsRequired()
				.HasMaxLength(255);

			builder.HasOne(item => item.Category)
				.WithMany(category => category.Items)
				.HasForeignKey(i => i.CategoryId)
				.IsRequired()
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
