using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NHibernate.Cfg;
using System.IO;
using Simple.IO.Serialization;

namespace Simple.Data
{
    public class NHibernateCache
    {
        [Serializable]
        public class Version
        {
            public Guid Identifier { get; set; }
            public Configuration Config { get; set; }
        }

        public string FilePath { get; protected set; }
        public Guid Identifier { get; protected set; }

        public NHibernateCache(string file, Guid id)
        {
            FilePath = file;
            Identifier = id;
        }

        public NHibernateCache(string file, Assembly asm)
            : this(file, asm.ManifestModule.ModuleVersionId)
        {
        }

        public Configuration Get()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    var cache = SimpleSerializer.Binary().Deserialize(File.ReadAllBytes(FilePath)) as Version;
                    if (cache != null && cache.Identifier == Identifier)
                        return cache.Config;
                }
            }
            catch (Exception e)
            {
                Simply.Do.Log(this).Warn(e.Message, e);
            }

            return null;
        }

        public void Set(Configuration value)
        {
            try
            {
                File.WriteAllBytes(FilePath,
                        SimpleSerializer.Binary().Serialize(
                            new Version() { Config = value, Identifier = Identifier }));
            }
            catch (Exception e)
            {
                Simply.Do.Log(this).Warn(e.Message, e);
            }
        }


    }
}
