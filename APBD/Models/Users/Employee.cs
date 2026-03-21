namespace APBD.Models.Users;

using APBD;

public class Employee : User
{
    public string Department  { get; private set; }
    public string Title  { get; private set; }
    
    public Employee(string firstName, string lastName, string department, string title) : base(firstName, lastName, RentalConfig.EmployeeMaxRentals)
    {
        Department = department;
        Title = title;
    }

    public override string ToString()
    {
        return base.ToString()  + $" | Department: {Department} | Title: {Title}";
    }
}