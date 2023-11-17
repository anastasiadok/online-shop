namespace OnlineShop.Data.Models;

public class Category
{
    public Guid CategoryId { get; set; }

    public Guid SectionId { get; set; }

    public Guid? ParentCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Section Section { get; set; } = null!;

    public virtual Category? ParentCategory { get; set; }
}