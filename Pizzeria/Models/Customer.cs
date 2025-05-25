using System;

namespace PizzeriaSystem.Models
{
    /// <summary>
    /// Клас, що представляє клієнта піцерії
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Унікальний ідентифікатор клієнта
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// Ім'я клієнта
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефону клієнта
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Електронна пошта клієнта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Адреса клієнта для доставки
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Конструктор класу Customer
        /// </summary>
        /// <param name="customerId">Ідентифікатор клієнта</param>
        /// <param name="name">Ім'я клієнта</param>
        /// <param name="phone">Телефон клієнта</param>
        /// <param name="email">Email клієнта</param>
        /// <param name="address">Адреса клієнта</param>
        public Customer(int customerId, string name, string phone, string email, string address)
        {
            CustomerId = customerId;
            Name = name ?? throw new ArgumentException("Ім'я не може бути порожнім");
            Phone = phone ?? throw new ArgumentException("Телефон не може бути порожнім");
            Email = email ?? throw new ArgumentException("Email не може бути порожнім");
            Address = address ?? throw new ArgumentException("Адреса не може бути порожньою");
        }

        /// <summary>
        /// Оновити інформацію про клієнта
        /// </summary>
        /// <param name="name">Нове ім'я</param>
        /// <param name="phone">Новий телефон</param>
        /// <param name="email">Новий email</param>
        /// <param name="address">Нова адреса</param>
        public void UpdateCustomer(string name, string phone, string email, string address)
        {
            if (!string.IsNullOrEmpty(name)) Name = name;
            if (!string.IsNullOrEmpty(phone)) Phone = phone;
            if (!string.IsNullOrEmpty(email)) Email = email;
            if (!string.IsNullOrEmpty(address)) Address = address;
        }

        /// <summary>
        /// Отримати інформацію про клієнта у вигляді рядка
        /// </summary>
        /// <returns>Рядок з інформацією про клієнта</returns>
        public string GetCustomerInfo()
        {
            return $"ID: {CustomerId}, Ім'я: {Name}, Телефон: {Phone}, Email: {Email}, Адреса: {Address}";
        }

        /// <summary>
        /// Перевизначений метод ToString
        /// </summary>
        /// <returns>Рядкове представлення клієнта</returns>
        public override string ToString()
        {
            return $"{Name} ({Phone})";
        }
    }
}