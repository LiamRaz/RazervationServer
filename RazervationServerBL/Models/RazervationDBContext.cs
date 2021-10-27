using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace RazervationServerBL.Models
{
    public partial class RazervationDBContext : DbContext
    {
        public RazervationDBContext()
        {
        }

        public RazervationDBContext(DbContextOptions<RazervationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bservice> Bservices { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<BusinessDay> BusinessDays { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Favorite> Favorites { get; set; }
        public virtual DbSet<History> Histories { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<ReserveStatus> ReserveStatuses { get; set; }
        public virtual DbSet<ServicesInBusiness> ServicesInBusinesses { get; set; }
        public virtual DbSet<SpecialNumberOfWorker> SpecialNumberOfWorkers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost\\sqlexpress;Database=RazervationDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Hebrew_CI_AS");

            modelBuilder.Entity<Bservice>(entity =>
            {
                entity.HasKey(e => e.ServiceId)
                    .HasName("PK__BService__C51BB00AE0E2FA2D");

                entity.ToTable("BServices");
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.Property(e => e.Bio).HasMaxLength(1);

                entity.Property(e => e.BusinessAddress)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.FacebookUrl).HasMaxLength(255);

                entity.Property(e => e.InstagramUrl).HasMaxLength(255);

                entity.Property(e => e.InternetUrl).HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businesses_categoryid_foreign");

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businesses_userid_foreign");
            });

            modelBuilder.Entity<BusinessDay>(entity =>
            {
                entity.HasKey(e => e.DayId)
                    .HasName("PK__Business__BF3DD8C5989439A9");

                entity.ToTable("BusinessDay");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessDays)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businessday_businessid_foreign");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.UserNameNavigation)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.UserName)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("clients_userid_foreign");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.AutoCommentId)
                    .HasName("PK__Comments__44A0477E16ABCD40");

                entity.Property(e => e.CommentText).HasMaxLength(1);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_businessid_foreign");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_clientid_foreign");
            });

            modelBuilder.Entity<Favorite>(entity =>
            {
                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favorites_Businessid_foreign");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Favorites)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("favorites_clientid_foreign");
            });

            modelBuilder.Entity<History>(entity =>
            {
                entity.ToTable("History");

                entity.Property(e => e.Hdate)
                    .HasColumnType("datetime")
                    .HasColumnName("HDate");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("history_businessid_foreign");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Histories)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("history_clientid_foreign");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservation");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.StartDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_businessid_foreign");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_clientid_foreign");

                entity.HasOne(d => d.Day)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.DayId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_dayid_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_BServicesid_foreign");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("reservation_StatusId_foreign");
            });

            modelBuilder.Entity<ReserveStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__ReserveS__C8EE206352649345");

                entity.ToTable("ReserveStatus");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<ServicesInBusiness>(entity =>
            {
                entity.ToTable("ServicesInBusiness");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.ServicesInBusinesses)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servicesinbusiness_businessid_foreign");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.ServicesInBusinesses)
                    .HasForeignKey(d => d.ServiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("servicesinbusiness_serviceid_foreign");
            });

            modelBuilder.Entity<SpecialNumberOfWorker>(entity =>
            {
                entity.HasKey(e => e.SpecialDate)
                    .HasName("PK__SpecialN__754CBCC4EF8BCD00");

                entity.Property(e => e.SpecialDate).HasColumnType("datetime");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.SpecialNumberOfWorkers)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("specialnumberofworkers_businessid_foreign");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Users__C9F2845752E2F352");

                entity.HasIndex(e => e.PhoneNumber, "UQ__Users__85FB4E387B851880")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534553229FA")
                    .IsUnique();

                entity.Property(e => e.UserName).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
