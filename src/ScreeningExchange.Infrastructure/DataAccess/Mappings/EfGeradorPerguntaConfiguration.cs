
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;

namespace ScreeningExchange.Infrastructure.DataAccess.Mappings;

public class EfGeradorPerguntaConfiguration : IEntityTypeConfiguration<BuildQuestion>
{
    public void Configure(EntityTypeBuilder<BuildQuestion> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

    }
}