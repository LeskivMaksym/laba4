using System;

struct Date
{
    public int Day { get; private set; }
    public int Month { get; private set; }
    public int Year { get; private set; }

    public Date(int day, int month, int year)
    {
        if (day < 1 || day > 31 || month < 1 || month > 12 || year < 0)
        {
            Console.WriteLine("Некоректна дата");
            Day = Month = Year = 1; // За замовчуванням - 01.01.0001
        }
        else
        {
            Day = day;
            Month = month;
            Year = year;
        }
    }

    public Date(DateTime dateTime)
    {
        Day = dateTime.Day;
        Month = dateTime.Month;
        Year = dateTime.Year;
    }

    public bool IsLeapYear()
    {
        return (Year % 4 == 0 && Year % 100 != 0) || (Year % 400 == 0);
    }

    public static Date operator +(Date date, int days)
    {
        DateTime dt = new DateTime(date.Year, date.Month, date.Day);
        dt = dt.AddDays(days);
        return new Date(dt);
    }

    public static Date operator -(Date date, int days)
    {
        DateTime dt = new DateTime(date.Year, date.Month, date.Day);
        dt = dt.AddDays(-days);
        return new Date(dt);
    }

    public static int DaysDifference(Date date1, Date date2)
    {
        DateTime dt1 = new DateTime(date1.Year, date1.Month, date1.Day);
        DateTime dt2 = new DateTime(date2.Year, date2.Month, date2.Day);
        TimeSpan difference = dt2 - dt1;
        return Math.Abs(difference.Days);
    }

    public override string ToString()
    {
        return $"{Month:D2}.{Day:D2}.{Year:D4}";
    }
}

class Program
{
    public static Date[] ReadDatesFromInput(int n)
    {
        Date[] dates = new Date[n];
        for (int i = 0; i < n; i++)
        {
            Console.Write($"Введіть дату {i + 1} (день місяць рік): ");
            string input = Console.ReadLine();
            string[] parts = input.Split('.');
            if (parts.Length == 3 && int.TryParse(parts[0], out int day) && int.TryParse(parts[1], out int month) && int.TryParse(parts[2], out int year))
            {
                dates[i] = new Date(day, month, year);
            }
            else
            {
                Console.WriteLine("Некоректний ввід. Спробуйте ще раз.");
                i--;
            }
        }
        return dates;
    }

    public static void PrintDate(Date date)
    {
        Console.WriteLine(date);
    }

    public static void SortDates(Date[] dates)
    {
        Array.Sort(dates, (d1, d2) => d1.Year == d2.Year ? d1.Month == d2.Month ? d1.Day.CompareTo(d2.Day) : d1.Month.CompareTo(d2.Month) : d1.Year.CompareTo(d2.Year));
    }

    public static void ModifyDate(ref Date date, int day, int month, int year)
    {
        date = new Date(day, month, year);
    }

    static void Main(string[] args)
    {
        // Читаємо масив дат з клавіатури
        Console.Write("Введіть кількість дат: ");
        if (int.TryParse(Console.ReadLine(), out int n))
        {
            Date[] dates = ReadDatesFromInput(n);

            // Виводимо введені дати
            Console.WriteLine("Введені дати:");
            foreach (var date in dates)
            {
                PrintDate(date);
            }

            // Сортуємо дати
            SortDates(dates);

            // Виводимо відсортовані дати
            Console.WriteLine("Відсортовані дати:");
            foreach (var date in dates)
            {
                PrintDate(date);
            }

            // Визначаємо майбутню дату
            Date futureDate = dates[0] + 7;
            Console.WriteLine("Майбутня дата: " + futureDate);

            // Віднімаємо 3 дні від першої дати
            Date subtractedDate = dates[0] - 3;
            Console.WriteLine("Дата після віднімання 3 днів: " + subtractedDate);

            // Перевіряємо, чи перший рік є високосним
            if (dates[0].IsLeapYear())
                Console.WriteLine("Перший рік є високосним");
            else
                Console.WriteLine("Перший рік не є високосним");

            // Обчислюємо різницю в днях між першою та другою датами
            int daysDifference = Date.DaysDifference(dates[0], dates[1]);
            Console.WriteLine("Різниця в днях між першою та другою датами: " + daysDifference);
        }
        else
        {
            Console.WriteLine("Некоректний ввід для кількості дат.");
        }
    }
}
