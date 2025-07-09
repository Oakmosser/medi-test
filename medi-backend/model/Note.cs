//Assuming a note will simply be text for the purposes of this test, I've moved them to a class in case future requirements demand something like file uploads or user assignment.

public class Note
{
    public required Guid Id { get; set; }
    public required DateTime DateCreated { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}
