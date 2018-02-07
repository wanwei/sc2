using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using com.wer.sc.data.market;

namespace com.wer.sc.ui.comp.trade
{
    public partial class GridTrade : UserControl
    {
        private List<OrderInfo> orders = new List<OrderInfo>();

        public GridTrade()
        {
            InitializeComponent();
        }

        private void AddOrderToGrid(OrderInfo orderInfo)
        {
            //this.dataGridView1.Rows.Add()
        }

        public void AddOrder(OrderInfo orderInfo)
        {
            
        }

        public IList<OrderInfo> GetAllOrders()
        {
            return null;
        }

        public void RemoveOrder(OrderInfo orderInfo)
        {

        }
    }
}
