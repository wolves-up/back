namespace UtilityService.Model.Model;

public class User
{
    public Guid Id { get; set; }

    public Role Role { get; set; }
    
    public DateTime RegistrationDate { get; set; }
    
    public Requisites Requisites { get; set; }
}

public enum Role
{
    User,
    Moderator,
    Admin
}