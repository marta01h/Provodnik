using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace PractosNumber7
{
    internal class Arrows
    {
        public static int position = 0;
        public static int max = 0;
        public static int min = 0;
        public static ConsoleKey key;
        Arrows(int MIN, int MAX) 
        {
            int min = MIN;
            int max = MAX;
        }
        public static int CursorPosition(int position, int max, int min, ConsoleKey key)
        {
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    position--;
                    if (position < min)
                    {
                        position = min;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    position++;
                    if (position > max)
                    {
                        position = max;
                    };
                    break;
            }
            return position;
        }
        public static void WriteCursor(int position)
        {
            Console.SetCursorPosition(0, position - 1);
            Console.WriteLine("  ");
            Console.SetCursorPosition(0, position);
            Console.WriteLine("->");
            Console.SetCursorPosition(0, position + 1);
            Console.WriteLine("  ");
        }
    }
}
