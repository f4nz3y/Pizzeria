using System;
using System.Text;
using PizzeriaSystem.UI;

namespace PizzeriaSystem
{
    /// <summary>
    /// Головний клас програми
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входу в програму
        /// </summary>
        /// <param name="args">Аргументи командного рядка</param>
        static void Main(string[] args)
        {
            try
            {
                // Встановлюємо кодування UTF-8 для правильного відображення української мови
                Console.OutputEncoding = Encoding.UTF8;
                Console.InputEncoding = Encoding.UTF8;

                // Встановлюємо заголовок вікна консолі
                Console.Title = "Система Управління Піцерією v1.0";

                // Створюємо та запускаємо консольний інтерфейс
                var consoleUI = new ConsoleUI();
                consoleUI.Run();
            }
            catch (Exception ex)
            {
                // Обробка критичних помилок
                Console.WriteLine("╔══════════════════════════════════════════════╗");
                Console.WriteLine("║            КРИТИЧНА ПОМИЛКА!                ║");
                Console.WriteLine("╚══════════════════════════════════════════════╝");
                Console.WriteLine($"Помилка: {ex.Message}");
                Console.WriteLine($"Деталі: {ex.StackTrace}");
                Console.WriteLine("\nПрограма буде завершена.");
                Console.WriteLine("Натисніть будь-яку клавішу для виходу...");
                Console.ReadKey();
            }
        }
    }
}