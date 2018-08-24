using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace 批模板生成
{
	public class InfoSetting
	{
		public Action AbortSetting;
		public Action OnSettingModify;
		public Action<bool> OnOutputModle;
		private string targetCol;//本信息框绑定
		private int x, y,w,h;
		private string fontName = "微软雅黑";
		private float fontSize = 16;
		private Color foreColor;
		private string tagName="";
		private bool autoNewLine;
		public string TargetCol { get => targetCol; set => targetCol = value; }

		internal void SettingModify(bool modify)
		{
			if (modify)
			{
				var f = new InfoInput(this,()=> { OnSettingModify?.Invoke(); });
				f.Show();
			}
			else { OnSettingModify?.Invoke(); }
			
		}

		public string FontName { get => fontName; set => fontName = value; }
		public float FontSize { get => fontSize; set => fontSize = value; }
		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int W { get => w; set => w = value; }
		public int H { get => h; set => h = value; }
		public string TagName { get => tagName; set => tagName = value; }
		public bool AutoNewLine { get => autoNewLine; set => autoNewLine = value; }
		public Color ForeColor { get => foreColor; set => foreColor = value; }

		public override string ToString()
		{
			return string.Format("{0}#{1}#{2}#{3}#{4}#{5}#{6}#{7}#{8}#{9}#",FontName,FontSize,X,Y,W,H,TargetCol,TagName,AutoNewLine?"1":"0",foreColor.ToArgb());
		}
	}
}
