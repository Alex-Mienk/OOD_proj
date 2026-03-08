namespace Game.Rendering;

using System.Text;
using Game.Components;
using Game.Entities;
using Game.World;
using Game.IItems;

public static class Render
{
    private static bool _initialized;

    public static void Draw(Room room, Player player)
    {
        if (!_initialized)
        {
            Console.CursorVisible = false;
            Console.Clear();
            _initialized = true;
        }

        Console.SetCursorPosition(0, 0);

        var sidebar = BuildSidebar(room, player);

        const int gap = 3;
        const int sidebarWidth = 70; // pad so old text doesn't remain
        string topBottomBorder = "+" + new string('-', Room.Width) + "+";

        for (int screenRow = 0; screenRow < Room.Height + 2; screenRow++)
        {
            string left;

            if (screenRow == 0 || screenRow == Room.Height + 1)
            {
                left = topBottomBorder;
            }
            else
            {
                int y = screenRow - 1;
                var row = new StringBuilder(Room.Width + 2);
                row.Append('|');

                for (int x = 0; x < Room.Width; x++)
                {
                    if (player.X == x && player.Y == y)
                    {
                        row.Append('@');
                        continue;
                    }

                    var tile = room.GetTile(x, y);

                    if (!tile.IsBlocked && tile.Items.Count > 0)
                    {
                        row.Append(tile.Items[^1].Symbol);
                    }
                    else
                    {
                        row.Append(tile.Symbol);
                    }
                }

                row.Append('|');
                left = row.ToString();
            }

            string right = (screenRow < sidebar.Count ? sidebar[screenRow] : string.Empty).PadRight(sidebarWidth);

            Console.Write(left.PadRight(Room.Width + 2));
            Console.Write(new string(' ', gap));
            Console.Write(right);
            Console.WriteLine();
        }
    }

    private static List<string> BuildSidebar(Room room, Player player)
    {
        List<string> lines = new()
        {
            $"PLAYER: {player.Symbol}",
            new string('-', 28),

            "EQUIPPED:",
            $"  Left : {FormatItem(player.LeftHand)}",
            $"  Right: {FormatItem(player.RightHand)}",
            string.Empty
        };

        var tile = room.GetTile(player.X, player.Y);
        lines.Add("ON TILE:");
        if (tile.Items.Count == 0)
        {
            lines.Add("  (none)");
        }
        else
        {
            for (int i = 0; i < tile.Items.Count; i++)
            {
                IItem it = tile.Items[i];
                lines.Add($"  {i + 1}. {it.Name}");
            }
        }
        lines.Add(string.Empty);

        lines.Add("INVENTORY:");
        if (player.Inventory.Count == 0)
        {
            lines.Add("  (empty)");
        }
        else
        {
            for (int i = 0; i < player.Inventory.Count; i++)
            {
                var it = player.Inventory[i];
                lines.Add($"  {i + 1}. {it.Name}");
            }
        }
        lines.Add(string.Empty);

        lines.Add("STATS:");
        lines.Add($"  HP : {player.Health}");
        lines.Add($"  STR: {player.Strength}");
        lines.Add($"  DEX: {player.Dexterity}");
        lines.Add($"  LCK: {player.Luck}");
        lines.Add($"  AGR: {player.Aggression}");
        lines.Add($"  WIS: {player.Wisdom}");

        while (lines.Count < Room.Height + 2)
            lines.Add(string.Empty);

        return lines;
    }

    private static string FormatItem(IItem? item)
        => item is null ? "(empty)" : $"{item.Symbol} {item.Name}";
}
