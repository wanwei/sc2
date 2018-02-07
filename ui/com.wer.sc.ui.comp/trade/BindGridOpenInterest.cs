using com.wer.sc.data.market;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.trade
{
    public class BindGridOpenInterest
    {
        private DataGridView dataGridView1;

        private List<PositionInfo> orders = new List<PositionInfo>();

        public BindGridOpenInterest()
        {
            
        }

        

        public void AddOrder(PositionInfo order)
        {
            this.orders.Add(order);
            //this.AddOrderToGrid(order);
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
                PositionInfo orderInfo = orders[i];
                if (orderInfo.InstrumentID.Equals(id))
                    return i;
            }
            return -1;
        }

        private void AddOrderToGrid(TradeInfo order)
        {
            int rowNum = this.dataGridView1.Rows.Add();
            DataGridViewRow row = this.dataGridView1.Rows[rowNum];
            //row.Cells[0].Value = order.OrderID;
            //row.Cells[1].Value = order.Instrumentid;
            //row.Cells[2].Value = order.Direction;
            //row.Cells[3].Value = order.Volume;
            //row.Cells[4].Value = order.LeavesQty;
            //row.Cells[5].Value = order.AvgPx;


            //row.Cells[6].Value = o;
            //row.Cells[7].Value = order.;
        }
    }
}
