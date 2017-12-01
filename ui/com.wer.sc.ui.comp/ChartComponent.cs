using com.wer.sc.data;
using com.wer.sc.data.datapackage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp
{
    public class ChartComponent : IChartComponent
    {
        private IChartComponentController controller;

        private ChartComponentInfo data;

        private IChartComponentView view;

        public ChartComponent(IDataPackage_Code dataPackage, Control control)
        {
            this.data = new ChartComponentInfo(dataPackage);
            this.view = new ChartComponentView(control, data);
            //this.controller = new ChartComponentController()
        }

        public IChartComponentController Controller
        {
            get
            {
                return controller;
            }
        }

        public IChartComponentInfo Data
        {
            get
            {
                return this.data;
            }
        }

        public IChartComponentView View
        {
            get
            {
                return view;
            }
        }
    }
}
