//
//  Data.Common.DbSchema - http://dbschema.codeplex.com
//
//  The contents of this file are subject to the New BSD
//  License (the "License"); you may not use this file
//  except in compliance with the License. You may obtain a copy of
//  the License at http://www.opensource.org/licenses/bsd-license.php
//
//  Software distributed under the License is distributed on an 
//  "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//  implied. See the License for the specific language governing
//  rights and limitations under the License.
//


using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Schema.Metadata.Providers
{
    public class OleDbSchemaProvider : DbSchemaProvider
    {
        public OleDbSchemaProvider(string connectionstring, string providername) : base(connectionstring, providername) { }

        #region ' IDbProvider Members '

        public override DataTable GetConstraints()
        {
            DataTable tbl = new DataTable("Constraints");
            using (OleDbConnection OleDbConn = (OleDbConnection)GetDBConnection())
            {
                tbl = OleDbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, new object[] { });
            }

            return tbl;
        }

        /// <summary>
        /// Converts ProviderType from GetSchemaTable()'s columns in DbType
        /// </summary>
        /// <remarks>
        /// Using as a guide:
        /// http://msdn.microsoft.com/en-us/library/system.data.oledb.oledbtype.aspx
        /// </remarks>
        /// <param name="providerDbType">ProviderType Column</param>
        /// <returns>DbType equilevent</returns>
        public override DbType GetDbColumnType(string providerDbType)
        {
            switch (providerDbType)
            {
                case "20":  // OleDbType.BigInt
                    return DbType.Int64;

                case "128": // OleDbType.Binary
                    return DbType.Binary;

                case "11":  // OleDbType.Boolean
                    return DbType.Boolean;

                case "8":   // OleDbType.BSTR
                    return DbType.String;

                case "129": // OleDbType.Char
                    return DbType.AnsiStringFixedLength;

                case "6":   // OleDbType.Currency
                    return DbType.Currency;

                case "7":   // OleDbType.Date
                    return DbType.DateTime;

                case "133": // OleDbType.DBDate
                    return DbType.DateTime;

                case "134": // OleDbType.DBTime
                    return DbType.Time;

                case "135": // OleDbType.DBTimeStamp
                    return DbType.DateTime;

                case "14":  // OleDbType.Decimal
                    return DbType.Decimal;

                case "5":   // OleDbType.Double
                    return DbType.Double;

                case "0":   // OleDbType.Empty
                    return DbType.Object;

                case "10":  // OleDbType.Error
                    return DbType.Object;

                case "64":  // OleDbType.Filetime
                    return DbType.DateTime;

                case "72":  // OleDbType.Guid
                    return DbType.Guid;

                case "9":   // OleDbType.IDispatch
                    return DbType.Object;

                case "3":   // OleDbType.Integer
                    return DbType.Int32;

                case "13":  // OleDbType.IUnknown
                    return DbType.Object;

                case "205": // OleDbType.LongVarBinary
                    return DbType.Binary;

                case "201": // OleDbType.LongVarChar
                    return DbType.AnsiString;

                case "203": // OleDbType.LongVarWChar
                    return DbType.String;

                case "131": // OleDbType.Numeric
                    return DbType.Decimal;

                case "138": // OleDbType.PropVariant
                    return DbType.Object;

                case "4":   // OleDbType.Single
                    return DbType.Single;

                case "2":   // OleDbType.SmallInt
                    return DbType.Int16;

                case "16":  // OleDbType.TinyInt
                    return DbType.SByte;

                case "21":  // OleDbType.UnsignedBigInt
                    return DbType.UInt64;

                case "19":  // OleDbType.UnsignedInt
                    return DbType.UInt32;

                case "18":  // OleDbType.UnsignedSmallInt
                    return DbType.UInt16;

                case "17":  // OleDbType.UnsignedTinyInt
                    return DbType.Byte;

                case "204": // OleDbType.VarBinary
                    return DbType.Binary;

                case "200": // OleDbType.VarChar
                    return DbType.AnsiString;

                case "12":  // OleDbType.Variant
                    return DbType.Object;

                case "139": // OleDbType.VarNumeric
                    return DbType.Decimal;

                case "202": // OleDbType.VarWChar
                    return DbType.String;

                case "130": // OleDbType.WChar
                    return DbType.StringFixedLength;


                default:
                    return DbType.AnsiString;
            }
        }

        #endregion


    }
}
