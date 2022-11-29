namespace PractosNumber7
{
    internal class Program
    {
        static int position = Arrows.position, max = Arrows.max, min = Arrows.min;
        static ConsoleKey key = Arrows.key;
        static string path = Osnova.path;
        static void Main()
        {
            Menu(position, max, min, key, path);
        }
        static void Menu(int position, int max, int min, ConsoleKey key, string path)
        {
            min = 1; max = 2;
            position= 1;
            Osnova.OsnovA(key, position,max,min, path);
        }
    }
}
