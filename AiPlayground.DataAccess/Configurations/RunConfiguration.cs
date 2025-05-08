using AiPlayground.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AiPlayground.DataAccess.Configurations
{
    public class RunConfiguration
    {
        public void Configure(EntityTypeBuilder<Run> builder) {

            builder.ToTable("Run")
                .HasKey(x => x.Id);


            builder.Property(r => r.Rating)
                   .HasDefaultValue(0);

            builder.Property(r => r.UserRating)
                   .HasDefaultValue(0);

            builder.HasOne(r => r.Model)
                   .WithMany(m => m.Runs)
                   .HasForeignKey(r => r.ModelId)
                   .HasConstraintName("FK_Run_Model");

            builder.HasOne(r => r.Prompt)
                   .WithMany(p => p.Runs)
                   .HasForeignKey(r => r.PromptId)
                   .HasConstraintName("FK_Run_Prompt"); ;

        }
}
}
