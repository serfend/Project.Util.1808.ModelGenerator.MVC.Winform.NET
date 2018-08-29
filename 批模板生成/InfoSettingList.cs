using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 批模板生成
{
	public class InfoSettingList
	{
		private List<InfoSetting> list;
		public InfoSettingList()
		{
			List = new List<InfoSetting>();
		}

		public List<InfoSetting> List { get => list; set => list = value; }

		public void New(InfoSetting l,bool modify=true)
		{
			l.SettingModify(modify);
			List.Add(l);
		}
		public void Clear()
		{
			List.Clear();
		}
	}
}
