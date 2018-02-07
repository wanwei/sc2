using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.datapackage
{
    /// <summary>
    /// 
    /// </summary>
    public class CodePackageInfo
    {
        private bool choosedByMainContract;

        private bool choosedByCatelog;

        private List<string> codes = new List<string>();

        private int start;

        private int end;

        public bool ChoosedByMainContract
        {
            get
            {
                return choosedByMainContract;
            }

            set
            {
                choosedByMainContract = value;
            }
        }

        public bool ChoosedByCatelog
        {
            get
            {
                return choosedByCatelog;
            }

            set
            {
                choosedByCatelog = value;
            }
        }

        public List<string> Codes
        {
            get
            {
                return codes;
            }
        }

        public int Start
        {
            get
            {
                return start;
            }

            set
            {
                start = value;
            }
        }

        public int End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(start).Append("-").Append(end).Append(",");
            sb.Append(choosedByCatelog).Append(",").Append(choosedByMainContract).Append("\r\n");
            for (int i = 0; i < codes.Count; i++)
            {
                sb.Append(codes[i]).Append(",");
            }
            return sb.ToString();
        }
    }
}
