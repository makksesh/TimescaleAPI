using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;


public class ResultConfiguration : IEntityTypeConfiguration<Result>
{
    public void Configure(EntityTypeBuilder<Result> builder)
    {
        builder.ToTable("Results");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.FileName)
            .IsRequired()
            .HasMaxLength(255);

        // Уникальный индекс — один файл = одна запись в Results
        builder.HasIndex(r => r.FileName)
            .IsUnique();

        builder.Property(r => r.AvgValue).IsRequired();
        builder.Property(r => r.AvgExecutionTime).IsRequired();
        builder.Property(r => r.MedianValue).IsRequired();
        builder.Property(r => r.MaxValue).IsRequired();
        builder.Property(r => r.MinValue).IsRequired();
        builder.Property(r => r.DeltaSeconds).IsRequired();
        builder.Property(r => r.FirstOperationStart).IsRequired();
    }
}
