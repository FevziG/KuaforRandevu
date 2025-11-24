using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KuaforRandevu.Models;

public partial class KuaforRandevuDbContext : DbContext
{
    public KuaforRandevuDbContext()
    {
    }

    public KuaforRandevuDbContext(DbContextOptions<KuaforRandevuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Appointment> Appointments { get; set; }

    public virtual DbSet<BlockedSlot> BlockedSlots { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=KuaforRandevuDb;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);

            // AdminId foreign key
            entity.HasOne(a => a.Admin)
          .WithMany()
          .HasForeignKey(a => a.AdminId)
          .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<BlockedSlot>(entity =>
        {
            entity.Property(e => e.BlockedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(100);

            // AdminId foreign key
            entity.HasOne(b => b.Admin)
                  .WithMany()
                  .HasForeignKey(b => b.AdminId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
