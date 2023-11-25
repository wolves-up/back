namespace UtilityService.Model.Transport;

public class CreateUtilityCommand
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Inn { get; set; }
    public string Type { get; set; }
}