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

            modelBuilder.Entity<PublicationHouse>().Property(ph => ph.Name).IsRequired();

            //Length configuration
            modelBuilder.Entity<Author>().Property(a => a.Name).HasMaxLength(100);
            modelBuilder.Entity<Author>().Property(a => a.Surname).HasMaxLength(100);

            modelBuilder.Entity<Category>().Property(c => c.Name).HasMaxLength(50);

            modelBuilder.Entity<Publication>().Property(p => p.Title).HasMaxLength(150);
            modelBuilder.Entity<Publication>().Property(p => p.ISBN).IsFixedLength().HasMaxLength(13);

            modelBuilder.Entity<PublicationHouse>().Property(ph => ph.Name).HasMaxLength(100);

            //Relations configurations
            modelBuilder.Entity<Publication>().HasOne(p => p.PublicationHouse).WithMany(ph => ph.Publications).HasForeignKey(p => p.PublicationHouseId);

            modelBuilder.Entity<PublicationCategories>().HasOne(pc => pc.Category).WithMany(c => c.PublicationCategories).HasForeignKey(pc => pc.CategoryId);
            modelBuilder.Entity<PublicationCategories>().HasOne(pc => pc.Publication).WithMany(p => p.PublicationCategories).HasForeignKey(pc => pc.PublicationId);

            modelBuilder.Entity<PublicationAuthors>().HasOne(pa => pa.Author).WithMany(a => a.PublicationAuthors).HasForeignKey(pa => pa.AuthorId);
            modelBuilder.Entity<PublicationAuthors>().HasOne(pa => pa.Publication).WithMany(p => p.PublicationAuthors).HasForeignKey(pa => pa.PublicationId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
