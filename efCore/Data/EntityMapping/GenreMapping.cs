using efCore.API.Data.ValueGenerators;
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

        //builder.Property(genre => genre.CreatedDate)
        //    //.HasDefaultValue(DateTime.Now); // one method
        //    //.HasDefaultValueSql("getdate()"); // generate default value at sql side
        //    //.HasDefaultValueSql("GETUTCDATE() AT TIME ZONE 'UTC' AT TIME ZONE 'Singapore Standard Time'"); // convert to Singapore TimeZone
        //    .HasValueGenerator<CreatedDateGenerator>(); // use custom generator

        builder.Property<DateTime>("CreatedDate")
            .HasColumnName("CreatedAt")
            .HasValueGenerator<CreatedDateGenerator>();

        builder.HasData(new Genre
        {
            Id = 1,
            Name = "Drama"
        });
    }
}
