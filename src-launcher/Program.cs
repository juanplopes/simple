using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SimpleLauncher
{
    class Program
    {
        static Process p = null;
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(args[0]);
            string dir = Path.GetDirectoryName(path);
            string file = Path.GetFileName(path);
            var watcher = new FileSystemWatcher(dir, file);
            Start(path);
            watcher.Changed += (o, p) =>
            {
                Start(p.FullPath);
            };
            watcher.EnableRaisingEvents = true;
            while (true) Console.ReadLine();
        }

        static void Start(string file)
        {
            string dir = Path.GetDirectoryName(file);
            Console.WriteLine("Loading " + file + "...");
            Console.WriteLine("Current directory: " + dir);

            if (p != null) p.Kill();

            var newFile = Path.GetTempFileName();
            File.Copy(file, newFile, true);

            var info = new ProcessStartInfo() {
                FileName = newFile,
                UseShellExecute = false,
                WorkingDirectory = dir               
            };
            p = Process.Start(info);
        }
    }
}
