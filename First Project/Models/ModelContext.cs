using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace First_Project.Models;

public partial class ModelContext : DbContext
{
    public ModelContext()
    {
    }

    public ModelContext(DbContextOptions<ModelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<About> Abouts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contactu> Contactus { get; set; }

    public virtual DbSet<Home> Homes { get; set; }

    public virtual DbSet<Login> Logins { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Testimonial> Testimonials { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRecipe> UserRecipes { get; set; }

    public virtual DbSet<Visa> Visas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseOracle("USER ID=C##First_Project;PASSWORD=Test321;DATA SOURCE=DESKTOP-RKOLS4I.home:1521/xe");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("C##FIRST_PROJECT")
            .UseCollation("USING_NLS_COMP");

        modelBuilder.Entity<About>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008377");

            entity.ToTable("ABOUT");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.ImageOne)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_ONE");
            entity.Property(e => e.ImageTwo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_TWO");
            entity.Property(e => e.ParagraphOne)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH_ONE");
            entity.Property(e => e.ParagraphTwo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH_TWO");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("SYS_C008332");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.CategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.ImageName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_NAME");
        });

        modelBuilder.Entity<Contactu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008379");

            entity.ToTable("CONTACTUS");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Ipaddress)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("IPADDRESS");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("NAME");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("PHONENUMBER");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS");
        });

        modelBuilder.Entity<Home>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008375");

            entity.ToTable("HOME");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.ImageOne)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_ONE");
            entity.Property(e => e.ImageTwo)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_TWO");
            entity.Property(e => e.ParagraphOne)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH_ONE");
            entity.Property(e => e.ParagraphTwo)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("PARAGRAPH_TWO");
        });

        modelBuilder.Entity<Login>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008365");

            entity.ToTable("LOGIN");

            entity.HasIndex(e => e.UserName, "SYS_C008366").IsUnique();

            entity.HasIndex(e => e.Password, "SYS_C008367").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.Password)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("PASSWORD");
            entity.Property(e => e.RoleId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");

            entity.HasOne(d => d.Role).WithMany(p => p.Logins)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_ROLEID");

            entity.HasOne(d => d.User).WithMany(p => p.Logins)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_U_ID_");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.RecipeId).HasName("SYS_C008334");

            entity.ToTable("RECIPE");

            entity.Property(e => e.RecipeId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.CatId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CAT_ID");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Ingredients)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("INGREDIENTS");
            entity.Property(e => e.Price)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("PRICE");
            entity.Property(e => e.RecipeName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("RECIPE_NAME");
            entity.Property(e => e.Sale)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("SALE");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");

            entity.HasOne(d => d.Cat).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_CAT_ID");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("SYS_C008349");

            entity.ToTable("ROLE");

            entity.Property(e => e.RoleId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        modelBuilder.Entity<Testimonial>(entity =>
        {
            entity.HasKey(e => e.TestimonialId).HasName("SYS_C008346");

            entity.ToTable("TESTIMONIAL");

            entity.Property(e => e.TestimonialId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("TESTIMONIAL_ID");
            entity.Property(e => e.CreateDate)
                .HasPrecision(6)
                .HasColumnName("CREATE_DATE");
            entity.Property(e => e.Message)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MESSAGE");
            entity.Property(e => e.Rating)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RATING");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("STATUS");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.User).WithMany(p => p.Testimonials)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_U_ID");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("SYS_C008337");

            entity.ToTable("USER_");

            entity.HasIndex(e => e.Email, "SYS_C008338").IsUnique();

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Fname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("FNAME");
            entity.Property(e => e.ImageName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("IMAGE_NAME");
            entity.Property(e => e.Lname)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("LNAME");
        });

        modelBuilder.Entity<UserRecipe>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("SYS_C008340");

            entity.ToTable("USER_RECIPE");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("ID");
            entity.Property(e => e.PurchaseDate)
                .HasPrecision(6)
                .HasColumnName("PURCHASE_DATE");
            entity.Property(e => e.Quantity)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("QUANTITY");
            entity.Property(e => e.RecipeId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("RECIPE_ID");
            entity.Property(e => e.UserId)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("USER_ID");

            entity.HasOne(d => d.Recipe).WithMany(p => p.UserRecipes)
                .HasForeignKey(d => d.RecipeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_RID");

            entity.HasOne(d => d.User).WithMany(p => p.UserRecipes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_UID");
        });

        modelBuilder.Entity<Visa>(entity =>
        {
            entity.HasKey(e => e.VisaId).HasName("SYS_C008371");

            entity.ToTable("VISA");

            entity.HasIndex(e => e.VisaNumber, "SYS_C008372").IsUnique();

            entity.HasIndex(e => e.Cvc, "SYS_C008373").IsUnique();

            entity.Property(e => e.VisaId)
                .ValueGeneratedOnAdd()
                .HasColumnType("NUMBER(38)")
                .HasColumnName("VISA_ID");
            entity.Property(e => e.Balance)
                .HasColumnType("FLOAT")
                .HasColumnName("BALANCE");
            entity.Property(e => e.Cvc)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("CVC");
            entity.Property(e => e.ExpirationDate)
                .HasColumnType("DATE")
                .HasColumnName("EXPIRATION_DATE");
            entity.Property(e => e.VisaNumber)
                .HasColumnType("NUMBER(38)")
                .HasColumnName("VISA_NUMBER");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
