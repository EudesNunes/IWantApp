using IWantApp.Domain.Orders;

namespace IWantApp.Endpoints.Products;

public record OrderResponse(Guid Id, string ClientName,IEnumerable<OrderProduct> Products, string DeliveryAddress);

public record OrderProduct(Guid Id, string Nome);