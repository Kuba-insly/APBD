namespace APBD.Models.Users;

public class Employee : User
{
    public string Department  { get; private set; }
    public string Title  { get; private set; }
    
    public Employee(string firstName, string lastName, int maxRentals, string department, string title) : base(firstName, lastName, maxRentals: 5)
    {
        Department = department;
        Title = title;
    }

    public override string ToString()
    {
        return base.ToString()  + $" | Department: {Department} | Title: {Title}";
    }
}