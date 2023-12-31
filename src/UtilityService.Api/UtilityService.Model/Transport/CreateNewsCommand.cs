﻿using UtilityService.Model.Model;
using UtilityService.Model.Model.News;

namespace UtilityService.Model.Transport;

public class CreateNewsCommand
{
    public Guid Id { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public string Title { get; set; }
    public string ShortBody { get; set; }
    public string Body { get; set; }

    public Guid HeaderContentId { get; set; }
    public Guid[]? BodyContentIds { get; set; }
    public NewsType Type { get; set; }
    public DateTime CreateDate { get; set; }
    public Status Status { get; set; }
    public Location? Location { get; set; }
    public string[] Tags { get; set; }
}