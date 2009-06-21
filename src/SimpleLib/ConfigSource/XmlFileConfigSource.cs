using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;
using Simple.Logging;

namespace Simple.ConfigSource
{
    public class XmlFileConfigSource<T> :
        XmlConfigSource<T>,
        IFileConfigSource<T>,
        IConfigSource<T, FileInfo>
        where T : new()
    {
        public FileInfo File { get; set; }
        protected FileSystemWatcher Watcher { get; set; }

        public bool Active { get; protected set; }
        public DateTime LastModification { get; protected set; }

        private void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            lock (this)
            {
                if (Active)
                {
                    SimpleLogger.Get(this).DebugFormat("The watch in file {0} has raised.", File.Name);
                    InvokeReload();
                }
            }
        }

        public virtual IConfigSource<T> Load(FileInfo input)
        {
            lock (this)
            {
                SimpleLogger.Get(this).DebugFormat("Loading XMLConfig for class {0}...", typeof(T).Name);

                if (Watcher != null) Watcher.Dispose();

                Watcher = new FileSystemWatcher(input.DirectoryName, input.Name);
                Watcher.Changed += Watcher_Changed;
                Watcher.Created += Watcher_Changed;
                Watcher.Deleted += Watcher_Changed;
                Watcher.EnableRaisingEvents = true;

                File = input;

                Active = true;
                using (Stream s = File.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var ret = Load(s);
                    s.Close();
                    return ret;
                }
            }
        }

        public virtual IConfigSource<T> LoadFile(string fileName)
        {
            return Load(new FileInfo(fileName));
        }

        public override bool Reload()
        {
            lock (this)
            {
                if (File == null) throw new InvalidOperationException("Cannot reload a non-loaded source");

                SimpleLogger.Get(this).DebugFormat("Reloading file {0}...", File.Name);

                try
                {
                    Load(File);
                    return true;
                }
                catch (IOException)
                {
                    return false;
                }
            }
        }

        public override void Dispose()
        {
            lock (this)
            {
                SimpleLogger.Get(this).DebugFormat("Disposing configurator for {0}...", typeof(T));
                Active = false;
                Watcher.Dispose();
            }
        }


    }
}
