namespace FormulaOne.Entities.Dtos.Responses;

public class GetDriverResponse
{
    public Guid Driverid { get; set; }

    public string FullName { get; set; } = string.Empty;

    public int Number { get; set; }

    public DateTime BirthDay { get; set; }
}