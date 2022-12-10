using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provod
{
    internal class Strelka
    {
        public static int posY = 0;
        public static int posY_max = 21;
        public static int posY_min = 0;
        public static ConsoleKey key;
        public static int strelka()
        {
            ConsoleKeyInfo kay = Console.ReadKey();
            while (kay.Key != ConsoleKey.Enter)
            {

                if (kay.Key == ConsoleKey.UpArrow)
                {
                    posY--;
                    if (posY < posY_min)
                    {
                        posY = posY_min;
                    }
                }
                else if (kay.Key == ConsoleKey.DownArrow)
                {
                    posY++;
                    if (posY > posY_max)
                    {
                        posY = posY_max;
                    }
                }
                clear();
                Console.SetCursorPosition(0, posY);
                Console.WriteLine("->");
                kay = Console.ReadKey();
            }
            return posY;
        }

        public static void clear()
        {
            if (posY > 0)
            {
                Console.SetCursorPosition(0, posY - 1);
                Console.WriteLine("  ");
            }
            if (posY < 7)
            {
                Console.SetCursorPosition(0, posY + 1);
                Console.WriteLine("  ");
            }
        }
    }
}
