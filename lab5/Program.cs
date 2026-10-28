using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

record Student(string Surname, string Group, int Grade, int Year);

class Program
{
    static void PrintAll(string title, IEnumerable<Student> items)
    {
        Console.WriteLine(title);
        foreach (var s in items)
            Console.WriteLine($"{s.Surname} {s.Group} {s.Grade} {s.Year}");
        Console.WriteLine();
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var students = new List<Student>
        {
            new("Ткаченко",  "ІПЗ-3/1", 85, 2024),
            new("Бондар",    "ІПЗ-3/2", 92, 2023),
            new("Іваненко",  "ІПЗ-3/1", 85, 2024),
            new("Коваль",    "ІПЗ-3/2", 78, 2024),
            new("Сидоренко", "ІПЗ-3/1", 85, 2023),
            new("Мельник",   "ІПЗ-3/2", 92, 2024),
            new("Гриценко",  "ІПЗ-3/1", 78, 2023),
            new("Дяченко",   "ІПЗ-3/2", 85, 2023),
        };

        // Український алфавітний порядок
        var uk = StringComparer.Create(new CultureInfo("uk-UA"), false);

        PrintAll("Вихідний список", students);

        // TODO 1: за прізвищем, за абеткою
        var bySurname = students.OrderBy(s => s.Surname, uk);
        PrintAll("1. За прізвищем", bySurname);

        // TODO 2: за балом, від вищого до нижчого
        var byGrade = students.OrderByDescending(s => s.Grade);
        PrintAll("2. За балом (від вищого)", byGrade);

        // TODO 3: за групою, а всередині групи — за балом від вищого
        var byGroupThenGrade = students
            .OrderBy(s => s.Group, uk)          // головний ключ
            .ThenByDescending(s => s.Grade);    // другий ключ
        PrintAll("3. За групою, потім за балом", byGroupThenGrade);

        // ---------- Частина 2 ----------
        // Спостереження: звичайний List.Sort (перевіряємо стабільність)
        var copy = new List<Student>(students);
        copy.Sort((a, b) => b.Grade.CompareTo(a.Grade));
        PrintAll("Частина 2: List.Sort за балом", copy);

        // OrderBy у LINQ — стабільне сортування, однакові зберігають вихідний порядок
        PrintAll("Частина 2: OrderByDescending за балом", students.OrderByDescending(s => s.Grade));

        // Виправлення: серед однакових балів — прізвища за абеткою
        var fixedOrder = students
            .OrderByDescending(s => s.Grade)
            .ThenBy(s => s.Surname, uk);
        PrintAll("Частина 2: бал, а серед однакових — за прізвищем", fixedOrder);
    }
}