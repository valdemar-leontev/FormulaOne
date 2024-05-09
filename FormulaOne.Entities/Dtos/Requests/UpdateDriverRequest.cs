namespace FormulaOne.Entities.Dtos.Requests;

public class UpdateDriverRequest
{
    public Guid DriverId { get; set; }
    
    public string FirstName { get; set; } = string.Empty;
    
    public string LastName { get; set; } = string.Empty;
    
    public int Number { get; set; }
    
    public DateTime BirthDay { get; set; }
}