using com.wer.sc.data.datacenter;
using com.wer.sc.mockdata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.data.store
{
    public class UriGetter
    {
        public static List<string> GetAllTestUris()
        {
            List<string> uris = new List<string>();
            string path = TestCaseManager.GetTestCasePath(typeof(UriGetter), "datacenter.config");
            DataCenterManager mgr = DataCenterManager.Create(path);
            List<DataCenterConfig> configs = mgr.GetAllConfig();
            foreach (DataCenterConfig config in configs)
            {
                uris.Add(config.Uri);
            }
            return uris;
        }
    }
}
