using IWantApp.Endpoints.Reports;

namespace IWantApp.Infra.Data;

public class QueryBestSeller
{
    private readonly IConfiguration configuration;

    public QueryBestSeller(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<IEnumerable<BestSellerResponse>> Execute(int page, int rows)
    {
        var db = new SqlConnection(configuration["ConnectionString:IWantDb"]);
        var query =
            @"SELECT P.Id, P.Name AS Nome,
            COUNT(Op.ProductsId) AS Vezes
            FROM Products AS P
            INNER JOIN OrderProducts 
            AS OP ON P.Id = OP.ProductsId        
            GROUP BY P.Id, P.Name
            ORDER BY
            Vezes DESC
            OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        return await db.QueryAsync<BestSellerResponse>(
            query,
            new { page, rows }
        );
    }
}
