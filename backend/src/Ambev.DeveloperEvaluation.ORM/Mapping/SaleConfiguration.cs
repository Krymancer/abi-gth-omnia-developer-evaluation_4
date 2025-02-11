using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnType("uuid")
                .HasDefaultValueSql("gen_random_uuid()");

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.CustomerName)
                .HasMaxLength(100);

            builder.Property(s => s.BranchName)
                .HasMaxLength(100);

            builder.Property(s => s.TotalAmount)
                .HasPrecision(18, 2);

            // One-to-many relationship with SaleItems
            builder.HasMany(s => s.Items)
                   .WithOne()
                   .HasForeignKey(i => i.SaleId);
        }
    }
}
