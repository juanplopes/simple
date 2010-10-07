using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using System.Collections;
using NPOI.HSSF.UserModel;
using System.IO;
using log4net;
using System.Reflection;

namespace Simple.IO.Excel
{
    public class SheetReader<T>
    {
        public int MaxNullLines { get; set; }

        private static ILog log = Simply.Do.Log(MethodBase.GetCurrentMethod());

        public RowReader<T> Reader { get; protected set; }
        public SheetReader(RowReader<T> reader)
        {
            Reader = reader;
            MaxNullLines = 10;
        }


        public SheetResult<T> Read(Sheet sheet)
        {
            try
            {
                log.DebugFormat("Reading sheet {0}", sheet.SheetName);

                var results = ReadInternal(sheet).ToList();
                return new SheetResult<T>(sheet.SheetName, results.ToList());
            }
            catch (Exception e)
            {
                var results = new SheetResult<T>(sheet.SheetName, new RowResult<T>[0]);
                results.PrivateErrors.Add(new SheetError(0, e.Message));
                return results;
            }
        }

        public IEnumerable<RowResult<T>> ReadInternal(Sheet sheet)
        {
            var first = sheet.FirstRowNum;
            var indexes = Reader.ReadHeader(sheet.GetRow(first));
            var nulls = 0;

            log.DebugFormat("Its first row is {0} and last row is {1}", sheet.FirstRowNum, sheet.LastRowNum);

            for (int i = first + 1; i <= sheet.LastRowNum; i++)
            {
                var row = Reader.Read(i, sheet.GetRow(i), indexes);
                if (row.HasValue)
                    nulls = 0;
                else
                {
                    nulls++;
                    if (nulls >= MaxNullLines)
                    {
                        log.DebugFormat("Max nulls achieved");
                        break;
                    }
                }

                yield return row;
            }
        }

    }
}
