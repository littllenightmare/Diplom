using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HomeMenu.Database;

public partial class MenuContext : DbContext
{
    public MenuContext()
    {
    }

    public MenuContext(DbContextOptions<MenuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Yum> Yums { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5000;Database=Menu;Username=postgres;Password=123456789");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Yum>(entity =>
        {
            entity.HasKey(e => e.Название).HasName("Yum_pkey");

            entity.ToTable("Yum");

            entity.Property(e => e.Название).HasMaxLength(26);
            entity.Property(e => e.БелкиФулл).HasColumnName("Белки_фулл");
            entity.Property(e => e.ЖирыФулл).HasColumnName("Жиры_фулл");
            entity.Property(e => e.ИнгридиентыФулл)
                .HasColumnType("json")
                .HasColumnName("Ингридиенты_фулл");
            entity.Property(e => e.КалорииФулл).HasColumnName("Калории_фулл");
            entity.Property(e => e.УглеводыФулл).HasColumnName("Углеводы_фулл");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
