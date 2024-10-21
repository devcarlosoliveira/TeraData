using System.ComponentModel.DataAnnotations;
namespace Web.Mvc.Domain;

public class Tag
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<PostTag> PostTags { get; set; } = [];

    public Tag() { }
}
