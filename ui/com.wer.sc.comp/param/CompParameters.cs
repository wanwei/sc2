using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.utils.param;

namespace com.wer.sc.comp.param
{
    public partial class CompParameters : UserControl
    {
        private List<IParameterControl> parameterControls = new List<IParameterControl>();

        private IParameters parameters;

        public CompParameters()
        {
            InitializeComponent();
        }

        public IParameters Parameters
        {
            get
            {
                for (int i = 0; i < parameterControls.Count; i++)
                {
                    IParameterControl pc = parameterControls[i];
                    if (pc != null)
                        parameters.SetParameterValue(pc.GetKey(), pc.GetValue());
                }
                return parameters;
            }

            set
            {
                parameters = value;
                if (parameters != null) { 
                    Init(parameters);
                }
            }
        }

        private void Init(IParameters parameters)
        {
            this.Controls.Clear();

            //this.Height = 10 + 30 * parameters.Count + 100;
            this.Height = 10 + 30 * parameters.Count;
            int x = 10;
            int y = 10;
            for (int i = 0; i < parameters.Count; i++)
            {
                IParameter param = parameters.GetParameter(i);
                Label lb = new Label();

                lb.Font = new System.Drawing.Font("宋体", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                lb.Location = new Point(x, y);
                lb.Text = param.Caption;
                lb.RightToLeft = RightToLeft.Yes;
                this.Controls.Add(lb);

                IParameterControl paramcontrol = ParameterControlFactory.Create(param);
                this.parameterControls.Add(paramcontrol);
                Control control = paramcontrol.GetControl();
                control.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                control.Width = 150;
                control.Location = new Point(x + 120, y);
                this.Controls.Add(control);
                y += 30;
            }
        }
    }
}
