using System;
using System.Collections.Generic;
using ABCIgnite.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCIgnite.datab;

public partial class AbcclientDatabaseContext : DbContext
{
    public AbcclientDatabaseContext()
    {
    }

    public AbcclientDatabaseContext(DbContextOptions<AbcclientDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=localhost;Database=ABCClientDatabase;user id=sa; password=iEHjhq5MwZGqSkO4JB8nImqlF5V;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__73951ACD49885936");

            entity.Property(e => e.BookingId).HasColumnName("BookingID");
            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.MemberName).HasMaxLength(255);

            entity.HasOne(d => d.Class).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bookings__ClassI__1367E606");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A0C3768B09");

            entity.Property(e => e.ClassId).HasColumnName("ClassID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
