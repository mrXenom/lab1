using System;

public class Product
{
    private string _name;
    private decimal _price;
    private int _quantity;
    private DateTime _lastUpdated;

    public Product(string name, decimal price, int quantity)
    {
        Name = name;
        Price = price;
        _quantity = Math.Max(0, quantity);
        _lastUpdated = DateTime.Now;
    }

    public string Name
    {
        get { return _name; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Назва товару не може бути порожньою!");
            }
            _name = value;
            _lastUpdated = DateTime.Now;
        }
    }

    public decimal Price
    {
        get { return _price; }
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Ціна не може бути від'ємною!");
            }
            _price = value;
            _lastUpdated = DateTime.Now;
        }
    }

    public int Quantity
    {
        get { return _quantity; }
    }

    public decimal TotalValue
    {
        get { return _price * _quantity; }
    }

    public DateTime LastUpdated
    {
        get { return _lastUpdated; }
    }

    public void Restock(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Кількість для поповнення повинна бути додатною!");
        }
        _quantity += amount;
        _lastUpdated = DateTime.Now;
        Console.WriteLine($"Склад поповнено на {amount} одиниць.");
    }

    public void Sell(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Кількість для продажу повинна бути додатною!");
        }
        
        if (amount > _quantity)
        {
            Console.WriteLine("Недостатньо товару на складі!");
            return;
        }
        
        _quantity -= amount;
        _lastUpdated = DateTime.Now;
        Console.WriteLine($"Продано {amount} одиниць товару.");
    }

    public string GetInfo()
    {
        return $"Товар: {_name}, Ціна: {_price} грн, Кількість: {_quantity}, Загальна вартість: {TotalValue} грн";
    }

    public string GetDetailedInfo()
    {
        return GetInfo() + $", Останнє оновлення: {_lastUpdated:dd.MM.yyyy HH:mm:ss}";
    }
}

public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("=== Система управління товарами на складі ===\n");

        try
        {
            Product apple = new Product("Яблуко", 5, 100);
            Console.WriteLine(apple.GetInfo());

            apple.Sell(20);
            Console.WriteLine(apple.GetInfo());

            apple.Restock(50);
            Console.WriteLine(apple.GetInfo());

            apple.Price = 7;
            Console.WriteLine(apple.GetInfo());

            apple.Name = "Зелене яблуко";
            Console.WriteLine(apple.GetInfo());

            Console.WriteLine("\n=== Тестування помилкових ситуацій ===");

            try
            {
                apple.Price = -10;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            try
            {
                apple.Name = "";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }

            apple.Sell(200);

            Console.WriteLine("\n=== Додаткове завдання - відстеження дати оновлення ===");
            Console.WriteLine(apple.GetDetailedInfo());

            Console.WriteLine("\n=== Демонстрація роботи з кількома товарами ===");
            
            Product bread = new Product("Хліб", 15, 50);
            Product milk = new Product("Молоко", 25, 30);
            
            Console.WriteLine(bread.GetInfo());
            Console.WriteLine(milk.GetInfo());
            
            Console.WriteLine($"\nЗагальна вартість всіх товарів: {apple.TotalValue + bread.TotalValue + milk.TotalValue} грн");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непередбачена помилка: {ex.Message}");
        }

    }