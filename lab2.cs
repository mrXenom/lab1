using System;
using System.Collections.Generic;

public class User
{
    public string UserName { get; set; }
    public string Email { get; set; }
    private string _password;
    
    public User(string userName, string email)
    {
        UserName = userName;
        Email = email;
    }
    
    public void SetPassword(string newPassword)
    {
        _password = newPassword;
    }
    
    public bool Authenticate(string inputPassword)
    {
        return _password == inputPassword;
    }
    
    public virtual void DisplayInfo()
    {
        Console.WriteLine($"Ім'я: {UserName} | Email: {Email}");
    }
}

public class Admin : User
{
    public Admin(string userName, string email) : base(userName, email)
    {
    }
    
    public void BlockUser(User user)
    {
        Console.WriteLine($"Користувача {user.UserName} заблоковано.");
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"Ім'я: {UserName} | Email: {Email} | Роль: Адміністратор");
    }
}

public class Moderator : User
{
    public Moderator(string userName, string email) : base(userName, email)
    {
    }
    
    public void ModerateContent()
    {
        Console.WriteLine("Контент модеровано.");
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"Ім'я: {UserName} | Email: {Email} | Роль: Модератор");
    }
}

public class RegularUser : User
{
    public RegularUser(string userName, string email) : base(userName, email)
    {
    }
    
    public void PostComment()
    {
        Console.WriteLine("Коментар опубліковано.");
    }
    
    public override void DisplayInfo()
    {
        Console.WriteLine($"Ім'я: {UserName} | Email: {Email} | Роль: Звичайний користувач");
    }
}

public class Program
{
    public static void Main()
    {
        List<User> users = new List<User>();
        
        Admin admin = new Admin("AdminUser", "admin@example.com");
        admin.SetPassword("adminpass123");
        
        Moderator moderator = new Moderator("ModUser", "mod@example.com");
        moderator.SetPassword("modpass123");
        
        RegularUser regularUser = new RegularUser("RegUser", "user@example.com");
        regularUser.SetPassword("userpass123");
        
        users.Add(admin);
        users.Add(moderator);
        users.Add(regularUser);
        
        Console.WriteLine("=== Інформація про користувачів ===");
        foreach (User user in users)
        {
            user.DisplayInfo();
        }
        
        Console.WriteLine("\n=== Тестування методів ===");
        
        foreach (User user in users)
        {
            if (user is Admin adminUser)
            {
                adminUser.BlockUser(regularUser);
            }
            else if (user is Moderator modUser)
            {
                modUser.ModerateContent();
            }
            else if (user is RegularUser regUser)
            {
                regUser.PostComment();
            }
        }
        
        Console.WriteLine("\n=== Перевірка аутентифікації ===");
        
        TestAuthentication(admin, "adminpass123");
        TestAuthentication(moderator, "wrongpass");
        TestAuthentication(regularUser, "userpass123");
    }
    
    private static void TestAuthentication(User user, string password)
    {
        bool isAuthenticated = user.Authenticate(password);
        string result = isAuthenticated ? "Успішна аутентифікація" : "Невірний пароль";
        Console.WriteLine($"{user.UserName}: {result}");
    }
}