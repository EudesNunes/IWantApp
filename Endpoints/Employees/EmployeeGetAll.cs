﻿namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employee";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy = "Employee005Policy")]
    public static async Task<IResult> Action(int? page, int? rows, QueryAllUsersWIithClaimName query)
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
