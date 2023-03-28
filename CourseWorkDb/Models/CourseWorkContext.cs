using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CourseWorkDb.Models;

public partial class CourseWorkContext : DbContext
{
    public CourseWorkContext()
    {
    }

    public CourseWorkContext(DbContextOptions<CourseWorkContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apartment> Apartments { get; set; }

    public virtual DbSet<CheckPayment> CheckPayments { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=CourseWorkDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Apartment>(entity =>
        {
            entity.HasKey(e => e.ApartmentId).HasName("PK_ID_ApartmentID");

            entity.ToTable("Apartment");

            entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");
            entity.Property(e => e.AppartmentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Comfort)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("money");
        });

        modelBuilder.Entity<CheckPayment>(entity =>
        {
            entity.HasKey(e => e.CheckPaymentId).HasName("PK_ID_CheckPaymentID");

            entity.ToTable("CheckPayment");

            entity.Property(e => e.CheckPaymentId).HasColumnName("CheckPaymentID");
            entity.Property(e => e.ApartmentId).HasColumnName("ApartmentID");
            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.DateOfCreation).HasColumnType("date");
            entity.Property(e => e.DateOfEviction).HasColumnType("date");
            entity.Property(e => e.DateOfSettlement).HasColumnType("date");

            entity.HasOne(d => d.Apartment).WithMany(p => p.CheckPayments)
                .HasForeignKey(d => d.ApartmentId)
                .HasConstraintName("FK_Client_ApartmentID");

            entity.HasOne(d => d.Client).WithMany(p => p.CheckPayments)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Client_ClientID");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK_ID_ClientID");

            entity.ToTable("Client");

            entity.Property(e => e.ClientId).HasColumnName("ClientID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(9)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.SecondName)
                .HasMaxLength(70)
                .IsUnicode(false);
            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
