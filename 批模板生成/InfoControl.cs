using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace 批模板生成
{
	public class InfoControl:Control
	{

		public InfoSetting setting;
		public bool OnOutputModle = false;
		public InfoControl(InfoSetting setting)
		{
			this.setting = setting;
			setting.OnSettingModify = () =>
			{
				this.Invoke((EventHandler)delegate {
					RefreshLayout();
					Text = setting.TagName == "" ? setting.TargetCol : setting.TagName;
					Font = new System.Drawing.Font(setting.FontName, setting.FontSize);
					this.Invalidate();
				});
			};
			setting.OnOutputModle = (x) =>
			{
				OnOutputModle = x;
				this.Invalidate();
			};
			setting.AbortSetting = () =>
			{
				this.Dispose();
			};
			RefreshLayout();
		}
		private bool selected;
		public Action<InfoControl,bool> OnControlSelected;
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button==MouseButtons.Left)
			{
				if (!Selected)
				{
					Selected = true;
					OnControlSelected?.Invoke(this,false);
				}
				else
				{
					NativeMethods.ReleaseCapture();
					NativeMethods.SendMessage(this.Handle, (uint)0xA1, new IntPtr(2), IntPtr.Zero);
				}
				
			}
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			setting.X = Left;
			setting.Y = Top;
		}
		/// <summary>
		/// 双击设置此信息框
		/// </summary>
		/// <param name="e"></param>
		protected override void OnDoubleClick(EventArgs e)
		{
			base.OnDoubleClick(e);
			setting.SettingModify(true);
		}
		public void RefreshLayout()
		{
			this.Left = setting.X;
			this.Top = setting.Y;
			this.Width = setting.W;
			this.Height = setting.H;
			autoNewLineInfo = new List<string>();
			var g = this.CreateGraphics();
			var cstr = new StringBuilder();
			int index = 0;
			while (index < Text.Length) {
				while (g.MeasureString(cstr.ToString(), Font).Width < Width & index < Text.Length)
				{
					cstr.Append(Text[index++]);
				}
				autoNewLineInfo.Add(cstr.ToString());
				cstr.Clear();
			}
			ForeColor = setting.ForeColor;
		}
		private Image img;
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (Text.ToLower().Contains(".jpg")|| Text.ToLower().Contains(".png"))
			{
				try
				{
					img = Image.FromFile(Text);
				}
				catch (Exception ex)
				{
					img = null;
					Console.WriteLine(ex.Message);
				}
			}
			else {
				img = null;
			}
		}
		protected override void OnForeColorChanged(EventArgs e)
		{
			base.OnForeColorChanged(e);
			foreBrush = new SolidBrush(ForeColor);
		}
		private SolidBrush foreBrush = new SolidBrush(Color.Black);
		private List<string> autoNewLineInfo;

		public bool Selected { get => selected; set => selected = value; }

		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
			if (!OnOutputModle)
				e.Graphics.FillRectangle(System.Drawing.Brushes.Green, new System.Drawing.RectangleF(0,0,Width,Height));
			if (img!=null)
			{
				e.Graphics.DrawImage(img, 0, 0, Width, Height);
			}
			else
			{
				var strSize = e.Graphics.MeasureString(Text, Font);
				
				if (setting.AutoNewLine)
				{
					int index = 0;
					
					foreach (var info in autoNewLineInfo)
					{
						var y = Height * index / autoNewLineInfo.Count + (index + 1 == autoNewLineInfo.Count && index == 1 ? Height * 0.1677f : 0f);
						index++;
						
						e.Graphics.DrawString(info, Font, foreBrush, 0, y);
					}
				}
				else
				{
					string outInfo = "";
					if (Text.Length == 2) outInfo = Text.Substring(0, 1) + " " + Text.Substring(1, 1); else outInfo = Text;
					e.Graphics.DrawString(outInfo, Font, foreBrush, 0.5f * (Width - strSize.Width), 0.5f * (Height - strSize.Height));
				}
				
			}
		}
	}
}
