namespace IWantApp.Endpoints.Categories;

public class CategoryGetAll
{
    public static string Template => "/categories";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static async Task<IResult> Action(ApplicationDbContext context)
    {
        var categories = await context.Categories.AsNoTracking().ToListAsync();
        var response = categories.Select(c => new CategoryResponse { Id = c.Id, Name = c.Name, Active = c.Active });
        return Results.Ok(response);
    }
}
