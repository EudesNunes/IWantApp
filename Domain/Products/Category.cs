using Flunt.Validations;

namespace IWantApp.Domain.Products;

public class Category : Entity
{

    public string Name { get; private set; }
    public bool Active { get; private set; }
    public Category(string name, string createdBy, string editeBy)
    {
        Name = name;
        Active = true;
        CreatedBy = createdBy;
        EditeBy = editeBy;
        CreatedOn = DateTime.Now;
        EditedOn = DateTime.Now;

        Validate();

    }

    private void Validate()
    {
        var contract = new Contract<Category>()
            .IsNotNullOrEmpty(Name, "Name", "Nome não pode ser nulo")
            .IsGreaterOrEqualsThan(Name, 3, "Name", "Precisa ser maior ou igual a 3 caracters")
            .IsNotNullOrEmpty(CreatedBy, "CreatedBy", "CreatedBy  está invalido")
            .IsNotNullOrEmpty(EditeBy, "EditeBy", "EditeBy está invalido");
        AddNotifications(contract);
    }

    public void EditInf(string name, bool active)
    {
        Active = active;
        Name = name;
        Validate();

        
    }
}
