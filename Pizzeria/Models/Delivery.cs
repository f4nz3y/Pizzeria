using System;
using PizzeriaSystem.Enums;

namespace PizzeriaSystem.Models
{
    /// <summary>
    /// Клас для управління доставкою замовлень
    /// </summary>
    public class Delivery
    {
        /// <summary>
        /// Унікальний ідентифікатор доставки
        /// </summary>
        public int DeliveryId { get; set; }

        /// <summary>
        /// Замовлення для доставки
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Ім'я кур'єра
        /// </summary>
        public string Courier { get; set; }

        /// <summary>
        /// Адреса доставки
        /// </summary>
        public string DeliveryAddress { get; set; }

        /// <summary>
        /// Очікуваний час доставки в хвилинах
        /// </summary>
        public int EstimatedTime { get; set; }

        /// <summary>
        /// Фактичний час доставки в хвилинах
        /// </summary>
        public int ActualTime { get; set; }

        /// <summary>
        /// Статус доставки
        /// </summary>
        public DeliveryStatus Status { get; set; }

        /// <summary>
        /// Час створення доставки
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Конструктор класу Delivery
        /// </summary>
        /// <param name="deliveryId">Ідентифікатор доставки</param>
        /// <param name="order">Замовлення</param>
        public Delivery(int deliveryId, Order order)
        {
            DeliveryId = deliveryId;
            Order = order ?? throw new ArgumentException("Замовлення не може бути null");
            DeliveryAddress = order.Customer.Address;
            EstimatedTime = CalculateDeliveryTime();
            ActualTime = 0;
            Status = DeliveryStatus.Assigned;
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// Призначити кур'єра для доставки
        /// </summary>
        /// <param name="courierName">Ім'я кур'єра</param>
        public void AssignCourier(string courierName)
        {
            if (string.IsNullOrEmpty(courierName))
                throw new ArgumentException("Ім'я кур'єра не може бути порожнім");

            Courier = courierName;
            Status = DeliveryStatus.Assigned;
        }

        /// <summary>
        /// Оновити статус доставки
        /// </summary>
        /// <param name="newStatus">Новий статус</param>
        public void UpdateDeliveryStatus(DeliveryStatus newStatus)
        {
            Status = newStatus;

            // Якщо доставка завершена, фіксуємо фактичний час
            if (newStatus == DeliveryStatus.Delivered)
            {
                ActualTime = (int)(DateTime.Now - CreatedAt).TotalMinutes;
            }
        }

        /// <summary>
        /// Розрахувати очікуваний час доставки
        /// </summary>
        /// <returns>Час доставки в хвилинах</returns>
        public int CalculateDeliveryTime()
        {
            // Базовий час доставки + час приготування найдовшої піци
            int baseDeliveryTime = 20; // 20 хвилин базовий час доставки
            int maxCookingTime = 0;

            foreach (var item in Order.OrderItems)
            {
                if (item.Pizza.CookingTime > maxCookingTime)
                {
                    maxCookingTime = item.Pizza.CookingTime;
                }
            }

            return baseDeliveryTime + maxCookingTime;
        }

        /// <summary>
        /// Отримати статус доставки українською мовою
        /// </summary>  
        /// <param name="status">Статус доставки</param>
        /// <returns>Статус українською</returns>
        private string GetStatusInUkrainian(DeliveryStatus status)
        {
            return status switch
            {
                DeliveryStatus.Assigned => "Призначено",
                DeliveryStatus.InProgress => "В дорозі",
                DeliveryStatus.Delivered => "Доставлено",
                _ => status.ToString()
            };
        }

        /// <summary>
        /// Рядкове представлення доставки
        /// </summary>
        /// <returns>Опис доставки</returns>
        public override string ToString()
        {
            return $"Доставка #{DeliveryId}: {DeliveryAddress} (Кур'єр: {Courier ?? "Не призначено"}) - {GetStatusInUkrainian(Status)}";
        }
    }
}