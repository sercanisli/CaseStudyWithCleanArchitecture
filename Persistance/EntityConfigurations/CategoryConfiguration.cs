using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories").HasKey(c => c.Id);

            builder.Property(c => c.Id).HasColumnName("Id").IsRequired();
            builder.Property(c => c.Name).HasColumnName("Title").IsRequired();
            builder.Property(c => c.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(c => c.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(c => c.DeletedDate).HasColumnName("DeletedDate");

            builder.HasIndex(indexExpression: c => c.Name, name: "UK_Categories_Name").IsUnique();

            builder.HasMany(c => c.Products);

            //Global Query Filter
            builder.HasQueryFilter(c => !c.DeletedDate.HasValue);
        }
    }
}
