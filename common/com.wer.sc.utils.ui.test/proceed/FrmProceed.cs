using com.wer.sc.utils.update;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.utils.ui.update
{
    public partial class FrmProceed : Form
    {
        public FrmProceed()
        {
            InitializeComponent();

            controlDataProceed1.BeforeProceedStart += ControlDataProceed1_BeforeProceedStart;
        }

        private void ControlDataProceed1_BeforeProceedStart()
        {
            MockUpdateStepGetter mockProceed = new MockUpdateStepGetter();
            mockProceed.Steps.Add(new MockStepImpl(int.Parse(tbTime1.Text), tbDesc1.Text));
            mockProceed.Steps.Add(new MockStepImpl(int.Parse(tbTime2.Text), tbDesc2.Text));
            mockProceed.Steps.Add(new MockStepImpl(int.Parse(tbTime3.Text), tbDesc3.Text));
            mockProceed.Steps.Add(new MockStepImpl(int.Parse(tbTime4.Text), tbDesc4.Text));
            controlDataProceed1.DataProceed = mockProceed;
        }
    }

    internal class MockUpdateStepGetter : IUpdateStepGetter
    {
        private List<IStep> steps = new List<IStep>();

        public List<IStep> Steps
        {
            get
            {
                return steps;
            }
        }

        public List<IStep> GetSteps()
        {
            Thread.Sleep(5 * 1000);
            //List<Step> steps = new List<Step>();
            //steps.Add(new MockStepImpl(5, "正在执行操作1"));
            //steps.Add(new MockStepImpl(15, "正在执行操作2"));
            //steps.Add(new MockStepImpl(25, "正在执行操作3"));
            //steps.Add(new MockStepImpl(20, "正在执行操作4"));
            return steps;
        }
    }

    internal class MockStepImpl : IStep
    {
        private int step;

        private string txt;
        public MockStepImpl(int step, string txt)
        {
            this.step = step;
            this.txt = txt;
        }

        public int ProgressStep
        {
            get
            {
                return this.step;
            }
        }

        public String StepDesc
        {
            get
            {
                return txt;
            }
        }

        public string Proceed()
        {
            Thread.Sleep(step * 1000);
            return "执行了" + step + "步";
        }
    }
}
