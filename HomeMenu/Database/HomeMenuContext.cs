using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HomeMenu.Database;

public partial class HomeMenuContext : DbContext
{
    public HomeMenuContext()
    {
    }

    public HomeMenuContext(DbContextOptions<HomeMenuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Dish> Dishes { get; set; }

    public virtual DbSet<Ingridient> Ingridients { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=HomeMenu;Username=postgres;Password=123456789");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Category_pkey");

            entity.ToTable("Category");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Dishes_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Ingridients).HasColumnType("json");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Photo).HasColumnType("character varying");
            entity.Property(e => e.Recipe).HasColumnType("character varying");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.Author)
                .HasConstraintName("Dishes_Author_fkey");

            entity.HasOne(d => d.CategoryNavigation).WithMany(p => p.Dishes)
                .HasForeignKey(d => d.Category)
                .HasConstraintName("Dishes_Category_fkey");
        });

        modelBuilder.Entity<Ingridient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Ingridients_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Payments_pkey");

            entity.Property(e => e.Id).HasColumnType("character varying");
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.FailUrl).HasColumnType("character varying");
            entity.Property(e => e.SuccessUrl).HasColumnType("character varying");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Payments_UserId_fkey");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Profile_pkey");

            entity.ToTable("Profile");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Name).HasColumnType("character varying");
            entity.Property(e => e.Photo).HasColumnType("character varying");
            entity.Property(e => e.UserId).HasColumnName("User_id");

            entity.HasOne(d => d.User).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("Profile_User_id_fkey");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Roles_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Email).HasColumnType("character varying");
            entity.Property(e => e.Password).HasColumnType("character varying");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("Users_Role_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
