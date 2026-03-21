using APBD.Models;
using APBD.Models.Equipments;
using APBD.Models.Users;

namespace APBD.Services;

public class ReportService
{
    private readonly List<Rental> _rentals;

    public ReportService(List<Rental> rentals)
    {
        _rentals = rentals;
    }

    public List<Rental> GetRentals()
    {
        return _rentals;
    }

    public List<Rental> GetActiveRentals()
    {
        return _rentals.Where(r => !r.WhenRefund.HasValue).ToList();
    }
    
    public List<Rental> GetExpiredRentals()
    {
        return _rentals.Where(r => r.UntilWhen < DateTime.Now && !r.WhenRefund.HasValue).ToList();
    }

    public List<Rental> GetActiveByUser(User user)
    {
        return _rentals.Where(r => r.Who.UserId == user.UserId && !r.WhenRefund.HasValue).ToList();
    }

    public string GetSummary()
    {
        int allRentals = GetRentals().Count;
        int activeRentals = GetActiveRentals().Count;
        int expiredRentals = GetExpiredRentals().Count;
        decimal totalPenalties = _rentals
            .Where(r => r.Penalty.HasValue)
            .Sum(r => r.Penalty!.Value);

        return $"All rentals: {allRentals} | Active: {activeRentals} | Overdue: {expiredRentals} | Total penalties: {totalPenalties}m";
    }
}