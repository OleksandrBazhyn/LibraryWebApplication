using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibraryWebApplication.Models;

public partial class DbAndInformationSystemsContext : DbContext
{
    public DbAndInformationSystemsContext()
    {
    }

    public DbAndInformationSystemsContext(DbContextOptions<DbAndInformationSystemsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BooksIssue> BooksIssues { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Library> Libraries { get; set; }

    public virtual DbSet<Reader> Readers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = Bazhyn\\BAZHYNSERVER; Database= DB and information systems; Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Автори");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Birth).HasColumnType("date");
            entity.Property(e => e.Citizenship)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Книга");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Author)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Genre)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Language)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PublicationYear).HasColumnType("date");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Author)
                .HasConstraintName("FK_Книги_Автори");

            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Genre)
                .HasConstraintName("FK_Книги_Жанри");

            entity.HasOne(d => d.LanguageNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Language)
                .HasConstraintName("FK_Книги_Мови");
        });

        modelBuilder.Entity<BooksIssue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Видача книг");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.BooksId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("BooksID");
            entity.Property(e => e.BorrowerLibraryId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("BorrowerLibraryID");
            entity.Property(e => e.IssuedDate).HasColumnType("date");
            entity.Property(e => e.ReaderId)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ReaderID");

            entity.HasOne(d => d.Books).WithMany(p => p.BooksIssues)
                .HasForeignKey(d => d.BooksId)
                .HasConstraintName("FK_Видачі книг_Книги");

            entity.HasOne(d => d.BorrowerLibrary).WithMany(p => p.BooksIssues)
                .HasForeignKey(d => d.BorrowerLibraryId)
                .HasConstraintName("FK_Видачі книг_Бібліотеки");

            entity.HasOne(d => d.Reader).WithMany(p => p.BooksIssues)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("FK_Видачі_книг_Читач");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Жанри");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Genre1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Genre");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Мови");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Language1)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Language");
        });

        modelBuilder.Entity<Library>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Бібліотека");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
            entity.Property(e => e.WorkHours).HasColumnType("text");
        });

        modelBuilder.Entity<Reader>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Читач");

            entity.Property(e => e.Id)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("ID");
            entity.Property(e => e.Address).HasColumnType("text");
            entity.Property(e => e.Email)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PhoneNumber).HasColumnType("text");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
