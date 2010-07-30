using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.Generator;
using System.Data.SqlClient;
using Simple;
using Simple.NVelocity;
using Sample.Project.Tools.Macros;

namespace Sample.Project.Tools.ResetDb
{
    public class ResetDbCommand : ICommand
    {
        public bool Prepare { get; set; }
        public ResetDbCommand() { Prepare = true; }

        #region ICommand Members

        public void Execute()
        {
            //this command can only be used with SqlServer
            var cs = @"Server=.\SQLExpress;initial catalog=master;Integrated Security=SSPI";
            ResetInternal(cs, "SampleProject");
            ResetInternal(cs, "SampleProject_Tests");
            
            if (Prepare)
                new PrepareMacro().Execute();
        }

        protected void ResetInternal(string cs, string dbName)
        {
            var scriptDrop = new SimpleTemplate(Scripts.DropDatabases);
            var scriptCreate = new SimpleTemplate(Scripts.CreateDatabases);
            scriptDrop["database"] = scriptCreate["database"] = dbName;

            Simply.Do.Log(this).InfoFormat("Reseting {0}...", dbName);

            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                ExecScript(scriptDrop, conn, false);
                ExecScript(scriptCreate, conn, true);
            }
        }

        private void ExecScript(SimpleTemplate scriptDrop, SqlConnection conn, bool logSqlException)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = scriptDrop.ToString();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                if (logSqlException)
                    Simply.Do.Log(this).Warn(e.Message, e);
            }
            catch (Exception e)
            {
                Simply.Do.Log(this).Warn(e.Message, e);
            }
        }

        #endregion
    }
}
