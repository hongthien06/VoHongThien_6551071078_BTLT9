using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Cau5_QuanLyDatTour.Models;

public partial class QlDatTourContext : DbContext
{
    public QlDatTourContext(DbContextOptions<QlDatTourContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DatTour> DatTours { get; set; }

    public virtual DbSet<KhachHang> KhachHangs { get; set; }

    public virtual DbSet<Tour> Tours { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DatTour>(entity =>
        {
            entity.HasKey(e => e.MaDat).HasName("PK__DatTour__3D8883304B528A4D");

            entity.ToTable("DatTour");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.NgayDat).HasColumnType("datetime");
            entity.Property(e => e.TongTienVnd)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TongTienVND");
            entity.Property(e => e.TyGia).HasColumnType("decimal(18, 4)");

            entity.HasOne(d => d.MaKhNavigation).WithMany(p => p.DatTours)
                .HasForeignKey(d => d.MaKh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DatTour__MaKH__4F7CD00D");

            entity.HasOne(d => d.MaTourNavigation).WithMany(p => p.DatTours)
                .HasForeignKey(d => d.MaTour)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DatTour__MaTour__5070F446");
        });

        modelBuilder.Entity<KhachHang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KhachHan__2725CF1EB6A63266");

            entity.ToTable("KhachHang");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.HoTen).HasMaxLength(100);
            entity.Property(e => e.SoDienThoai)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tour>(entity =>
        {
            entity.HasKey(e => e.MaTour).HasName("PK__Tour__4E5557DE427948C3");

            entity.ToTable("Tour");

            entity.Property(e => e.DiemDen).HasMaxLength(50);
            entity.Property(e => e.GiaNgoaiTe).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.NgoaiTe)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.TenTour).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
