using APBD.Models.Equipments;

namespace APBD.Services;

public class EquipmentService
{
    private readonly List<Equipment> _equipments;
    
    public EquipmentService(List<Equipment> equipments)
    {
        _equipments = equipments;
    }

    public void AddEquipment(Equipment equipment)
    {
        _equipments.Add(equipment);
    }

    public void RemoveEquipment(Equipment equipment)
    {
        _equipments.Remove(equipment);
    }
    
    public List<Equipment> GetAllEquipments()
    {
        return _equipments;
    }
    
    public Equipment? GetEquipmentById(Guid id)
    {
        return _equipments.FirstOrDefault(e => e.Id == id);
    }
    
    public List<Equipment> GetAvailableEquipments()
    {
        return _equipments.Where(e => e.IsAvailable).ToList();
    }

    public void SetUnavailable(Guid id)
    {
        Equipment? eq = _equipments.FirstOrDefault(e => e.Id == id);
        if (eq == null)
            throw new InvalidOperationException("No equipment with the specified ID was found.");
        eq.SetAvailability(false);
    }
}