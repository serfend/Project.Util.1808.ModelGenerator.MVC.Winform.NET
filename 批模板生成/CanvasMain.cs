using DotNet4.Utilities.UtilDrawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 批模板生成
{
	public class CanvasMain:Panel
	{
		private const string defaultText="";
		public Canvas canvas;
		protected override void OnPaintBackground(PaintEventArgs e)
		{
		}
		protected override void OnPaint(PaintEventArgs e)
		{

			//TODO 绘制主要
			BufferedGraphicsContext currentContext = BufferedGraphicsManager.Current;

			BufferedGraphics myBuffer = currentContext.Allocate(e.Graphics, e.ClipRectangle);

			Graphics g = myBuffer.Graphics;

			g.Clear(this.BackColor);
			g.CompositingQuality = CompositingQuality.HighQuality;
			//g.ScaleTransform ( (float)canvas.ScalePercent,(float)canvas.ScalePercent);
			for (int x = 0; x < Width; x += 20)
			{
				for (int y = x % 40 == 0 ? 0 : 20; y < Height; y += 40)
				{
					g.FillRectangle(Brushes.LightGray, x, y, 20, 20);
				}
			}
			g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
			var strSize = e.Graphics.MeasureString(defaultText, Font);
			g.DrawString(defaultText, Font, Brushes.Gray, (Width - strSize.Width) * 0.5f, (Height - strSize.Height) * 0.5f);
			if (BackgroundImage == null)
			{

			}
			else
			{
				g.DrawImage(BackgroundImage, 0, 0, Width, Height);
			}
			foreach (var element in canvas.List)
			{
				element.DrawToCanvas(g, false);
			}
			foreach (var element in canvas.List) element.HoverPaint(g);

			myBuffer.Render(e.Graphics);  //呈现图像至关联的Graphics

			myBuffer.Dispose();
			g.Dispose();

		}
	}
}
