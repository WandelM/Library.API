using Library.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.API.DbContexts
{
    public class LibraryContext : DbContext
    {
        /// <summary>
        /// Inherited default constructor
        /// </summary>
        public LibraryContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Authors table table
        /// </summary>
        public virtual DbSet<Author> Authors { get; set; }
        /// <summary>
        /// Categories table
        /// </summary>
        public virtual DbSet<Category> Categories { get; set; }
        /// <summary>
        /// Publication table
        /// </summary>
        public virtual DbSet<Publication> Publications { get; set; }
        /// <summary>
        /// PublicationHouses table
        /// </summary>
        public virtual DbSet<PublicationHouse> PublicationHouses { get; set; }
        /// <summary>
        /// PublicationAuthors table
        /// </summary>
        public virtual DbSet<PublicationAuthors> PublicationAuthors { get; set; }
        /// <summary>
        /// PublicationCategories table
        /// </summary>
        public virtual DbSet<PublicationCategories> PublicationCategories { get; set; }
        /// <summary>
        /// Users of a library
        /// </summary>
        public virtual DbSet<LibraryUser> LibraryUsers { get; set; }
        /// <summary>
        /// Card of a user
        /// </summary>
        public virtual DbSet<UserCard> UserCards { get; set; }

        /// <summary>
        /// Configuration of the entities
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Primary keys cofiguration
            modelBuilder.Entity<Author>().HasKey(a => a.Id);
            modelBuilder.Entity<Category>().HasKey(c => c.Id);
            modelBuilder.Entity<Publication>().HasKey(p => p.Id);
            modelBuilder.Entity<PublicationHouse>().HasKey(ph => ph.Id);
            modelBuilder.Entity<LibraryUser>().HasKey(lu => lu.Id);
            modelBuilder.Entity<UserCard>().HasKey(uc => uc.Id);

            //Composite key configuration
            modelBuilder.Entity<PublicationAuthors>().HasKey(pa => new { pa.PublicationId, pa.AuthorId });
            modelBuilder.Entity<PublicationCategories>().HasKey(pc => new { pc.PublicationId, pc.CategoryId });

            //Required fields configuration
            modelBuilder.Entity<Author>().Property(a => a.Name).IsRequired();
            modelBuilder.Entity<Author>().Property(a => a.Surname).IsRequired();
            modelBuilder.Entity<Author>().Property(a => a.DateOfBirth).IsRequired();

            modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();

            modelBuilder.Entity<Publication>().Property(p => p.Title).IsRequired();
            modelBuilder.Entity<Publication>().Property(p => p.PublicationType).IsRequired();
            modelBuilder.Entity<Publication>().Property(p => p.ISBN).IsRequired();
            modelBuilder.Entity<Publication>().Property(p => p.PublicationDate).IsRequired();
            modelBuilder.Entity<Publication>().Property(p => p.PageCount).IsRequired();
            
            modelBuilder.Entity<Publication>().Property(p => p.InsertDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<Publication>().Property(p => p.InsertDate).HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<PublicationHouse>().Property(ph => ph.Name).IsRequired();

            modelBuilder.Entity<LibraryUser>().Property(lu => lu.Name).IsRequired();
            modelBuilder.Entity<LibraryUser>().Property(lu => lu.Surname).IsRequired();
            modelBuilder.Entity<LibraryUser>().Property(lu => lu.BirthDate).IsRequired();
            modelBuilder.Entity<LibraryUser>().Property(lu => lu.InsertDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<LibraryUser>().Property(lu => lu.InsertDate).HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<UserCard>().Property(uc => uc.DueDate).IsRequired();
            modelBuilder.Entity<UserCard>().Property(uc => uc.CreatedDate).ValueGeneratedOnAdd();
            modelBuilder.Entity<UserCard>().Property(uc => uc.CreatedDate).HasDefaultValueSql("GETUTCDATE()");
            modelBuilder.Entity<UserCard>().Property(uc => uc.CardNumber).IsRequired();
            modelBuilder.Entity<UserCard>().Property(uc => uc.UserId).IsRequired();
            //Length configuration
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(100);
            modelBuilder.Entity<Author>().Property(a => a.Surname).HasMaxLength(100);

            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50);

            modelBuilder.Entity<Publication>().Property(p => p.Title).HasMaxLength(150);
            modelBuilder.Entity<Publication>().Property(p => p.ISBN).IsFixedLength().HasMaxLength(13);

            modelBuilder.Entity<PublicationHouse>().Property(ph => ph.Name).HasMaxLength(100);

            modelBuilder.Entity<LibraryUser>().Property(lu => lu.Name).HasMaxLength(50);
            modelBuilder.Entity<LibraryUser>().Property(lu => lu.Surname).HasMaxLength(80);
            //Relations configurations
            modelBuilder.Entity<Publication>().HasOne(p => p.PublicationHouse).WithMany(ph => ph.Publications).HasForeignKey(p => p.PublicationHouseId);

            modelBuilder.Entity<PublicationCategories>().HasOne(pc => pc.Category).WithMany(c => c.PublicationCategories).HasForeignKey(pc => pc.CategoryId);
            modelBuilder.Entity<PublicationCategories>().HasOne(pc => pc.Publication).WithMany(p => p.PublicationCategories).HasForeignKey(pc => pc.PublicationId);

            modelBuilder.Entity<PublicationAuthors>().HasOne(pa => pa.Author).WithMany(a => a.PublicationAuthors).HasForeignKey(pa => pa.AuthorId);
            modelBuilder.Entity<PublicationAuthors>().HasOne(pa => pa.Publication).WithMany(p => p.PublicationAuthors).HasForeignKey(pa => pa.PublicationId);

            modelBuilder.Entity<UserCard>().HasOne(uc => uc.LibraryUser).WithOne(lu => lu.UserCard).HasForeignKey<UserCard>(uc => uc.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
