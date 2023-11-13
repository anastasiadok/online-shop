namespace ConsoleApp1.db.Models;

public class Section
{
    public Guid SectionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
