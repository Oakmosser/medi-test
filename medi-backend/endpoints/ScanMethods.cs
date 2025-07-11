using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

public class ScanMethods : PointRouter.IRouter
{
    //Defining our routes in the file where their methods are defined for the sake of seperation of concern. This is called by the routing function on initialisation.
    public void MapRoutes(WebApplication app)
    {
        app.MapGet("/scans", (ApplicationDbContext dbContext) => GetScans(dbContext));
        app.MapGet("/scans/{id}/notes", ([FromRoute] Guid id, ApplicationDbContext dbContext) => GetNotes(dbContext, id));
        app.MapPost("/scans/{id}/notes", async ([FromRoute] Guid id, [FromBody] Note note, ApplicationDbContext dbContext, NoteValidator noteValidator) => await CreateNote(dbContext, noteValidator, id, note))
            .DisableAntiforgery();
    }

    //Nice simple data return in JSON
    public IResult GetScans(ApplicationDbContext dbContext)
    {
        return Results.Json(dbContext.Scans);
    }

    //Same thing, although, making sure theres actually data to return.
    public async Task<IResult> GetNotes(ApplicationDbContext dbContext, Guid Id)
    {
        try
        {
            var notes = await dbContext.Scans
                .Include(scan => scan.Notes)
                .FirstAsync(scan => scan.Id == Id);

            return Results.Json(notes.Notes);
        }
        catch
        {
            return Results.Json(new List<Note>());
        }
    }

    //We attempt to pass in a note object from the request, validate it and create it under the scan object, checking to make sure the scan exists.
    public async Task<IResult> CreateNote(ApplicationDbContext dbContext, NoteValidator noteValidator, Guid scanId, Note note)
    {
        try
        {
            if (noteValidator.Validate(note).IsValid)
            {
                //Making sure to grab those notes!
                Scan scan = await dbContext.Scans
                .Include(scan => scan.Notes)
                .FirstAsync(scan => scan.Id == scanId);
                
                scan.Notes.Add(note);
                await dbContext.SaveChangesAsync();
            }
            return Results.Json(note);
        }
        catch
        {
            return Results.Json(new List<Note>());
        }
    }
}