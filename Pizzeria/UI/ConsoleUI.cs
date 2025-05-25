// ===============================
// Файл: UI/ConsoleUI.cs
// Призначення: Консольний інтерфейс користувача для системи піцерії
// ===============================
using System;
using System.Collections.Generic;
using System.Linq;
using PizzeriaSystem.Core;
using PizzeriaSystem.Models;
using PizzeriaSystem.Enums;

namespace PizzeriaSystem.UI
{
    /// <summary>
    /// Консольний інтерфейс для роботи з системою піцерії
    /// </summary>
    public class ConsoleUI
    {
        private readonly PizzeriaSystemCore _system;
        private bool _isRunning;

        /// <summary>
        /// Конструктор консольного інтерфейсу
        /// </summary>
        public ConsoleUI()
        {
            _system = new PizzeriaSystemCore();
            _isRunning = true;
        }

        /// <summary>
        /// Запустити головне меню програми
        /// </summary>
        public void Run()
        {
            ShowWelcomeMessage();

            while (_isRunning)
            {
                try
                {
                    ShowMainMenu();
                    var choice = ReadMenuChoice();
                    ProcessMainMenuChoice(choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка: {ex.Message}");
                    Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
                    Console.ReadKey();
                }
            }
        }

        #region Головне меню

        /// <summary>
        /// Показати привітальне повідомлення
        /// </summary>
        private void ShowWelcomeMessage()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║          СИСТЕМА УПРАВЛІННЯ ПІЦЕРІЄЮ        ║");
            Console.WriteLine("║                 Версія 1.0                   ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");
            Console.WriteLine();
            Console.WriteLine("Ласкаво просимо до автоматизованої системи піцерії!");
            Console.WriteLine("Натисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }

        /// <summary>
        /// Показати головне меню
        /// </summary>
        private void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════ ГОЛОВНЕ МЕНЮ ═══════════════╗");
            Console.WriteLine("║ 1. Управління клієнтами                   ║");
            Console.WriteLine("║ 2. Управління меню                        ║");
            Console.WriteLine("║ 3. Управління замовленнями                ║");
            Console.WriteLine("║ 4. Управління доставкою                   ║");
            Console.WriteLine("║ 5. Звіти та статистика                    ║");
            Console.WriteLine("║ 6. Показати статус системи                ║");
            Console.WriteLine("║ 0. Вихід                                  ║");
            Console.WriteLine("╚═══════════════════════════════════════════╝");
            Console.Write("Оберіть пункт меню (0-6): ");
        }

        /// <summary>
        /// Прочитати вибір користувача з меню
        /// </summary>
        /// <returns>Номер пункту меню</returns>
        private int ReadMenuChoice()
        {
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                return choice;
            }
            return -1;
        }

        /// <summary>
        /// Обробити вибір з головного меню
        /// </summary>
        /// <param name="choice">Вибір користувача</param>
        private void ProcessMainMenuChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    ShowCustomerMenu();
                    break;
                case 2:
                    ShowMenuManagement();
                    break;
                case 3:
                    ShowOrderMenu();
                    break;
                case 4:
                    ShowDeliveryMenu();
                    break;
                case 5:
                    ShowReportsMenu();
                    break;
                case 6:
                    ShowSystemStatus();
                    break;
                case 0:
                    ExitApplication();
                    break;
                default:
                    Console.WriteLine("Невірний вибір! Спробуйте ще раз.");
                    PauseExecution();
                    break;
            }
        }

        #endregion

        #region Управління клієнтами

        /// <summary>
        /// Показати меню управління клієнтами
        /// </summary>
        private void ShowCustomerMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔══════════ УПРАВЛІННЯ КЛІЄНТАМИ ═══════════╗");
                Console.WriteLine("║ 1. Додати нового клієнта                  ║");
                Console.WriteLine("║ 2. Показати всіх клієнтів                 ║");
                Console.WriteLine("║ 3. Знайти клієнта за телефоном            ║");
                Console.WriteLine("║ 4. Показати замовлення клієнта            ║");
                Console.WriteLine("║ 0. Повернутися до головного меню          ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.Write("Оберіть дію (0-4): ");

                var choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddNewCustomer();
                        break;
                    case 2:
                        ShowAllCustomers();
                        break;
                    case 3:
                        FindCustomerByPhone();
                        break;
                    case 4:
                        ShowCustomerOrders();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        PauseExecution();
                        break;
                }
            }
        }

        /// <summary>
        /// Додати нового клієнта
        /// </summary>
        private void AddNewCustomer()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАВАННЯ НОВОГО КЛІЄНТА ===");

            Console.Write("Введіть ім'я клієнта: ");
            var name = Console.ReadLine();

            Console.Write("Введіть номер телефону: ");
            var phone = Console.ReadLine();

            Console.Write("Введіть email: ");
            var email = Console.ReadLine();

            Console.Write("Введіть адресу доставки: ");
            var address = Console.ReadLine();

            try
            {
                var customer = _system.AddCustomer(name, phone, email, address);
                Console.WriteLine($"\nКлієнта успішно додано! ID: {customer.CustomerId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні клієнта: {ex.Message}");
            }

            PauseExecution();
        }

        /// <summary>
        /// Показати всіх клієнтів
        /// </summary>
        private void ShowAllCustomers()
        {
            Console.Clear();
            Console.WriteLine("=== ВСІХ КЛІЄНТИ ===");

            if (_system.Customers.Count == 0)
            {
                Console.WriteLine("Клієнтів не знайдено.");
            }
            else
            {
                foreach (var customer in _system.Customers)
                {
                    Console.WriteLine(customer.GetCustomerInfo());
                    Console.WriteLine("---");
                }
            }

            PauseExecution();
        }

        /// <summary>
        /// Знайти клієнта за номером телефону
        /// </summary>
        private void FindCustomerByPhone()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК КЛІЄНТА ===");

            Console.Write("Введіть номер телефону: ");
            var phone = Console.ReadLine();

            var customer = _system.FindCustomerByPhone(phone);
            if (customer != null)
            {
                Console.WriteLine("\nКлієнта знайдено:");
                Console.WriteLine(customer.GetCustomerInfo());
            }
            else
            {
                Console.WriteLine("Клієнта з таким номером телефону не знайдено.");
            }

            PauseExecution();
        }

        /// <summary>
        /// Показати замовлення клієнта
        /// </summary>
        private void ShowCustomerOrders()
        {
            Console.Clear();
            Console.WriteLine("=== ЗАМОВЛЕННЯ КЛІЄНТА ===");

            Console.Write("Введіть ID клієнта: ");
            if (int.TryParse(Console.ReadLine(), out int customerId))
            {
                var orders = _system.GetCustomerOrders(customerId);
                if (orders.Count == 0)
                {
                    Console.WriteLine("У клієнта немає замовлень.");
                }
                else
                {
                    foreach (var order in orders.OrderByDescending(o => o.OrderDate))
                    {
                        Console.WriteLine(order);
                        Console.WriteLine("═══════════════════");
                    }
                }
            }
            else
            {
                Console.WriteLine("Невірний ID клієнта!");
            }

            PauseExecution();
        }

        #endregion

        #region Управління меню

        /// <summary>
        /// Показати управління меню
        /// </summary>
        private void ShowMenuManagement()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔══════════ УПРАВЛІННЯ МЕНЮ ════════════╗");
                Console.WriteLine("║ 1. Переглянути меню                    ║");
                Console.WriteLine("║ 2. Додати нову піцу                    ║");
                Console.WriteLine("║ 3. Видалити піцу з меню                ║");
                Console.WriteLine("║ 0. Повернутися до головного меню       ║");
                Console.WriteLine("╚════════════════════════════════════════╝");
                Console.Write("Оберіть дію (0-3): ");

                var choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        _system.DisplayMenu();
                        PauseExecution();
                        break;
                    case 2:
                        AddNewPizza();
                        break;
                    case 3:
                        RemovePizza();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        PauseExecution();
                        break;
                }
            }
        }

        /// <summary>
        /// Додати нову піцу до меню
        /// </summary>
        private void AddNewPizza()
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАВАННЯ НОВОЇ ПІЦИ ===");

            Console.Write("Введіть назву піци: ");
            var name = Console.ReadLine();

            Console.WriteLine("Введіть інгредієнти (через кому):");
            var ingredientsInput = Console.ReadLine();
            var ingredients = ingredientsInput?.Split(',').Select(i => i.Trim()).ToList() ?? new List<string>();

            Console.WriteLine("Оберіть розмір піци:");
            Console.WriteLine("1. Мала (Small)");
            Console.WriteLine("2. Середня (Medium)");
            Console.WriteLine("3. Велика (Large)");
            Console.Write("Ваш вibбір (1-3): ");

            PizzaSize size = PizzaSize.Medium;
            if (int.TryParse(Console.ReadLine(), out int sizeChoice))
            {
                size = sizeChoice switch
                {
                    1 => PizzaSize.Small,
                    2 => PizzaSize.Medium,
                    3 => PizzaSize.Large,
                    _ => PizzaSize.Medium
                };
            }

            Console.Write("Введіть базову ціну (грн): ");
            if (!double.TryParse(Console.ReadLine(), out double basePrice))
            {
                basePrice = 150.0;
            }

            Console.Write("Введіть час приготування (хвилини): ");
            if (!int.TryParse(Console.ReadLine(), out int cookingTime))
            {
                cookingTime = 15;
            }

            try
            {
                var pizza = _system.AddPizzaToMenu(name, ingredients, size, basePrice, cookingTime);
                Console.WriteLine($"\nПіцу '{pizza.Name}' успішно додано до меню!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка при додаванні піци: {ex.Message}");
            }

            PauseExecution();
        }

        /// <summary>
        /// Видалити піцу з меню
        /// </summary>
        private void RemovePizza()
        {
            Console.Clear();
            Console.WriteLine("=== ВИДАЛЕННЯ ПІЦИ З МЕНЮ ===");

            _system.DisplayMenu();

            Console.Write("Введіть ID піци для видалення: ");
            if (int.TryParse(Console.ReadLine(), out int pizzaId))
            {
                if (_system.RemovePizzaFromMenu(pizzaId))
                {
                    Console.WriteLine("Піцу успішно видалено з меню!");
                }
                else
                {
                    Console.WriteLine("Піцу з таким ID не знайдено!");
                }
            }
            else
            {
                Console.WriteLine("Невірний ID піци!");
            }

            PauseExecution();
        }

        #endregion

        #region Управління замовленнями

        /// <summary>
        /// Показати меню управління замовленнями
        /// </summary>
        private void ShowOrderMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════ УПРАВЛІННЯ ЗАМОВЛЕННЯМИ ═════════╗");
                Console.WriteLine("║ 1. Створити нове замовлення               ║");
                Console.WriteLine("║ 2. Переглянути всі замовлення             ║");
                Console.WriteLine("║ 3. Знайти замовлення за ID                ║");
                Console.WriteLine("║ 4. Оновити статус замовлення              ║");
                Console.WriteLine("║ 0. Повернутися до головного меню          ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.Write("Оберіть дію (0-4): ");

                var choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        CreateNewOrder();
                        break;
                    case 2:
                        _system.DisplayAllOrders();
                        PauseExecution();
                        break;
                    case 3:
                        FindOrderById();
                        break;
                    case 4:
                        UpdateOrderStatus();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        PauseExecution();
                        break;
                }
            }
        }

        /// <summary>
        /// Створити нове замовлення
        /// </summary>
        private void CreateNewOrder()
        {
            Console.Clear();
            Console.WriteLine("=== СТВОРЕННЯ НОВОГО ЗАМОВЛЕННЯ ===");

            // Знайти або створити клієнта
            Console.Write("Введіть номер телефону клієнта: ");
            var phone = Console.ReadLine();

            var customer = _system.FindCustomerByPhone(phone);
            if (customer == null)
            {
                Console.WriteLine("Клієнта не знайдено. Створюємо нового клієнта.");
                Console.Write("Введіть ім'я: ");
                var name = Console.ReadLine();
                Console.Write("Введіть email: ");
                var email = Console.ReadLine();
                Console.Write("Введіть адресу: ");
                var address = Console.ReadLine();

                try
                {
                    customer = _system.AddCustomer(name, phone, email, address);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Помилка при створенні клієнта: {ex.Message}");
                    PauseExecution();
                    return;
                }
            }

            // Створити замовлення
            var order = _system.CreateOrder(customer);

            // Додати позиції до замовлення
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== ЗАМОВЛЕННЯ #{order.OrderId} ===");
                Console.WriteLine($"Клієнт: {customer.Name}");

                if (order.OrderItems.Count > 0)
                {
                    Console.WriteLine("\nПоточні позиції:");
                    foreach (var item in order.OrderItems)
                    {
                        Console.WriteLine($"- {item}");
                    }
                    Console.WriteLine($"\nПоточна сума: {order.TotalAmount:C} грн.");
                }

                Console.WriteLine("\n1. Додати піцу до замовлення");
                Console.WriteLine("2. Завершити замовлення та оплатити");
                Console.WriteLine("0. Скасувати замовлення");
                Console.Write("Ваш вибір: ");

                var choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        AddPizzaToOrder(order);
                        break;
                    case 2:
                        if (order.OrderItems.Count > 0)
                        {
                            ProcessOrderPayment(order);
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Додайте хоча б одну піцу до замовлення!");
                            PauseExecution();
                        }
                        break;
                    case 0:
                        Console.WriteLine("Замовлення скасовано.");
                        _system.Orders.Remove(order);
                        PauseExecution();
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        PauseExecution();
                        break;
                }
            }
        }

        /// <summary>
        /// Додати піцу до замовлення
        /// </summary>
        /// <param name="order">Замовлення</param>
        private void AddPizzaToOrder(Order order)
        {
            Console.Clear();
            Console.WriteLine("=== ДОДАВАННЯ ПІЦИ ДО ЗАМОВЛЕННЯ ===");

            _system.DisplayMenu();

            Console.Write("Введіть ID піци: ");
            if (int.TryParse(Console.ReadLine(), out int pizzaId))
            {
                var pizza = _system.FindPizzaById(pizzaId);
                if (pizza != null)
                {
                    Console.Write("Введіть кількість: ");
                    if (int.TryParse(Console.ReadLine(), out int quantity) && quantity > 0)
                    {
                        var orderItem = new OrderItem(pizza, quantity);
                        order.AddOrderItem(orderItem);
                        Console.WriteLine($"Додано: {orderItem}");
                    }
                    else
                    {
                        Console.WriteLine("Невірна кількість!");
                    }
                }
                else
                {
                    Console.WriteLine("Піцу з таким ID не знайдено!");
                }
            }
            else
            {
                Console.WriteLine("Невірний ID піци!");
            }

            PauseExecution();
        }

        /// <summary>
        /// Обробити оплату замовлення
        /// </summary>
        /// <param name="order">Замовлення</param>
        private void ProcessOrderPayment(Order order)
        {
            Console.Clear();
            Console.WriteLine("=== ОПЛАТА ЗАМОВЛЕННЯ ===");
            Console.WriteLine($"Сума до сплати: {order.TotalAmount:C} грн.");

            Console.WriteLine("\nОберіть спосіб оплати:");
            Console.WriteLine("1. Готівка");
            Console.WriteLine("2. Картка");
            Console.WriteLine("3. Онлайн оплата");
            Console.Write("Ваш вибір (1-3): ");

            PaymentMethod paymentMethod = PaymentMethod.Cash;
            if (int.TryParse(Console.ReadLine(), out int methodChoice))
            {
                paymentMethod = methodChoice switch
                {
                    1 => PaymentMethod.Cash,
                    2 => PaymentMethod.Card,
                    3 => PaymentMethod.Online,
                    _ => PaymentMethod.Cash
                };
            }

            if (_system.ProcessOrder(order.OrderId, paymentMethod))
            {
                Console.WriteLine("\nЗамовлення успішно оброблено!");
                Console.WriteLine("Доставка автоматично створена.");
            }
            else
            {
                Console.WriteLine("\nПомилка при обробці замовлення!");
            }

            PauseExecution();
        }

        /// <summary>
        /// Знайти замовлення за ID
        /// </summary>
        private void FindOrderById()
        {
            Console.Clear();
            Console.WriteLine("=== ПОШУК ЗАМОВЛЕННЯ ===");

            Console.Write("Введіть ID замовлення: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var order = _system.FindOrderById(orderId);
                if (order != null)
                {
                    Console.WriteLine("\nЗамовлення знайдено:");
                    Console.WriteLine(order);
                }
                else
                {
                    Console.WriteLine("Замовлення з таким ID не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Невірний ID замовлення!");
            }

            PauseExecution();
        }

        /// <summary>
        /// Оновити статус замовлення
        /// </summary>
        private void UpdateOrderStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ОНОВЛЕННЯ СТАТУСУ ЗАМОВЛЕННЯ ===");

            Console.Write("Введіть ID замовлення: ");
            if (int.TryParse(Console.ReadLine(), out int orderId))
            {
                var order = _system.FindOrderById(orderId);
                if (order != null)
                {
                    Console.WriteLine($"Поточний статус: {order.Status}");
                    Console.WriteLine("\nОберіть новий статус:");
                    Console.WriteLine("1. Очікує обробки (Pending)");
                    Console.WriteLine("2. Готується (Cooking)");
                    Console.WriteLine("3. Готове (Ready)");
                    Console.WriteLine("4. Доставлене (Delivered)");
                    Console.Write("Ваш вибір (1-4): ");

                    if (int.TryParse(Console.ReadLine(), out int statusChoice))
                    {
                        OrderStatus newStatus = statusChoice switch
                        {
                            1 => OrderStatus.Pending,
                            2 => OrderStatus.Cooking,
                            3 => OrderStatus.Ready,
                            4 => OrderStatus.Delivered,
                            _ => order.Status
                        };

                        order.UpdateStatus(newStatus);
                        Console.WriteLine("Статус успішно оновлено!");
                    }
                    else
                    {
                        Console.WriteLine("Невірний вибір статусу!");
                    }
                }
                else
                {
                    Console.WriteLine("Замовлення з таким ID не знайдено.");
                }
            }
            else
            {
                Console.WriteLine("Невірний ID замовлення!");
            }

            PauseExecution();
        }

        #endregion

        #region Управління доставкою

        /// <summary>
        /// Показати меню управління доставкою
        /// </summary>
        private void ShowDeliveryMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════ УПРАВЛІННЯ ДОСТАВКОЮ ════════════╗");
                Console.WriteLine("║ 1. Переглянути активні доставки           ║");
                Console.WriteLine("║ 2. Призначити кур'єра                     ║");
                Console.WriteLine("║ 3. Оновити статус доставки                ║");
                Console.WriteLine("║ 0. Повернутися до головного меню          ║");
                Console.WriteLine("╚═══════════════════════════════════════════╝");
                Console.Write("Оберіть дію (0-3): ");

                var choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        _system.DisplayActiveDeliveries();
                        PauseExecution();
                        break;
                    case 2:
                        AssignCourier();
                        break;
                    case 3:
                        UpdateDeliveryStatus();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        PauseExecution();
                        break;
                }
            }
        }

        /// <summary>
        /// Призначити кур'єра для доставки
        /// </summary>
        private void AssignCourier()
        {
            Console.Clear();
            Console.WriteLine("=== ПРИЗНАЧЕННЯ КУР'ЄРА ===");

            _system.DisplayActiveDeliveries();

            Console.Write("Введіть ID доставки: ");
            if (int.TryParse(Console.ReadLine(), out int deliveryId))
            {
                Console.Write("Введіть ім'я кур'єра: ");
                var courierName = Console.ReadLine();

                if (!string.IsNullOrEmpty(courierName))
                {
                    if (_system.AssignCourierToDelivery(deliveryId, courierName))
                    {
                        Console.WriteLine("Кур'єра успішно призначено!");
                    }
                    else
                    {
                        Console.WriteLine("Доставку з таким ID не знайдено!");
                    }
                }
                else
                {
                    Console.WriteLine("Ім'я кур'єра не може бути порожнім!");
                }
            }
            else
            {
                Console.WriteLine("Невірний ID доставки!");
            }

            PauseExecution();
        }

        /// <summary>
        /// Оновити статус доставки
        /// </summary>
        private void UpdateDeliveryStatus()
        {
            Console.Clear();
            Console.WriteLine("=== ОНОВЛЕННЯ СТАТУСУ ДОСТАВКИ ===");

            _system.DisplayActiveDeliveries();

            Console.Write("Введіть ID доставки: ");
            if (int.TryParse(Console.ReadLine(), out int deliveryId))
            {
                Console.WriteLine("Оберіть новий статус:");
                Console.WriteLine("1. Призначено (Assigned)");
                Console.WriteLine("2. В дорозі (InProgress)");
                Console.WriteLine("3. Доставлено (Delivered)");
                Console.Write("Ваш вибір (1-3): ");

                if (int.TryParse(Console.ReadLine(), out int statusChoice))
                {
                    DeliveryStatus newStatus = statusChoice switch
                    {
                        1 => DeliveryStatus.Assigned,
                        2 => DeliveryStatus.InProgress,
                        3 => DeliveryStatus.Delivered,
                        _ => DeliveryStatus.Assigned
                    };

                    if (_system.UpdateDeliveryStatus(deliveryId, newStatus))
                    {
                        Console.WriteLine("Статус доставки успішно оновлено!");
                    }
                    else
                    {
                        Console.WriteLine("Доставку з таким ID не знайдено!");
                    }
                }
                else
                {
                    Console.WriteLine("Невірний вибір статусу!");
                }
            }
            else
            {
                Console.WriteLine("Невірний ID доставки!");
            }

            PauseExecution();
        }

        #endregion

        #region Звіти та статистика

        /// <summary>
        /// Показати меню звітів
        /// </summary>
        private void ShowReportsMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("╔═════════ ЗВІТИ ТА СТАТИСТИКА ═════════╗");
                Console.WriteLine("║ 1. Звіт про продажі                   ║");
                Console.WriteLine("║ 2. Топ популярних піц                 ║");
                Console.WriteLine("║ 3. Статистика системи                 ║");
                Console.WriteLine("║ 0. Повернутися до головного меню      ║");
                Console.WriteLine("╚═══════════════════════════════════════╝");
                Console.Write("Оберіть дію (0-3): ");

                var choice = ReadMenuChoice();
                switch (choice)
                {
                    case 1:
                        ShowSalesReport();
                        break;
                    case 2:
                        ShowPopularPizzasReport();
                        break;
                    case 3:
                        _system.DisplaySystemStats();
                        PauseExecution();
                        break;
                    case 0:
                        return;
                    default:
                        Console.WriteLine("Невірний вибір!");
                        PauseExecution();
                        break;
                }
            }
        }

        /// <summary>
        /// Показати звіт про продажі
        /// </summary>
        private void ShowSalesReport()
        {
            Console.Clear();
            Console.WriteLine("=== ЗВІТ ПРО ПРОДАЖІ ===");

            Console.Write("Введіть початкову дату (дд.мм.рррр): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime startDate))
            {
                Console.Write("Введіть кінцеву дату (дд.мм.рррр): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime endDate))
                {
                    var report = _system.GenerateSalesReport(startDate, endDate);
                    Console.WriteLine(report);
                }
                else
                {
                    Console.WriteLine("Невірний формат кінцевої дати!");
                }
            }
            else
            {
                Console.WriteLine("Невірний формат початкової дати!");
            }

            PauseExecution();
        }

        /// <summary>
        /// Показати звіт про популярні піци
        /// </summary>
        private void ShowPopularPizzasReport()
        {
            Console.Clear();

            var report = _system.GeneratePopularPizzasReport();
            Console.WriteLine(report);

            PauseExecution();
        }

        #endregion

        #region Допоміжні методи

        /// <summary>
        /// Показати статус системи
        /// </summary>
        private void ShowSystemStatus()
        {
            Console.Clear();
            Console.WriteLine("╔═══════════ СТАТУС СИСТЕМИ ═══════════╗");
            _system.DisplaySystemStats();

            Console.WriteLine("\n=== ОСТАННІ ЗАМОВЛЕННЯ ===");
            var recentOrders = _system.Orders.OrderByDescending(o => o.OrderDate).Take(5);
            if (recentOrders.Any())
            {
                foreach (var order in recentOrders)
                {
                    Console.WriteLine($"#{order.OrderId} - {order.Customer.Name} - {order.Status} - {order.TotalAmount:C} грн.");
                }
            }
            else
            {
                Console.WriteLine("Замовлень немає");
            }

            Console.WriteLine("\n=== АКТИВНІ ДОСТАВКИ ===");
            var activeDeliveries = _system.GetActiveDeliveries();
            if (activeDeliveries.Any())
            {
                foreach (var delivery in activeDeliveries.Take(5))
                {
                    Console.WriteLine($"#{delivery.DeliveryId} - Замовлення #{delivery.Order.OrderId} - {delivery.Status} - Кур'єр: {delivery.Courier ?? "Не призначено"}");
                }
            }
            else
            {
                Console.WriteLine("Активних доставок немає");
            }

            Console.WriteLine("╚═══════════════════════════════════════╝");
            PauseExecution();
        }

        /// <summary>
        /// Пауза виконання з очікуванням натискання клавіші
        /// </summary>
        private void PauseExecution()
        {
            Console.WriteLine("\nНатисніть будь-яку клавішу для продовження...");
            Console.ReadKey();
        }

        /// <summary>
        /// Завершити роботу програми
        /// </summary>
        private void ExitApplication()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════╗");
            Console.WriteLine("║         Дякуємо за використання системи!     ║");
            Console.WriteLine("║                  До побачення!               ║");
            Console.WriteLine("╚══════════════════════════════════════════════╝");

            Console.WriteLine("\nСтатистика сесії:");
            Console.WriteLine($"- Оброблено замовлень: {_system.Orders.Count}");
            Console.WriteLine($"- Зареєстровано клієнтів: {_system.Customers.Count}");
            Console.WriteLine($"- Створено доставок: {_system.Deliveries.Count}");

            if (_system.Payments.Any(p => p.Status == PaymentStatus.Completed))
            {
                var totalRevenue = _system.Payments.Where(p => p.Status == PaymentStatus.Completed).Sum(p => p.Amount);
                Console.WriteLine($"- Загальний дохід: {totalRevenue:C} грн.");
            }

            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
            _isRunning = false;
        }

        #endregion
    }
}