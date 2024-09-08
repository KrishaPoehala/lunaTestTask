using LunaTestTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LunaTestTask.Infrastructure.Persistance.Configuration;

public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(t => t.DueDate)
            .HasColumnType("date");

        builder.HasOne(x => x.User)
            .WithMany(x => x.Tasks)
            .HasForeignKey(x => x.UserId);
    }
}