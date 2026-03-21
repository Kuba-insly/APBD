using APBD.Models;
using APBD.Models.Equipments;
using APBD.Models.Users;
using APBD.Services;
using APBD.UI;

public class Program
{
    public static void Main()
    {
        List<Rental> rentals = new List<Rental>();
        List<Equipment> equipment = new List<Equipment>();
        List<User> users = new List<User>();

        RentalService rentalService = new RentalService(rentals);
        ReportService reportService = new ReportService(rentals);
        EquipmentService equipmentService = new EquipmentService(equipment);
        UserService userService = new UserService(users);

        Menu menu = new Menu(rentalService, reportService, equipmentService, userService);
        menu.Run();
    }
}