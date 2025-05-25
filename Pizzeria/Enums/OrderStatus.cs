namespace PizzeriaSystem.Enums
{
    /// <summary>
    /// Енумерація статусів замовлення
    /// </summary>
    public enum OrderStatus
    {
        Pending,    // Очікує обробки
        Cooking,    // Готується
        Ready,      // Готове
        Delivered   // Доставлене
    }
}