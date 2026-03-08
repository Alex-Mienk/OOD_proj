namespace Game;

using Game.Entities;
using Game.IItems;
using Game.Rendering;

using Game.World;
using System;


/*

TODO:
- Inventory management (pickup, drop, equip, unequip) - still not fully done yet
- Weapons addition
- Spawning items in the world

*/

internal static class Program
{
    private static readonly Random Rng = new();

    private static (int X, int Y) RandomPosition(int width, int height)
    {
        int x = Rng.Next(0, width);
        int y = Rng.Next(0, height);
        return (x, y);
    }

    private static void Main()
    {
        Console.CursorVisible = false;

        // The renderer prints ~113 columns (40 grid + gap + sidebar).
        var room = new Room();

        var player = new Player(
            symbol: '@',
            health: 100,
            strength: 5,
            dexterity: 5,
            luck: 5,
            aggression: 5,
            wisdom: 5);


        // Spawn some items in the world for testing

        for (int i = 0; i < 5; i++)
        {
            var goldPos = RandomPosition(Room.Width, Room.Height);
            room.PlaceItem(goldPos.X, goldPos.Y, new Gold(10));   
        }

        for (int i = 0; i < 5; i++)
        {
            var coinpos = RandomPosition(Room.Width, Room.Height);
            room.PlaceItem(coinpos.X, coinpos.Y, new Coin(1));
        }

        for(int i = 0; i < 3; i++)
        {
            var swordPos = RandomPosition(Room.Width, Room.Height);
            room.PlaceItem(swordPos.X, swordPos.Y, new Sword());
        }
        
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
                    player.TryMove(0, -1, room);
                    break;

                case ConsoleKey.S:
                    player.TryMove(0, 1, room);
                    break;

                case ConsoleKey.A:
                    player.TryMove(-1, 0, room);
                    break;

                case ConsoleKey.D:
                    player.TryMove(1, 0, room);
                    break;
                
                case ConsoleKey.R:
                    player.TryDrop(room);
                    break;

                case ConsoleKey.X:
                    player.TryDropRightHand(room);
                    break;

                case ConsoleKey.Z:
                    player.TryDropLeftHand(room);
                    break;
                    
                case ConsoleKey.E:
                    player.TryPickUp(room);
                    break;      
            }
        }

        Console.Clear();
        Console.CursorVisible = true;
    }
}