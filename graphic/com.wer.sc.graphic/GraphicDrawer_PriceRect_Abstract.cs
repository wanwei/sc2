﻿using com.wer.sc.graphic.shape;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.graphic
{
    public abstract class GraphicDrawer_PriceRect_Abstract : GraphicDrawer_Abstract, IGraphicDrawer_PriceRect
    {
        private PriceShapePainter shapePainter = new PriceShapePainter();

        private PriceRectangle priceRect;

        private List<IPriceShape> shapes = new List<IPriceShape>();

        /// <summary>
        /// 设置价格块
        /// </summary>
        public PriceRectangle PriceRect
        {
            get { return priceRect; }
            set { priceRect = value; }
        }

        public abstract IGraphicData GraphicData { get; set; }

        public override void Paint(Graphics graphic)
        {
            base.Paint(graphic);
            if (priceRect == null)
                return;
            PriceGraphicMapping mapping = new PriceGraphicMapping(DisplayRect, priceRect);

            graphic.SmoothingMode = SmoothingMode.AntiAlias;
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            for (int i = 0; i < shapes.Count; i++)
            {
                shapePainter.Paint(graphic, mapping, shapes[i]);
            }

            graphic.SmoothingMode = SmoothingMode.None;
        }

        public void DrawPriceShape(IPriceShape priceShape)
        {
            this.shapes.Add(priceShape);
        }

        public List<IPriceShape> GetAllShapes()
        {
            return this.shapes;
        }

        public void RemoveShape(IPriceShape shape)
        {
            this.shapes.Remove(shape);
        }

        public void ClearPriceShapes()
        {
            this.shapes.Clear();
        }
    }
}
