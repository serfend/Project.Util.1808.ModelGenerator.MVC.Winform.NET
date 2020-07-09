using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Inst;

namespace 批模板生成
{
	partial class FrmMain
	{
		private bool OutputAllImg()
		{
			if (app == null)
			{
				this.BeginInvoke((EventHandler)delegate {
					var info = new InfoShower()
					{
						Title = "未打开数据",
						TitleColor = Color.FromArgb(255, 255, 0, 0),
						ExistTime = 5000
					};
					InfoShower.ShowOnce(info);
				});
				Xls数据ToolStripMenuItem_Click(this,EventArgs.Empty);
				return false;
			}
			
			canvas.OutPut(sh,()=> {
				this.BeginInvoke((EventHandler)delegate {
					var info = new InfoShower()
					{
						Title = "导出完成",
						TitleColor = Color.FromArgb(255, 255, 0, 0),
						ExistTime = 15000
					};
					InfoShower.ShowOnce(info);
				});

			});

			return true;
		}

	}
}
