using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace htmx_kitchensink.Partials.WorkItem;

[Route("/partials/workitems/{workItemId}/readonly")]
public class ReadOnlyController : Controller
{
    public static string GetLink(IUrlHelper urlHelper, Guid workItemId) => urlHelper.Content($"~/partials/workitems/{workItemId}/readonly");
    [HttpGet]
    public IActionResult Get(Guid workItemId)
    {
        // Save the new work item
        var partial = ReadOnlyPartial.Create(Url, workItemId);

        return PartialView(ReadOnlyPartial.PartialName, partial);
    }
}

public class ReadOnlyPartial
{
    public static readonly string PartialName = "/Partials/WorkItem/ReadOnly.cshtml";
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }

    private ReadOnlyPartial() { }
    public static ReadOnlyPartial Create(IUrlHelper urlHelper, Guid workItemId)
    {
        var workItem = Model.WorkItem.WorkItems.FirstOrDefault(wi => wi.Id == workItemId);
        if (workItem == null)
        {
            return new ReadOnlyPartial();
        }

        return new ReadOnlyPartial
        {
            Id = workItem.Id,
            Title = workItem.Title,
            Description = workItem.Description
        };
    }
}
