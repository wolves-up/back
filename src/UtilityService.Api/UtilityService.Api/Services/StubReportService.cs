using UtilityService.Model;
using UtilityService.Model.Model;
using UtilityService.Model.Model.Reports;
using UtilityService.Model.Transport;

namespace UtilityService.Api.Services;

public class StubReportService : IReportService
{
    private static Guid _waterServiceGuid = Guid.Parse("9836534E-280A-4B78-ABAE-0283FD2FA5C9");
    private static Guid _userGuid = Guid.Parse("511DD071-7091-4356-A483-D9920A0ECC09");

    private static Report[] _reports = new[]
    {
        new Report()
        {
            ContentIds = null,
            UserId = _userGuid,
            CreationDate = DateTime.Now,
            Id = Guid.Parse("2397561E-3284-4294-B1BC-033A60C7D782"),
            Tags = new[] { "Водоканал", "Прорыв трубы" },
            Title = "На Садовом прорвало трубу",
            Message = "Качалку у дома сани залило говном",
            ResponsibleServiceId = _waterServiceGuid,
            Location = new Location(),
            Status = Status.New
        },
        new Report()
        {
            ContentIds = null,
            UserId = _userGuid,
            CreationDate = DateTime.Now,
            Id = Guid.Parse("DFACD9E5-4663-4E92-B5DA-32B8C0378797"),
            Tags = new[] { "Водоканал", "Канализационный люк" },
            Title = "На Ленина нету канализационного люка",
            Message = "Упали две бабки",
            ResponsibleServiceId = _waterServiceGuid,
            Location = new Location(),
            Status = Status.Completed
        }
    };
    
    public Task<Report> GetReportById(Guid reportId)
    {
        return Task.FromResult(_reports.First(x => x.Id == reportId));
    }
    
    public Task<Report[]> GetReports(GetReportsCommand getReportsCommand)
    {
        return Task.FromResult(_reports
            .Where(x => getReportsCommand.StartDate < x.CreationDate &&
                        getReportsCommand.EndDate > x.CreationDate &&
                        getReportsCommand.Statuses.Contains(x.Status) &&
                        getReportsCommand.UserId == x.UserId &&
                        getReportsCommand.ResponsibleServiceId == x.ResponsibleServiceId)
            .ToArray());
    }

    public Task<Report[]> FindUtilityServiceReports(Guid utilityServiceId)
    {
        return Task.FromResult(_reports.Where(x => x.ResponsibleServiceId == utilityServiceId)
            .ToArray());
    }
    
    public Task<Report[]> FindUserReports(Guid userId)
    {
        return Task.FromResult(_reports.Where(x => x.UserId == userId)
            .ToArray());
    }

    public Task<Report> Create(CreateReportCommand createReportCommand, Guid userId)
    {
        return Task.FromResult(new Report());
    }
    
    public Task Update(UpdateReportCommand updateReportCommand)
    {
        return Task.CompletedTask;
    }
}