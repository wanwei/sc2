using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.plugin.cnstock.historydata.download
{
    public class HttpParser
    {
        public static HttpComp_Select ParseSelectByName(string content, string name)
        {
            string tag = "<select name=\"" + name + "\">";
            int startIndex = content.IndexOf(tag);
            int lastIndex = content.IndexOf("</select>", startIndex);

            HttpComp_Select selectComp = new HttpComp_Select();
            int index = startIndex;
            while (index < lastIndex && index > 0)
            {
                string optiontag = "<option value=";
                int optionStartIndex = content.IndexOf(optiontag, index);
                if (optionStartIndex >= lastIndex)
                    break;
                int valueStartIndex = optionStartIndex + optiontag.Length + 1;
                int valueEndIndex = content.IndexOf("\"", valueStartIndex + 1);
                string value = content.Substring(valueStartIndex, valueEndIndex - valueStartIndex);
                selectComp.Values.Add(value);

                int textStartIndex = content.IndexOf(">", valueEndIndex) + 1;
                int textEndIndex = content.IndexOf("<", textStartIndex);
                string text = content.Substring(textStartIndex, textEndIndex - textStartIndex);
                selectComp.Texts.Add(text);
                index = textEndIndex;
            }
            return selectComp;
        }

        public static HttpComp_Table ParseTableById(string content, string id)
        {
            string tag = "<table id=\"" + id;
            int startIndex = content.IndexOf(tag);
            if (startIndex < 0)
                return null;
            int tableEndIndex = content.IndexOf("</table>", startIndex);
            HttpComp_Table tableComp = new HttpComp_Table();
            int index = startIndex;
            while (index < tableEndIndex && index > 0)
            {
                int endIndex;
                List<string> row = ParseTableRow(content, index, tableEndIndex, out endIndex);
                if (row != null)
                    tableComp.Rows.Add(row);
                index = endIndex;
            }
            return tableComp;
        }

        private static List<string> ParseTableRow(string content, int startIndex, int tableEndIndex, out int endIndex)
        {
            string rowEndTag = "</tr>";
            endIndex = content.IndexOf(rowEndTag, startIndex);
            if (endIndex < 0)
                return null;
            endIndex += rowEndTag.Length;
            if (endIndex > tableEndIndex)
                return null;
            //Console.WriteLine(content.Substring(startIndex, endIndex - startIndex));
            //Console.WriteLine("-----------------------------");
            List<string> arr = new List<string>();
            int index = startIndex;
            while (index < endIndex)
            {
                string tdStartTag = "<td";
                string tdEndTag = "</td>";
                int tdStart = content.IndexOf(tdStartTag, index);
                if (tdStart < 0 || tdStart > endIndex)
                    break;
                int tdEnd = content.IndexOf(tdEndTag, index);
                //if (tdEnd < tdStart)
                //    break;
                int realTdStart = content.IndexOf(">", tdStart) + 1;
                arr.Add(content.Substring(realTdStart, tdEnd - tdStart));
                index = tdEnd + tdEndTag.Length;
            }
            return arr;
        }
    }

    public class HttpComp_Select
    {
        public string Name;

        //public int SelectedIndex;

        public List<string> Values = new List<string>();

        public List<string> Texts = new List<string>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name).Append("\r\n");
            for (int i = 0; i < Values.Count; i++)
            {
                sb.Append(Values[i]).Append(",");
                sb.Append(Texts[i]).Append("\r\n");
            }
            return sb.ToString();
        }
    }

    public class HttpComp_Table
    {
        public string Id;

        public List<List<string>> Rows = new List<List<string>>();

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Id).Append("\r\n");
            for (int i = 0; i < Rows.Count; i++)
            {
                List<string> row = Rows[i];
                sb.Append(ListUtils.ToString(row)).Append("\r\n");
            }
            return sb.ToString();
        }
    }
}
