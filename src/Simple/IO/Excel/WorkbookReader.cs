using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace Simple.IO.Excel
{
    public class WorkbookReader
    {
        public static WorkbookReader<T> Create<T>(HeaderDefinition<T> header)
        {
            return Create(header, 10);
        }

        public static WorkbookReader<T> Create<T>(HeaderDefinition<T> header, int maxNullLines)
        {
            return new WorkbookReader<T>(
                new SheetReader<T>(
                    new RowReader<T>(header)) { MaxNullLines = maxNullLines });
        }
    }

    public class WorkbookReader<T>
    {
        public SheetReader<T> Reader { get; protected set; }

        public WorkbookReader(SheetReader<T> reader)
        {
            Reader = reader;
        }

        public SheetResultCollection<T> Read(byte[] bytes)
        {
            using (var mem = new MemoryStream(bytes))
                return Read(new HSSFWorkbook(mem));
        }

        public SheetResultCollection<T> Read(Workbook workbook)
        {
            return new SheetResultCollection<T>(ReadInternal(workbook).ToList());
        }

        private IEnumerable<SheetResult<T>> ReadInternal(Workbook workbook)
        {
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                var sheet = workbook.GetSheetAt(i);

                yield return Reader.Read(sheet);
            }

        }

    }
}