using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace 批模板生成
{
	public  class XmlUtil
	{
		private XmlElement element;
		public XmlUtil(XmlElement element)
		{
			this.element = element;
		} 
		public string GetElement(string tagName,string defaultInfo=null)
		{
			var f = element.GetElementsByTagName(tagName);
			if (f.Count == 0 || f[0].FirstChild==null) return defaultInfo;
			return f[0].FirstChild.Value;
		}
	}
}
