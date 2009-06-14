using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.ComponentModel;

namespace Simple.ConfigSource
{
    public class XmlFileConfigSource<T> :
        XmlConfigSource<T>,
        IConfigSource<T, FileInfo>
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
                    InvokeExpired();
            }
        }

        public virtual T Load(FileInfo input)
        {
            lock (this)
            {
                if (Watcher != null) Watcher.Dispose();

                Watcher = new FileSystemWatcher(input.DirectoryName, input.Name);
                Watcher.Changed += Watcher_Changed;
                Watcher.Created += Watcher_Changed;
                Watcher.Deleted += Watcher_Changed;
                Watcher.EnableRaisingEvents = true;

                File = input;

                Active = true;
                using (Stream s = File.OpenRead())
                {
                    T ret = Load(s);
                    s.Close();
                    return ret;
                }
            }
        }



        public override T Reload()
        {
            lock (this)
            {
                if (File == null) throw new InvalidOperationException("Cannot reload a non-loaded source");
                return Load(File);
            }
        }

        public override void Dispose()
        {
            lock (this)
            {
                Active = false;
                Watcher.Dispose();
            }
        }

       
    }
}
