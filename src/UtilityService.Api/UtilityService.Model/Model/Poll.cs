namespace UtilityService.Model;

public class Poll
{
    public Guid Id { get; set; }
    public string[] Tags { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public string Question { get; set; }
    public PollOption[] Options { get; set; }
    public bool MultipleAnswers { get; set; }
    public DateTime CreationDate { get; set; }
}