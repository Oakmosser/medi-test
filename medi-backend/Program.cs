using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "medi-backend");
});
builder.Services.AddScoped<PointRouter.IRouter, ScanMethods>();
builder.Services.AddScoped<NoteValidator>();
builder.Services.AddScoped<PointRouter>();
builder.Services.AddScoped<SeedData>();

var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin();
        });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var pointRouter = scope.ServiceProvider.GetRequiredService<PointRouter>();
    pointRouter.RouteEndpoints(app);

    var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
    seedData.Seed();
}

app.UseCors("_MyAllowSubdomainPolicy");

app.Run();