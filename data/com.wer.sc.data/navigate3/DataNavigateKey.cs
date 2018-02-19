using System;

namespace com.wer.sc.data.navigate
{

    class DataNavigateKey
    {
        private string code;
        private KLinePeriod period;
        private float time;

        public DataNavigateKey(String code, KLinePeriod period, float time)
        {
            this.code = code;
            this.period = period;
            this.time = time;
        }

        public string Code
        {
            get
            {
                return code;
            }
        }

        public KLinePeriod Period
        {
            get
            {
                return period;
            }
        }

        public float Time
        {
            get
            {
                return time;
            }
        }
    }
}
