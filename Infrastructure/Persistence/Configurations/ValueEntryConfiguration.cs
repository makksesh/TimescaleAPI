using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;


public class ValueEntryConfiguration : IEntityTypeConfiguration<ValueEntry>
{
    public void Configure(EntityTypeBuilder<ValueEntry> builder)
    {
        builder.ToTable("Values");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(v => v.Date)
            .IsRequired();

        builder.Property(v => v.ExecutionTime)
            .IsRequired();

        builder.Property(v => v.Value)
            .IsRequired();

        // Индекс на FileName — ускоряет фильтрацию и GetLast10
        builder.HasIndex(v => v.FileName);
        // Составной индекс для сортировки по Date в рамках файла
        builder.HasIndex(v => new { v.FileName, v.Date });
    }
}
