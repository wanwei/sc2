using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.trade
{
   public class BindGridOrder
    {
        private DataGridView dataGridView1;

        private List<OrderInfo> orders = new List<OrderInfo>();

        public DataGridView GridViewOrder
        {
            get { return this.GridViewOrder; }
        }

        public BindGridOrder()
        {
            
        }

        public void AddOrder(OrderInfo order)
        {
            this.orders.Add(order);
            this.AddOrderToGrid(order);
        }

        public void RemoveOrder(string id)
        {
            int rowNum = Index(id);
            this.orders.RemoveAt(rowNum);
            this.dataGridView1.Rows.RemoveAt(rowNum);
        }

        private int Index(string id)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                OrderInfo orderInfo = orders[i];
                if (orderInfo.OrderID.Equals(id))
                    return i;
            }
            return -1;
        }

        private void AddOrderToGrid(OrderInfo order)
        {
            int rowNum = this.dataGridView1.Rows.Add();
            DataGridViewRow row = this.dataGridView1.Rows[rowNum];
            //row.Cells[columnOrderID.Name].Value = order.OrderID;
            //row.Cells[columnTime.Name].Value = order.OrderTime;
            //row.Cells[columnCode.Name].Value = order.Instrumentid;
            ////row.Cells[columnState.Name].Value = order.;
            //row.Cells[columnDirection.Name].Value = order.Direction;
            //row.Cells[columnOpenClose.Name].Value = order.OpenClose;
            //row.Cells[columnOrderPrice.Name].Value = order.Price;
            //row.Cells[columnOrderMount.Name].Value = order.Volume;
        }
    }
}
