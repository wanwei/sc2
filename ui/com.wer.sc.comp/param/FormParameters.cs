using com.wer.sc.utils;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.param
{
    public partial class FormParameters : Form
    {
        private List<IParameterControl> parameterControls = new List<IParameterControl>();

        private IParameters parameters;

        public IParameters Parameters
        {
            get
            {
                return parameters;
            }
        }

        public FormParameters(IParameters parameters)
        {
            InitializeComponent();
            this.parameters = parameters;
            Init(parameters);
        }

        private void Init(IParameters parameters)
        {
            this.Height = 10 + 30 * parameters.Count + 100;
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
                this.panel1.Controls.Add(lb);

                IParameterControl paramcontrol = ParameterControlFactory.Create(param);
                this.parameterControls.Add(paramcontrol);
                Control control = paramcontrol.GetControl();
                control.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                control.Width = 150;
                control.Location = new Point(x + 120, y);
                this.panel1.Controls.Add(control);
                y += 30;
            }
        }

        private Control CreateControl(IParameter parameter)
        {
            TextBox box = new TextBox();
            box.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            box.Width = 150;
            return box;
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < parameterControls.Count; i++)
                {
                    IParameterControl pc = parameterControls[i];
                    parameters.SetParameterValue(pc.GetKey(), pc.GetValue());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }

    class ParameterControlFactory
    {
        public static IParameterControl Create(IParameter parameter)
        {
            if (parameter.Options != null)
                return new ParameterControl_Option(parameter);
            switch (parameter.ParameterType)
            {
                case ParameterType.BOOLEAN:
                    return new ParameterControl_Boolean(parameter);
                case ParameterType.FLOAT:
                    return new ParameterControl_Float(parameter);
                case ParameterType.INTEGER:
                    return new ParameterControl_Int(parameter);
                case ParameterType.STRING:
                    return new ParameterControl_String(parameter);
            }
            return null;
        }
    }

    interface IParameterControl
    {
        string GetKey();

        /// <summary>
        /// 得到控件
        /// </summary>
        /// <returns></returns>
        Control GetControl();

        /// <summary>
        /// 得到控件值
        /// </summary>
        /// <returns></returns>
        object GetValue();
    }

    abstract class ParameterControl_Abstract : IParameterControl
    {
        IParameter parameter;

        public ParameterControl_Abstract(IParameter parameter)
        {
            this.parameter = parameter;
        }

        public abstract Control GetControl();

        public string GetKey()
        {
            return parameter.Key;
        }

        public abstract object GetValue();
    }

    class ParameterControl_Option : ParameterControl_Abstract
    {
        ComboBox comboBox;

        public ParameterControl_Option(IParameter parameter) : base(parameter)
        {
            this.comboBox = new ComboBox();
            this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            IParameterOptions options = parameter.Options;
            List<IParameterOption> optionList = options.Options;
            int selectIndex = 0;
            for(int i = 0; i < optionList.Count; i++)
            {
                IParameterOption option = optionList[i];
                this.comboBox.Items.Add(option.Value);
                if (option.Value.Equals(parameter.Value))
                    selectIndex = i;
            }
            comboBox.SelectedIndex = selectIndex;
        }

        public override Control GetControl()
        {
            return comboBox;
        }

        public override object GetValue()
        {
            return comboBox.SelectedItem;
        }
    }

    class ParameterControl_Boolean : ParameterControl_Abstract
    {
        ComboBox comboBox;

        public ParameterControl_Boolean(IParameter parameter) : base(parameter)
        {
            this.comboBox = new ComboBox();
            this.comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this.comboBox.Items.Add("是");
            this.comboBox.Items.Add("否");
            bool b = (bool)parameter.Value;
            comboBox.SelectedIndex = b ? 0 : 1;
        }

        public override Control GetControl()
        {
            return comboBox;
        }

        public override object GetValue()
        {
            return comboBox.SelectedIndex == 0;
        }
    }

    class ParameterControl_Int : ParameterControl_Abstract
    {

        TextBox comboBox;

        public ParameterControl_Int(IParameter parameter) : base(parameter)
        {
            this.comboBox = new TextBox();
            this.comboBox.Text = StringUtils.obj2Str(parameter.Value, "");
        }

        public override Control GetControl()
        {
            return comboBox;
        }

        public override object GetValue()
        {
            return int.Parse(comboBox.Text);
        }
    }

    class ParameterControl_Float : ParameterControl_Abstract
    {
        TextBox comboBox;

        public ParameterControl_Float(IParameter parameter) : base(parameter)
        {
            this.comboBox = new TextBox();
            this.comboBox.Text = StringUtils.obj2Str(parameter.Value, "");
        }

        public override Control GetControl()
        {
            return comboBox;
        }

        public override object GetValue()
        {
            return float.Parse(comboBox.Text);
        }
    }

    class ParameterControl_String : ParameterControl_Abstract
    {
        TextBox textBox;

        public ParameterControl_String(IParameter parameter) : base(parameter)
        {
            this.textBox = new TextBox();
            this.textBox.Text = StringUtils.obj2Str(parameter.Value, "");
        }

        public override Control GetControl()
        {
            return textBox;
        }

        public override object GetValue()
        {
            return textBox.Text;
        }
    }

}