using com.wer.sc.data;
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
    public partial class FormTrade : Form
    {
        private IPlugin_MarketTrader trader;

        private IDataNavigate dataNavigater;

        public FormTrade(IPlugin_MarketTrader trader, IDataNavigate dataNavigater)
        {
            InitializeComponent();

            this.trader = trader;
            this.trader.OnReturnOrder = OnReturnOrder;
            this.trader.OnReturnTrade = OnReturnTrade;
            this.trader.OnRspInvestorPosition = OnReturnInvestorPosition;
            this.trader.OnRspOrder = OnRspOrders;
            this.trader.OnRspTrade = OnRspTrade;

            this.dataNavigater = dataNavigater;
            this.dataNavigater.OnNavigateTo += RealTimeDataReader_OnNavigateTo;

            this.tbCode.Text = dataNavigater.Code;
            this.numberPrice.Value = dataNavigater.Price;
            this.numberPrice.OnStateChange += NumberPrice_OnStateChange;
            this.numberPrice.MinPeriod = 1;
            this.numberPrice.NormalText = "当前价";
            this.numberMount.IsInputState = true;
            this.numberMount.MinValue = 0;
            this.numberMount.MinPeriod = 1;

            this.trader.QueryPosition();
            this.trader.QueryOrders();
            this.trader.QueryTrades();
        }

        #region 当前属性

        private string CurrentCode
        {
            get { return tbCode.Text.Trim(); }
        }

        private int CurrentMount
        {
            get { return (int)numberMount.Value; }
        }

        private float CurrentPrice
        {
            get
            {
                return (float)numberPrice.Value;
            }
        }

        private PositionInfo SelectedPosition
        {
            get
            {
                if (gridPosition.SelectedRows.Count == 0)
                    return null;
                DataGridViewRow row = gridPosition.SelectedRows[0];

                for (int i = 0; i < gridPosition.Rows.Count; i++)
                {
                    if (i >= positions.Count)
                        return null;
                    if (gridPosition.Rows[i] == row)
                    {
                        return positions[i];
                    }
                }
                return null;
            }
        }

        #endregion

        private void NumberPrice_OnStateChange(object sender, bool state)
        {
            if (state)
                numberPrice.Value = dataNavigater.Price;
        }

        //private void GridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        //{
        //    DataGridView gridView = (DataGridView)sender;
        //    if (e.RowIndex == gridView.Rows.Count - 1)
        //        return;
        //    DataGridViewRow dgr = gridView.Rows[e.RowIndex];
        //    try
        //    {
        //        using (SolidBrush brush = new SolidBrush(Color.DarkGray))
        //        {
        //            for (int i = 0; i < gridView.SelectedRows.Count; i++)
        //            {
        //                if (gridView.SelectedRows[i] == dgr)
        //                    e.Graphics.FillRectangle(brush, e.RowBounds);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void RealTimeDataReader_OnNavigateTo(object sender, DataNavigateEventArgs e)
        {
            this.tbCode.Text = e.Code;
        }

        public void OnReturnOrder(object sender, ref OrderInfo order)
        {
            if (order.ExecType == ExecType.New)
            {
                this.AddOrder(order);
            }
            else if (order.ExecType == ExecType.Cancelled)
            {
                this.RemoveOrder(order.OrderID);
            }
            else if (order.ExecType == ExecType.Trade)
            {
                if (order.LeavesQty == 0)
                    this.RemoveOrder(order.OrderID);
                else
                {
                    this.RefreshOrder(order.OrderID);
                }
            }
        }

        public void OnReturnTrade(object sender, ref TradeInfo trade)
        {
            this.trader.QueryPosition();
        }

        public void OnRspOrders(object sender, ref IList<OrderInfo> orders)
        {
            foreach (OrderInfo order in orders)
            {
                this.AddOrder(order);
            }
        }

        private void OnRspTrade(object sender, ref IList<TradeInfo> trades)
        {
            foreach (TradeInfo trade in trades)
            {
                this.AddTrade(trade);
            }
        }

        public void OnReturnInvestorPosition(object sender, ref IList<PositionInfo> positions)
        {
            this.ClearPositions();
            this.AddPositions(positions);
        }

        private void btOrderUp_MouseUp(object sender, MouseEventArgs e)
        {
            string code = CurrentCode;
            int mount = CurrentMount;
            float price = CurrentPrice;
            double orderTime = dataNavigater.Time;
            OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Open, price, mount, OrderSide.Buy);
            this.trader.SendOrder(order);
        }

        private void btOrderDown_MouseUp(object sender, MouseEventArgs e)
        {
            string code = CurrentCode;
            int mount = CurrentMount;
            float price = CurrentPrice;
            double orderTime = dataNavigater.Time;
            OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Open, price, mount, OrderSide.Sell);
            this.trader.SendOrder(order);
        }

        private void SendClose()
        {
            PositionInfo position = SelectedPosition;
            if (position == null)
            {
                MessageBox.Show("请选中要平仓的持仓");
                return;
            }

            string code = CurrentCode;
            int mount = CurrentMount;
            float price = CurrentPrice;
            if (price == 0)
            {
                ITickData tickData = dataNavigater.GetTickData();
                if (position.Side == PositionSide.Long)
                    price = tickData.BuyPrice;
                else
                    price = tickData.SellPrice;
            }
            double orderTime = dataNavigater.Time;

            //OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Close, price, mount, position.Side == PositionSide.Long ? OrderSide.Buy : OrderSide.Sell);
            OrderInfo order = new OrderInfo(code, orderTime, OpenCloseType.Close, price, mount, position.Side == PositionSide.Long ? OrderSide.Sell : OrderSide.Buy);
            this.trader.SendOrder(order);
        }

        private void btClose_MouseUp(object sender, MouseEventArgs e)
        {
            SendClose();
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
            if (gridPosition.SelectedRows.Count == 0)
                return;
            PositionInfo position = SelectedPosition;
            if (position == null)
                return;
            DialogResult result = MessageBox.Show("是否按市价平掉" + position.InstrumentID + "的所有" + position.Side + "持仓", "确认平仓", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                SendClose();
            }
        }

        private void gridPosition_MouseClick(object sender, MouseEventArgs e)
        {
            PositionInfo position = SelectedPosition;
            if (position != null)
                this.numberMount.Value = position.Position;
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
            if (rowNum < 0)
                return;
            this.orders.RemoveAt(rowNum);
            this.gridWaitingOrder.Rows.RemoveAt(rowNum);
        }

        public void RefreshOrder(string id)
        {
            int rowNum = Index(id);
            if (rowNum < 0)
                return;
            DataGridViewRow row = this.gridWaitingOrder.Rows[rowNum];
            OrderToRow(orders[rowNum], row);
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
            int rowNum = this.gridWaitingOrder.Rows.Add();
            DataGridViewRow row = this.gridWaitingOrder.Rows[rowNum];
            OrderToRow(order, row);
        }

        private void OrderToRow(OrderInfo order, DataGridViewRow row)
        {
            row.Cells[columnOrderID.Name].Value = order.OrderID;
            row.Cells[columnOrderTime.Name].Value = order.OrderTime;
            row.Cells[columnOrderCode.Name].Value = order.Instrumentid;
            row.Cells[columnOrderState.Name].Value = order.ExecType;
            row.Cells[columnOrderDirection.Name].Value = order.Direction;
            row.Cells[columnOpenClose.Name].Value = order.OpenClose;
            row.Cells[columnOrderPrice.Name].Value = order.Price;
            row.Cells[columnOrderMount.Name].Value = order.Volume;
            row.Cells[columnOrderCanCancelCount.Name].Value = order.LeavesQty;
            row.Cells[columnOrderTradeCount.Name].Value = order.CumQty;
        }

        private void btCurrentPrice_Click(object sender, EventArgs e)
        {
            this.numberPrice.NormalText = "当前价";
        }

        private void gridOrder_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (gridWaitingOrder.SelectedRows.Count == 0)
                return;
            DialogResult result = MessageBox.Show("是否取消该委托", "取消委托", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                DataGridViewRow row = gridWaitingOrder.SelectedRows[0];
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

        private void AddTradeToGrid(TradeInfo trade)
        {
            int rowNum = this.gridTrade.Rows.Add();
            DataGridViewRow row = this.gridTrade.Rows[rowNum];

            row.Cells[columnTradeID.Name].Value = trade.TradeID;
            row.Cells[columnTradeCode.Name].Value = trade.InstrumentID;
            row.Cells[columnTradeTime.Name].Value = trade.Time;
            row.Cells[columnTradePrice.Name].Value = trade.Price;
            row.Cells[columnTradeMount.Name].Value = trade.Qty;
            row.Cells[columnTradeDirection.Name].Value = trade.Side;
            row.Cells[columnTradeOpenClose.Name].Value = trade.OpenClose;
        }

        #endregion   
    }
}