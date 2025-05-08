using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
    {
        public void Configure(EntityTypeBuilder<Prompt> builder)
        {
            builder.ToTable("Prompt")
                .HasKey(x => x.Id);
        }
    }
}
