using FluentValidation;
using Models;

public class ScanValidator : AbstractValidator<Scan>
{
    public ScanValidator()
    {
        RuleFor(scan => scan.Id).NotNull();
        RuleFor(scan => scan.DateCreated).NotNull();
        RuleFor(scan => scan.Patient).NotNull();
    }
}

public class NoteValidator : AbstractValidator<Note>
{
    public NoteValidator()
    {
        RuleFor(note => note.Id).NotNull();
        RuleFor(note => note.DateCreated).NotNull();
        RuleFor(note => note.Content).NotNull();
        RuleFor(note => note.Title).NotNull();
    }
}