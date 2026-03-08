namespace Game.World;

using Game.IItems;
public class Room
{
    public const int Height = 20;
    public const int Width = 40;
    private readonly Tile[,] _grid;
    
    public Room()
    {
        _grid = new Tile[Height, Width];
        Initialize();
    }

    public void PlaceWall(int x, int y)
    {
        _grid[y, x] = Tile.CreateWall();
    }

    private void Initialize()
    {
        for (int y = 0; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                _grid[y, x] = Tile.CreateFloor();
            }
        }
    }

    public bool IsInside(int x, int y)
    {
        return x >= 0 && x < Width &&
               y >= 0 && y < Height;
    }

    public bool IsWalkable(int x, int y)
    {
        return IsInside(x, y) &&
               !_grid[y, x].IsBlocked;
    }

    public Tile GetTile(int x, int y)
    {
        if (!IsInside(x, y))
            throw new ArgumentOutOfRangeException();

        return _grid[y, x];
    }

    public void PlaceItem(int x, int y, IItem item)
    {
        GetTile(x, y).AddItem(item);
    }

}