namespace FormulaOne.Entities;

public class Driver : BaseEntity
{
    public Driver()
    {
        Achievements = new HashSet<Achievement>();
    }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public int Number { get; set; }

    public DateTime BirthDay { get; set; }


    public virtual ICollection<Achievement>? Achievements { get; set; }
}