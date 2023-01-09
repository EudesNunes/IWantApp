using IWantApp.Domain.Orders;
using IWantApp.Domain.Users;

namespace IWantApp.Endpoints.Client;

public class OrderPost
{
    public static string Template => "/orders";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handle => Action;

    [Authorize(Policy ="CpfPolicy")]
    public static async Task<IResult> Action(OrderRequest orderRequest,HttpContext http, ApplicationDbContext context)
    {
        var clientId = http.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier).Value;
        var clientName = http.User.Claims
            .First(c => c.Type == "Name").Value;

        if (orderRequest.ProductIds == null || !orderRequest.ProductIds.Any())
            return Results.BadRequest("Produto é obrigatorio para o pedido");
        if (string.IsNullOrEmpty(orderRequest.DeliveryAddress))
            return Results.BadRequest("Endereço de entrega é obrigatorio");
        
        var productsFound = context.Products.Where(p => orderRequest.ProductIds.Contains(p.Id)).ToList();

        var order = new Order(clientId, clientName, productsFound, orderRequest.DeliveryAddress);
        await context.Orders.AddAsync(order);
        await context.SaveChangesAsync();

        return Results.Created($"/orders/{order.Id}", order.Id);
    }
}
