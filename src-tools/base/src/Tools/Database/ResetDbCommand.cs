using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Data.SqlClient;
using Simple;
using Simple.NVelocity;
using Example.Project.Tools.Macros;
using Simple.Generator.Misc;

namespace Example.Project.Tools.Database
{
    public class ResetDbCommand : MsSqlResetDbCommand
    {
        public bool Prepare { get; set; }
        public ResetDbCommand() { Prepare = true; }

        public override void Execute()
        {
            //this command can only be used with SqlServer
            var cs = @"Server=.\SQLExpress;initial catalog=master;Integrated Security=SSPI";
            ResetInternal(cs, "ExampleProject");
            ResetInternal(cs, "ExampleProject_Tests");
            
            if (Prepare)
                new PrepareMacro().Execute();
        }

        protected override string MakeCreate(string dbName)
        {
            return new SimpleTemplate(Scripts.CreateDatabases)
                .SetMany(database => dbName).ToString();
        }

        protected override string MakeDrop(string dbName)
        {
            return new SimpleTemplate(Scripts.DropDatabases)
                .SetMany(database => dbName).ToString();
        }
    }
}
