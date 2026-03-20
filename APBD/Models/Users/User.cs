namespace APBD.Models.Users;

public abstract class User
{
    public Guid UserId { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int MaxRentals { get; private set; }
    
    protected  User(string firstName, string lastName, int maxRentals)
    {
        UserId = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        MaxRentals = maxRentals;
    }
    
    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        return $"{UserId} | {FirstName} {LastName}";
    }
}