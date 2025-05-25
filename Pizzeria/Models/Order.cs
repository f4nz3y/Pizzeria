using System;
using System.Collections.Generic;
using System.Linq;
using PizzeriaSystem.Enums;

namespace PizzeriaSystem.Models
{
    /// <summary>
    /// Клас, що представляє замовлення клієнта
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Унікальний ідентифікатор замовлення
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// Клієнт, який зробив замовлення
        /// </summary>
        public Customer Customer { get; set; }

        /// <summary>
        /// Список позицій замовлення
        /// </summary>
        public List<OrderItem> OrderItems { get; set; }

        /// <summary>
        /// Дата та час створення замовлення
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Статус замовлення
        /// </summary>
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Загальна сума замовлення
        /// </summary>
        public double TotalAmount => CalculateTotal();

        /// <summary>
        /// Конструктор класу Order
        /// </summary>
        /// <param name="orderId">Ідентифікатор замовлення</param>
        /// <param name="customer">Клієнт</param>
        public Order(int orderId, Customer customer)
        {
            OrderId = orderId;
            Customer = customer ?? throw new ArgumentException("Клієнт не може бути null");
            OrderItems = new List<OrderItem>();
            OrderDate = DateTime.Now;
            Status = OrderStatus.Pending;
        }

        /// <summary>
        /// Додати позицію до замовлення
        /// </summary>
        /// <param name="item">Позиція замовлення</param>
        public void AddOrderItem(OrderItem item)
        {
            if (item == null)
                throw new ArgumentException("Позиція замовлення не може бути null");

            // Перевіряємо, чи вже є така піца в замовленні
            var existingItem = OrderItems.FirstOrDefault(oi => oi.Pizza.PizzaId == item.Pizza.PizzaId && oi.Pizza.Size == item.Pizza.Size);

            if (existingItem != null)
            {
                // Якщо піца вже є, збільшуємо кількість
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                // Якщо піци немає, додаємо нову позицію
                OrderItems.Add(item);
            }
        }

        /// <summary>
        /// Видалити позицію з замовлення
        /// </summary>
        /// <param name="item">Позиція для видалення</param>
        public void RemoveOrderItem(OrderItem item)
        {
            if (item != null)
            {
                OrderItems.Remove(item);
            }
        }

        /// <summary>
        /// Розрахувати загальну суму замовлення
        /// </summary>
        /// <returns>Загальна сума</returns>
        public double CalculateTotal()
        {
            return OrderItems.Sum(item => item.CalculateSubtotal());
        }

        /// <summary>
        /// Оновити статус замовлення
        /// </summary>
        /// <param name="newStatus">Новий статус</param>
        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Console.WriteLine($"Статус замовлення #{OrderId} змінено на: {GetStatusInUkrainian(newStatus)}");
        }

        /// <summary>
        /// Отримати статус українською мовою
        /// </summary>
        /// <param name="status">Статус</param>
        /// <returns>Статус українською</returns>
        private string GetStatusInUkrainian(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Pending => "Очікує обробки",
                OrderStatus.Cooking => "Готується",
                OrderStatus.Ready => "Готове",
                OrderStatus.Delivered => "Доставлене",
                _ => status.ToString()
            };
        }

        /// <summary>
        /// Рядкове представлення замовлення
        /// </summary>
        /// <returns>Опис замовлення</returns>
        public override string ToString()
        {
            var itemsDescription = string.Join("\n", OrderItems.Select(item => $"  - {item}"));
            return $"Замовлення #{OrderId}\nКлієнт: {Customer.Name}\nДата: {OrderDate:dd.MM.yyyy HH:mm}\nСтатус: {GetStatusInUkrainian(Status)}\nПозиції:\n{itemsDescription}\nЗагалом: {TotalAmount:C} грн.";
        }
    }
}