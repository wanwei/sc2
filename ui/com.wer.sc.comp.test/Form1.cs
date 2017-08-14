using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.comp.test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btCandle_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Candle frm = new FrmGraphicDrawer_Candle();
            frm.ShowDialog();
        }


        private void btCandle2_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Candle2 frm = new FrmGraphicDrawer_Candle2();
            frm.ShowDialog();
        }

        private void btTest_Click(object sender, EventArgs e)
        {
            FrmTest frm = new FrmTest();
            frm.ShowDialog();
        }

        private void btAna_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_Ana frm = new FrmGraphicDrawer_Ana();
            //frm.ShowDialog();
        }

        private void btReal_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Real frm = new FrmGraphicDrawer_Real();
            frm.ShowDialog();
        }

        private void btReal2_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Real2 frm = new FrmGraphicDrawer_Real2();
            frm.ShowDialog();
        }

        private void btModel_Click(object sender, EventArgs e)
        {
            //FrmModel model = new FrmModel();
            //model.ShowDialog();
        }

        private void btAna2_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_Ana2 frm = new FrmGraphicDrawer_Ana2();
            //frm.ShowDialog();
        }

        private void btAna3_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_Ana3 frm = new FrmGraphicDrawer_Ana3();
            //frm.ShowDialog();
        }

        private void btDataLoader_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_DataLoader frm = new FrmGraphicDrawer_DataLoader();
            //frm.ShowDialog();
        }

        private void btDataNavigate_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_DataNavigate frm = new FrmGraphicDrawer_DataNavigate();
            //frm.ShowDialog();
        }

        private void btSwitch_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_Switch frm = new FrmGraphicDrawer_Switch();
            frm.ShowDialog();
        }

        private void btSwitch2_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_Switch2 frm = new FrmGraphicDrawer_Switch2();
            //frm.ShowDialog();
        }

        private void btCurrentInfo_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_CurrentInfo frm = new FrmGraphicDrawer_CurrentInfo();
            frm.ShowDialog();
        }

        private void btMain_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_Main frm = new FrmGraphicDrawer_Main();
            //frm.ShowDialog();
        }

        private void btTestRegion_Click(object sender, EventArgs e)
        {
            FrmTestRegion frm = new FrmTestRegion();
            frm.ShowDialog();
        }

        private void btCross_Click(object sender, EventArgs e)
        {
            FrmTestCross frm = new FrmTestCross();
            frm.ShowDialog();
        }

        private void btNavigate2_Click(object sender, EventArgs e)
        {
            //FrmGraphicDrawer_DataNavigate2 frm = new FrmGraphicDrawer_DataNavigate2();
            //frm.ShowDialog();
        }

        private void btCandlePriceRect_Click(object sender, EventArgs e)
        {
            FrmGraphicDrawer_PriceRect frm = new FrmGraphicDrawer_PriceRect();
            frm.ShowDialog();
        }
    }
}
