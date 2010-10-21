using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;

namespace Simple.IO.Excel
{
    public class WorkbookWriter
    {
        public static WorkbookWriter<T> Create<T>(HeaderDefinition<T> header)
        {
            return new WorkbookWriter<T>(header);
        }
    }


    public class WorkbookWriter<T>
    {
        public SheetWriter<T> Writer { get; protected set; }
        public HeaderDefinition<T> Header { get; protected set; }

        public WorkbookWriter(HeaderDefinition<T> header)
        {
            Header = header;
            Writer = new SheetWriter<T>(header);
        }

        public byte[] WriteBytes(string sheetName, IEnumerable<T> items)
        {
            return WriteBytes(null, sheetName, items);
        }

        public byte[] WriteBytes(byte[] template, string sheetName, IEnumerable<T> items)
        {
            Workbook workbook;
            if (template != null)
                using (var stream = new MemoryStream(template))
                    workbook = new HSSFWorkbook(stream);
            else
                workbook = new HSSFWorkbook();

            Write(workbook, sheetName, items);
            RemoveTemplate(workbook);

            var mem = new MemoryStream();
            workbook.Write(mem);
            return mem.ToArray();
        }

        public void Write(Workbook workbook, string sheetName, IEnumerable<T> items)
        {
            workbook = CheckWorkbook(workbook);
            Sheet sheet = null;
            if (workbook.NumberOfSheets == 0)
                throw new Exception("Must have at least one template sheet");

            sheet = workbook.CloneSheet(0);
            if (workbook is HSSFWorkbook)
                (workbook as HSSFWorkbook).SetSheetName(workbook.GetSheetIndex(sheet), sheetName);

            Writer.Write(sheet, items);
        }

        public void RemoveTemplate(Workbook workbook)
        {
            workbook.RemoveSheetAt(0);
        }

        private static Workbook GetWorkbook(byte[] baseSheet)
        {
            if (baseSheet == null) return null;
            using (var stream = new MemoryStream(baseSheet))
                return GetWorkbook(stream);
        }

        private static Workbook GetWorkbook(Stream baseSheet)
        {
            if (baseSheet == null) return null;
            using (var wb = new HSSFWorkbook(baseSheet))
                return wb;
        }

        public static Workbook CheckWorkbook(Workbook workbook)
        {
            workbook = workbook ?? new HSSFWorkbook();
            if (workbook.NumberOfSheets == 0)
                workbook.CreateSheet();
            return workbook;
        }

        private static Sheet GetSheetFromWorkbook(Workbook baseWorkbook)
        {
            return baseWorkbook.GetSheetAt(0);
        }


    }
}
