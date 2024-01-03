namespace Tp1_CoreApplication.Domain;

public class Branch
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? StreetNumber { get; set; }
    public string? StreetName { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? Country { get; set; }
    public bool ActiveBranch { get; set; } = false;

    //Propriété de navigation
    public List<Car> Cars { get; set; } = new();

    public override string ToString()
    {
        return $"{Id}, {Name}, {StreetNumber},{StreetName}, {City}, {Province}, {PostalCode}, {Country}, {Cars.Count}";
    }
}
