using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class ScopeConfiguration
    {
        public void Configure(EntityTypeBuilder<Scope> builder)
        {
            builder.ToTable("Scope")
                   .HasKey(s => s.Id);

            builder.HasMany(s => s.Prompts)
                   .WithOne(p => p.Scope)
                   .HasForeignKey(p => p.ScopeId);

        }
    }
}
