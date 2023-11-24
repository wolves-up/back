namespace UtilityService.Api.DataSources.Model;

public class UserEntity : EntityBase
{
	public DateTime RegistrationDate { get; set; }
	public RequisitesEntity Requisites { get; set; }
	public string PasswordHash { get; set; }
}

public class RequisitesEntity
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string MiddleName { get; set; }
	public string EmailAddress { get; set; }
}