
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;


namespace ScreeningExchange.Infrastructure.DataAccess.Mappings;

public class EfBuildQuestionConfiguration : IEntityTypeConfiguration<BuildQuestion>
{
    public void Configure(EntityTypeBuilder<BuildQuestion> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .HasConversion<UlidToStringConverter>()
            .HasConversion<UlidToBytesConverter>();

        builder.Property(c => c.Questions)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<BuildQuestion.QuestionCollection>>(
                    v,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new PrivateResolver(),
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    }
                )!
        );

        builder.Property(c => c.Flows)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<BuildQuestion.FlowCollection>>(
                    v,
                    new JsonSerializerSettings()
                    {
                        ContractResolver = new PrivateResolver(),
                        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                    }
                )!
        );
    }
}