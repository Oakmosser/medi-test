//Assuming a note will simply be text for the purposes of this test, I've moved them to a class in case future requirements demand something like file uploads or user assignment.

public class Note : BaseObject
{

    public required string Title { get; set; }
    public required string Content { get; set; }
}
