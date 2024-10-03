using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace htmx_kitchensink.Partials.WorkItem;

[Route("/partials/workitem/list")]
public class ListController : Controller
{
    public static string GetLink(IUrlHelper urlHelper, Guid workItemId) => urlHelper.Content($"~/partials/workitem/{workItemId}/list");
    [HttpGet]
    public IActionResult Get(Guid workItemId)
    {
        var partial = ListPartial.Create(Url);

        return PartialView(ListPartial.PartialName, partial);
    }
}

public class ListPartial
{
    public static readonly string PartialName = "/Partials/WorkItem/List.cshtml";
    public List<ListItem> ListItems { get; set; } = new();

    private ListPartial() { }

    public static ListPartial Create(IUrlHelper urlHelper)
    {
        var listItems = Model.WorkItem.WorkItems.Select(wi => new ListItem
        {
            Id = wi.Id,
            Title = wi.Title,
            Description = wi.Description,
            ReadOnlyLink = ReadOnlyController.GetLink(urlHelper, wi.Id)
        })
        .ToList();

        return new ListPartial
        {
            ListItems = listItems
        };
    }
}

public record ListItem
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public string ReadOnlyLink { get; set; }
}
