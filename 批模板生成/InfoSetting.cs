using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace 批模板生成
{
	public class InfoSetting
	{
		public Action<InfoSetting> OnSettingModify;
		private string targetCol;//本信息框绑定
		private int x, y,w,h;
		private string fontName = "微软雅黑";
		private float fontSize = 16;
		private Color foreColor;
		private string tagName="";
		private bool autoNewLine;
		public string TargetCol { get => targetCol; set => targetCol = value; }
		private string textAlign;
		internal void SettingModify(bool modify)
		{
			if (modify)
			{
				var f = new InfoInput(this,()=> {
					OnSettingModify?.Invoke(this);
				});
				f.Show();
			}
			else { OnSettingModify?.Invoke(this); }
			
		}

		public string FontName { get => fontName; set => fontName = value; }
		public float FontSize { get => fontSize; set => fontSize = value; }
		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int W { get => w; set => w = value; }
		public int H { get => h; set => h = value; }
		public string TagName { get => tagName; set => tagName = value; }
		public bool AutoNewLine { get => autoNewLine; set => autoNewLine = value; }
		public Color ForeColor { get => foreColor; set {
				foreColor = value.ToArgb() == 0 ? Color.Black : value;
			} }

		public string TextAlign { get => textAlign; set => textAlign = value; }

		public override string ToString()
		{
			return string.Format("<element><fontName>{0}</fontName>\n<fontSize>{1}</fontSize>\n<x>{2}</x>\n<y>{3}</y>\n<w>{4}</w>\n<h>{5}</h>\n<binding>{6}</binding>\n<tag>{7}</tag>\n<newline>{8}</newline>\n<foreColor>{9}</foreColor><TextAlign>{10}</TextAlign></element>", FontName,FontSize,X,Y,W,H,TargetCol,TagName,AutoNewLine?"1":"0",foreColor.ToArgb(),TextAlign);
		}
	}
}
