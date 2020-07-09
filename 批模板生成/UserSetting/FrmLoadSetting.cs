using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using 批模板生成.UserSetting;

namespace 批模板生成
{
	public partial class FrmLoadSetting : Form
	{
		private FrmLoadSetting_SettingList list;
		private Thread threadUI;

		private void UIHandle()
		{
			while (true)
			{
				if (Program.AnimationOn)
				{
					if(list.RefreshLayout())Program.AnimationOn=false;
				}
;				Thread.Sleep(30);
			}
			
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if(threadUI.IsAlive)threadUI.Abort();
			base.OnFormClosing(e);
		}

		private string rootPath;
		public FrmLoadSetting(string rootPath)
		{
			this.rootPath = rootPath;
			InitializeComponent();
			threadUI = new Thread(UIHandle){IsBackground = true};
			list=new FrmLoadSetting_SettingList()
			{
				Parent = this,
				Width = this.Width,
				Height = this.Height,
				SettingSelect = ShowSetting
			};
			threadUI.Start();
		}

		public Action<string> OnFileSelect;
		public void ShowSetting(string name)
		{
			OnFileSelect?.Invoke(name);
		}
		private void FrmLoadSetting_Load(object sender, EventArgs e)
		{
			var dic = new DirectoryInfo($"{Environment.CurrentDirectory}\\{rootPath}");
			var dicFile = dic.GetFiles("*.cmx");
			foreach (var item in dicFile)
			{
				list.AddSettingFile(item.Name);
			}

			if (dicFile.Length == 0)
			{
				list.AddSettingFile("当前无.cmx文件，请先创建");
			}
			Program.AnimationOn = true;
		}

		private int offsetScoll = 0;
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			offsetScoll -= e.Delta;
			if (offsetScoll > 0) offsetScoll = 0;
			foreach (var i in list.list)
			{
				var item = i.Value;
				item.OffsetRect.Y = offsetScoll;
				var tmp=new RectangleF(item.DefaultRect.Location,item.DefaultRect.Size);
				tmp.Offset(item.OffsetRect.Location);
				tmp.Inflate(item.OffsetRect.Size);
				item.Animate(500,0,tmp,item.DefaultColor);
			}
			base.OnMouseWheel(e);
		}

		private void FrmLoadSetting_MouseClick(object sender, MouseEventArgs e)
		{
		}
	}
}
