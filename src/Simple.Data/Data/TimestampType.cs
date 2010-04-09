using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Type;
using NHibernate.SqlTypes;

namespace Simple.Data
{
    /// <summary>
    /// UserTypeTimestamp implements the Nhibernate BinaryType that is used to handle Nhibernate version . 
    /// </summary>
    public class TimestampType : NHibernate.UserTypes.IUserVersionType
    {
        #region IUserVersionType Members

        public object Next(object current, NHibernate.Engine.ISessionImplementor session)
        {
            try
            {
                return current;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public object Seed(NHibernate.Engine.ISessionImplementor session)
        {
            try
            {
                return new byte[8];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IUserType Members

        public object Assemble(object cached, object owner)
        {
            try
            {
                return DeepCopy(cached);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object DeepCopy(object value)
        {
            try
            {
                return value;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Disassemble(object value)
        {
            try
            {
                return DeepCopy(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetHashCode(object x)
        {
            try
            {
                return ((byte[])x).GetHashCode();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            try
            {
                return ((byte[])rs.GetValue(rs.GetOrdinal(names[0])));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            try
            {
                NHibernateUtil.Binary.NullSafeSet(cmd, value, index);
                return;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return typeof(byte[]); }
        }

        public SqlType[] SqlTypes
        {
            get
            {
                try
                {

                    SqlType[] types = { new NHibernate.SqlTypes.SqlType(System.Data.DbType.Binary) }; return types;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region IComparer Members

        public int Compare(object x, object y)
        {
            try
            {
                byte[] xbytes = (byte[])x;
                byte[] ybytes = (byte[])y;
                if (xbytes.Length < ybytes.Length)
                {
                    return -1;
                }
                if (xbytes.Length > ybytes.Length)
                {
                    return 1;
                }
                for (int i = 0; i < xbytes.Length; i++)
                {
                    if (xbytes[i] < ybytes[i])
                    {
                        return -1;
                    }
                    if (xbytes[i] > ybytes[i])
                    {
                        return 1;
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        bool NHibernate.UserTypes.IUserType.Equals(object x, object y)
        {
            return (x == y);

        }
        #endregion
    }
}
