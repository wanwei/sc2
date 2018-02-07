using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data
{
    /// <summary>
    /// 主力合约信息类
    /// </summary>
    public class MainContractInfo : IComparable<MainContractInfo>, ITextExchange
    {
        private string variety;

        private int start;

        private int end;

        private string code;

        public string Variety
        {
            get
            {
                return variety;
            }

            set
            {
                variety = value;
            }
        }

        public string Code
        {
            get
            {
                return code;
            }

            set
            {
                code = value;
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

        public int Last
        {
            get { return End - Start; }
        }

        public bool IsBetween(int date)
        {
            return date >= Start && date <= End;
        }

        public int CompareTo(MainContractInfo other)
        {
            if (other == null)
                return 1;
            int compareVariety = this.Variety.CompareTo(other.Variety);
            if (compareVariety != 0)
                return compareVariety;
            if (this.start == other.start)
                return 0;
            if (this.start > other.start)
                return 1;
            return -1;
        }

        public void CopyFrom(MainContractInfo contractInfo)
        {
            this.code = contractInfo.code;
            this.variety = contractInfo.variety;
            this.start = contractInfo.start;
            this.end = contractInfo.end;            
        }

        public override string ToString()
        {
            return SaveToString();
        }

        public string SaveToString()
        {
            return this.variety + "," + this.start + "," + this.end + "," + this.code;
        }

        public void LoadFromString(string content)
        {
            string[] arr = content.Split(',');
            this.variety = arr[0];
            this.start = int.Parse(arr[1]);
            this.end = int.Parse(arr[2]);
            this.code = arr[3];
        }
    }
}