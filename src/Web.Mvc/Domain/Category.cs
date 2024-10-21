﻿using System.ComponentModel.DataAnnotations;
namespace Web.Mvc.Domain;

public class Category
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;

    public virtual ICollection<Post> Posts { get; set; } = [];

    public Category() { }
}
