using System;
using Microsoft.EntityFrameworkCore;
using ProdApi.Models;

namespace ProdApi.Context;

public class TaskDbContext : DbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
    {
    }

    public DbSet<TaskItem> Tasks { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<User> Users { get; set; }



    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TaskItem>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Title).IsRequired().HasMaxLength(100);
            e.Property(t => t.CreatedAt).IsRequired();
            e.Property(t => t.Description).HasMaxLength(500);
            e.Property(t => t.Status).IsRequired().HasConversion<string>();
            e.HasMany(t => t.Tags)
                .WithMany(t => t.Tasks)
                .UsingEntity<Dictionary<string, object>>(
                    "TaskItemTags",
                    j => j.HasOne<Tag>().WithMany().HasForeignKey("TagId").OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<TaskItem>().WithMany().HasForeignKey("TaskItemId").OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("TaskItemId", "TagId");
                        j.ToTable("TaskItemTags");
                        j.HasIndex("TagId");
                    });
        });

        modelBuilder.Entity<Tag>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Name).IsRequired().HasMaxLength(50);
            e.HasIndex(t => t.Name).IsUnique();
        });

        modelBuilder.Entity<User>(e =>
        {
            e.HasKey(t => t.Id);
            e.Property(t => t.Email).IsRequired();
            e.Property(t => t.HashPassword).IsRequired();
            e.Property(t => t.Role).IsRequired();
            e.HasIndex(t => t.Email).IsUnique();

        });
    }
}
