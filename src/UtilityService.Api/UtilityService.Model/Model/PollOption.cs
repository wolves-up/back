namespace UtilityService.Model;

public class PollOption
{
    public string Title { get; set; }
    public Guid[] Voters { get; set; }
}