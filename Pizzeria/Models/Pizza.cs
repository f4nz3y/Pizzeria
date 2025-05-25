using System;
using System.Collections.Generic;
using System.Linq;
using PizzeriaSystem.Enums;

namespace PizzeriaSystem.Models
{
    /// <summary>
    /// Клас, що представляє піцу в меню
    /// </summary>
    public class Pizza
    {
        /// <summary>
        /// Унікальний ідентифікатор піци
        /// </summary>
        public int PizzaId { get; set; }

        /// <summary>
        /// Назва піци
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Список інгредієнтів піци
        /// </summary>
        public List<string> Ingredients { get; set; }

        /// <summary>
        /// Розмір піци
        /// </summary>
        public PizzaSize Size { get; set; }

        /// <summary>
        /// Базова ціна піци
        /// </summary>
        public double BasePrice { get; set; }

        /// <summary>
        /// Час приготування в хвилинах
        /// </summary>
        public int CookingTime { get; set; }

        /// <summary>
        /// Конструктор класу Pizza
        /// </summary>
        /// <param name="pizzaId">Ідентифікатор піци</param>
        /// <param name="name">Назва піци</param>
        /// <param name="ingredients">Список інгредієнтів</param>
        /// <param name="size">Розмір піци</param>
        /// <param name="basePrice">Базова ціна</param>
        /// <param name="cookingTime">Час приготування</param>
        public Pizza(int pizzaId, string name, List<string> ingredients, PizzaSize size, double basePrice, int cookingTime)
        {
            PizzaId = pizzaId;
            Name = name ?? throw new ArgumentException("Назва піци не може бути порожньою");
            Ingredients = ingredients ?? new List<string>();
            Size = size;
            BasePrice = basePrice > 0 ? basePrice : throw new ArgumentException("Ціна повинна бути більше 0");
            CookingTime = cookingTime > 0 ? cookingTime : throw new ArgumentException("Час приготування повинен бути більше 0");
        }

        /// <summary>
        /// Розрахувати ціну піци залежно від розміру
        /// </summary>
        /// <returns>Фінальна ціна піци</returns>
        public double CalculatePrice()
        {
            double multiplier = Size switch
            {
                PizzaSize.Small => 0.8,   // Мала піца - 80% від базової ціни
                PizzaSize.Medium => 1.0,  // Середня піца - базова ціна
                PizzaSize.Large => 1.3,   // Велика піца - 130% від базової ціни
                _ => 1.0
            };
            return BasePrice * multiplier;
        }

        /// <summary>
        /// Додати інгредієнт до піци
        /// </summary>
        /// <param name="ingredient">Назва інгредієнта</param>
        public void AddIngredient(string ingredient)
        {
            if (!string.IsNullOrEmpty(ingredient) && !Ingredients.Contains(ingredient))
            {
                Ingredients.Add(ingredient);
            }
        }

        /// <summary>
        /// Видалити інгредієнт з піци
        /// </summary>
        /// <param name="ingredient">Назва інгредієнта</param>
        public void RemoveIngredient(string ingredient)
        {
            if (!string.IsNullOrEmpty(ingredient))
            {
                Ingredients.Remove(ingredient);
            }
        }

        /// <summary>
        /// Отримати рядкове представлення піци
        /// </summary>
        /// <returns>Рядок з описом піци</returns>
        public override string ToString()
        {
            var ingredientsList = string.Join(", ", Ingredients);
            return $"{Name} ({Size}) - {CalculatePrice():C} грн. Інгредієнти: {ingredientsList}";
        }
    }
}