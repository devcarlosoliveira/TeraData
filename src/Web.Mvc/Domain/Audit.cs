namespace Web.Mvc.Domain;

public interface IAudit
{
    public bool IsDeleted { get; set; }
    public Guid CreateBy { get; set; }
    public DateTime CreateAt { get; set; }
    public Guid UpdateBy { get; set; }
    public DateTime UpdateAt { get; set; }
}
