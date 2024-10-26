
namespace Web.Mvc.Domain;

public abstract class Entity
{
    public virtual Guid Id { get; set; }
    protected Entity() { }
}
