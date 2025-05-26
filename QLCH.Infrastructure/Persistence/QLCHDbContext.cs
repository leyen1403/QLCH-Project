using Microsoft.EntityFrameworkCore;
using QLCH.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.Infrastructure.Persistence
{
    public class QLCHDbContext : DbContext
    {
        public QLCHDbContext(DbContextOptions<QLCHDbContext> options) : base(options)
        {
        }
        public DbSet<ChucVu> ChucVus { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChucVu>(entity =>
            {
                entity.ToTable("ChucVu");

                entity.HasKey(e => e.MaChucVu); 

                entity.Property(e => e.TenChucVu)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.HasIndex(e => e.TenChucVu)
                      .IsUnique();

                entity.Property(e => e.HeSoLuong)
                      .HasColumnType("decimal(5,2)")
                      .IsRequired();

                entity.Property(e => e.MoTa)
                      .HasMaxLength(255);
            });
        }

    }
}
