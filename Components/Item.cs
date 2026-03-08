using Game.Entities;

namespace Game.IItems;

public interface IItem
{
    char Symbol { get; }
    string Name { get; }

    bool PickUp(Player player);
    void DropFromLeft(Player player);
    void DropFromRight(Player player);
}

public interface IWeapon : IItem
{
    int Damage { get; }
    bool IsTwoHanded { get; }
}


public class Gold : IItem
{
    public char Symbol => '$';
    public string Name => "Gold";
    public int Amount { get; }

    public Gold(int amount)
    {
        Amount = amount;
    }
    
    public bool PickUp(Player player)
    {
        return player.PickUp(this);
    }

    public void DropFromLeft(Player player)
    {
        player.DropFromLeft(this);
    }

    public void DropFromRight(Player player)
    {
        player.DropFromRight(this);
    }
}

public class Coin : IItem
{
    public char Symbol => 'C';
    public string Name => "Coin";
    public int Amount { get; }

    public Coin(int amount)
    {
        Amount = amount;
    }

    public bool PickUp(Player player)
    {
        return player.PickUp(this);
    }

    public void DropFromLeft(Player player)
    {
        player.DropFromLeft(this);
    }

    public void DropFromRight(Player player)
    {
        player.DropFromRight(this);
    }
}


