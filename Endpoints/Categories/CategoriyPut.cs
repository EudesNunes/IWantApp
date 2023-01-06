namespace IWantApp.Endpoints.Categories;

public class CategoriyPut
{
    public static string Template => "/categories/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handle => Action;

    public async static Task<IResult> Action([FromRoute] Guid id, HttpContext http, CategoryRequest categoryRequest, ApplicationDbContext context)
    {

        var userId = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

        var category = context.Categories.Where(c => c.Id == id).FirstOrDefault();

        if (category == null)
            return Results.NotFound("Não encontrado");

        category.EditInf(categoryRequest.Name, categoryRequest.Active, userId);

        if (!category.IsValid)
            return Results.ValidationProblem(category.Notifications.ConvertToProblemDetails());


        await context.SaveChangesAsync();
        return Results.Ok();
    }
}
