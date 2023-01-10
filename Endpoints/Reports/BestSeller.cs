using IWantApp.Endpoints.Reports;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace IWantApp.Endpoints.Relatorio;

public class BestSeller
{
    public static string Template => "/BestSeller";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee111Policy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryBestSeller query)
    {

        if (page == null)
            return Results.NotFound("page não declarado");
        if (rows == null)
            return Results.NotFound("rows não declarado");
        if (rows > 10)
            return Results.NotFound("rows não pode ser maior que 10");

        var result = await query.Execute(page.Value, rows.Value);
        return Results.Ok(result);

    }
}
