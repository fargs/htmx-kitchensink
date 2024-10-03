using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace htmx_kitchensink.Partials.WorkItem;

[Route("/partials/workitem/new")]
public class NewController : Controller
{
    public static readonly string PartialName = "/Partials/WorkItem/New.cshtml";

    public static string GetLink(IUrlHelper urlHelper) => $"{urlHelper.Content("~/partials/workitem/new")}";
    [HttpGet]
    public IActionResult Get()
    {
        return PartialView(PartialName);
    }

    public static string PostLink(IUrlHelper urlHelper) => $"{urlHelper.Content("~/partials/workitem/new")}";
    [HttpPost]
    public IActionResult Post([FromQuery] NewConfig config, [FromForm] NewModel model)
    {
        if (!ModelState.IsValid)
        {
            // Set the status code to 400 bad request
            HttpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            HttpContext.Response.Headers.Add("HX-Trigger", "newWorkItemFailed");
            // The form is rendered with the validation errors.
            return PartialView(NewPartial.PartialName,
                new NewPartial
                {
                    Model = model

                }
            );
        }

        // Save the new work item
        var workItem = new Model.WorkItem
        {
            Id = Guid.NewGuid(),
            Title = model.Title,
            Description = model.Description
        };

        // Construct the links (you can generate as many as you need)
        var data = new
        {
            createdId = workItem.Id,
            readOnlyLink = WorkItem.ReadOnlyController.GetLink(Url, workItem.Id),
        };

        // Pass the links in the HX-Trigger header as JSON
        var jsonData = new
        {
            workItemCreated = data
        };
        Response.Headers.Add("HX-Trigger", System.Text.Json.JsonSerializer.Serialize(jsonData));
        HttpContext.Response.StatusCode = StatusCodes.Status201Created;

        return PartialView(NewPartial.PartialName,
            new NewPartial
            {
                Model = model,
            });
    }
}

public class NewPartial
{
    public static string PartialName = "~/Partials/WorkItem/New.cshtml";
    public NewConfig Config { get; set; } = new NewConfig();
    public NewModel Model { get; set; } = new NewModel();
}

public class NewModel
{
    [Display(Name = "Title")]
    [Required]
    public string? Title { get; set; }

    [Display(Name = "Description")]
    public string? Description { get; set; }
}

public class NewConfig
{
    public bool ShowSuccessToastMessage { get; set; } = true;
}
