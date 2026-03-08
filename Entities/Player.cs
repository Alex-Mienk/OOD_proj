namespace Game.Entities;

using Game.Components;
using Game.World;
using Game.IItems;

public class Player
{
    public Player(
        char symbol,
        int health,
        int strength = 0,
        int dexterity = 0,
        int luck = 0,
        int aggression = 0,
        int wisdom = 0)
    {
        Symbol = symbol;
        Health = health;
        Strength = strength;
        Dexterity = dexterity;
        Luck = luck;
        Aggression = aggression;
        Wisdom = wisdom;
        X = 0;
        Y = 0;
    }

    public char Symbol { get; }

    public int X { get; private set; }
    public int Y { get; private set; }

    public int Health { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Luck { get; private set; }
    public int Aggression { get; private set; }
    public int Wisdom { get; private set; }
    public IItem? LeftHand { get; private set; }
    public IItem? RightHand { get; private set; }

    private readonly List<IItem> _inventory = new();
    public IReadOnlyList<IItem> Inventory => _inventory;

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
    }

    public bool PickUp(IItem item)
    {
        _inventory.Add(item);
        return true;
    }

    public bool PickUp(Sword weapon)
    {
        if (LeftHand == null)
        {
            LeftHand = weapon;
            return true;
        }

        if (RightHand == null)
        {
            RightHand = weapon;
            return true;
        }
        return false;
    }

    public bool PickUp(DoubleSword weapon)
    {
        if (LeftHand == null && RightHand == null)
        {
            LeftHand = weapon;
            RightHand = weapon;
            return true;
        }
        return false;
    }

    public void DropFromLeft(IItem item)
    {
    }

    public void DropFromLeft(DoubleSword weapon)
    {
        RightHand = null;
    }

    public void DropFromRight(IItem item)
    {
    }

    public void DropFromRight(DoubleSword weapon)
    {
        LeftHand = null;
    }

    public bool TryMove(int dx, int dy, Room room)
    {
        int newX = X + dx;
        int newY = Y + dy;

        if (!room.IsWalkable(newX, newY))
            return false;

        X = newX;
        Y = newY;
        return true;
    }

    public bool TryPickUp(Room room)
    {
        var tile = room.GetTile(X, Y);

        if (tile.IsBlocked)
            return false;

        var item = tile.TakeTopItem();
        if (item is null)
            return false;

        bool pickedUp = item.PickUp(this);
        if (pickedUp)
            return true;

        room.PlaceItem(X, Y, item);
        return false;
    }

    public bool TryDrop(Room room)
    {
        if (_inventory.Count == 0)
            return false;

        var item = _inventory[^1];
        _inventory.RemoveAt(_inventory.Count - 1);

        room.PlaceItem(X, Y, item);
        return true;
    }

    public bool TryDropLeftHand(Room room)
    {
        if (LeftHand == null)
            return false;

        var item = LeftHand;
        LeftHand = null;
        item.DropFromLeft(this);
        room.PlaceItem(X, Y, item);
        return true;
    }

    public bool TryDropRightHand(Room room)
    {
        if (RightHand == null)
            return false;

        var item = RightHand;
        RightHand = null;
        item.DropFromRight(this);
        room.PlaceItem(X, Y, item);
        return true;
    }
}