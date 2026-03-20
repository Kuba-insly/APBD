namespace APBD.Models.Equipments;

public abstract class Equipment
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public bool IsAvailable { get; private set; }
    
    protected Equipment(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
        IsAvailable = true;
    }

    public void SetAvailability(bool available)
    {
        IsAvailable = available;
    }

    public override string ToString()
    {
        return $"{Id} | {Name} – {(IsAvailable ? "Available" : "Unavailable")}";
    }
}