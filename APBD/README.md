# Uczelniana Wypożyczalnia Sprzętu

## Opis projektu
Aplikacja konsolowa w C# symulująca system wypożyczalni sprzętu na uczelni PJATK.
Umożliwia zarządzanie sprzętem, użytkownikami oraz wypożyczeniami poprzez
interaktywne menu konsolowe.

## Instrukcja uruchomienia
1. Sklonuj repozytorium: `git clone <link>`
2. Otwórz projekt w JetBrains Rider
3. Uruchom projekt: Run > Run 'APBD'

## Struktura projektu
```
APBD/
├── Models/
│   ├── Equipment/     – klasy opisujące sprzęt
│   ├── Users/         – klasy opisujące użytkowników
│   └── Rental.cs      – klasa opisująca wypożyczenie
├── Services/
│   ├── RentalService.cs    – logika wypożyczeń i zwrotów
│   ├── EquipmentService.cs – zarządzanie sprzętem
│   ├── ReportService.cs    – raporty i zestawienia
│   └── UserService.cs      – zarządzanie użytkownikami
├── UI/
│   └── Menu.cs        – interaktywne menu konsolowe
├── RentalConfig.cs    – stałe i reguły biznesowe
└── Program.cs         – punkt wejścia aplikacji
```

## Decyzje projektowe

### Podział na warstwy
Kod podzieliłem na trzy warstwy: `Models`, `Services` i `UI`.
Każda warstwa odpowiada za coś innego – `Models` opisuje jak wyglądają
obiekty i co zawierają, `Services` wykonuje całą logikę biznesową,
a `UI` odpowiada wyłącznie za wyświetlanie i obsługę menu.
Dzięki temu gdy chcę coś zmienić, od razu wiem gdzie zajrzeć,
a zmiana w jednej warstwie nie wpływa na pozostałe.

### Kohezja
Każda klasa ma jedną wyraźną odpowiedzialność. Przykładowo `RentalService`
zajmuje się tylko logiką wypożyczeń, `ReportService` tylko generowaniem
raportów, a `Menu` tylko obsługą interfejsu użytkownika. Żadna z tych klas
nie wchodzi w kompetencje innej.

### Coupling
Serwisy nie są od siebie bezpośrednio zależne. Listy danych (`Rental`,
`Equipment`, `User`) są tworzone w `Program.cs` i przekazywane do serwisów
przez konstruktor (Dependency Injection). Dzięki temu `RentalService`
i `ReportService` operują na tej samej liście wypożyczeń bez konieczności
znajomości siebie nawzajem.

### Klasy abstrakcyjne
`Equipment` i `User` są klasami abstrakcyjnymi, ponieważ nie ma sensu
tworzyć "sprzętu w ogóle" ani "użytkownika w ogóle" – zawsze musi to być
konkretny typ jak `Laptop`, `Student` czy `Employee`. Klasy abstrakcyjne
wymuszają stworzenie konkretnego obiektu w podklasie.

### Reguły biznesowe
Limity wypożyczeń i stawka kary za opóźnienie są zebrane w jednej klasie
`RentalConfig`. Dzięki temu gdy logika biznesowa się zmieni, modyfikacja
odbywa się w jednym miejscu, a nie w wielu różnych klasach.

## Scenariusz demonstracyjny

### 1. Dodanie sprzętu
- Menu główne → `1` (Sprzęt) → `1` (Dodaj sprzęt)
- Typ: `laptop`, nazwa: `Dell XPS`, procesor: `Intel i7`, RAM: `16`
- Powtórz dla projektora i kamery

### 2. Dodanie użytkowników
- Menu główne → `2` (Użytkownicy) → `1` (Dodaj użytkownika)
- Dodaj studenta (s) i pracownika (p)

### 3. Poprawne wypożyczenie
- Menu główne → `3` (Wypożyczenia) → `1` (Wypożycz sprzęt)
- Skopiuj ID użytkownika i sprzętu z listy
- Data wypożyczenia: `2025-03-01`, data zwrotu: `2025-03-10`

### 4. Próba niepoprawnej operacji
- Spróbuj wypożyczyć ten sam sprzęt ponownie → błąd: sprzęt niedostępny
- Dodaj studenta i wypożycz mu 2 rzeczy, przy 3. → błąd: przekroczono limit

### 5. Zwrot w terminie
- Menu główne → `3` → `2` (Zwróć sprzęt)
- Podaj ID wypożyczenia, data zwrotu: `2025-03-08` (przed terminem)
- Kara: 0

### 6. Zwrot opóźniony
- Wypożycz kolejny sprzęt, zwróć z datą po terminie np. `2025-03-15`
- System naliczy karę: liczba dni × 50 zł

### 7. Raport końcowy
- Menu główne → `4` (Raporty) → `5` (Raport podsumowujący)