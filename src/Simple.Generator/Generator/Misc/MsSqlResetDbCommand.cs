using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Simple.NVelocity;
using System.Data.SqlClient;

namespace Simple.Generator.Misc
{
    public abstract class MsSqlResetDbCommand : ICommand
    {
        protected abstract string MakeCreate(string dbName);
        protected abstract string MakeDrop(string dbName);
        public abstract void Execute();

        protected virtual void ResetInternal(string cs, string dbName)
        {
            var scriptDrop = MakeDrop(dbName);
            var scriptCreate = MakeCreate(dbName);

            Simply.Do.Log(this).InfoFormat("Reseting {0}...", dbName);

            using (var conn = new SqlConnection(cs))
            {
                conn.Open();
                ExecScript(scriptDrop, conn, false);
                ExecScript(scriptCreate, conn, true);
            }
        }

        private void ExecScript(string scriptDrop, SqlConnection conn, bool logSqlException)
        {
            try
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = scriptDrop;
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
    }
}
