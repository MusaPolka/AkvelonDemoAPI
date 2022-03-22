using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccessLayer.Configs
{
    public class ProjectConfig : IEntityTypeConfiguration<Project>
    {
        // Configuring our Project model
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Projects");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.StartedAt).IsRequired();
            builder.Property(x => x.CompletedAt);
            builder.Property(x => x.Status).IsRequired();
        }
    }
}
