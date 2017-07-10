using com.wer.sc.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.wer.sc.data.market.impl
{
    /// <summary>
    /// 账户持仓执行类
    /// </summary>
    public class AccountPosition : IXmlExchange
    {
        private double money;

        private List<PositionInfo> allPositions = new List<PositionInfo>();

        private Dictionary<String, PositionInfo> position_Code_Long = new Dictionary<string, PositionInfo>();

        private Dictionary<String, PositionInfo> position_Code_Short = new Dictionary<string, PositionInfo>();

        private AccountFee accountFee;

        public double Money
        {
            get
            {
                return money;
            }
        }

        public AccountPosition(double money)
        {
            this.money = money;
            this.accountFee = new AccountFee();
        }

        public AccountPosition(double money, AccountFee fee)
        {
            this.money = money;
            this.accountFee = fee;
        }

        public List<PositionInfo> GetAllPositions()
        {
            return allPositions;
        }

        #region Check

        public void CheckCanSendOrder(OrderInfo orderInfo)
        {
            if (orderInfo.OpenClose == OpenCloseType.Open)
                CheckCanSendOrder_Open(orderInfo);
            else
                CheckCanSendOrder_Close(orderInfo);
        }

        private void CheckCanSendOrder_Open(OrderInfo orderInfo)
        {
            //TODO 
        }

        private void CheckCanSendOrder_Close(OrderInfo orderInfo)
        {
            string code = orderInfo.Instrumentid;
            int qty = orderInfo.Volume;
            if (orderInfo.Direction == OrderSide.Buy)
            {
                if (!position_Code_Long.ContainsKey(code))
                {
                    throw new ApplicationException("未持有" + code + "的多头仓位");
                }
                PositionInfo position = position_Code_Long[code];
                if (position.Position < qty)
                {
                    throw new ApplicationException("要平仓的仓位超过了持有" + code + "的多头仓位");
                }
            }
            else if (orderInfo.Direction == OrderSide.Sell)
            {
                if (!position_Code_Short.ContainsKey(code))
                {
                    throw new ApplicationException("未持有" + code + "的空头仓位");
                }
                PositionInfo position = position_Code_Short[code];
                if (position.Position < qty)
                {
                    throw new ApplicationException("要平仓的仓位超过了持有" + code + "的空头仓位");
                }
            }
        }

        #endregion

        /// <summary>
        /// 修改持仓量
        /// </summary>
        /// <param name="order">修改持仓对应的委托</param>
        /// <param name="mount">修改数量</param>
        public void SendPosition(OrderInfo order, int mount)
        {
            if (order.OpenClose == OpenCloseType.Open)
            {
                OpenPosition(order, mount);
            }
            else if (order.OpenClose == OpenCloseType.Close)
            {
                ClosePosition(order, mount);
            }
        }

        private void OpenPosition(OrderInfo order, int mount)
        {
            string code = order.Instrumentid;
            if (order.Direction == OrderSide.Buy)
            {
                if (position_Code_Long.ContainsKey(code))
                {
                    PositionInfo currentPosition = position_Code_Long[code];
                    int totalPosition = currentPosition.Position + mount;
                    double avgPrice = (currentPosition.Position * currentPosition.PositionCost + order.Price * mount) / totalPosition;
                    currentPosition.Position = totalPosition;
                    currentPosition.PositionCost = avgPrice;
                }
                else
                {
                    PositionInfo position = new PositionInfo(code, PositionSide.Long, mount, order.Price);
                    AddPosition(position);
                }
            }
            else
            {
                if (position_Code_Short.ContainsKey(code))
                {
                    PositionInfo currentPosition = position_Code_Short[code];
                    int totalPosition = currentPosition.Position + mount;
                    double avgPrice = (currentPosition.Position * currentPosition.PositionCost + order.Price * mount) / totalPosition;
                    currentPosition.Position = totalPosition;
                    currentPosition.PositionCost = avgPrice;
                }
                else
                {
                    PositionInfo position = new PositionInfo(code, PositionSide.Short, mount, order.Price);
                    AddPosition(position);
                }
            }
            AccountFeeInfo feeInfo = accountFee.GetAccountFee(code);
            this.money -= AccountFee.CalcMoney_Open(order, feeInfo, mount);
        }

        private void AddPosition(PositionInfo position)
        {
            this.allPositions.Add(position);
            if (position.Side == PositionSide.Long)
                this.position_Code_Long.Add(position.InstrumentID, position);
            else
                this.position_Code_Short.Add(position.InstrumentID, position);
        }

        private void RemovePosition(PositionInfo position)
        {
            this.allPositions.Remove(position);
            if (position.Side == PositionSide.Long)
                this.position_Code_Long.Remove(position.InstrumentID);
            else
                this.position_Code_Short.Remove(position.InstrumentID);
        }

        private void ClosePosition(OrderInfo order, int mount)
        {
            string code = order.Instrumentid;
            if (order.Direction == OrderSide.Buy)
            {
                if (position_Code_Long.ContainsKey(code))
                {
                    PositionInfo currentPosition = position_Code_Long[code];
                    int totalPosition = currentPosition.Position - mount;
                    if (totalPosition == 0)
                        RemovePosition(currentPosition);
                    else
                    {
                        double avgPrice = (currentPosition.Position * currentPosition.PositionCost - order.Price * mount) / totalPosition;
                        currentPosition.Position = totalPosition;
                        currentPosition.PositionCost = avgPrice;
                    }
                }
            }
            else
            {
                if (position_Code_Short.ContainsKey(code))
                {
                    PositionInfo currentPosition = position_Code_Short[code];
                    int totalPosition = currentPosition.Position - mount;
                    if (totalPosition == 0)
                    {
                        RemovePosition(currentPosition);
                    }
                    else
                    {
                        double avgPrice = (currentPosition.Position * currentPosition.PositionCost - order.Price * mount) / totalPosition;
                        currentPosition.Position = totalPosition;
                        currentPosition.PositionCost = avgPrice;
                    }
                }
            }
            AccountFeeInfo feeInfo = accountFee.GetAccountFee(code);
            this.money += AccountFee.CalcMoney_Close(order, feeInfo, mount);
        }

        public void Save(XmlElement xmlElem)
        {
            xmlElem.SetAttribute("money", money.ToString());
            for (int i = 0; i < allPositions.Count; i++)
            {
                PositionInfo orderInfo = allPositions[i];
                XmlElement elemOrder = xmlElem.OwnerDocument.CreateElement("position");
                SavePosition(orderInfo, elemOrder);
                xmlElem.AppendChild(elemOrder);
            }
        }

        private void SavePosition(PositionInfo positionInfo, XmlElement xmlElem)
        {
            xmlElem.SetAttribute("date", positionInfo.Date.ToString());
            xmlElem.SetAttribute("code", positionInfo.InstrumentID);
            xmlElem.SetAttribute("side", positionInfo.Side.ToString());
            xmlElem.SetAttribute("position", positionInfo.Position.ToString());
            xmlElem.SetAttribute("positioncost", positionInfo.PositionCost.ToString());
        }

        public void Load(XmlElement xmlElem)
        {
            this.money = double.Parse(xmlElem.GetAttribute("money"));
            XmlNodeList nodeList = xmlElem.ChildNodes;
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlNode node = nodeList[i];
                if (node is XmlElement)
                {
                    PositionInfo positionInfo = LoadPosition((XmlElement)node);
                    this.AddPosition(positionInfo);
                }
            }
        }

        private PositionInfo LoadPosition(XmlElement xmlElem)
        {
            PositionInfo positionInfo = new PositionInfo();
            positionInfo.Date = int.Parse(xmlElem.GetAttribute("date"));
            positionInfo.InstrumentID = xmlElem.GetAttribute("code");
            positionInfo.Side = (PositionSide)Enum.Parse(typeof(PositionSide), xmlElem.GetAttribute("side"));
            positionInfo.Position = int.Parse(xmlElem.GetAttribute("position"));
            positionInfo.PositionCost = double.Parse(xmlElem.GetAttribute("positioncost"));
            return positionInfo;
        }

        public override string ToString()
        {
            return XmlUtils.ToString(this);
        }
    }
}