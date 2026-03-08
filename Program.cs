namespace Game;

using Game.Entities;
using Game.Rendering;
using Game.World;


/*

TODO:
- Inventory management (pickup, drop, equip, unequip) - still not fully done yet
- Weapons addition
- Spawning items in the worlds

*/

internal static class Program
{
    private static void Main()
    {
        // Smoother rendering
        Console.CursorVisible = false;

        // The renderer prints ~113 columns (40 grid + gap + sidebar).
        var room = new Room();

        var player = new Player(
            name: '@',
            health: 100,
            strength: 5,
            dexterity: 5,
            luck: 5,
            aggression: 5,
            wisdom: 5);

        player.SetPosition(0, 0);

        bool running = true;

        while (running)
        {
            Render.Draw(room, player);

            ConsoleKey key = Console.ReadKey(intercept: true).Key;

            switch (key)
            {
                case ConsoleKey.Q:
                case ConsoleKey.Escape:
                    running = false;
                    break;

                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    player.TryMove(0, -1, room);
                    break;

                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    player.TryMove(0, 1, room);
                    break;

                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    player.TryMove(-1, 0, room);
                    break;

                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    player.TryMove(1, 0, room);
                    break;

                // Placeholder for pickup
                case ConsoleKey.E:
                    // room.GetTile(player.X, player.Y).TakeTopItem()
                    break;
            }
        }

        Console.Clear();
        Console.CursorVisible = true;
    }
}