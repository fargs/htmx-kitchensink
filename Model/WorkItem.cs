using System.Reflection;

namespace htmx_kitchensink.Model;
public class WorkItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }

    public static List<WorkItem> WorkItems { get; set; } = new List<WorkItem>
    {
        new WorkItem
        {
            Id = Guid.NewGuid(), 
            Title = "First Work Item", 
            Description = "This is the first work item" 
        },
        new WorkItem
        {
            Id = Guid.NewGuid(),
            Title = "Second Work Item",
            Description = "This is the second work item"
        },
        new WorkItem
        {
            Id = Guid.NewGuid(),
            Title = "Third Work Item",
            Description = "This is the third work item"
        }

    };
}
