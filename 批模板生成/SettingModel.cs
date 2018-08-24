using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace 批模板生成
{
	/// <summary>
	/// FontName FontSize X Y W H TargetCol TargetName
	/// </summary>
	public static class SettingModel
	{
		private static string backGroundImg;
		public static Action<int, int> OnBckSizeModefy;
		public static Action<Image> OnBackImgModefy;
		public static string BackGroundImg { get => backGroundImg; set
			{
				backGroundImg = value;
				var image = Image.FromFile(backGroundImg);
				OnBckSizeModefy?.Invoke(image.Width,image.Height);
				OnBackImgModefy?.Invoke(image);
			}
		}

		public static void Save(List<InfoSetting>list,string baseName,string filterName)
		{
			var cstr = new StringBuilder();
			foreach(var i in list)
			{
				cstr.AppendLine(i.ToString());
			}
			switch (baseName.Substring(baseName.LastIndexOf('.')+1))
			{
				case "cmx":
					{
						cstr.AppendLine("Bg=" +BackGroundImg);
						cstr.AppendLine("Canvans=");
						break;
					}
			}
			File.WriteAllText(baseName, cstr.ToString());
		}
		public static List<InfoSetting> Load(string baseName)
		{
			var l = new List<InfoSetting>();
			var info = File.ReadAllLines(baseName);
			foreach(var i in info)
			{
				if (i.Length < 2) continue;
				if (i.StartsWith("Bg="))
				{
					BackGroundImg = i.Substring(3);
					continue;
				}
				var raw = i.Split('#');
				var tmp = new InfoSetting() {
					FontName = raw[0],
					FontSize = Convert.ToSingle(raw[1]),
					X = Convert.ToInt32(raw[2]),
					Y = Convert.ToInt32(raw[3]),
					W = Convert.ToInt32(raw[4]),
					H = Convert.ToInt32(raw[5]),
					TargetCol = raw[6],
					TagName = raw[7],
					AutoNewLine = raw[8] == "1",
					ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(raw[9]==""? "-16777216" : raw[9]))
				};
				l.Add(tmp);
			}
			return l;
		}
	}
}
