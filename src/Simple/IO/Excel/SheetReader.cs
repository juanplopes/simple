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

                var results = ReadInternal(sheet);
                return new SheetResult<T>(sheet.SheetName,
                    results.Select(x => x.Result)
                    , results.SelectMany(x => x.Errors));
            }
            catch (Exception e)
            {
                return new SheetResult<T>(sheet.SheetName, new T[0], new[] { new SheetError(0, e.Message) });
            }
        }

        public IEnumerable<RowReader<T>.RowResult> ReadInternal(Sheet sheet)
        {
            var first = sheet.FirstRowNum;
            var indexes = Reader.ReadHeader(sheet.GetRow(first));
            var nulls = 0;

            log.DebugFormat("Its first row is {0} and last row is {1}", sheet.FirstRowNum, sheet.LastRowNum);

            for (int i = first + 1; i <= sheet.LastRowNum; i++)
            {
                var row = Reader.Read(i, sheet.GetRow(i), indexes);
                if (row != null)
                {
                    nulls = 0;
                    yield return row;
                }
                else
                {
                    nulls++;
                    if (nulls >= MaxNullLines)
                    {
                        log.DebugFormat("Max nulls achieved");
                        break;
                    }
                }
            }
        }

    }
}
