

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;


namespace ScreeningExchange.Infrastructure.DataAccess.Mappings;

public class EfSchoolConfiguration : IEntityTypeConfiguration<School>
{
    public void Configure(EntityTypeBuilder<School> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.Property(c => c.UserId).HasMaxLength(200);

        builder.ComplexProperty(c => c.Name);

        builder.ComplexProperty(c => c.Email);

        builder.ComplexProperty(c => c.Phone);
    }
}