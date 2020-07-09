using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 批模板生成.UserSetting
{
	public class FrmLoadSetting_SettingList:AnimateControl
	{
		public Dictionary<string, FrmLoadSetting_SettingFile> list=new Dictionary<string, FrmLoadSetting_SettingFile>();
		public Action<string> SettingSelect;
		public void AddSettingFile(string name)
		{
			var item = new FrmLoadSetting_SettingFile(Color.FromArgb(255,200,100,100),new RectangleF(10,50*list.Count,400,45))
			{
				Parent = this,
				Title = name,
				SettingSelect=(x)=>SettingSelect?.Invoke(x)
			};
			item.Top = (int)item.DefaultRect.Y;
			item.Height = (int) item.DefaultRect.Height;
			item.Animate(500,100*list.Count);
			;
			list.Add(name,item);
		}

		public void RemoveSettingFile(string name)
		{
			if (!list.ContainsKey(name)) return;;
			list.Remove(name);
			int count=0;
			foreach (var item in list)
			{
				item.Value.DefaultRect.Y = (count++) * 50;
				item.Value.Animate(500,count*500);
			}

		}

		public override bool RefreshLayout()
		{
			bool noRefresh = true;
			try
			{
				foreach (var item in list)
				{
					noRefresh &= item.Value.RefreshLayout();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return false;
			}
			return noRefresh&base.RefreshLayout();
		}
	}
}
