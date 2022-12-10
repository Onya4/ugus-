using provod;

namespace PPr0v0dn1k
{
    internal class Program
    {
        static int position = Strelka.posY, max = Strelka.posY_max, min = Strelka.posY_min;
        static ConsoleKey key = Strelka.key;
        static string path = Head.path;
        static void Main()
        {
            Menu(position, max, min, key, path);
        }
        static void Menu(int posY, int posY_max, int posY_min, ConsoleKey key, string path)
        {
            posY_min = 1;
            posY_max = 2;
            posY = 1;
            Head.Brain(key, posY, posY_max, posY_min, path);
        }
    }
}