﻿namespace UtilityService.Model.Model.News;

public class ShortNews
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string ShortBody { get; set; }
    public Guid HeaderContentId { get; set; }
    public NewsType Type { get; set; }
    public Guid ResponsibleServiceId { get; set; }
    public DateTime CreateDate { get; set; }
    public Status Status { get; set; }
    public Location? Location { get; set; }
    public string[] Tags { get; set; }
    public int LikeCount { get; set; }
}