using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Models;
using Microsoft.AspNetCore.Http.HttpResults;

public class ScanMethods : PointRouter.IRouter
{
    public void MapRoutes(WebApplication app)
    {
        app.MapGet("/scans", (ApplicationDbContext dbContext) => GetScans(dbContext));
        app.MapGet("/scans/{id}/notes", (Guid id, ApplicationDbContext dbContext) => GetNotes(dbContext, id));
        app.MapPost("/scans/{id}/notes", (Guid id, Note note, ApplicationDbContext dbContext, NoteValidator noteValidator) => CreateNote(dbContext, noteValidator, id, note));
    }



    public IResult GetScans(ApplicationDbContext dbContext)
    {
        return Results.Json(dbContext.Scans);
    }

    public IResult GetNotes(ApplicationDbContext dbContext, Guid Id)
    {
        try
        {
            List<Note> notes = dbContext.Scans.First(scan => scan.Id == Id).Notes;
            return Results.Json(notes);
        }
        catch
        {
            return Results.Json(null);
        }
    }

    public IResult CreateNote(ApplicationDbContext dbContext, NoteValidator noteValidator, Guid Id, Note note)
    {
        try
        {
            if (noteValidator.Validate(note).IsValid)
            {
                dbContext.Scans.First(scan => scan.Id == Id).Notes.Add(note);
            }
            return Results.Json(note);
        }
        catch
        {
            return Results.Json(null);
        }
    }
}