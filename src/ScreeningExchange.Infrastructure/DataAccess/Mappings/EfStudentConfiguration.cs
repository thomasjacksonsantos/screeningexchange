

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;


namespace ScreeningExchange.Infrastructure.DataAccess.Mappings;

public class EfStudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.ComplexProperty(c => c.Name);

        builder.ComplexProperty(c => c.Email);
        
        builder.ComplexProperty(c => c.Phone);
    }
}