using JobnetPrompt.services;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient<IRagFetcher, RagFetcher>(client =>
{
  
});
// Register the Swagger generator
builder.Services.AddEndpointsApiExplorer();
/*builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Title = "My API", 
        Version = "v1",
        Description = "An ASP.NET Core Web API for managing XYZ",
        // You can set other properties like contact or license here
    });
});*/

var app = builder.Build();

//app.UseSwagger();

// Enable middleware to serve swagger-ui
/*app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; // To serve the Swagger UI at the app's root
});*/

app.MapGet("/performRag", async (string query, IRagFetcher ragFetcher) =>
{
    try
    {
        // Using the RagFetcher service to perform the operation
        string result = await ragFetcher.PerformRagAsync(query);
        return Results.Ok(result);
    }
    catch (Exception ex)
    {
        // Handle exceptions (e.g., logging)
        Console.WriteLine(ex.Message);
        return Results.Problem("An error occurred while processing your request.");
    }
});

app.MapGet("/", () => "Hello World!");

app.Run();