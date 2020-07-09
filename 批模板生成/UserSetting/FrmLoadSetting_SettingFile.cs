using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using 批模板生成.Util;

namespace 批模板生成.UserSetting
{
	/// <summary>
	/// 单个预设文件
	/// </summary>
	public sealed class FrmLoadSetting_SettingFile:AnimateControl
	{
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawString(Title,Font,foreBrush,e.ClipRectangle, stringFormat);
		}
		private StringFormat stringFormat=new StringFormat()
		{
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Center
		};

		public FrmLoadSetting_SettingFile(Color defaultColor, RectangleF defaultRect)
		{
			DefaultColor = defaultColor;
			DefaultRect = defaultRect;
		}
		public Color DefaultColor;
		public RectangleF DefaultRect;
		public RectangleF OffsetRect;

		public void Animate(int interval,int delay=0)
		{
			var tmp=new RectangleF(DefaultRect.Location,DefaultRect.Size);
			tmp.Offset(OffsetRect.Location);
			tmp.Inflate(OffsetRect.Size);
			Animate(interval,delay,tmp,DefaultColor);
		}
		/// <summary>
		/// 预设的标题
		/// </summary>
		public string Title { get; set; }
	
		protected override void OnPaintBackground(PaintEventArgs pevent)
		{
			pevent.Graphics.FillRectangle(bckBrush,pevent.ClipRectangle);
		}

		private int lastActiveTime = 0;
		protected override void OnMouseEnter(EventArgs e)
		{
			int nowTime = Environment.TickCount;
			if (nowTime - lastActiveTime < 1000) return;
			lastActiveTime = nowTime;
			OffsetRect.X = 50;
			Animate(500);
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			OffsetRect.X =0;
			Animate(500);
			base.OnMouseLeave(e);
		}

		public Action<string> SettingSelect;
		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			SettingSelect?.Invoke(Title);
			base.OnMouseDoubleClick(e);
		}
	}
}
