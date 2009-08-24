using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.IO.Serialization;
using System.IO;

namespace Simple.Generator
{
    public class Program
    {
        static int Main(string[] args)
        {
            var db = SimpleSerializer.Xml<Database>().Deserialize(
                File.ReadAllBytes("NorthwindSampleModel.dbml")) as Database;
            
            return 0;
        }
    }
}
