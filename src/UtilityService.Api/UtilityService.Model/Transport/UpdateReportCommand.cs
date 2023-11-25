﻿using UtilityService.Model.Model;

namespace UtilityService.Model.Transport;

public class UpdateReportCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Message { get; set; }
    public byte[] Content { get; set; }
    public string[] Tags { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public Status Status { get; set; }
}