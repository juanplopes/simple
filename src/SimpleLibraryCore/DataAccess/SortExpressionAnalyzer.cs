using System;
using System.Collections.Generic;
using System.Text;
using Simple.Filters;

namespace Simple.DataAccess
{
    public class SortExpressionAnalyzer
    {
        public static OrderByCollection Analyze(string sortExpression)
        {
            string[] sortParameters = sortExpression.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            OrderByCollection col = new OrderByCollection();
            foreach (string s in sortParameters)
            {
                string[] ss = s.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (ss.Length == 1)
                {
                    col = col.Asc(ss[0].Trim());
                }
                else if (ss.Length == 2)
                {
                    if (ss[1].Trim().Equals("ASC", StringComparison.OrdinalIgnoreCase))
                    {
                        col = col.Asc(ss[0].Trim());
                    }
                    else if (ss[1].Trim().Equals("DESC", StringComparison.OrdinalIgnoreCase))
                    {
                        col = col.Desc(ss[0].Trim());
                    }
                    else
                    {
                        throw new InvalidOperationException("Invalid sort direction: " + ss[1]);
                    }
                }
                else
                {
                    throw new InvalidOperationException("Invalid sort expression");
                }
            }

            return col;
        }
    }
}

