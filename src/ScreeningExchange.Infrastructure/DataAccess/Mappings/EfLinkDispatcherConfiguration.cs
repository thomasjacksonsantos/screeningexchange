

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;

namespace ScreeningExchange.Infrastructure.DataAccess.Mappings;

public class EfLinkDispatcherConfiguration : IEntityTypeConfiguration<LinkDispatcher>
{
    public void Configure(EntityTypeBuilder<LinkDispatcher> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.Property(c => c.BuildQuestionId)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.Property(c => c.Link).HasMaxLength(600);

        builder.Property(c => c.Status).HasConversion<string>().HasMaxLength(100);

        builder.ComplexProperty(c => c.Customer);

        builder.OwnsOne(c => c.Logs);
    }
}