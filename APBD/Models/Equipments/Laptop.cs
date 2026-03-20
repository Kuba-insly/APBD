namespace APBD.Models.Equipments;

public class Laptop : Equipment
{
    public string Processor { get; private set; }
    public int RamGb { get; private set; }

    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        Processor = processor;
        RamGb = ramGb;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Laptop | {Processor} | {RamGb} GB RAM";
    }
}