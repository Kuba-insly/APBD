using APBD.Models;
using APBD.Models.Equipments;
using APBD.Models.Users;
using APBD.Services;

namespace APBD.UI;

public class Menu
{
    private readonly RentalService _rentalService;
    private readonly ReportService _reportService;
    private readonly EquipmentService _equipmentService;
    private readonly UserService _userService;

    public Menu(RentalService rentalService, ReportService reportService, EquipmentService equipmentService,
        UserService userService)
    {
        _rentalService = rentalService;
        _reportService = reportService;
        _equipmentService = equipmentService;
        _userService = userService;
    }

    public void Run()
    {
        while (true)
        {
            Console.WriteLine("1. Przejdź do zarządzania sprzętem");
            Console.WriteLine("2. Przejdź do zarządzania użytkownikami");
            Console.WriteLine("3. Przejdź do wypożyczeń");
            Console.WriteLine("4. Przejdź do raportów");
            Console.WriteLine("0. Wyjście");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1": ShowEquipmentMenu(); break;
                case "2": ShowUserMenu(); break;
                case "3": ShowRentalMenu(); break;
                case "4": ShowReportMenu(); break;
                case "0": return;
                default: Console.WriteLine("Brak takiej opcji"); break;
            }
        }
    }

    private void ShowEquipmentMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== SPRZĘT ===");
            Console.WriteLine("1. Dodaj sprzęt");
            Console.WriteLine("2. Wyświetl cały sprzęt");
            Console.WriteLine("3. Wyświetl dostępny sprzęt");
            Console.WriteLine("4. Oznacz sprzęt jako niedostępny");
            Console.WriteLine("5. Usuń konkretny sprzęt");
            Console.WriteLine("0. Powrót");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Typ sprzętu (laptop/projector/camera): ");
                    string? equipmentType = Console.ReadLine();

                    Console.Write("Nazwa: ");
                    string? name = Console.ReadLine()!;

                    switch (equipmentType)
                    {
                        case "laptop":
                            Console.Write("Procesor: ");
                            string? processor = Console.ReadLine()!;
                            int? ram = ReadInt("RAM (GB)");
                            if (ram == null) break;
                            _equipmentService.AddEquipment(new Laptop(name, processor, ram.Value));
                            Console.WriteLine("Sprzęt dodany!");
                            break;
                        case "projector":
                            Console.Write("Jasność: ");
                            string? brightness = Console.ReadLine()!;
                            Console.Write("Kontrast: ");
                            string? contrast = Console.ReadLine()!;
                            _equipmentService.AddEquipment(new Projector(name, brightness, contrast));
                            Console.WriteLine("Sprzęt dodany!");
                            break;
                        case "camera":
                            Console.Write("Rozdzielczość: ");
                            string? resolution = Console.ReadLine()!;
                            Console.Write("Klasa szczelności: ");
                            string? sealClass = Console.ReadLine()!;
                            _equipmentService.AddEquipment(new Camera(name, resolution, sealClass));
                            Console.WriteLine("Sprzęt dodany!");
                            break;
                        default:
                            Console.WriteLine("Nieznany typ sprzętu.");
                            break;
                    }
                    break;
                case "2":
                    foreach (var equipment in _equipmentService.GetAllEquipments())
                        Console.WriteLine(equipment);
                    break;
                case "3":
                    foreach (var equipment in _equipmentService.GetAvailableEquipments())
                        Console.WriteLine(equipment);
                    break;
                case "4":
                    Guid? equipmentId = ReadGuid("Podaj ID sprzętu");
                    if (equipmentId == null) break;
                    Equipment? oneEquipment = _equipmentService.GetEquipmentById(equipmentId.Value);
                    if (oneEquipment == null) { Console.WriteLine("Nie znaleziono sprzętu."); break; }
                    _equipmentService.SetUnavailable(equipmentId.Value);
                    Console.WriteLine($"Sprzęt {oneEquipment.Name} został ustawiony jako niedostępny.");
                    break;
                case "5":
                    Guid? equipmentIdToRemove = ReadGuid("Podaj ID sprzętu");
                    if (equipmentIdToRemove == null) break;
                    Equipment? oneEquipmentToRemove = _equipmentService.GetEquipmentById(equipmentIdToRemove.Value);
                    if (oneEquipmentToRemove == null) { Console.WriteLine("Nie znaleziono sprzętu."); break; }
                    _equipmentService.RemoveEquipment(oneEquipmentToRemove);
                    Console.WriteLine($"Sprzęt {oneEquipmentToRemove.Name} został usunięty.");
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Brak takiej opcji");
                    break;
            }
        }
    }

    private void ShowUserMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== UŻYTKOWNIK ===");
            Console.WriteLine("1. Dodaj Użytkownika");
            Console.WriteLine("2. Wyświetl wszystkich użytkowników");
            Console.WriteLine("3. Wyświetl konkretnego użytkownika");
            Console.WriteLine("0. Powrót");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Console.Write("Student czy Pracownik? (s/p): ");
                    string? type = Console.ReadLine();

                    Console.Write("Imię: ");
                    string? firstName = Console.ReadLine()!;

                    Console.Write("Nazwisko: ");
                    string? lastName = Console.ReadLine()!;

                    if (type == "s")
                    {
                        Console.Write("Specjalizacja: ");
                        string? specialization = Console.ReadLine()!;
                        Console.Write("Kierunek: ");
                        string? degreeCourse = Console.ReadLine()!;
                        _userService.AddUser(new Student(firstName, lastName, specialization, degreeCourse));
                        Console.WriteLine("Użytkownik dodany!");
                    }
                    else if (type == "p")
                    {
                        Console.Write("Dział: ");
                        string? department = Console.ReadLine()!;
                        Console.Write("Stanowisko: ");
                        string? title = Console.ReadLine()!;
                        _userService.AddUser(new Employee(firstName, lastName, department, title));
                        Console.WriteLine("Użytkownik dodany!");
                    }
                    else
                    {
                        Console.WriteLine("Nieznany typ użytkownika.");
                    }
                    break;
                case "2":
                    foreach (var user in _userService.GetAllUsers())
                        Console.WriteLine(user);
                    break;
                case "3":
                    Guid? userId = ReadGuid("Podaj ID użytkownika");
                    if (userId == null) break;
                    User? oneUser = _userService.GetUserById(userId.Value);
                    if (oneUser == null) { Console.WriteLine("Nie znaleziono użytkownika."); break; }
                    Console.WriteLine(oneUser);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Brak takiej opcji");
                    break;
            }
        }
    }

    private void ShowRentalMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== WYPOŻYCZENIE ===");
            Console.WriteLine("1. Wypożycz sprzęt");
            Console.WriteLine("2. Zwróć sprzęt");
            Console.WriteLine("3. Aktywne wypożyczenia użytkownika");
            Console.WriteLine("4. Sprawdź konkretne wypożyczenie");
            Console.WriteLine("0. Powrót");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    Guid? rentUserId = ReadGuid("Podaj ID użytkownika");
                    if (rentUserId == null) break;
                    User? rentUser = _userService.GetUserById(rentUserId.Value);
                    if (rentUser == null) { Console.WriteLine("Nie znaleziono użytkownika."); break; }

                    Guid? rentEquipmentId = ReadGuid("Podaj ID sprzętu");
                    if (rentEquipmentId == null) break;
                    Equipment? rentEquipment = _equipmentService.GetEquipmentById(rentEquipmentId.Value);
                    if (rentEquipment == null) { Console.WriteLine("Nie znaleziono sprzętu."); break; }

                    DateTime? from = ReadDate("Data wypożyczenia (yyyy-MM-dd)");
                    if (from == null) break;
                    DateTime? until = ReadDate("Data zwrotu (yyyy-MM-dd)");
                    if (until == null) break;

                    try
                    {
                        _rentalService.RentEquipment(rentUser, rentEquipment, from.Value, until.Value);
                        Console.WriteLine("Sprzęt wypożyczony!");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Błąd: {e.Message}");
                    }
                    break;
                case "2":
                    Guid? returnId = ReadGuid("Podaj ID wypożyczenia");
                    if (returnId == null) break;
                    Rental? rent = _rentalService.GetRentalById(returnId.Value);
                    if (rent == null) { Console.WriteLine("Nie znaleziono wypożyczenia."); break; }

                    DateTime? returnDate = ReadDate("Data zwrotu (yyyy-MM-dd)");
                    if (returnDate == null) break;

                    try
                    {
                        _rentalService.ReturnEquipment(returnId.Value, returnDate.Value);
                        Console.WriteLine("Sprzęt zwrócony!");
                    }
                    catch (InvalidOperationException e)
                    {
                        Console.WriteLine($"Błąd: {e.Message}");
                    }
                    break;
                case "3":
                    Guid? userId3 = ReadGuid("Podaj ID użytkownika");
                    if (userId3 == null) break;
                    User? user3 = _userService.GetUserById(userId3.Value);
                    if (user3 == null) { Console.WriteLine("Nie znaleziono użytkownika."); break; }
                    foreach (var rental in _reportService.GetActiveByUser(user3))
                        Console.WriteLine(rental);
                    break;
                case "4":
                    Guid? rentalId = ReadGuid("Podaj ID wypożyczenia");
                    if (rentalId == null) break;
                    Rental? oneRental = _rentalService.GetRentalById(rentalId.Value);
                    if (oneRental == null) { Console.WriteLine("Nie znaleziono wypożyczenia."); break; }
                    Console.WriteLine(oneRental);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Brak takiej opcji");
                    break;
            }
        }
    }

    private void ShowReportMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== RAPORTY ===");
            Console.WriteLine("1. Wszystkie wypożyczenia");
            Console.WriteLine("2. Aktywne wypożyczenia");
            Console.WriteLine("3. Przeterminowane wypożyczenia");
            Console.WriteLine("4. Aktywne wypożyczenia dla konkretnego użytkownika");
            Console.WriteLine("5. Raport podsumowujący");
            Console.WriteLine("0. Powrót");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    foreach (var rental in _reportService.GetRentals())
                        Console.WriteLine(rental);
                    break;
                case "2":
                    foreach (var rental in _reportService.GetActiveRentals())
                        Console.WriteLine(rental);
                    break;
                case "3":
                    foreach (var rental in _reportService.GetExpiredRentals())
                        Console.WriteLine(rental);
                    break;
                case "4":
                    Guid? userId4 = ReadGuid("Podaj ID użytkownika");
                    if (userId4 == null) break;
                    User? user4 = _userService.GetUserById(userId4.Value);
                    if (user4 == null) { Console.WriteLine("Nie znaleziono użytkownika."); break; }
                    foreach (var rental in _reportService.GetActiveByUser(user4))
                        Console.WriteLine(rental);
                    break;
                case "5":
                    Console.WriteLine(_reportService.GetSummary());
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Brak takiej opcji");
                    break;
            }
        }
    }

    private Guid? ReadGuid(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + " (q = anuluj): ");
            string? input = Console.ReadLine();
            if (input == "q") return null;
            if (Guid.TryParse(input, out Guid result))
                return result;
            Console.WriteLine("Nieprawidłowy format ID.");
        }
    }

    private DateTime? ReadDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + " (q = anuluj): ");
            string? input = Console.ReadLine();
            if (input == "q") return null;
            if (DateTime.TryParse(input, out DateTime result))
                return result;
            Console.WriteLine("Nieprawidłowy format daty, użyj formatu yyyy-MM-dd.");
        }
    }

    private int? ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt + " (q = anuluj): ");
            string? input = Console.ReadLine();
            if (input == "q") return null;
            if (int.TryParse(input, out int result))
                return result;
            Console.WriteLine("Nieprawidłowa wartość, podaj liczbę całkowitą.");
        }
    }
}