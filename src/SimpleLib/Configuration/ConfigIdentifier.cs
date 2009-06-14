using System;
using System.Collections.Generic;

using System.Text;
using System.IO;

namespace Simple.Configuration2
{
    public class ConfigIdentifier
    {
        public string File { get; set; }
        public string Localization { get; set; }
        public ConfigIdentifier(string file, string localization)
        {
            this.File = file;
            this.Localization = localization;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConfigIdentifier)) return false;
            ConfigIdentifier id = obj as ConfigIdentifier;
            return id.File == this.File && id.Localization == this.Localization;
        }

        public override int GetHashCode()
        {
            return (this.File ?? string.Empty).GetHashCode() * (this.Localization ?? string.Empty).GetHashCode();
        }

        public override string ToString()
        {
            return Path.GetFileName(File) + ":" + Localization;
        }
    }
}
