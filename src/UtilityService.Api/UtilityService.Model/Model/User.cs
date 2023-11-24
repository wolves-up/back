namespace UtilityService.Model;

public class User
{
    public Guid Id { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public Requisites Requisites { get; set; }
}