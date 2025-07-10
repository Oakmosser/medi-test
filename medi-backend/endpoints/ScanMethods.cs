using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.AspNetCore.Http.HttpResults;

public class ScanMethods : PointRouter.IRouter
{
    //Defining our routes in the file where their methods are defined for the sake of seperation of concern. This is called by the routing function on initialisation.
    public void MapRoutes(WebApplication app)
    {
        app.MapGet("/scans", (ApplicationDbContext dbContext) => GetScans(dbContext));
        app.MapGet("/scans/{id}/notes", (Guid id, ApplicationDbContext dbContext) => GetNotes(dbContext, id));
        app.MapPost("/scans/{id}/notes", (Guid id, Note note, ApplicationDbContext dbContext, NoteValidator noteValidator) => CreateNote(dbContext, noteValidator, id, note));
    }

    //Nice simple data return in JSON
    public IResult GetScans(ApplicationDbContext dbContext)
    {
        return Results.Json(dbContext.Scans);
    }

    //Same thing, although, making sure theres actually data to return.
    public IResult GetNotes(ApplicationDbContext dbContext, Guid Id)
    {
        try
        {
            return Results.Json(dbContext.Scans.FirstOrDefault(scan => scan.Id == Id)?.Notes ?? new List<Note>());
        }
        catch
        {
            return Results.Json(new List<Note>());
        }
    }

    //We attempt to pass in a note object from the request, validate it and create it under the scan object, checking to make sure the scan exists.
    public IResult CreateNote(ApplicationDbContext dbContext, NoteValidator noteValidator, Guid Id, Note note)
    {
        try
        {
            if (noteValidator.Validate(note).IsValid)
            {
                dbContext.Scans.First(scan => scan.Id == Id)?.Notes.Add(note);
            }
            return Results.Json(note);
        }
        catch
        {
            return Results.Json(new List<Note>());
        }
    }
}