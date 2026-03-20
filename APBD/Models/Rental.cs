using APBD.Models.Users;
using APBD.Models.Equipments;

namespace APBD.Models;

public class Rental
{
    public Guid Id { get; private set; }
    public User Who { get; private set; }
    public Equipment What { get; private set; }
    public DateTime When { get; private set; } // kiedy wypożyczył
    public DateTime UntilWhen { get; private set; } // do kiedy wypożyczył
    public DateTime? WhenRefund { get; private set; } // kiedy zwrócił
    public decimal? Penalty { get; private set; }

    public Rental(User who, Equipment what, DateTime when, DateTime untilWhen)
    {
        Id = Guid.NewGuid();
        Who = who;
        What = what;
        When = when;
        UntilWhen = untilWhen;
    }
    
    public void ProcessReturn(DateTime returnDate, decimal? penalty = null)
    {
        WhenRefund = returnDate;
        Penalty = penalty;
    }
    
   public bool IsRefundOnTime()
   {
       if (WhenRefund == null) return false;
       return WhenRefund <= UntilWhen;
   }

    public override string ToString()
    {
        string returnInfo = WhenRefund.HasValue ? $"WhenRefund: {WhenRefund:d} | Is refund on time: {IsRefundOnTime()} | Penalty: {Penalty ?? 0}" : "It hasn't been returned yet";
        return $"{Id} | Who: {Who.FullName} | What: {What.Name}  | When: {When:d} | Until when: {UntilWhen:d} | {returnInfo}";
    }
}