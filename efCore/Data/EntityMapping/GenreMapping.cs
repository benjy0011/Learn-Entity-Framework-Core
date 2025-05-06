using efCore.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace efCore.API.Data.EntityMapping;

public class GenreMapping : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        //builder
        //    .HasMany(genre => genre.Movies)
        //    .WithOne(movie => movie.Genre)
        //    .HasPrincipalKey(genre => genre.Id)
        //    .HasForeignKey(movie => movie.MainGenreId);

        builder.HasData(new Genre
        {
            Id = 1,
            Name = "Drama"
        });
    }
}
