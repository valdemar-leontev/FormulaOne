namespace FormulaOne.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }

    public DateTime AddedDate { get; set; } = DateTime.UtcNow;
    
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;

    public int Status { get; set; }
}