﻿using com.wer.sc.comp.graphic.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wer.sc.comp.graphic
{
    public abstract class GraphicDrawer_PriceRect_Abstract : GraphicDrawer_Abstract, IGraphicDrawer_PriceRect
    {
        private PriceShapePainter shapePainter = new PriceShapePainter();

        private PriceRectangle priceRect;

        private List<PriceShape> shapes = new List<PriceShape>();

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
            if (priceRect == null)
                return;
            PriceGraphicMapping mapping = new PriceGraphicMapping(DisplayRect, priceRect);
            for (int i = 0; i < shapes.Count; i++)
            {
                shapePainter.Paint(graphic, mapping, shapes[i]);
            }
        }

        public void DrawShape(PriceShape priceShape)
        {
            this.shapes.Add(priceShape);
        }

        public List<PriceShape> GetAllShapes()
        {
            return this.shapes;
        }

        public void RemoveShape(PriceShape shape)
        {
            this.shapes.Remove(shape);
        }
    }
}