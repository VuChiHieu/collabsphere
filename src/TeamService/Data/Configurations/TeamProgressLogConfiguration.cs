using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeamService.Models.Entities;

namespace TeamService.Data.Configurations;

public class TeamProgressLogConfiguration : IEntityTypeConfiguration<TeamProgressLog>
{
    public void Configure(EntityTypeBuilder<TeamProgressLog> builder)
    {
        builder.HasKey(tpl => tpl.LogId);

        // Indexes
        builder.HasIndex(tpl => tpl.TeamId);
        builder.HasIndex(tpl => tpl.LoggedAt);

        // Properties
        builder.Property(tpl => tpl.ProgressPercentage).HasColumnType("decimal(5,2)").IsRequired();
        builder.Property(tpl => tpl.Notes).HasColumnType("text");
        builder.Property(tpl => tpl.LoggedAt).HasDefaultValueSql("NOW()");
    }
}