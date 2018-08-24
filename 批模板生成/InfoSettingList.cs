using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 批模板生成
{
	public class InfoSettingList
	{
		public List<InfoSetting> list;
		public InfoSettingList()
		{
			list = new List<InfoSetting>();
		}
		private bool opModle;
		public bool OutputModle { get=>opModle;set {
				opModle = value;
				foreach (var l in list)
				{
					l.OnOutputModle(opModle);
				}
			} }

		public void New(InfoSetting l,bool modify=true)
		{
			//var l = new InfoSetting();
			
			l.SettingModify(modify);
			list.Add(l);
		}
		public void Clear()
		{
			foreach(var i in list)
			{
				i.AbortSetting.Invoke();
			}
			list.Clear();
		}
	}
}
