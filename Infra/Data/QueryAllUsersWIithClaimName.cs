using Dapper;
using IWantApp.Endpoints.Employees;
using Microsoft.Data.SqlClient;

namespace IWantApp.Infra.Data;

public class QueryAllUsersWIithClaimName
{
    private readonly IConfiguration configuration;

    public QueryAllUsersWIithClaimName(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IEnumerable<EmployeeResponse> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query =
            @"SELECT Email, claimValue as Name
              FROM AspNetUsers u INNER 
              JOIN AspNetUserClaims c
              on u .id =c.UserId and ClaimType = 'Name'
              ORDER BY name
              OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return db.Query<EmployeeResponse>(
            query,
            new { page, rows }
        );
    }
}
