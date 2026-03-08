namespace Game.Entities;

using Game.Components;
using Game.World;
using Game.IItems;

public class Player
{
    public Player(
        char name,
        int health,
        int strength = 0,
        int dexterity = 0,
        int luck = 0,
        int aggression = 0,
        int wisdom = 0)
    {
        Name = name;
        Health = health;
        Strength = strength;
        Dexterity = dexterity;
        Luck = luck;
        Aggression = aggression;
        Wisdom = wisdom;
        X = 0;
        Y = 0;
    }

    public char Name { get; }

    public int X { get; private set; }
    public int Y { get; private set; }

    public int Health { get; private set; }
    public int Strength { get; private set; }
    public int Dexterity { get; private set; }
    public int Luck { get; private set; }
    public int Aggression { get; private set; }
    public int Wisdom { get; private set; }

    // Presumably only for the weapons, ask on the lab!!!
    public IItem? LeftHand { get; private set; }
    public IItem? RightHand { get; private set; }

    private readonly List<IItem> _inventory = new(); // Private - only the player can modify
    public IReadOnlyList<IItem> Inventory => _inventory; // Public read-only for the renderer to have access

    public void EquipLeft(IItem? item) => LeftHand = item;
    public void EquipRight(IItem? item) => RightHand = item;
    public void UnequipLeft() => LeftHand = null;
    public void UnequipRight() => RightHand = null;

    public void SetPosition(int x, int y)
    {
        X = x;
        Y = y;
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

        _inventory.Add(item);
        return true;
    }
}