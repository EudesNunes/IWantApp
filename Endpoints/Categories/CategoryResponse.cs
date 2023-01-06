namespace IWantApp.Endpoints.Categories;

public record CategoryResponse()
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public bool Active { get; set; }
}