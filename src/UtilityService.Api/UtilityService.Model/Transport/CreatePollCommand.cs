namespace UtilityService.Model.Transport;

public class CreatePollCommand
{
    public string Question { get; set; }
    public string[] Options { get; set; }
    public string[] Tags { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public bool MultipleAnswers { get; set; }
}