using System.Reflection.Emit;
using efCore.API.Data.ValueConverters;
using efCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace efCore.API.Data.EntityMapping;

public class MovieMapping : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder // create a table based on 'Movie' model
            .ToTable("Picture") // create a table and change/give a name like earlier
            .HasQueryFilter(movie => movie.ReleaseDate >= new DateTime(2000, 1, 1)) // Filter and not visible to user (only in SQL), logic is add WHERE in every clause
            .HasKey(movie => movie.Id);

        builder.Property(movie => movie.Title)
            .HasColumnType("varchar")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(movie => movie.ReleaseDate)
            //.HasColumnType("date");
            .HasColumnType("char(8)")
            .HasConversion(new DateTimeToChar8Converter());  // value conversion in EF

        builder.Property(movie => movie.Synopsis)
            .HasColumnType("varchar(max)")
            .HasColumnName("Plot");

        builder.Property(movie => movie.AgeRating)
            .HasColumnType("varchar(32)")
            .HasConversion<string>();  // convert 18 to 'Adult', but will cause misbehaviour cuz diff data type


        builder.OwnsOne(movie => movie.Director) // more power and control
            .ToTable("Movie_Directors"); // move them to a child(seperated) table 
        //builder.ComplexProperty(movie => movie.Director);
        //.Property(director => director.FirstName) // can use this for further config



        builder.OwnsMany(movie => movie.Actors) // more power and control
            .ToTable("Movie_Actors"); // move them to a child(seperated) table 





        // Movie (many to one) Genre
        builder
            .HasOne(movie => movie.Genre)
            .WithMany(genre => genre.Movies)
            .HasPrincipalKey(genre => genre.Id)
            .HasForeignKey(movie => movie.MainGenreId);




        // Seed - data that needs to be created always
        builder.HasData(new Movie
        {
            Id = 1,
            Title = "Fight Club",
            ReleaseDate = new DateTime(1999, 9, 10),
            Synopsis = "Ed Norton and Brad Pitt have a couple of fist fights with each other.",
            MainGenreId = 1,
            AgeRating = AgeRating.Adolescent
        });



        builder.OwnsOne(movie => movie.Director)
            .HasData(new { MovieId = 1, FirstName = "David", LastName = "Fincher" });


        builder.OwnsMany(movie => movie.Actors)
            .HasData(
                new { MovieId = 1, Id = 1, FirstName = "Edward", LastName = "Norton" },
                new { MovieId = 1, Id = 2, FirstName = "Brad", LastName = "Pitt" }
            );
    
    }
}
