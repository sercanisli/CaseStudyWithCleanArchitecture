using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products").HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("Id").IsRequired();
            builder.Property(p => p.Title).HasColumnName("Title").IsRequired();
            builder.Property(p => p.Description).HasColumnName("Description").IsRequired();
            builder.Property(p => p.CreatedDate).HasColumnName("CreatedDate").IsRequired();
            builder.Property(p => p.UpdatedDate).HasColumnName("UpdatedDate");
            builder.Property(p => p.DeletedDate).HasColumnName("DeletedDate");

            builder.HasOne(p => p.Category);

            //Global Query Filter
            //ilgili product için bir deleted değeri yoksa;
            builder.HasQueryFilter(p => !p.DeletedDate.HasValue);
        }
    }
}
