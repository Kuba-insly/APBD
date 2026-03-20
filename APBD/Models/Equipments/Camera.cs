namespace  APBD.Models.Equipments;

public class Camera : Equipment
{
    public string Resolution { get; private set; }
    public string SealClass { get; private set; }
    
    public Camera(string name, string resolution, string sealClass) : base(name)
    {
        Resolution = resolution;
        SealClass = sealClass;
    }

    public override string ToString()
    {
        return $"{base.ToString()} | Camera | Seal class: {SealClass} | Resolution: {Resolution}";
    }
}