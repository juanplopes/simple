using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Simple.IO
{
    public class StringUtils
    {
        public static string RemoveDiacritics(string s)
        {
            String normalizedString = s.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < normalizedString.Length; i++)
            {
                Char c = normalizedString[i];
                if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                    stringBuilder.Append(c);
            }

            return stringBuilder.ToString();
        } 
    }
}
