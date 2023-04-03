using PatientPortalRepository;
using PatientPortalApplication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddOpenApiDocument((configure, serviceProvider) =>
{
    configure.Title = "Patient Portal API";
});

// Register Dependencies
builder.Services.AddScoped<DbContextInitialiser>();
builder.Services.AddPatientRepoDbContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Initialise and seed database
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();
        await initialiser.Perform();
    }
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwaggerUi3(settings =>
{
    settings.Path = "/api";
    settings.DocumentPath = "/api/specification.json";
});
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
