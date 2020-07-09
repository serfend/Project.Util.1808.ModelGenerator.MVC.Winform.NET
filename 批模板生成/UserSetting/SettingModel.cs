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
using Newtonsoft.Json;
using 批模板生成.UserSetting;

namespace 批模板生成
{
	/// <summary>
	///
	/// </summary>
	public static class SettingModel
	{
		//private static string backGroundImg;
		//public static Action<int, int> OnBckSizeModefy;
		//public static Action<Image> OnBackImgModefy;
		//public static string BackGroundImg { get => backGroundImg; set
		//	{
		//		backGroundImg = value;
		//		try
		//		{
		//			if(backGroundImg==null|| backGroundImg.Length == 0)
		//			{
		//				Image = null;
		//			}
		//			else
		//			{
		//				Image = Image.FromFile(backGroundImg);
		//				OnBckSizeModefy?.Invoke(Image.Width, Image.Height);
		//			}
		//			OnBackImgModefy?.Invoke(Image);
		//		}
		//		catch (FileNotFoundException ex)
		//		{
		//			var info = new InfoShower()
		//			{

		//				Title = "底图未找到",
		//				Info = ex.Message,
		//				TitleColor = Color.FromArgb(255, 255, 100, 50),
		//				ExistTime = 15000,
		//			};
		//			InfoShower.ShowOnce(info);
		//		}
		//	}
		//}

		public static Image Image { get; set; }

		public static void Save(IEnumerable<InfoSetting>list,string path,string filterName,Canvas canvas)
		{
			var data = new SettingFile()
			{
				Name = path,
				Canvas = canvas,
				Content = list
			};
			var setting = JsonConvert.SerializeObject(data);
			File.WriteAllText(path, setting);
			InfoShower.ShowOnce(new InfoShower()
			{
				Title = "已保存",
				Info = path,
				TitleColor = Color.Green
			});
		}
		public static SettingFile Load(string path)
		{
			try
			{
				var l = new List<InfoSetting>();
				var setting = JsonConvert.DeserializeObject<SettingFile>(File.ReadAllText(path));
				if (setting.Canvas==null)
				{
					var f = new InfoShower()
					{
						Title = "加载失败",
						Info = "文件格式错误，未发现Setting节点",
						TitleColor = Color.Red,
						ExistTime = 10000
					};
					InfoShower.ShowOnce(f);
				}
				return setting;
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
				return null;
			}
		}
	}
}
