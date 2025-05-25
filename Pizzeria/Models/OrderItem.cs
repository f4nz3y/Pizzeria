using System;

namespace PizzeriaSystem.Models
{
    /// <summary>
    /// Клас, що представляє окрему позицію в замовленні
    /// </summary>
    public class OrderItem
    {
        /// <summary>
        /// Піца в замовленні
        /// </summary>
        public Pizza Pizza { get; set; }

        /// <summary>
        /// Кількість піц цього типу
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Підсумок за цю позицію
        /// </summary>
        public double Subtotal => CalculateSubtotal();

        /// <summary>
        /// Конструктор класу OrderItem
        /// </summary>
        /// <param name="pizza">Піца</param>
        /// <param name="quantity">Кількість</param>
        public OrderItem(Pizza pizza, int quantity)
        {
            Pizza = pizza ?? throw new ArgumentException("Піца не може бути null");
            Quantity = quantity > 0 ? quantity : throw new ArgumentException("Кількість повинна бути більше 0");
        }

        /// <summary>
        /// Розрахувати підсумок за позицію
        /// </summary>
        /// <returns>Сума за позицію</returns>
        public double CalculateSubtotal()
        {
            return Pizza.CalculatePrice() * Quantity;
        }

        /// <summary>
        /// Рядкове представлення позиції замовлення
        /// </summary>
        /// <returns>Опис позиції</returns>
        public override string ToString()
        {
            return $"{Pizza.Name} x{Quantity} = {Subtotal:C} грн.";
        }
    }
}