namespace Game.World;

using Game.IItems;
public class Tile
{
    private readonly List<IItem> _items = new();

    public bool IsBlocked { get; }
    public char Symbol { get; }

    public IReadOnlyList<IItem> Items => _items;

    public Tile(bool isBlocked, char symbol)
    {
        IsBlocked = isBlocked;
        Symbol = symbol;
    }

    public static Tile CreateFloor()
        => new Tile(isBlocked: false, symbol: ' ');

    public static Tile CreateWall()
        => new Tile(isBlocked: true, symbol: '#');

    public bool CanEnter()
        => !IsBlocked;

    public bool HasItems()
        => _items.Count > 0;

    public void AddItem(IItem item)
    {
        if (IsBlocked)
            throw new InvalidOperationException("Cannot place items on a wall tile.");

        _items.Add(item);
    }

    public void RemoveItem(IItem item)
    {
        _items.Remove(item);
    }

    public IItem? TakeTopItem()
    {
        if (_items.Count == 0)
            return null;

        var item = _items[^1];
        _items.RemoveAt(_items.Count - 1);
        return item;
    }
}