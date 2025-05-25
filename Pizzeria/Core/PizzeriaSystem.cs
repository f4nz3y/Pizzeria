using System;
using System.Collections.Generic;
using System.Linq;
using PizzeriaSystem.Models;
using PizzeriaSystem.Enums;

namespace PizzeriaSystem.Core
{
    /// <summary>
    /// Головний клас системи піцерії, координує роботу всіх компонентів
    /// </summary>
    public class PizzeriaSystemCore
    {
        /// <summary>
        /// Список клієнтів системи
        /// </summary>
        public List<Customer> Customers { get; private set; }

        /// <summary>
        /// Меню піцерії
        /// </summary>
        public List<Pizza> Menu { get; private set; }

        /// <summary>
        /// Список всіх замовлень
        /// </summary>
        public List<Order> Orders { get; private set; }

        /// <summary>
        /// Список всіх платежів
        /// </summary>
        public List<Payment> Payments { get; private set; }

        /// <summary>
        /// Список всіх доставок
        /// </summary>
        public List<Delivery> Deliveries { get; private set; }

        /// <summary>
        /// Лічильники для генерації унікальних ID
        /// </summary>
        private int _nextCustomerId = 1;
        private int _nextPizzaId = 1;
        private int _nextOrderId = 1;
        private int _nextPaymentId = 1;
        private int _nextDeliveryId = 1;

        /// <summary>
        /// Конструктор системи піцерії
        /// </summary>
        public PizzeriaSystemCore()
        {
            Customers = new List<Customer>();
            Menu = new List<Pizza>();
            Orders = new List<Order>();
            Payments = new List<Payment>();
            Deliveries = new List<Delivery>();
            InitializeDefaultMenu();
        }

        #region Управління клієнтами

        /// <summary>
        /// Додати нового клієнта
        /// </summary>
        /// <param name="name">Ім'я клієнта</param>
        /// <param name="phone">Телефон клієнта</param>
        /// <param name="email">Email клієнта</param>
        /// <param name="address">Адреса клієнта</param>
        /// <returns>Створений клієнт</returns>
        public Customer AddCustomer(string name, string phone, string email, string address)
        {
            var customer = new Customer(_nextCustomerId++, name, phone, email, address);
            Customers.Add(customer);
            return customer;
        }

        /// <summary>
        /// Знайти клієнта за ID
        /// </summary>
        /// <param name="customerId">ID клієнта</param>
        /// <returns>Клієнт або null</returns>
        public Customer FindCustomerById(int customerId)
        {
            return Customers.FirstOrDefault(c => c.CustomerId == customerId);
        }

        /// <summary>
        /// Знайти клієнта за телефоном
        /// </summary>
        /// <param name="phone">Номер телефону</param>
        /// <returns>Клієнт або null</returns>
        public Customer FindCustomerByPhone(string phone)
        {
            return Customers.FirstOrDefault(c => c.Phone == phone);
        }

        #endregion

        #region Управління меню

        /// <summary>
        /// Ініціалізувати меню за замовчуванням
        /// </summary>
        private void InitializeDefaultMenu()
        {
            AddPizzaToMenu("Маргарита", new List<string> { "Томатний соус", "Моцарела", "Базилік" },
                          PizzaSize.Medium, 150.0, 15);
            AddPizzaToMenu("Пепероні", new List<string> { "Томатний соус", "Моцарела", "Пепероні" },
                          PizzaSize.Medium, 180.0, 18);
            AddPizzaToMenu("Гавайська", new List<string> { "Томатний соус", "Моцарела", "Шинка", "Ананас" },
                          PizzaSize.Medium, 200.0, 20);
            AddPizzaToMenu("Чотири сири", new List<string> { "Вершковий соус", "Моцарела", "Пармезан", "Горгонзола", "Чеддер" },
                          PizzaSize.Medium, 220.0, 16);
        }

        /// <summary>
        /// Додати піцу до меню
        /// </summary>
        /// <param name="name">Назва піци</param>
        /// <param name="ingredients">Список інгредієнтів</param>
        /// <param name="size">Розмір піци</param>
        /// <param name="basePrice">Базова ціна</param>
        /// <param name="cookingTime">Час приготування</param>
        /// <returns>Створена піца</returns>
        public Pizza AddPizzaToMenu(string name, List<string> ingredients, PizzaSize size, double basePrice, int cookingTime)
        {
            var pizza = new Pizza(_nextPizzaId++, name, ingredients, size, basePrice, cookingTime);
            Menu.Add(pizza);
            return pizza;
        }

        /// <summary>
        /// Видалити піцу з меню
        /// </summary>
        /// <param name="pizzaId">ID піци</param>
        /// <returns>Результат операції</returns>
        public bool RemovePizzaFromMenu(int pizzaId)
        {
            var pizza = Menu.FirstOrDefault(p => p.PizzaId == pizzaId);
            if (pizza != null)
            {
                Menu.Remove(pizza);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Знайти піцу в меню за ID
        /// </summary>
        /// <param name="pizzaId">ID піци</param>
        /// <returns>Піца або null</returns>
        public Pizza FindPizzaById(int pizzaId)
        {
            return Menu.FirstOrDefault(p => p.PizzaId == pizzaId);
        }

        /// <summary>
        /// Показати повне меню
        /// </summary>
        public void DisplayMenu()
        {
            Console.WriteLine("\n=== МЕНЮ ПІЦЕРІЇ ===");
            foreach (var pizza in Menu)
            {
                Console.WriteLine($"ID: {pizza.PizzaId} | {pizza}");
            }
            Console.WriteLine("===================\n");
        }

        #endregion

        #region Управління замовленнями

        /// <summary>
        /// Створити нове замовлення
        /// </summary>
        /// <param name="customer">Клієнт</param>
        /// <returns>Створене замовлення</returns>
        public Order CreateOrder(Customer customer)
        {
            if (customer == null)
                throw new ArgumentException("Клієнт не може бути null");

            var order = new Order(_nextOrderId++, customer);
            Orders.Add(order);
            return order;
        }

        /// <summary>
        /// Обробити замовлення
        /// </summary>
        /// <param name="orderId">ID замовлення</param>
        /// <param name="paymentMethod">Метод оплати</param>
        /// <returns>Результат обробки</returns>
        public bool ProcessOrder(int orderId, PaymentMethod paymentMethod)
        {
            var order = Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return false;
            }

            if (order.OrderItems.Count == 0)
            {
                return false;
            }

            try
            {
                // Створюємо платіж
                var payment = new Payment(_nextPaymentId++, order, order.TotalAmount, paymentMethod);
                Payments.Add(payment);

                // Обробляємо платіж
                if (payment.ProcessPayment())
                {
                    // Оновлюємо статус замовлення
                    order.UpdateStatus(OrderStatus.Cooking);

                    // Створюємо доставку
                    var delivery = new Delivery(_nextDeliveryId++, order);
                    Deliveries.Add(delivery);

                    return true;
                }
                else
                {
                    Console.WriteLine($"Помилка обробки платежу для замовлення #{orderId}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка під час обробки замовлення: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Знайти замовлення за ID
        /// </summary>
        /// <param name="orderId">ID замовлення</param>
        /// <returns>Замовлення або null</returns>
        public Order FindOrderById(int orderId)
        {
            return Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        /// <summary>
        /// Отримати замовлення клієнта
        /// </summary>
        /// <param name="customerId">ID клієнта</param>
        /// <returns>Список замовлень клієнта</returns>
        public List<Order> GetCustomerOrders(int customerId)
        {
            return Orders.Where(o => o.Customer.CustomerId == customerId).ToList();
        }

        #endregion

        #region Управління доставкою

        /// <summary>
        /// Призначити кур'єра для доставки
        /// </summary>
        /// <param name="deliveryId">ID доставки</param>
        /// <param name="courierName">Ім'я кур'єра</param>
        /// <returns>Результат операції</returns>
        public bool AssignCourierToDelivery(int deliveryId, string courierName)
        {
            var delivery = Deliveries.FirstOrDefault(d => d.DeliveryId == deliveryId);
            if (delivery != null)
            {
                delivery.AssignCourier(courierName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Оновити статус доставки
        /// </summary>
        /// <param name="deliveryId">ID доставки</param>
        /// <param name="newStatus">Новий статус</param>
        /// <returns>Результат операції</returns>
        public bool UpdateDeliveryStatus(int deliveryId, DeliveryStatus newStatus)
        {
            var delivery = Deliveries.FirstOrDefault(d => d.DeliveryId == deliveryId);
            if (delivery != null)
            {
                delivery.UpdateDeliveryStatus(newStatus);

                // Якщо доставка завершена, оновлюємо статус замовлення
                if (newStatus == DeliveryStatus.Delivered)
                {
                    delivery.Order.UpdateStatus(OrderStatus.Delivered);
                }

                return true;
            }
            return false;
        }

        /// <summary>
        /// Отримати активні доставки
        /// </summary>
        /// <returns>Список активних доставок</returns>
        public List<Delivery> GetActiveDeliveries()
        {
            return Deliveries.Where(d => d.Status != DeliveryStatus.Delivered).ToList();
        }

        #endregion

        #region Генерація звітів

        /// <summary>
        /// Сформувати звіт про продажі
        /// </summary>
        /// <param name="startDate">Початкова дата</param>
        /// <param name="endDate">Кінцева дата</param>
        /// <returns>Звіт у вигляді рядка</returns>
        public string GenerateSalesReport(DateTime startDate, DateTime endDate)
        {
            var ordersInPeriod = Orders.Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate).ToList();
            var completedPayments = Payments.Where(p => p.Status == PaymentStatus.Completed &&
                                                        p.PaymentDate >= startDate && p.PaymentDate <= endDate).ToList();

            var totalRevenue = completedPayments.Sum(p => p.Amount);
            var totalOrders = ordersInPeriod.Count;
            var averageOrderValue = totalOrders > 0 ? totalRevenue / totalOrders : 0;

            var report = $@"
=== ЗВІТ ПРО ПРОДАЖІ ===
Період: {startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy}

Загальна кількість замовлень: {totalOrders}
Загальний дохід: {totalRevenue:C} грн.
Середня вартість замовлення: {averageOrderValue:C} грн.

Розподіл за способами оплати:";

            var paymentMethods = completedPayments.GroupBy(p => p.PaymentMethod)
                                                 .Select(g => new { Method = g.Key, Count = g.Count(), Total = g.Sum(p => p.Amount) });

            foreach (var method in paymentMethods)
            {
                report += $"\n- {GetPaymentMethodInUkrainian(method.Method)}: {method.Count} платежів, {method.Total:C} грн.";
            }

            report += "\n========================";
            return report;
        }

        /// <summary>
        /// Сформувати звіт про популярні піци
        /// </summary>
        /// <returns>Звіт у вигляді рядка</returns>
        public string GeneratePopularPizzasReport()
        {
            var pizzaStats = Orders.SelectMany(o => o.OrderItems)
                                  .GroupBy(oi => oi.Pizza.Name)
                                  .Select(g => new {
                                      Name = g.Key,
                                      TotalQuantity = g.Sum(oi => oi.Quantity),
                                      TotalRevenue = g.Sum(oi => oi.Subtotal)
                                  })
                                  .OrderByDescending(p => p.TotalQuantity)
                                  .Take(10);

            var report = "\n=== ТОП-10 ПОПУЛЯРНИХ ПІЦ ===\n";
            int position = 1;

            foreach (var pizza in pizzaStats)
            {
                report += $"{position}. {pizza.Name}: {pizza.TotalQuantity} шт., {pizza.TotalRevenue:C} грн.\n";
                position++;
            }

            report += "===============================";
            return report;
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

        #endregion

        #region Допоміжні методи

        /// <summary>
        /// Показати статистику системи
        /// </summary>
        public void DisplaySystemStats()
        {
            Console.WriteLine("\n=== СТАТИСТИКА СИСТЕМИ ===");
            Console.WriteLine($"Клієнтів: {Customers.Count}");
            Console.WriteLine($"Позицій в меню: {Menu.Count}");
            Console.WriteLine($"Всього замовлень: {Orders.Count}");
            Console.WriteLine($"Активних замовлень: {Orders.Count(o => o.Status != OrderStatus.Delivered)}");
            Console.WriteLine($"Платежів: {Payments.Count}");
            Console.WriteLine($"Доставок: {Deliveries.Count}");
            Console.WriteLine($"Активних доставок: {GetActiveDeliveries().Count}");
            Console.WriteLine("===========================\n");
        }

        /// <summary>
        /// Показати всі замовлення
        /// </summary>
        public void DisplayAllOrders()
        {
            Console.WriteLine("\n=== ВСІ ЗАМОВЛЕННЯ ===");
            if (Orders.Count == 0)
            {
                Console.WriteLine("Замовлень немає");
            }
            else
            {
                foreach (var order in Orders.OrderByDescending(o => o.OrderDate))
                {
                    Console.WriteLine(order);
                    Console.WriteLine("---");
                }
            }
            Console.WriteLine("=====================\n");
        }

        /// <summary>
        /// Показати активні доставки
        /// </summary>
        public void DisplayActiveDeliveries()
        {
            var activeDeliveries = GetActiveDeliveries();
            Console.WriteLine("\n=== АКТИВНІ ДОСТАВКИ ===");

            if (activeDeliveries.Count == 0)
            {
                Console.WriteLine("Активних доставок немає");
            }
            else
            {
                foreach (var delivery in activeDeliveries)
                {
                    Console.WriteLine(delivery);
                    Console.WriteLine($"Очікуваний час: {delivery.EstimatedTime} хв.");
                    Console.WriteLine("---");
                }
            }
            Console.WriteLine("========================\n");
        }

        #endregion
    }
}