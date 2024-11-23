

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;


namespace ScreeningExchange.Infrastructure.DataAccess.Mappings;

public class EfDestinationConfiguration : IEntityTypeConfiguration<Destination>
{
    public void Configure(EntityTypeBuilder<Destination> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.Property(c => c.StudentId)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.Property(c => c.BuildQuestionId)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();
    }
}