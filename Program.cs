var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/messages", async (context) => await context.Response.WriteAsync($"Click me again to trigger another GET request and watch the time change. {DateTime.UtcNow}")); 

app.UseStaticFiles();
app.MapRazorPages();
app.Run();
