using Game.Entities;

namespace Game.IItems;

public interface IItem
{
    char Symbol { get; }
    string Name { get; }

    bool PickUp(Player player);
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
}


public class Sword : IWeapon
{
    public char Symbol => '!';
    public string Name => "Sword";
    public int Damage => 5;
    public bool IsTwoHanded => false;
    public bool PickUp(Player player)
    {
        return player.PickUp(this);
    }

}