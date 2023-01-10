using IWantApp.Domain.Products;
using IWantApp.Domain.Users;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace IWantApp.Endpoints.Client;

public class OrderGet
{
    public static string Template => "/orders/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;


    [Authorize]
    public static async Task<IResult> Action([FromRoute] Guid id, ApplicationDbContext context, HttpContext http, UserManager<IdentityUser> userManager)
    {
        var clientClaim = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier);
        var employeeClaim = http.User.Claims
           .FirstOrDefault(c => c.Type == "EmployeeCode");

        //Obter o pedido
        var order = context.Orders.Include(o => o.Products).FirstOrDefault(c => c.Id == id);

        if (order == null)
            return Results.NotFound("Não encontrado");

        //o usuario pode ver o resultado
        if (order.ClientId != clientClaim.Value && employeeClaim != null)
            return Results.Forbid();

        //buscar o cliente
        var client = await userManager.FindByIdAsync(order.ClientId);

        //preencher o dto
        var productsResponse = order.Products.Select(p => new OrderProduct(p.Id, p.Name));//todos produtos do pedido
        var orderResponse = new OrderResponse(
            order.Id,client.Email, productsResponse, order.DeliveryAddress);


        return Results.Ok(orderResponse);
      
    }
}
