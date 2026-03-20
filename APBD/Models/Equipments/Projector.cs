namespace APBD.Models.Equipments;

public  class Projector : Equipment
{
    public string Brightness  { get; private set; }
    public string Contrast  { get; private set; }
    public Projector(string name, string brightness, string contrast) : base(name)
    {
        Brightness = brightness;
        Contrast = contrast;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Projector | Brightness: {Brightness} | Contrast: {Contrast} ";
    }
}