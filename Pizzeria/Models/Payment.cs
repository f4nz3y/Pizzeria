using System;
using PizzeriaSystem.Enums;

namespace PizzeriaSystem.Models
{
    /// <summary>
    /// Клас для обробки платежів
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// Унікальний ідентифікатор платежу
        /// </summary>
        public int PaymentId { get; set; }

        /// <summary>
        /// Замовлення, за яке здійснюється платіж
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Сума платежу
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        /// Метод оплати
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; }

        /// <summary>
        /// Дата та час платежу
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Статус платежу
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Конструктор класу Payment
        /// </summary>
        /// <param name="paymentId">Ідентифікатор платежу</param>
        /// <param name="order">Замовлення</param>
        /// <param name="amount">Сума</param>
        /// <param name="paymentMethod">Метод оплати</param>
        public Payment(int paymentId, Order order, double amount, PaymentMethod paymentMethod)
        {
            PaymentId = paymentId;
            Order = order ?? throw new ArgumentException("Замовлення не може бути null");
            Amount = amount > 0 ? amount : throw new ArgumentException("Сума повинна бути більше 0");
            PaymentMethod = paymentMethod;
            PaymentDate = DateTime.Now;
            Status = PaymentStatus.Pending;
        }

        /// <summary>
        /// Обробити платіж
        /// </summary>
        /// <returns>Результат обробки платежу</returns>
        public bool ProcessPayment()
        {
            try
            {
                Console.WriteLine($"Обробка платежу на суму {Amount:C} грн. методом {GetPaymentMethodInUkrainian(PaymentMethod)}...");

                // Симуляція обробки платежу
                System.Threading.Thread.Sleep(1000); // Імітація затримки обробки

                // Простий алгоритм "успішності" платежу (90% успіху для демонстрації)
                Random random = new Random();
                bool isSuccessful = random.Next(1, 11) <= 9; // 90% шанс успіху

                if (isSuccessful)
                {
                    Status = PaymentStatus.Completed;
                    Console.WriteLine("Платіж успішно оброблено!");
                    return true;
                }
                else
                {
                    Status = PaymentStatus.Failed;
                    Console.WriteLine("Помилка обробки платежу!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Status = PaymentStatus.Failed;
                Console.WriteLine($"Помилка під час обробки платежу: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Повернути кошти
        /// </summary>
        /// <returns>Результат повернення коштів</returns>
        public bool Refund()
        {
            if (Status != PaymentStatus.Completed)
            {
                Console.WriteLine("Неможливо повернути кошти - платіж не завершений!");
                return false;
            }

            Console.WriteLine($"Повернення коштів на суму {Amount:C} грн...");
            // Симуляція повернення коштів
            Status = PaymentStatus.Failed; // Статус змінюється на Failed після повернення
            Console.WriteLine("Кошти успішно повернуто!");
            return true;
        }

        /// <summary>
        /// Отримати метод оплати українською мовою
        /// </summary>
        /// <param name="method">Метод оплати</param>
        /// <returns>Метод українською</returns>
        private string GetPaymentMethodInUkrainian(PaymentMethod method)
        {
            return method switch
            {
                PaymentMethod.Cash => "Готівка",
                PaymentMethod.Card => "Картка",
                PaymentMethod.Online => "Онлайн",
                _ => method.ToString()
            };
        }

        /// <summary>
        /// Рядкове представлення платежу
        /// </summary>
        /// <returns>Опис платежу</returns>
        public override string ToString()
        {
            return $"Платіж #{PaymentId}: {Amount:C} грн. ({GetPaymentMethodInUkrainian(PaymentMethod)}) - {Status}";
        }
    }
}
