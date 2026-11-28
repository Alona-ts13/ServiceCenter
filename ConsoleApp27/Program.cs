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
                ShowMenu();
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1": ShowAbout(); Pause(); break;
                    case "2": RunCostTable(); Pause(); break;
                    case "3": RunWorkloadTable(); Pause(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("Немає такого пункту. Спробуйте ще раз.");
                        Pause();
                        break;
                }
            }
        }

        // 1. Виведення головного меню
        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Сервісний центр ===");
            Console.WriteLine("1 — Про програму та сутність");
            Console.WriteLine("2 — Таблиця вартості ремонту (табулювання)");
            Console.WriteLine("3 — Завантаження майстрів за тиждень");
            Console.WriteLine("0 — Вихід");
            Console.Write("Ваш вибір: ");
        }

        // 2. Пауза перед поверненням у меню
        static void Pause()
        {
            Console.WriteLine();
            Console.Write("Натисніть Enter, щоб повернутися в меню...");
            Console.ReadLine();
        }

        // 3. Інформація про тему, сутність та поля (Рівень А, п. 2)
        static void ShowAbout()
        {
            Console.WriteLine("=== Про програму ===");
            Console.WriteLine("Тема проєкту: Автоматизація обліку сервісного центру");
            Console.WriteLine("Основна сутність: Замовлення на ремонт (RepairOrder)");
            Console.WriteLine("Поля сутності та їхні типи:");
            Console.WriteLine(" - orderId (int) — унікальний номер замовлення;");
            Console.WriteLine(" - deviceName (string) — назва або модель пристрою;");
            Console.WriteLine(" - workHours (double) — витрачений час майстра (у годинах);");
            Console.WriteLine(" - hourlyRate (double) — вартість години роботи майстра (грн);");
            Console.WriteLine(" - partsCost (double) — вартість використаних запчастин (грн);");
            Console.WriteLine(" - isCompleted (bool) — статус виконання замовлення.");
        }

        // 4. Обчислення вартості ремонту без виведення (Рівень А, п. 3)
        static double CalculateRepairCost(double hours, double rate, double parts)
        {
            return hours * rate + parts;
        }

        // 5. Зчитування дробового числа з перевіркою меж (Рівень Б)
        static double ReadDouble(string prompt, double min, double max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!double.TryParse(input, out double value))
                {
                    Console.WriteLine("Помилка: введено не число. Спробуйте ще раз.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути в межах від {min} до {max}.");
                    continue;
                }

                return value;
            }
        }

        // 6. Зчитування цілого числа з перевіркою меж (Рівень Б)
        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int value))
                {
                    Console.WriteLine("Помилка: введено не ціле число. Спробуйте ще раз.");
                    continue;
                }

                if (value < min || value > max)
                {
                    Console.WriteLine($"Помилка: число має бути в межах від {min} до {max}.");
                    continue;
                }

                return value;
            }
        }

        // 7. Підготовка даних та запуск табулювання вартості
        static void RunCostTable()
        {
            Console.WriteLine("=== Розрахунок вартості ремонту ===");
            double rate = ReadDouble("Введіть ставку майстра за годину (100 - 2000 грн): ", 100, 2000);
            double parts = ReadDouble("Введіть базову вартість запчастин (0 - 50000 грн): ", 0, 50000);
            double startHours = ReadDouble("Початкова кількість годин (0.5 - 20): ", 0.5, 20);
            double endHours = ReadDouble($"Кінцева кількість годин ({startHours} - 40): ", startHours, 40);
            double step = ReadDouble("Крок зміни годин (0.5 - 5): ", 0.5, 5);

            Console.WriteLine();
            PrintCostTable(startHours, endHours, step, rate, parts);
        }

        // 8. Табулювання вартості ремонту за переданими параметрами (Рівень А, п. 4)
        static void PrintCostTable(double start, double end, double step, double rate, double parts)
        {
            if (step <= 0 || start > end)
            {
                Console.WriteLine("Некоректні межі або крок табулювання.");
                return;
            }

            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("| Години роботи | Загальна вартість, грн |");
            Console.WriteLine("-----------------------------------------");

            for (double h = start; h <= end + 0.0001; h += step)
            {
                double totalCost = CalculateRepairCost(h, rate, parts);
                Console.WriteLine($"| {h,13:F1} | {totalCost,22:F2} |");
            }

            Console.WriteLine("-----------------------------------------");
        }

        // 9. Підготовка даних для таблиці завантаження
        static void RunWorkloadTable()
        {
            Console.WriteLine("=== Завантаження майстрів за тиждень ===");
            int mastersCount = ReadInt("Введіть кількість майстрів (1 - 10): ", 1, 10);
            int daysCount = ReadInt("Введіть кількість робочих днів (1 - 7): ", 1, 7);

            Console.WriteLine();
            PrintWorkloadTable(mastersCount, daysCount);
        }

        // 10. Виведення таблиці завантаження майстрів
        static void PrintWorkloadTable(int mastersCount, int daysCount)
        {
            if (mastersCount <= 0 || daysCount <= 0)
            {
                Console.WriteLine("Нема чого виводити.");
                return;
            }

            Console.Write("Майстер \\ День |");
            for (int d = 1; d <= daysCount; d++)
            {
                Console.Write($" День {d} |");
            }
            Console.WriteLine();

            for (int m = 1; m <= mastersCount; m++)
            {
                Console.Write($"Майстер #{m,-5} |");
                for (int d = 1; d <= daysCount; d++)
                {
                    int orders = (m * 2 + d) % 6 + 1;
                    Console.Write($" {orders,4} зам|");
                }
                Console.WriteLine();
            }
        }
    }
}







