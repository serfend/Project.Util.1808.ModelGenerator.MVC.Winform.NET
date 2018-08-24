using DotNet4.Utilities.UtilDrawing;
using Inst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 批模板生成
{
	public partial class FrmMain : Form
	{
		private InfoSettingList list=new InfoSettingList();
		private Canvas canvas;
		private Image bgImage;
		public FrmMain()
		{
			InitializeComponent();
			canvas = new Canvas();
		}
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			canvas.Cancel = true;
			if (app != null)
			{
				try
				{
					if (bk != null) bk.Close(false);
					app.Quit();
					app = null;
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
		
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
			canvas.List.Clear();
			foreach(var element in Controls)
			{
				if(element is InfoControl ele)
				{
					canvas.List.Add(new Element()
					{
						AutoNewLine= ele.setting.AutoNewLine,
						X= ele.setting.X,
						Y= ele.setting.Y,
						W= ele.setting.W,
						H= ele.setting.H,
						ForeColor= ele.setting.ForeColor,
						BindingFiled= ele.setting.TargetCol,
						Font= new Font(ele.setting.FontName, ele.setting.FontSize),
					});
					
				}
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
		Microsoft.Office.Interop.Excel.Application app;
		Microsoft.Office.Interop.Excel.Workbook bk;
		Microsoft.Office.Interop.Excel.Worksheet sh;
		private string lastFileName;
		private bool onFileLoading;



		private void 背景ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var t = new OpenFileDialog()
			{
				Title = "导入背景",
				Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp"
			};
			if (t.ShowDialog() == DialogResult.OK)
			{
				this.BgImage = Image.FromFile(t.FileName);
				SettingModel.BackGroundImg = t.FileName;
			}
		}
		private void 读取ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var f = new OpenFileDialog()
			{
				Title = "读取布局",
				Filter = "卡片模板|*.cm;*.cmx;*.cmm"
			};
			if (f.ShowDialog() == DialogResult.OK)
			{
				list.Clear();
				var l = SettingModel.Load(f.FileName);
				foreach (var i in l)
				{
					var c = new InfoControl(i)
					{
						Parent = this,
						
					};
					
					list.New(i, false);
					
				}
				switch (f.FileName.Substring(f.FileName.LastIndexOf('.') + 1))
				{
					case "cmx":
						{
							BgImage = Image.FromFile(SettingModel.BackGroundImg);
							break;
						}
				}
				this.Invalidate();
			}
		}
		private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ClearInfoControl();
			this.BgImage = null;
			this.BeginInvoke((EventHandler)delegate {
				var info = new InfoShower()
				{
					Title = "新建文件",
					TitleColor = Color.FromArgb(255, 50, 200, 50),
					ExistTime = 5000
				};
				InfoShower.ShowOnce(info);
			});
		}
		private const string defaultText = "暂无数据,右键呼出菜单~";

		public Image BgImage { get => bgImage; set {
				bgImage = value;
				if (bgImage != null)
				{
					ClientSize = new Size(bgImage.Width, bgImage.Height);
				}
			} }

		protected override void OnPaint(PaintEventArgs e)
		{
			if (BgImage == null)
			{
				e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
				var strSize = e.Graphics.MeasureString(defaultText,Font);
				e.Graphics.DrawString(defaultText,Font,Brushes.Gray,(Width- strSize.Width)*0.5f,(Height- strSize.Height)*0.5f);
			}
			else
			{
				e.Graphics.DrawImage(BgImage, 0, 0, ClientSize.Width, ClientSize.Height);
			}
		}

		protected override void OnResize(EventArgs e)
		{
			Invalidate();
		}

		private void ClearInfoControl()
		{
			var list = new List<InfoControl>();
			foreach (var ctl in Controls)
			{
				if (ctl is InfoControl infoCtl)
				{
					list.Add(infoCtl);
				}
			}
			foreach(var ctl in list)
			{
				this.Controls.Remove(ctl);
			}
			Invalidate();
		}
		private void 保存ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			var f = new SaveFileDialog()
			{
				Title = "保存当前布局",
				Filter = "带底图的卡片模板|*.cmx|卡片模板|*.cm"
			};
			if (f.ShowDialog() == DialogResult.OK)
			{
				SettingModel.Save(list.list, f.FileName, f.FileName.Substring(f.FileName.LastIndexOf('.') + 1));
			}
		}

		private void Xls数据ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var t = new OpenFileDialog()
			{
				Title = "导入数据",
				Filter = "Office表格|*.xls;*.xlsx"
			};
			if (t.ShowDialog() == DialogResult.OK)
			{
				var fileName = t.FileName.Substring(t.FileName.LastIndexOf('\\')+1);
				if (onFileLoading)
				{
					this.BeginInvoke((EventHandler)delegate {
						var info = new InfoShower()
						{
							Title = "上一文件正在处理中",
							TitleColor=Color.FromArgb(255,255,0,0),
							ExistTime = 5000
						};
						InfoShower.ShowOnce(info);
					});
					return;
				}	
				var thr = new Thread(()=> {
					onFileLoading = true;
					if (app != null)
					{
						this.BeginInvoke((EventHandler)delegate {
							var info = new InfoShower()
							{
								Title = string.Format("{0} 加载中...", fileName),
								Info = string.Format("正在关闭已开启的数据 {0}", lastFileName),
								ExistTime = 5000
							};
							InfoShower.ShowOnce(info);
						});
						if(bk!=null)bk.Close(false);
						app.Quit();
						app = null;
					}
					else
					{
						this.BeginInvoke((EventHandler)delegate {
							var info = new InfoShower()
							{
								Title = string.Format("{0} 加载中...", fileName),
								Info = string.Format("新的数据正在打开中"),
								ExistTime = 5000
							};
							InfoShower.ShowOnce(info);
						});
							
						
					}

					app = new Microsoft.Office.Interop.Excel.Application();
					if (app == null)
					{
						MessageBox.Show("无法创建excel对象，可能您的电脑未安装excel");
					}

					bk = app.Workbooks.Open(t.FileName, ReadOnly: true);
					lastFileName = bk.Name;
					sh = bk.Worksheets[1];
					this.BeginInvoke((EventHandler)delegate {
						var info = new InfoShower()
						{
							Title = string.Format("{0} 加载完成",fileName) ,
							Info=string.Format("{0}行{1}列已加载",sh.UsedRange.Rows.Count, sh.UsedRange.Columns.Count),
							TitleColor=Color.FromArgb(255,50,255,50),
							ExistTime = 5000
						};
						InfoShower.ShowOnce(info);
					});
					onFileLoading = false;
				});
				thr.Start();
			}
		}

		private void 导出ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			list.OutputModle = true;
			if (OutputAllImg()) {
				this.BeginInvoke((EventHandler)delegate {
					var info = new InfoShower()
					{
						Title = "开始导出",
						TitleColor = Color.FromArgb(255, 255, 255, 0),
						Info="导出过程中 按[Esc]以停止进程",
						ExistTime = 10000
					};
					InfoShower.ShowOnce(info);
				});
			}
			list.OutputModle = true;
		}

		private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var info = new InfoShower()
			{

				Title = "模板生成器",
				Info = "	这是一个旨在减少加班强度，延长发际线寿命的PC端程序。你可以用它做任何程序化的材料，当然，如果有好的想法:\nQQ:331945833 备注【模板生成器】",
				TitleColor = Color.FromArgb(255, 50, 255, 100),
				InfoColor=Color.FromArgb(255,200,100,200),
				ExistTime = 60000,
			};
			InfoShower.ShowOnce(info);
		}

		private void 信息框ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var l = new InfoSetting();
			var c = new InfoControl(l)
			{
				Parent = this,
				ContextMenuStrip=this.ContextMenuStrip
			};

			list.New(l);
		}

		private void 尺寸ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var f = new FrmSizeSetting(canvas.Width,canvas.Height,(w,h)=> {
				canvas.Width = w;
				canvas.Height = h;
				var lastW = w;
				var lastH = h;
				var info = new InfoShower()
				{

					Title = "尺寸已修改",
					Info = string.Format("已修改尺寸为{0}*{1}点击此处以恢复输出图像尺寸", w, h),
					TitleColor = Color.FromArgb(255, 50, 200, 50),
					ExistTime = 5000,
					CallBack = () => {
						canvas.Width = lastW;
						canvas.Height = lastH;
						var info2 = new InfoShower()
						{
							Title = "尺寸已恢复",
							Info = string.Format("已恢复尺寸为{0}*{1}", w, h),
							TitleColor = Color.FromArgb(255, 50, 200, 50),
							ExistTime = 5000
						};
						InfoShower.ShowOnce(info2);
					}
				};
				InfoShower.ShowOnce(info);
			});
			f.Show();
		}
	}
}
