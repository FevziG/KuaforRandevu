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
        => optionsBuilder.UseSqlServer("Server=(LocalDB)\\MSSQLLocalDB;Database=KuaforRandevuDb;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Photo).HasColumnType("image");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(e => e.AppointmentDateTime).HasColumnType("datetime");
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Admin).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_Appointments_Admins");
        });

        modelBuilder.Entity<BlockedSlot>(entity =>
        {
            entity.Property(e => e.BlockedDateTime).HasColumnType("datetime");
            entity.Property(e => e.Reason).HasMaxLength(100);

            entity.HasOne(d => d.Admin).WithMany(p => p.BlockedSlots)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK_BlockedSlots_Admins");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
