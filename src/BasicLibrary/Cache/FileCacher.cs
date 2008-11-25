using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using BasicLibrary.Logging;
using System.Timers;
using System.Collections.Specialized;
using BasicLibrary.LibraryConfig;
using System.Diagnostics;

namespace BasicLibrary.Cache
{
    public class FileCacher : BaseCacher<string, string>
    {
        public override event CacheExpired<string> CacheExpiredEvent;

        protected int TimeOutValue
        {
            get
            {
                return 300 * 1000;
            }
        }

        public static Dictionary<string, FileCacher> CachedValues =
            new Dictionary<string, FileCacher>();

        public FileSystemWatcher Watcher { get; set; }
        public Timer Timer { get; set; }

        protected bool IsStarted { get; set; }
        protected bool IsValid { get; set; }
        protected string Value { get; set; }

        public static FileCacher GetCacher(string file)
        {
            Debug.Assert(file != null);

            lock (CachedValues)
            {
                string id = GetBasedFile(file);
                try
                {
                    FileCacher cacher = CachedValues[id];
                    return cacher;
                }
                catch (KeyNotFoundException)
                {
                    FileCacher cacher = new FileCacher();
                    cacher.Start(file);
                    CachedValues[id] = cacher;
                    return cacher;
                }
            }
        }

        public static string GetValue(string file)
        {
            lock (CachedValues)
            {
                FileCacher cacher = GetCacher(file);
                return cacher.GetValue();
            }
        }

        public override string GetValue()
        {
            lock (CachedValues)
            {
                Log(sTrying);
                if (!Validate())
                {
                    Log(sNotValid);

                    IsValid = true;

                    try
                    {
                        Value = File.ReadAllText(Identifier);
                    }
                    catch (IOException)
                    {
                        Log("IOException, returning null...");
                        Value = null;
                    }
                    if (Timer.Enabled) Timer.Stop();
                    Timer.Start();
                }
                Log(sReturningCached);
                return Value;
            }
        }

        public override bool Validate()
        {
            return IsValid;
        }

        public FileCacher()
        {
            IsStarted = false;
        }

        public void Start(string filePath)
        {
            Identifier = GetBasedFile(filePath);

            IsValid = false;

            Watcher = new FileSystemWatcher(Path.GetDirectoryName(Identifier), Path.GetFileName(Identifier));
            Watcher.Changed += new FileSystemEventHandler(Watcher_Changed);
            Watcher.Deleted += new FileSystemEventHandler(Watcher_Changed);
            Watcher.Created += new FileSystemEventHandler(Watcher_Changed);

            Timer = new Timer(TimeOutValue);
            Timer.AutoReset = true;
            Timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);

            Watcher.EnableRaisingEvents = true;

            IsStarted = true;
        }

        void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (IsValid)
            {
                Log("Cache expired: timeout");
                if (CacheExpiredEvent != null) CacheExpiredEvent.Invoke(this.Identifier);
                IsValid = false;
            }
        }

        protected void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            lock (this)
            {
                if (IsValid)
                {
                    Log("Cache expired: file has changed");
                    IsValid = false;
                    if (CacheExpiredEvent != null) CacheExpiredEvent.Invoke(this.Identifier);
                }
            }
        }



        public static string GetBasedFile(string file)
        {
            string oldFile = file;
            if (!Path.IsPathRooted(file))
            {
                if (AppDomain.CurrentDomain.RelativeSearchPath != null)
                    file = Path.Combine(AppDomain.CurrentDomain.RelativeSearchPath, oldFile);
                if (AppDomain.CurrentDomain.RelativeSearchPath == null || !File.Exists(file))
                    file = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, oldFile);
            }
            return Path.GetFullPath(file);
        }

        protected override string GetFormattedId()
        {
            return Path.GetFileName(Identifier);
        }
    }
}
