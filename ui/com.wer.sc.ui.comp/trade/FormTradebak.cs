using com.wer.sc.data.market;
using com.wer.sc.data.navigate;
using com.wer.sc.data.reader;
using com.wer.sc.plugin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace com.wer.sc.ui.comp.trade
{
    public partial class FormTradeBak : Form
    {
        private IPlugin_MarketTrader trader;

        private IDataNavigate dataNavigater;

        public FormTradeBak(IPlugin_MarketTrader trader, IDataNavigate dataNavigater)
        {
            InitializeComponent();
            this.trader = trader;
            this.trader.OnReturnOrder = OnReturnOrder;
            this.trader.OnReturnTrade = OnReturnTrade;
            this.trader.OnReturnInvestorPosition = OnReturnInvestorPosition;

            this.dataNavigater = dataNavigater;
            this.dataNavigater.OnNavigateTo += RealTimeDataReader_OnNavigateTo;

            this.tbCode.Text = dataNavigater.Code;
            this.tbPrice.Text = dataNavigater.Price.ToString();
        }

        private void RealTimeDataReader_OnNavigateTo(object sender, DataNavigateEventArgs e)
        {

        }

        public void OnReturnOrder(object sender, ref OrderInfo order)
        {
            if (order.ExecType == ExecType.New)
            {
                this.AddOrder(order);
            }
            else if (order.ExecType == ExecType.Trade ||
                order.ExecType == ExecType.Cancelled)
            {
                this.RemoveOrder(order.OrderID);
            }
        }

        public void OnReturnTrade(object sender, ref TradeInfo trade)
        {
            //this.AddTrade(trade);
            this.trader.QueryPosition();
        }

        public void OnReturnInvestorPosition(object sender, ref IList<PositionInfo> positions)
        {
            this.ClearPositions();
            this.AddPositions(positions);
        }

        private void btOrderUp_MouseUp(object sender, MouseEventArgs e)
        {
            string code = tbCode.Text.Trim();
            int mount = int.Parse(tbMount.Text.Trim());
            float price = float.Parse(tbPrice.Text.Trim());
            double orderTime = 0;
            OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Open, price, mount, OrderSide.Buy);
            this.trader.SendOrder(order);
        }

        private void btOrderDown_MouseUp(object sender, MouseEventArgs e)
        {
            string code = tbCode.Text.Trim();
            int mount = int.Parse(tbMount.Text.Trim());
            float price = float.Parse(tbPrice.Text.Trim());
            double orderTime = 0;
            OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Open, price, mount, OrderSide.Sell);
            this.trader.SendOrder(order);
        }

        private void btClose_MouseUp(object sender, MouseEventArgs e)
        {
            //string code = tbCode.Text.Trim();
            //int mount = int.Parse(tbMount.Text.Trim());
            //float price = float.Parse(tbPrice.Text.Trim());
            //double orderTime = 0;
            //OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Open, price, mount, OrderSide.Buy);
            //this.trader.SendOrder(order);
        }

        #region GridOpenInterest

        private List<PositionInfo> positions = new List<PositionInfo>();

        public void AddPosition(PositionInfo position)
        {
            this.positions.Add(position);
            this.AddPositionToGrid(position);
        }

        public void AddPositions(IList<PositionInfo> positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                AddPosition(positions[i]);
            }
        }

        public void RemovePosition(string codeId, PositionSide positionSide)
        {
            int rowNum = IndexPosition(codeId, positionSide);
            this.positions.RemoveAt(rowNum);
            this.gridPosition.Rows.RemoveAt(rowNum);
        }

        public void ClearPositions()
        {
            this.positions.Clear();
            this.gridPosition.Rows.Clear();
        }

        private int IndexPosition(string codeId, PositionSide positionSide)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                PositionInfo positionInfo = positions[i];
                if (positionInfo.InstrumentID.Equals(codeId) && positionInfo.Side == positionSide)
                    return i;
            }
            return -1;
        }

        private void AddPositionToGrid(PositionInfo positionInfo)
        {
            int rowNum = this.gridPosition.Rows.Add();
            DataGridViewRow row = this.gridPosition.Rows[rowNum];
            row.Cells[columnPositionCode.Name].Value = positionInfo.InstrumentID;
            row.Cells[columnPositionDirection.Name].Value = positionInfo.Side;
            row.Cells[columnPositionMount.Name].Value = positionInfo.Position;
            row.Cells[columnPositionCurrentMount.Name].Value = positionInfo.Position;
            row.Cells[columnPositionPrice.Name].Value = positionInfo.PositionCost;
            //row.Cells[columnPositionEarn.Name].Value = positionInfo.PositionCost;
            //row.Cells[columnPositionEarnPercent.Name].Value = positionInfo.InstrumentID;
            //row.Cells[columnAsset.Name].Value = "";
        }

        private void gridPosition_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
        #endregion

        #region GridOrder        

        private List<OrderInfo> orders = new List<OrderInfo>();

        public void AddOrder(OrderInfo order)
        {
            this.orders.Add(order);
            this.AddOrderToGrid(order);
        }

        public void RemoveOrder(string id)
        {
            int rowNum = Index(id);
            this.orders.RemoveAt(rowNum);
            this.gridOrder.Rows.RemoveAt(rowNum);
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
            int rowNum = this.gridOrder.Rows.Add();
            DataGridViewRow row = this.gridOrder.Rows[rowNum];
            row.Cells[columnOrderID.Name].Value = order.OrderID;
            row.Cells[columnOrderTime.Name].Value = order.OrderTime;
            row.Cells[columnOrderCode.Name].Value = order.Instrumentid;
            //row.Cells[columnState.Name].Value = order.;
            row.Cells[columnOrderDirection.Name].Value = order.Direction;
            row.Cells[columnOpenClose.Name].Value = order.OpenClose;
            row.Cells[columnOrderPrice.Name].Value = order.Price;
            row.Cells[columnOrderMount.Name].Value = order.Volume;
        }

        private void gridOrder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gridOrder.SelectedRows.Count == 0)
                return;

            DialogResult result = MessageBox.Show("是否取消该委托", "取消委托", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                DataGridViewRow row = gridOrder.SelectedRows[0];
                string orderId = (string)row.Cells[columnOrderID.Name].Value;
                this.trader.CancelOrder(orderId);
            }
        }

        #endregion

        #region GridTrade

        private List<TradeInfo> trades = new List<TradeInfo>();

        public void AddTrade(TradeInfo trade)
        {
            this.trades.Add(trade);
            this.AddTradeToGrid(trade);
        }

        public void RemoveTrade(string id)
        {
            int rowNum = IndexTrade(id);
            this.trades.RemoveAt(rowNum);
            this.gridTrade.Rows.RemoveAt(rowNum);
        }

        private int IndexTrade(string id)
        {
            for (int i = 0; i < trades.Count; i++)
            {
                TradeInfo orderInfo = trades[i];
                if (orderInfo.TradeID.Equals(id))
                    return i;
            }
            return -1;
        }

        private void AddTradeToGrid(TradeInfo order)
        {
            int rowNum = this.gridTrade.Rows.Add();
            DataGridViewRow row = this.gridTrade.Rows[rowNum];
            //row.Cells[columntr.Name].Value = order.OrderID;
            //row.Cells[columnOrderTime.Name].Value = order.OrderTime;
            //row.Cells[columnOrderCode.Name].Value = order.Instrumentid;
            ////row.Cells[columnState.Name].Value = order.;
            //row.Cells[columnOrderDirection.Name].Value = order.Direction;
            //row.Cells[columnOpenClose.Name].Value = order.OpenClose;
            //row.Cells[columnOrderPrice.Name].Value = order.Price;
            //row.Cells[columnOrderMount.Name].Value = order.Volume;
        }

        #endregion   
    }
}
