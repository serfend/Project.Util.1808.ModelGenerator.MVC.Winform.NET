using DotNet4.Utilities.UtilDrawing;
using Inst;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace 批模板生成
{
	/// <summary>
	/// FontName FontSize X Y W H TargetCol TargetName
	/// </summary>
	public static class SettingModel
	{
		private static string backGroundImg;
		private static Image image;
		public static Action<int, int> OnBckSizeModefy;
		public static Action<Image> OnBackImgModefy;
		public static string BackGroundImg { get => backGroundImg; set
			{
				backGroundImg = value;
				try
				{
					Image = Image.FromFile(backGroundImg);
					OnBckSizeModefy?.Invoke(Image.Width, Image.Height);
					OnBackImgModefy?.Invoke(Image);
				}
				catch (FileNotFoundException ex)
				{
					var info = new InfoShower()
					{

						Title = "底图未找到",
						Info = ex.Message,
						TitleColor = Color.FromArgb(255, 255, 100, 50),
						ExistTime = 15000,
					};
					InfoShower.ShowOnce(info);
				}
			}
		}

		public static Image Image { get => image; set => image = value; }

		public static void Save(IEnumerable<InfoSetting>list,string baseName,string filterName,Canvas canvas)
		{
			var cstr = new StringBuilder();
			cstr.Append("<Setting>");
			foreach(var i in list)
			{
				cstr.AppendLine(i.ToString());
			}

			cstr.AppendLine("<canvas>");
			switch (baseName.Substring(baseName.LastIndexOf('.') + 1))
			{
				case "cmx":
					{
						cstr.AppendLine(string.Format("<bg>{0}</bg>", backGroundImg));
						break;
					}
			}
			cstr.Append(string.Format("<width>{0}</width>\n<height>{1}</height>",canvas.ScaleWidth, canvas.ScaleHeight));
			cstr.AppendLine("</canvas>");
			cstr.AppendLine("</Setting>");
			File.WriteAllText(baseName, cstr.ToString());
		}
		public static List<InfoSetting> Load(string baseName,out string bg,out int w,out int h)
		{
			try
			{
				var l = new List<InfoSetting>();
				var document = new XmlDocument();
				document.Load(baseName);
				var canvas = document.GetElementsByTagName("canvas");
				bg = "";
				w = 1;
				h = 1;
				if (canvas.Count > 0)
				{
					var cvs = (XmlElement)canvas[0];
					var property = new XmlUtil(cvs);
					w = Convert.ToInt32( property.GetElement("height","1"));
					h = Convert.ToInt32(property.GetElement("width", "1"));
					bg = property.GetElement("bg", "");
				}
				var elements = document.GetElementsByTagName("Setting");
				foreach(var setting1 in elements)
				{
					var setting = (XmlElement)setting1;
					var e = setting.GetElementsByTagName("element");
					foreach (var ele in e)
					{
						XmlElement element = (XmlElement)ele;
						var Element = new XmlUtil(element);
						var tmp = new InfoSetting();
						tmp.FontName = Element.GetElement("fontName", "微软雅黑");
						tmp.FontSize = Convert.ToSingle(Element.GetElement("fontSize", "16"));
						tmp.X = Convert.ToInt32(Element.GetElement("x", "0"));
						tmp.Y = Convert.ToInt32(Element.GetElement("y", "0"));
						tmp.W = Convert.ToInt32(Element.GetElement("w", "100"));
						tmp.H = Convert.ToInt32(Element.GetElement("h", "100"));
						tmp.TargetCol = Element.GetElement("binding", "无绑定");
						tmp.TagName = Element.GetElement("tag", "");
						tmp.AutoNewLine = Element.GetElement("newLine", "0") == "1";
						tmp.ForeColor = System.Drawing.Color.FromArgb(Convert.ToInt32(Element.GetElement("foreColor", "-16777216") == "0" ? "-16777216" : Element.GetElement("foreColor", "-16777216")));
						tmp.TextAlign = Element.GetElement("TextAlign","Center");
						l.Add(tmp);

					}
					return l;
				}
				var f = new InfoShower()
				{
					Title = "加载失败",
					Info = "文件格式错误，未发现Setting节点",
					TitleColor = Color.Red,
					ExistTime = 10000
				};
				InfoShower.ShowOnce(f);
				return null;
			}
			catch (Exception ex)
			{
				var f = new InfoShower() {
					Title = "加载失败",
					Info = ex.Message,
					TitleColor = Color.Red,
					ExistTime = 10000
				};
				InfoShower.ShowOnce(f);
				bg = null;
				w =h= 1;
				return null;
			}
		}
	}
}
