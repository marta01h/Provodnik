using System.Diagnostics;

namespace PractosNumber7
{
    static class Osnova
    {
        public static string path;
        static string openFile;
        public static void OsnovA(ConsoleKey key, int position, int max, int min, string path)
        {
            List<DriveInfo> allDrives = DriveInfo.GetDrives().ToList();
            DriveInfo d = allDrives.First(x => x.Name == "D:\\");
            allDrives.Remove(d);
            Console.WriteLine("  Выберите диск: ");
            foreach (DriveInfo drive in allDrives)
            {
                if (drive.IsReady == true)
                {
                    Console.WriteLine($"  Диск: {drive.Name} " + "   Свободное пространство: " + drive.AvailableFreeSpace / 1024 / 1024 / 1024 + " ГБ");
                }
            }
            do
            {
                Arrows.WriteCursor(position);
                key = Console.ReadKey().Key;
                position = Arrows.CursorPosition(position, max, min, key);
                if (key == ConsoleKey.Enter)
                {
                    path = Convert.ToString(allDrives[position - 1]);
                    string dirName = ($"{path}\\");
                    Console.Clear();
                    var directory = new DirectoryInfo(dirName);
                    Console.WriteLine("  Содержимое:   F1 - создать файл, F2 - удалить файл, F3 - создать директорию, F4 - удалить директорию");
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
                        max = count;
                        bool check = true;
                        Console.WriteLine();
                        do
                        {
                            Arrows.WriteCursor(position);
                            key = Console.ReadKey().Key;
                            position = Arrows.CursorPosition(position, max, min, key);
                            if (key == ConsoleKey.Enter)
                            {
                                path = Convert.ToString(dirs[position - 1]);
                                openFile = path;
                                openFile.TrimEnd('\\');
                                path = ($"{path}\\");
                                position = 1;
                                DIrectory(key, max, min, position, path);
                            }
                            else if (key == ConsoleKey.F1)
                            {
                                NewFile(path, position, key, max, min);
                            }
                            else if (key == ConsoleKey.F2)
                            {
                                DeleteFile(openFile, dirs, position, key, max, min);
                            }
                            else if (key == ConsoleKey.F3)
                            {
                                NewDirectory(path, position, key, max, min);
                            }
                            else if (key == ConsoleKey.F4)
                            {
                                DeleteDirectory(path, dirs, position, key, max, min);
                            }
                            else if (key == ConsoleKey.Escape)
                            {
                                Console.Clear();
                                position = 1;
                                min = 1;
                                max = 2;
                                OsnovA(key, position, max, min, path);
                                check = false;
                            }
                        } while (check);
                    }
                    else
                    {
                        OpenFile(openFile, key, position, max, min, path);
                    }
                }

                else if (key == ConsoleKey.Escape)
                {
                    Console.Clear();
                    Console.WriteLine("  ППОКА СУЧКА");
                    Environment.Exit(0);
                }
            } while (true);
        }
        private static void DIrectory(ConsoleKey key, int position, int max, int min, string path)
        {
            Console.Clear();
            position = 1;
            var directory = new DirectoryInfo(path);
            var file = new FileInfo(path);
            Console.WriteLine("  Содержимое:   F1 - создать файл, F2 - удалить файл, F3 - создать директорию, F4 - удалить директорию");
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
                max = count;
                min = count - count + 1;
                bool check = true;
                Console.WriteLine();
                do
                {
                    Arrows.WriteCursor(position);
                    key = Console.ReadKey().Key;
                    position = Arrows.CursorPosition(position, max, min, key);
                    if (key == ConsoleKey.Enter)
                    {
                        path = Convert.ToString(dirs[position - 1]);
                        openFile = path;
                        openFile.TrimEnd('\\');
                        path = ($"{path}\\");
                        position = 1;
                        DIrectory(key, max, min, position, path);

                    }
                    else if (key == ConsoleKey.F1)
                    {
                        NewFile(path, position, key, max, min);
                    }
                    else if (key == ConsoleKey.F2)
                    {
                        DeleteFile(openFile, dirs, position, key, max, min);
                    }
                    else if (key == ConsoleKey.F3)
                    {
                        NewDirectory(path, position, key, max, min);
                    }
                    else if (key == ConsoleKey.F4)
                    {
                        DeleteDirectory(path, dirs, position, key, max, min);
                    }
                    else if (key == ConsoleKey.Escape)
                    {
                        Console.Clear();
                        position = 1;
                        min = 1;
                        max = 2;
                        OsnovA(key, position, max, min, path);
                        check = false;
                    }
                } while (check);
            }
            else if (file.Exists)
            {
                OpenFile(openFile, key, position, max, min, path);
            }
        }
        static private void OpenFile(string openFile, ConsoleKey key, int position, int max, int min, string path)
        {
            Process.Start(new ProcessStartInfo($"{openFile}") { UseShellExecute = true });
            Console.Clear();
            position = 1;
            min = 1;
            max = 2;
            OsnovA(key, position, max, min, path);
        }
        static private void NewFile(string path, int position, ConsoleKey key, int max, int min)
        {
            Console.Clear();
            Console.WriteLine("Введите название файла");
            string name = Console.ReadLine();
            path = path + name;
            path = path.Trim('.');
            Console.WriteLine(path);
            File.Create(path);
            Console.Clear();
            position = 1;
            min = 1;
            max = 2;
            OsnovA(key, position, max, min, path);
        }
        static private void DeleteFile(string openFile, FileSystemInfo[] dirs, int position, ConsoleKey key, int max, int min)
        {
            openFile = Convert.ToString(dirs[position - 1]);
            File.Delete(openFile);
            Console.Clear();
            position = 1;
            min = 1;
            max = 2;
            OsnovA(key, position, max, min, path);
        }
        static private void NewDirectory(string path, int position, ConsoleKey key, int max, int min)
        {
            Console.Clear();
            Console.WriteLine("Введите название директории");
            string name = Console.ReadLine();
            path = path + name;
            path = path.Trim('.');
            Console.WriteLine(path);
            Directory.CreateDirectory(path);
            Console.Clear();
            position = 1;
            min = 1;
            max = 2;
            OsnovA(key, position, max, min, path);
        }
        static private void DeleteDirectory(string path, FileSystemInfo[] dirs, int position, ConsoleKey key, int max, int min)
        {
            path = Convert.ToString(dirs[position - 1]);
            Directory.Delete(path, true);
            Console.Clear();
            position = 1;
            min = 1;
            max = 2;
            OsnovA(key, position, max, min, path);
        }
    }
}
