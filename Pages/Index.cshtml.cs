using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;

namespace htmx_kitchensink.Pages;

public class IndexPage : PageModel
{
    public void OnGet(Guid workItemId)
    {
        NewPartial = new Partials.WorkItem.NewPartial();
        ListPartial = Partials.WorkItem.ListPartial.Create(Url);
        ReadOnlyPartial = Partials.WorkItem.ReadOnlyPartial.Create(Url, workItemId);

    }
    public Partials.WorkItem.NewPartial NewPartial { get; set; }
    public Partials.WorkItem.ListPartial ListPartial { get; set; }
    public Partials.WorkItem.ReadOnlyPartial ReadOnlyPartial { get; set; }
}