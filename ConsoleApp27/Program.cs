using System;
using System.Text;

namespace ServiceCenter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            bool running = true;

            while (running)
            {
                Console.WriteLine("=== Автосервіс ===");
                Console.WriteLine("1 - довідка про програму");
                Console.WriteLine("2 - таблиця вартості ремонту (табулювання)");
                Console.WriteLine("3 - звіт завантаження майстрів (таблиця на два входи)");
                Console.WriteLine("0 - вихід");
                Console.Write("Ваш вибір: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        ShowAbout();
                        Pause();
                        break;

                    case "2":
                        ShowCostTable();
                        Pause();
                        break;

                    case "3":
                        ShowWorkloadTable();
                        Pause();
                        break;

                    case "0":
                        running = false;
                        Console.WriteLine("Роботу завершено. До побачення!");
                        break;

                    default:
                        Console.WriteLine("Помилка: немає такого пункту меню. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        static void Pause()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для повернення в меню...");
            Console.ReadKey(true);
            Console.Clear();
        }

        static void ShowAbout()
        {
            Console.WriteLine("=== Довідка про застосунок ===");
            Console.WriteLine("Тема проєкту: Автосервіс (облік заявок на ремонт автомобілів).");
            Console.WriteLine("Головна сутність: Заявка на ремонт (Order).");
            Console.WriteLine("Підсумкова величина: Загальна вартість виконаних ремонтів (грн).");
            Console.WriteLine("\nПоля запису (7 полів):");
            Console.WriteLine("1. Номер заявки (int) — призначається автоматично, > 0");
            Console.WriteLine("2. Марка та модель авто (string) — непорожній рядок");
            Console.WriteLine("3. Державний номер (string) — формат АА0000АА, непорожній");
            Console.WriteLine("4. Категорія робіт (string) — Двигун, Ходова, Електрика, Кузов");
            Console.WriteLine("5. Прізвище майстра (string) — непорожній рядок");
            Console.WriteLine("6. Витрачено нормо-годин (double) — від 0.5 до 100.0");
            Console.WriteLine("7. Вартість ремонту, грн (double) — > 0");
        }

        static void ShowCostTable()
        {
            Console.WriteLine("=== Розрахунок вартості ремонту залежно від тривалості ===");
            Console.WriteLine("Формула: Вартість = 400 (діагностика) + 650 * години + 50 * години^1.2\n");

            double from = 0.5;
            double to = 5.0;
            double step = 0.5;

            if (step <= 0 || from > to)
            {
                Console.WriteLine("Помилка: некоректні межі або крок табулювання.");
                return;
            }

            Console.WriteLine($"{"Нормо-год",12} | {"Вартість (грн)",16}");
            Console.WriteLine(new string('-', 31));

            for (double hours = from; hours <= to + 1e-9; hours += step)
            {
                double cost = 400.0 + 650.0 * hours + 50.0 * Math.Pow(hours, 1.2);
                Console.WriteLine($"{hours,12:F2} | {cost,16:F2}");
            }
        }

        static void ShowWorkloadTable()
        {
            Console.WriteLine("=== Звіт: кількість виконаних заявок за майстрами та днями ===");

            Console.Write("Введіть кількість майстрів (рядків, від 1 до 15): ");
            if (!int.TryParse(Console.ReadLine(), out int rows) || rows <= 0 || rows > 15)
            {
                Console.WriteLine("Помилка: кількість рядків має бути цілим числом від 1 до 15.");
                return;
            }

            Console.Write("Введіть кількість робочих днів (стовпців, від 1 до 7): ");
            if (!int.TryParse(Console.ReadLine(), out int cols) || cols <= 0 || cols > 7)
            {
                Console.WriteLine("Помилка: кількість стовпців має бути цілим числом від 1 до 7.");
                return;
            }

            Console.WriteLine();

            int[] colTotals = new int[cols];
            int grandTotal = 0;

            Console.Write($"{"Майстер \\ День",-15}");
            for (int col = 1; col <= cols; col++)
            {
                Console.Write($"{"День " + col,9}");
            }
            Console.WriteLine($"{"Разом",10}");

            Console.WriteLine(new string('-', 15 + cols * 9 + 10));

            for (int row = 1; row <= rows; row++)
            {
                int rowSum = 0;
                Console.Write($"{"Майстер #" + row,-15}");

                for (int col = 1; col <= cols; col++)
                {
                    int ordersCount = (row * 2 + col) % 5 + 1;

                    Console.Write($"{ordersCount,9}");

                    rowSum += ordersCount;
                    colTotals[col - 1] += ordersCount;
                }

                grandTotal += rowSum;
                Console.WriteLine($"{rowSum,10}");
            }

            Console.WriteLine(new string('-', 15 + cols * 9 + 10));
            Console.Write($"{"Всього за день",-15}");

            for (int col = 0; col < cols; col++)
            {
                Console.Write($"{colTotals[col],9}");
            }

            Console.WriteLine($"{grandTotal,10}");
        }
    }
}





 