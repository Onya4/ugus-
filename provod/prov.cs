using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace provod
{
    internal class Head
    {
        public static string path;
        static string openFile;
        public static void Brain(ConsoleKey key, int posY, int posY_max, int posY_min, string path)
        {
            List<DriveInfo> allDrives = DriveInfo.GetDrives().ToList();
            DriveInfo d = allDrives.First(x => x.Name == "D:\\");
            allDrives.Remove(d);
            Console.WriteLine("  Выберите диск: ");
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.IsReady == true)
                {
                    Console.WriteLine($"  Диск: {drive.Name} " + "   Да диске свободно: " + drive.AvailableFreeSpace / 1024 / 1024 / 1024 + " ГБ");
                }
            }
            do
            {
                Strelka.clear();
                key = Console.ReadKey().Key;
                posY = Strelka.strelka();
                if (key == ConsoleKey.Enter)
                {
                    path = Convert.ToString(allDrives[posY - 1]);
                    string dirName = ($"{path}\\");
                    Console.Clear();
                    var directory = new DirectoryInfo(dirName);
                    Console.WriteLine("  Содержимое:F1 - создать файл, F2 - удалить файл, F3 - создать директорию, F4 - удалить директорию");
                    if (directory.Exists)
                    {
                        FileSystemInfo[] dirs = directory.GetFileSystemInfos();
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            Console.WriteLine($"  {dirs[i].Name}");
                            Console.SetCursorPosition(47, i + 1);
                            Console.WriteLine($"{dirs[i].CreationTime}");
                            Console.SetCursorPosition(47, i + 1);
                            Console.WriteLine("   ");
                            Console.SetCursorPosition(75, i + 1);
                            Console.WriteLine($"{dirs[i].Extension}");
                        }
                        int count = dirs.Length;
                        posY_max = count;
                        bool check = true;
                        Console.WriteLine();
                        do
                        {
                            Strelka.clear();
                            key = Console.ReadKey().Key;
                            posY = Strelka.strelka();
                            switch (key)
                            {
                                case ConsoleKey.Enter:
                                    path = Convert.ToString(dirs[posY - 1]);
                                    openFile = path;
                                    openFile.TrimEnd('\\');
                                    path = ($"{path}\\");
                                    posY = 1;
                                    DIrectory(key, posY_max, posY_min, posY, path);
                                    break;
                                case ConsoleKey.F1:
                                    new_file(key, posY, posY_max, posY_min, path);
                                    break;
                                case ConsoleKey.F2:
                                    delete_file(openFile, dirs, posY, key, posY_max, posY_min);
                                    break;
                                case ConsoleKey.F3:
                                    new_directory(path, posY, key, posY_max, posY_min);
                                    break;
                                case ConsoleKey.F4:
                                    delete_directory(path, dirs, posY, key, posY_max, posY_min);
                                    break;
                                case ConsoleKey.Escape:
                                    Console.Clear();
                                    posY = 1;
                                    posY_min = 1;
                                    posY_max = 2;
                                    Brain(key, posY_max, posY_min, posY, path);
                                    check = false;
                                    break;
                            }
                        } while (check);
                    }
                    else
                    {
                        open_file(key, posY_max, posY_min, posY, path);
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("_До скорых встреч!");
                    Environment.Exit(0);
                }
            } while (true);
        }
        private static void DIrectory(ConsoleKey key, int posY, int posY_max, int posY_min, string path)
        {
            Console.Clear();
            posY = 1;
            var directory = new DirectoryInfo(path);
            var file = new FileInfo(path);
            Console.WriteLine("  Содержимое:F1 - создать файл, F2 - удалить файл, F3 - создать директорию, F4 - удалить директорию");
            if (directory.Exists)
            {
                FileSystemInfo[] dirs = directory.GetFileSystemInfos();
                for (int i = 0; i < dirs.Length; i++)
                {
                    Console.WriteLine($"  {dirs[i].Name}");
                    Console.SetCursorPosition(47, i + 1);
                    Console.WriteLine($"{dirs[i].CreationTime}");
                    Console.SetCursorPosition(47, i + 1);
                    Console.WriteLine("   ");
                    Console.SetCursorPosition(75, i + 1);
                    Console.WriteLine($"{dirs[i].Extension}");
                }
                int count = dirs.Length;
                posY_max = count;
                posY_min = count - count + 1;
                bool check = true;
                Console.WriteLine();
                do
                {
                    Strelka.clear();
                    key = Console.ReadKey().Key;
                    posY = Strelka.strelka();
                    switch (key)
                    {
                        case ConsoleKey.Enter:
                            path = Convert.ToString(dirs[posY - 1]);
                            openFile = path;
                            openFile.TrimEnd('\\');
                            path = ($"{path}\\");
                            posY = 1;
                            DIrectory(key, posY_max, posY_min, posY, path);
                            break;
                        case ConsoleKey.F1:
                            new_file(key, posY, posY_max, posY_min, path);
                            break;
                        case ConsoleKey.F2:
                            delete_file(openFile, dirs, posY, key, posY_max, posY_min);
                            break;
                        case ConsoleKey.F3:
                            new_directory(path, posY, key, posY_max, posY_min);
                            break;
                        case ConsoleKey.F4:
                            delete_directory(path, dirs, posY, key, posY_max, posY_min);
                            break;
                        case ConsoleKey.Escape:
                            Console.Clear();
                            posY = 1;
                            posY_min = 1;
                            posY_max = 2;
                            Brain(key, posY_max, posY_min, posY, path);
                            check = false;
                            break;
                    }
                } while (check);
            }
            else if (file.Exists)
            {
                open_file(key, posY, posY_max, posY_min, path);
            }
        }
        static private void open_file(ConsoleKey key, int posY, int posY_max, int posY_min, string path)
        {
            Process.Start(new ProcessStartInfo($"{openFile}") { UseShellExecute = true });
            Console.Clear();
            posY = 1;
            posY_min = 1;
            posY_max = 2;
            Brain(key, posY, posY_max, posY_min, path);
        }
        static private void new_file(ConsoleKey key, int posY, int posY_max, int posY_min, string path)
        {
            Console.Clear();
            Console.WriteLine("Введите название файла");
            string name = Console.ReadLine();
            path = path + name;
            path = path.Trim('.');
            Console.WriteLine(path);
            File.Create(path);
            Console.Clear();
            posY = 1;
            posY_min = 1;
            posY_max = 2;
            Brain(key, posY, posY_max, posY_min, path);
        }
        static private void delete_file(string openFile, FileSystemInfo[] dirs, int posY, ConsoleKey key, int posY_max, int posY_min)
        {
            openFile = Convert.ToString(dirs[posY - 1]);
            File.Delete(openFile);
            Console.Clear();
            posY = 1;
            posY_min = 1;
            posY_max = 2;
            Brain(key, posY, posY_max, posY_min, path);
        }
        static private void new_directory(string path, int posY, ConsoleKey key, int posY_max, int posY_min)
        {
            Console.Clear();
            Console.WriteLine("Введите название директории");
            string name = Console.ReadLine();
            path = path + name;
            path = path.Trim('.');
            Console.WriteLine(path);
            Directory.CreateDirectory(path);
            Console.Clear();
            posY = 1;
            posY_min = 1;
            posY_max = 2;
            Brain(key, posY, posY_max, posY_min, path);
        }
        static private void delete_directory(string path, FileSystemInfo[] dirs, int posY, ConsoleKey key, int posY_max, int posY_min)
        {
            path = Convert.ToString(dirs[posY - 1]);
            Directory.Delete(path, true);
            Console.Clear();
            posY = 1;
            posY_min = 1;
            posY_max = 2;
            Brain(key, posY, posY_max, posY_min, path);
        }
    }
}
