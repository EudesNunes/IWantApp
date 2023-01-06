namespace IWantApp.Endpoints.Products;

public class ProductGetShowCase
{
    public static string Template => "/products/showcase";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [AllowAnonymous]

    public static async Task<IResult> Action(ApplicationDbContext context)
    {
        var products = await context.Products
            .Include(p => p.Category)
            .Where(p => p.HasStock && p.Category.Active)
            .OrderBy(p => p.Name)
            .ToListAsync();
        var results =  products.Select(p => new ProductResponse(p.Name, p.Category.Name, p.Description, p.HasStock, p.Price ,p.Active));
        return Results.Ok(results);
    }
}
