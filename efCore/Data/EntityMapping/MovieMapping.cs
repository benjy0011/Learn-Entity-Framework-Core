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
    }
}
