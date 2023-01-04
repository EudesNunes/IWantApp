using IWantApp.Infra.Data;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int? page, int? rows, QueryAllUsersWIithClaimName query)
    {
        if (page == null)
            return Results.NotFound("page não declarado");
        if (rows == null)
            return Results.NotFound("rows não declarado");
        if (rows > 10)
            return Results.NotFound("rows não pode ser maior que 10");


        return Results.Ok(query.Execute(page.Value, rows.Value));

    }
}
