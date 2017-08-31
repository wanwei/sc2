using com.wer.sc.utils;
using com.wer.sc.utils.param;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.param
{

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
            for (int i = 0; i < optionList.Count; i++)
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
