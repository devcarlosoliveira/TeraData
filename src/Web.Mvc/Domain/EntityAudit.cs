
using System.ComponentModel.DataAnnotations;

namespace Web.Mvc.Domain;

public abstract class EntityAudit
{
    [Key]
    public virtual Guid Id { get; set; } = Guid.NewGuid();
    public bool IsDeleted { get; set; } = false;
    public Guid CreateBy { get; set; } = new();
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public Guid UpdateBy { get; set; } = new();
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    protected EntityAudit() { }
}


