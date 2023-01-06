namespace IWantApp.Endpoints.Products;

public record ProductResponse(string name, string CategoryName, string Description, bool HasStock, bool Active);