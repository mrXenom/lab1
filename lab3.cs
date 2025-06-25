using System;

public interface IDamageable
{
    void TakeDamage(int damage);
    bool IsDestroyed { get; }
}

public abstract class Projectile
{
    protected int damage;

    public Projectile(int damage)
    {
        this.damage = damage;
    }

    public abstract void HitTarget(IDamageable target);
}

public class Bullet : Projectile
{
    public Bullet(int damage) : base(damage)
    {
    }

    public override void HitTarget(IDamageable target)
    {
        Console.WriteLine($"Bullet hits target and deals {damage} damage!");
        target.TakeDamage(damage);
    }
}

public class Enemy : IDamageable
{
    private int health;
    private string name;

    public Enemy(string name, int health)
    {
        this.name = name;
        this.health = health;
        Console.WriteLine($"Enemy '{name}' spawned with {health} health.");
    }

    public bool IsDestroyed => health <= 0;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;

        Console.WriteLine($"Enemy '{name}' takes {damage} damage. Health: {health}");
        
        if (IsDestroyed)
        {
            Console.WriteLine($"Enemy '{name}' has been defeated!");
        }
    }
}

public class BreakableWall : IDamageable
{
    private int durability;
    private string location;

    public BreakableWall(string location, int durability)
    {
        this.location = location;
        this.durability = durability;
        Console.WriteLine($"Breakable wall at '{location}' constructed with {durability} durability.");
    }

    public bool IsDestroyed => durability <= 0;

    public void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability < 0) durability = 0;

        Console.WriteLine($"Wall at '{location}' takes {damage} damage. Durability: {durability}");
        
        if (IsDestroyed)
        {
            Console.WriteLine($"Wall at '{location}' has been destroyed!");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Console.WriteLine("=== Damage System Demonstration ===\n");

        Enemy orc = new Enemy("Orc Warrior", 50);
        BreakableWall wall = new BreakableWall("Main Gate", 30);
        
        Console.WriteLine();

        Bullet lightBullet = new Bullet(15);
        Bullet heavyBullet = new Bullet(25);
        
        Console.WriteLine("=== Combat Simulation ===");

        Console.WriteLine("\n--- Light bullet attacks ---");
        lightBullet.HitTarget(orc);
        lightBullet.HitTarget(wall);
        
        Console.WriteLine("\n--- Heavy bullet attacks ---");
        heavyBullet.HitTarget(orc);
        heavyBullet.HitTarget(wall);

        Console.WriteLine("\n--- Final light bullet attack ---");
        lightBullet.HitTarget(orc);

        Console.WriteLine("\n=== Battle Results ===");
        Console.WriteLine($"Enemy status: {(orc.IsDestroyed ? "Defeated" : "Still alive")}");
        Console.WriteLine($"Wall status: {(wall.IsDestroyed ? "Destroyed" : "Still standing")}");
    }
}