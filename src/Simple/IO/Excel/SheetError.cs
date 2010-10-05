using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Simple.IO.Excel
{
    [Serializable]
    public class SheetError
    {
        public int Row { get; protected set; }
        public int? Column { get; set; }
        public string Message { get; protected set; }

        public string DisplayMessage
        {
            get
            {
                if (Column != null)
                    return string.Format("Cell {0}{1}: {2}", (char)(Column + 'A'), Row + 1, Message);
                else
                    return string.Format("Row {0}: {1}", Row + 1, Message);
            }
        }

        public override string ToString()
        {
            return DisplayMessage;
        }

        public SheetError(int row, string message)
        {
            this.Row = row;
            this.Message = message;
        }

        public SheetError(int row, int column, string message)
            : this(row, message)
        {
            this.Column = column;
        }

    }
}
