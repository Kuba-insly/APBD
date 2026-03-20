namespace APBD.Models.Users;

using APBD;

public  class Student : User
{
    public string Specialization { get; private set; }
    public string DegreeCourse  {get; private set;}
    
    public Student(string firstName, string lastName, int maxRentals, string specialization, string degreeCourse) : base(firstName, lastName, RentalConfig.StudentMaxRentals)
    {
        Specialization = specialization;
        DegreeCourse = degreeCourse;
    }

    public override string ToString()
    {
        return base.ToString() + $" | Specialization : {Specialization} | DegreeCourse: {DegreeCourse}";
    }
}