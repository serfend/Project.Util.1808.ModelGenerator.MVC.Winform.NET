using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace 批模板生成
{
	public class InfoSetting
	{
		[JsonIgnoreAttribute]
		public Action<InfoSetting> OnSettingModify;
		private Color foreColor;
		public string TargetCol { get; set; }

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

		public string FontName { get; set; } = "微软雅黑";
		public float FontSize { get; set; } = 16;
		public int X { get; set; }

		public int Y { get; set; }

		public int W { get; set; }

		public int H { get; set; }

		public string TagName { get; set; } = "";
		public bool AutoNewLine { get; set; }

		public Color ForeColor { get => foreColor; set => foreColor = value.ToArgb() == 0 ? Color.Black : value;
		}

		public string TextAlign { get; set; }

		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
