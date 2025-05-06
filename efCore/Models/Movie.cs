using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace efCore.API.Models;

//[Table("Pictures")] // change the name of table, can change because got settings in Program.cs
public class Movie
{
    //[Key]
    public int Id { get; set; }

    //[MaxLength(128)]
    //[Column(TypeName = "varchar")]
    //[Required]
    public string? Title { get; set; }

    //[Column(TypeName = "date")]
    public DateTime ReleaseDate { get; set; }

    //[Column("Plot", TypeName = "varchar(max)")]
    public string? Synopsis { get; set; }  
    


    public Genre? Genre { get; set; } // like this is enough to declare 1 to many
    public int? MainGenreId { get; set; }
}

public class MovieTitle
{
    public int Id { get; set; }
    public string? Title { get; set; }
}
