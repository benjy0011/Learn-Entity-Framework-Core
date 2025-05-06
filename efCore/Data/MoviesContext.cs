using efCore.API.Data.EntityMapping;
using efCore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace efCore.API.Data;

public class MoviesContext : DbContext
{
    //public DbSet<Movie> Movies { get; set; }

    public DbSet<Movie> Movies => Set<Movie>();


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("""
            Data Source=localhost,1433;
            Initial Catalog=MovieDB;
            User ID=sa;
            Password=System8188!;
            TrustServerCertificate=True;
            """);

        // Not proper logging
        optionsBuilder.LogTo(Console.WriteLine);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        //modelBuilder.Entity<Movie>() // create a table based on 'Movie' model
        //    .ToTable("Picture") // create a table and change/give a name like earlier
        //    .HasKey(movie => movie.Id);

        //modelBuilder.Entity<Movie>().Property(movie => movie.Title)
        //    .HasColumnType("varchar")
        //    .HasMaxLength(128)
        //    .IsRequired();

        //modelBuilder.Entity<Movie>().Property(movie => movie.ReleaseDate)
        //    .HasColumnType("date");

        //modelBuilder.Entity<Movie>().Property(movie => movie.Synopsis)
        //    .HasColumnType("varchar(max)")
        //    .HasColumnName("Plot");

        modelBuilder.ApplyConfiguration(new GenreMapping());
        modelBuilder.ApplyConfiguration(new MovieMapping());
    }
}   
