using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace Simple.Launcher
{
    class Program
    {
        static Process p = null;
        static bool flag = false;
        static object lockObj = new object();
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

            foreach (string ext in watchExt)
                Console.WriteLine("Watching extension: " + ext);

            Start(dirPath, filePath);
            Console.WriteLine("Waiting...");

            watcher.Changed += (o, p) =>
            {
                lock (lockObj)
                {
                    if (flag) { Console.WriteLine("blocked..."); return; }
                    flag = true;
                    watcher.EnableRaisingEvents = false;
                }
                Console.WriteLine("not blocked...");
                if (watchExt.Any(y => p.Name.EndsWith(y)) || watchExt.Count == 0)
                {
                    Console.Clear();
                    Console.WriteLine("$$$" + p.FullPath);
                    Thread.Sleep(1000);
                    Start(dirPath, filePath);
                }
                lock (lockObj)
                {
                    flag = false;
                    watcher.EnableRaisingEvents = true;
                }
            };
            watcher.EnableRaisingEvents = true;
            Thread.Sleep(Timeout.Infinite);
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

            if (p != null && !p.HasExited) p.Kill();

            var newFile = Path.GetTempFileName();
            File.Delete(newFile);
            CopyDirectory(dir, newFile);

            var info = new ProcessStartInfo()
            {
                FileName = Path.Combine(newFile, file),
                UseShellExecute = false,
                WorkingDirectory = dir
            };
            try
            {
                p = Process.Start(info);

                try
                {
                    p.WaitForInputIdle();
                }
                catch { }

                var np = p;
                new Thread(() =>
                {
                    np.WaitForExit();
                    Console.WriteLine("$$$ Process ended.");
                }).Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error starting: " + e.Message);
                Console.WriteLine();
                Console.WriteLine(e.StackTrace);
                Console.WriteLine();
                Console.WriteLine("Waiting...");
            }
        }
    }
}
