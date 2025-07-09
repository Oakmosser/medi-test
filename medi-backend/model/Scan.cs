//Assuming scans will have some other data beside notes and may need to accomodate a O2M relationship with notes. I have omitted most other information for lack of knowing but have represented it using the patient property. This would likely be a reference to a record in the database
//but we're using a string as a stand in


namespace Models
{
    public class Scan
    {
        public required Guid Id { get; set; }
        public required DateTime DateCreated { get; set; }
        public string Patient { get; set; }
        public required List<Note> Notes { get; set; }
    }
}