using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNet4.Utilities.UtilDrawing;

namespace 批模板生成.UserSetting
{
	public class SettingFile
	{
		public string Name { get; set; }
		public Canvas Canvas { get; set; }
		public IEnumerable<InfoSetting> Content { get; set; }
	}
}
