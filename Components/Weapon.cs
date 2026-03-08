namespace Game.Components;

using Game.Entities;
using Game.IItems;
// TODO


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

    public void DropFromLeft(Player player)
    {
        player.DropFromLeft(this);
    }

    public void DropFromRight(Player player)
    {
        player.DropFromRight(this);
    }

}

public class DoubleSword : IWeapon
{
    public char Symbol => 'D';
    public string Name => "Double Sword";
    public int Damage => 7;
    public bool IsTwoHanded => true;
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