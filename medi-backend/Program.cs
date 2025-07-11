using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Here is where we create out database context, following the in-memory database technique. I was unfamiliar with this particular design but found it shockingly similar to the process used for a dedicated database so got on swimingly!
//Doing this also allows me to avoiding having to create a singleton since .NET has native support for a DB context via this method which can then be accessed via DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase(databaseName: "medi-backend");
});

//Got a couple scopted services here. This is for three features. One, to initialise all the nessessary DI objects for accessing endpoint routing which I handle using an abstracted routing service method I whipped up. Secondly for initialising 
//the validator for each class model that needs to be validated (FluentValidation, the library I use registers rules in a class which needs to be initialised and then used elsewhere). Finally to seed the database data on application startup.
//Each of these have further documentation on how they worked.
builder.Services.AddScoped<PointRouter.IRouter, ScanMethods>();
builder.Services.AddScoped<NoteValidator>();
builder.Services.AddScoped<PointRouter>();
builder.Services.AddScoped<SeedData>();

//A little (really) insecure to allow any cross origin! But I think for the scope we're working for here, delivering this for use in a test environment should meet the MVP. Hopefully acknowledging this fact will suffice for now 🙏
var MyAllowSpecificOrigins = "_MyAllowSubdomainPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.WithHeaders(["content-type"]);
        });
});

var app = builder.Build();

//Using those fantastic services whipped up above
using (var scope = app.Services.CreateScope())
{
    //Get our router depedency injected
    var pointRouter = scope.ServiceProvider.GetRequiredService<PointRouter>();
    pointRouter.RouteEndpoints(app);

    //and our data seeded :)
    var seedData = scope.ServiceProvider.GetRequiredService<SeedData>();
    seedData.Seed();
}

app.UseCors("_MyAllowSubdomainPolicy");

app.Run();