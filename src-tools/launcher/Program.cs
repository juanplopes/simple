using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace SimpleLauncher
{
    class Program
    {
        static Process p = null;
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong number of arguments. Expected 2: <directory> <file>");
                return;
            }


            string dirPath = Path.GetFullPath(args[0]);
            string filePath = args[1];
            string fullPath = Path.Combine(dirPath, filePath);
            var watchExt = args.Skip(2).ToList();

            var watcher = new FileSystemWatcher(dirPath);
            watcher.IncludeSubdirectories = true;

            var timer = new Timer((x) =>
            {
                Start(dirPath, filePath);
            });
            timer.Change(1000, Timeout.Infinite);

            watcher.Changed += (o, p) =>
            {
                Console.WriteLine("Detected change of: " + p.FullPath);
                if (watchExt.Any(y => p.Name.EndsWith(y)) || watchExt.Count == 0)
                {
                    Console.WriteLine("Catching...");
                    timer.Change(1000, Timeout.Infinite);
                }
            };
            watcher.EnableRaisingEvents = true;
            while (true) Console.ReadLine();
        }

        static void CopyDirectory(string src, string dst)
        {

            if (dst[dst.Length - 1] != Path.DirectorySeparatorChar)
                dst += Path.DirectorySeparatorChar;
            if (!Directory.Exists(dst)) Directory.CreateDirectory(dst);
            var files = Directory.GetFileSystemEntries(src);
            foreach (string Element in files)
            {
                if (Directory.Exists(Element))
                    CopyDirectory(Element, dst + Path.GetFileName(Element));
                else
                    File.Copy(Element, dst + Path.GetFileName(Element), true);

            }

        }


        static void Start(string dir, string file)
        {
            Console.WriteLine("Loading " + file + "...");
            Console.WriteLine("Current directory: " + dir);

            if (p != null) p.Kill();

            var newFile = Path.GetTempFileName();
            File.Delete(newFile);
            CopyDirectory(dir, newFile);

            var info = new ProcessStartInfo()
            {
                FileName = Path.Combine(newFile, file),
                UseShellExecute = false,
                WorkingDirectory = dir
            };
            p = Process.Start(info);
        }
    }
}
