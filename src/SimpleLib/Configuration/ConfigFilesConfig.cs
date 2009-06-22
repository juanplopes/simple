using System;
using System.Collections.Generic;
using System.Text;
using Simple.Common;
using System.IO;
using Simple.Patterns;

namespace Simple.Configuration2
{
    [ConfigFilesIgnore]
    [DefaultFile("ConfigFiles.xml",ThrowException=false)]
    public class ConfigFilesConfig : ConfigRoot<ConfigFilesConfig>
    {
        [ConfigElement("configGroup")]
        public List<ConfigGroupElement> Groups { get; set; }

        private SafeDictionary<string, string> configFiles = null;
        public SafeDictionary<string, string> ConfigFiles
        {
            get
            {
                if (configFiles == null)
                {
                    configFiles = new SafeDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    foreach (ConfigGroupElement group in Groups)
                    {
                        foreach (ConfigFileElement file in group.Files)
                        {
                            configFiles[file.Type] = Path.Combine(group.Path, file.File);
                        }
                    }
                }

                return configFiles;
            }
        }

        public override string DefaultXmlString
        {
            get
            {
                return "";
            }
        }

    }
}
