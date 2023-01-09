namespace IWantApp.Endpoints.Client;

public record OrderRequest(List<Guid>ProductIds, string DeliveryAddress);