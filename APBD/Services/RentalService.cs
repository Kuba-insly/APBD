using APBD.Models;
using APBD.Models.Equipments;
using APBD.Models.Users;

namespace APBD.Services;

public class RentalService
{
    private readonly List<Rental> _rentals;

    public RentalService(List<Rental> rentals)
    {
        _rentals = rentals;
    }

    public Rental? GetRentalById(Guid id)
    {
        return _rentals.FirstOrDefault(r => r.Id == id);
    }

    public bool CheckUserLimit(User user)
    {
        int activeRentals = _rentals.Count(r => r.Who.UserId == user.UserId && r.WhenRefund == null);
        return activeRentals < user.MaxRentals;
    }

    public decimal CalculatePenalty(Rental rental, DateTime returnDate)
    {
        if (returnDate <= rental.UntilWhen) return 0m;
        int daysLate = (returnDate - rental.UntilWhen).Days;
        return daysLate * RentalConfig.PenaltyPerDay;
    }

    public void RentEquipment(User user, Equipment equipment, DateTime from, DateTime until)
    {
        if (!equipment.IsAvailable)
            throw new InvalidOperationException($"Equipment '{equipment.Name}' is unavailable.");

        if (!CheckUserLimit(user))
            throw new InvalidOperationException($"{user.FullName} has exceeded the borrowing limit.");

        equipment.SetAvailability(false);
        _rentals.Add(new Rental(user, equipment, from, until));
    }

    public void ReturnEquipment(Guid rentalId, DateTime returnDate)
    {
        Rental? rental = GetRentalById(rentalId);

        if (rental == null)
            throw new InvalidOperationException("No rentals were found with the specified ID.");

        if (rental.WhenRefund.HasValue)
            throw new InvalidOperationException("This equipment has already been returned.");

        decimal penalty = CalculatePenalty(rental, returnDate);
        rental.ProcessReturn(returnDate, penalty > 0 ? penalty : null);
        rental.What.SetAvailability(true);
    }
}