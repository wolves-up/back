using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public interface IPollService
{
    Task<Guid> CreatePoll(CreatePollCommand createPollCommand);
    Task Vote(Guid pollId, string[] options, Guid voterId);
    Task GetPolls(GetPollsCommand getPollsCommand);
}