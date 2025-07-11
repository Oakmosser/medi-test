using Models;

public class SeedData
{
    private readonly ApplicationDbContext _db;

    public SeedData(ApplicationDbContext db)
    {
        _db = db;
    }

    public void Seed()
    {
        _db.Scans.AddRange(CreateScans());
        _db.SaveChanges();
    }

    //Some data to test with
    private IEnumerable<Scan> CreateScans()
    {
        for (int i = 0; i <= 15; i++)
        {
            yield return new Scan()
            {
                Notes = new List<Note>(),
                Patient = $"Boston George - {i}"
            };
        }
    }
}
