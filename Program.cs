var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

var app = builder.Build();

app.MapGet("/messages", async (context) => await context.Response.WriteAsync($"Click me again to trigger another GET request and watch the time change. {DateTime.UtcNow}"));
app.MapPost("/setup/yourname", async (context) =>
{
    // Read the name entered by the user
    var formData = await context.Request.ReadFormAsync();
    var name = formData["Name"].ToString();

    // Serialize the name to an XML file
    var xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(string));
    using (var writer = new System.IO.StreamWriter($"Data/{name}.xml"))
    {
        xmlSerializer.Serialize(writer, name);
    }

    // Return 200 OK
    context.Response.StatusCode = 200;
});

app.UseStaticFiles();
app.MapControllers();
app.MapRazorPages();
app.UseRouting();

app.Run();
