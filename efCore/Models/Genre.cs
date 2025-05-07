using System.Text.Json.Serialization;

namespace efCore.API.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    [JsonIgnore] // prevent infinite linking between movie and genre
    public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>(); // one to many relation
}
