using DotNet4.Utilities.UtilDrawing;
using Inst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
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
		private CanvasMain CanvasMain=new CanvasMain() ;
		public FrmMain()
		{
			InitializeComponent();
			var setting = Program.RegMain.In("Setting").In("Default").In("Canvas");
			canvas = new Canvas() {
				OnSizeModefy = (w, h) => {
					canvas.ScaleWidth = w;
					canvas.ScaleHeight = h;
					CanvasMain.Width = w;
					CanvasMain.Height = h;
					setting.SetInfo("ScaleWidth", w.ToString());
					setting.SetInfo("ScaleHeight", h.ToString());
				}
				
			};
			CanvasMain.canvas = canvas;
			canvas.ScaleWidth = Convert.ToInt32(setting.GetInfo("ScaleWidth", "1000"));
			canvas.ScaleHeight = Convert.ToInt32(setting.GetInfo("ScaleHeight", "1000"));
			canvas.OnSizeModefy(canvas.ScaleWidth, canvas.ScaleHeight);
			CanvasMain.Paint += CanvasMain_Paint;
			CanvasMain.MouseDown += CanvasMain_MouseDown;
			CanvasMain.MouseUp += CanvasMain_MouseUp;
			CanvasMain.MouseMove += CanvasMain_MouseMove;
			CanvasMain.MouseDoubleClick += CanvasMain_MouseDoubleClick;
			CanvasMain.MouseWheel += (x, xx) =>
			{
				//if (Control.ModifierKeys == Keys.Control)
				//{
				//	if (xx.Delta > 0)
				//	{
				//		canvas.ScalePercent *=1.1;
				//		canvas.ScalePercent += 0.01;
				//	}
				//	else
				//	{
				//		canvas.ScalePercent *= 0.9;
				//	}
					
				//	CanvasMain.Width = canvas.ScaleWidth ;
				//	CanvasMain.Height = canvas.ScaleHeight;
				//	var f = new InfoShower() {
				//		Title = "修改" + canvas.ScalePercent
				//	};
				//	InfoShower.ShowOnce(f);
				//}
			};
			CanvasMain.Parent = this;
			canvasRefresh = new BackgroundWorker() ;
			canvasRefresh.DoWork += CanvasRefresh_DoWork;
			canvasRefresh.RunWorkerAsync();
		}

		private void CanvasMain_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			foreach (var element in canvas.List)
			{
				if (element.Contain(e.X, e.Y))
				{
					element.Setting.SettingModify(true);
					break;
				}
			}
		}
		private bool canvasNeedRefresh=false;
		private void CanvasRefresh_DoWork(object sender, DoWorkEventArgs e)
		{
			while (true)
			{
				if (canvasNeedRefresh)
				{
					CanvasMain.Invalidate();
					canvasNeedRefresh = false;
				}
				Thread.Sleep(50);
			}
		}

		private BackgroundWorker canvasRefresh;
		private int mouseDownX, mouseDownY;
		private void CanvasMain_MouseMove(object sender, MouseEventArgs e)
		{
			if(e.Button == MouseButtons.Left)
			{
				var deltaX = mouseDownX - e.X;
				var deltaY = mouseDownY - e.Y;
				if (onResizeIndex>0) {
					foreach(var element in selectedCtl)
					{
						switch (onResizeIndex)
						{
							case 1: {
									//左上
									element.MoveTemp(-deltaX,-deltaY,deltaX,deltaY);
									break;
								}
							case 2:
								{
									//右上
									element.MoveTemp(0, -deltaY, -deltaX, deltaY);
									break;
								}
							case 3:
								{
									//左下
									element.MoveTemp(-deltaX, 0, deltaX, -deltaY);
									break;
								}
							case 4:
								{
									//右下
									element.MoveTemp(0, 0, -deltaX, -deltaY);
									break;
								}
						}
					}
				}
				else
				{
					
					bool lockDirection = Control.ModifierKeys == Keys.Shift;
					bool useDirectionX = Math.Abs(deltaX) > Math.Abs(deltaY);
					foreach (var element in selectedCtl)
					{
						if (lockDirection)
						{
							if (useDirectionX) element.MoveTemp(-deltaX, 0, 0, 0);
							else element.MoveTemp(0, -deltaY, 0, 0);
						}
						else
						{
							element.MoveTemp(-deltaX, -deltaY, 0, 0);
						}

					}
				}
				canvasNeedRefresh = true;
			}
		}

		private void CanvasMain_MouseUp(object sender, MouseEventArgs e)
		{
			foreach(var element in selectedCtl)
			{
				element.MoveConfirm();
			}
			canvasNeedRefresh = true;
			onResizeIndex = 0;
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.KeyCode == Keys.Escape)
			{
				canvas.Cancel = true;
			}
		}
		private bool anyCtlIsSelect = false;
		private short onResizeIndex=0;
		private void CanvasMain_MouseDown(object sender, MouseEventArgs e)
		{
			mouseDownX = e.X;
			mouseDownY = e.Y;
			bool mutilSelect = Control.ModifierKeys == Keys.Control || Control.ModifierKeys == Keys.Shift;
			bool cleared=false;
			if (!mutilSelect)
			{
				foreach (var element in canvas.List)
				{
					onResizeIndex = element.HoverContain(e.X, e.Y);
					if (onResizeIndex > 0)
					{
						//调整大小开始
						return;
					}
				}

				//取消选择
				cleared = true;
				selectedCtl.Clear();
				foreach (var element in canvas.List)
				{
					element.Selected = false;
				}
			}
			anyCtlIsSelect = false;
			foreach (var element in canvas.List)
			{
				if (element.Contain(e.X,e.Y))
				{
					element.OnCtlSelected.Invoke(element, mutilSelect);
					anyCtlIsSelect = true;
					break;
				}
			}
			if (!anyCtlIsSelect && !cleared)
			{
				selectedCtl.Clear();
				foreach (var element in canvas.List)
				{
					element.Selected = false;
				}
			}
			canvasNeedRefresh = true;
		}
		


		private const string defaultText = "暂无底图,右键呼出菜单~";
		private void CanvasMain_Paint(object sender, PaintEventArgs e)
		{
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
				var nowPath = Application.StartupPath;
				var relatePath = t.FileName.Replace(nowPath, "");
				if (relatePath.StartsWith("\\")) relatePath = relatePath.Substring(1);
				if (relatePath.Contains(":"))
				{
					File.Copy(t.FileName, nowPath + "\\" + t.SafeFileName);
					relatePath = t.SafeFileName;
				}
				this.BgImage = Image.FromFile(relatePath);
				SettingModel.BackGroundImg = relatePath;
				canvasNeedRefresh = true;
			}
		}
		private List<Element> selectedCtl=new List<Element>();
		private void 读取ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var f = new OpenFileDialog()
			{
				Title = "读取布局",
				Filter = "卡片模板|*.cm;*.cmx;*.cmm|所有文件|*.*"
			};
			if (f.ShowDialog() == DialogResult.OK)
			{
				list.Clear();
				canvas.List.Clear();
				var l = SettingModel.Load(f.FileName,out string bg,out int w,out int h);//TODO 输入文件
				if (bg.Length > 0) {
					SettingModel.BackGroundImg = bg;
				}
				canvas.OnSizeModefy(w, h);
				if (l == null) return;
				foreach (var i in l)
				{
					var c = new Element(i)
					{
						OnCtlSelected = (ctl,mutiSelect) => {
							if (ctl != null&&!ctl.Selected)
							{
								selectedCtl.Add(ctl);
								ctl.MoveConfirm();
								ctl.Selected = true;
							}
						}
					};
					i.OnSettingModify = (setting) => {
						c.RefreshAnySetting();
					};
					canvas.List.Add(c);
					list.New(i, false);
				}
				switch (f.FileName.Substring(f.FileName.LastIndexOf('.') + 1))
				{
					case "cmx":
						{
							BgImage = SettingModel.Image;
							break;
						}
				}
				canvasNeedRefresh = true;
			}
		}
		private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			canvas.List.Clear();
			SettingModel.BackGroundImg = "";
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
			canvasNeedRefresh = true;
		}
		public Image BgImage { get => bgImage; set {
				bgImage = value;
				if (bgImage != null)
				{
					//ClientSize = new Size(bgImage.Width, bgImage.Height);
					CanvasMain.Size = new Size(bgImage.Width, bgImage.Height);
				}
				CanvasMain.BackgroundImage = bgImage;
				canvasNeedRefresh = true;
			} }
		private void 保存ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			var f = new SaveFileDialog()
			{
				Title = "保存当前布局",
				Filter = "带底图的卡片模板|*.cmx|卡片模板|*.cm"
			};
			if (f.ShowDialog() == DialogResult.OK)
			{
				SettingModel.Save(list.List, f.FileName, f.FileName.Substring(f.FileName.LastIndexOf('.') + 1),canvas);
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
			var c = new Element(l)
			{
				OnCtlSelected = (ctl, mutiSelect) => {
					if (ctl != null)
					{
						selectedCtl.Add(ctl);
						ctl.Selected = true;
					}
				}
			};
			canvas.List.Add(c);
			l.OnSettingModify = (setting) => {
				c.RefreshAnySetting();
				c.MoveConfirm();
				canvasNeedRefresh = true;

			};
			
			list.New(l);
			canvasNeedRefresh = true;
		}

		private void 编辑ToolStripMenuItem_Click(object sender, EventArgs e)
		{

		}

		private void 复制信息框ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach(var item in selectedCtl)
			{
				var l = new InfoSetting() {
					AutoNewLine=item.Setting.AutoNewLine,
					FontName=item.Setting.FontName,
					FontSize=item.Setting.FontSize,
					TagName=item.Setting.TagName,
					ForeColor=item.Setting.ForeColor,
					H=	item.Setting.H,
					W = item.Setting.W,
					X = item.Setting.X,
					Y = item.Setting.Y,
					TargetCol=item.Setting.TargetCol,
					TextAlign=item.Setting.TextAlign
				};
				var c = new Element(l)
				{
					OnCtlSelected = (ctl, mutiSelect) => {
						if (ctl != null)
						{
							selectedCtl.Add(ctl);
							ctl.Selected = true;
						}
					},
				};
				c.MoveConfirm();
				canvas.List.Add(c);
				l.OnSettingModify = (setting) => {
					c.RefreshAnySetting();
					canvasNeedRefresh = true;
				};
				list.New(l,false);
				
				var info = new InfoShower()
				{

					Title = "复制对象",
					Info=item.Text,
					TitleColor = Color.FromArgb(255, 50, 200, 50),
					ExistTime = 5000,
					
				};
				InfoShower.ShowOnce(info);
			}
			canvasNeedRefresh = true;
		}

		private void 清除无效框ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var waitToClear = new List<Element>();
			foreach(var element in canvas.List)
			{
				if (element.BindingFiled == null || element.BindingFiled.Length == 0)
				{
					waitToClear.Add(element);
				}
			}
			
			if (waitToClear.Count > 0)
			{
				var f = new InfoShower()
				{
					Title = "确认清除?",
					Info = string.Format("共计{0}个信息框可清除,点击此处确认。", waitToClear.Count),
					ExistTime = 10000,
					TitleColor = Color.Yellow,
					CallBack = () => {
						foreach (var element in waitToClear)
						{
							canvas.List.Remove(element);
						}
						var f2 = new InfoShower()
						{
							Title = "清除完成",
							TitleColor = Color.LawnGreen
						};
						InfoShower.ShowOnce(f2);
					}
				};
				InfoShower.ShowOnce(f);
			}
			else
			{
				var f = new InfoShower() { Title="暂无需要清除的信息框",Info="只有未绑定字段的信息框会被清除"};
				InfoShower.ShowOnce(f);
			}
		}

		private void 删除选中ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach(var s in selectedCtl)
			{
				canvas.List.Remove(s);
			}
			selectedCtl.Clear();
			canvasNeedRefresh = true;
		}

		private void 尺寸ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var f = new FrmSizeSetting(canvas.ScaleWidth, canvas.ScaleHeight, (w,h)=> {
				var lastW = canvas.ScaleWidth;
				var lastH = canvas.ScaleHeight;
				canvas.ScaleWidth = w;
				canvas.ScaleHeight = h;
				canvas.OnSizeModefy?.Invoke(w,h);
				canvasNeedRefresh = true;
				var info = new InfoShower()
				{

					Title = "尺寸已修改",
					Info = string.Format("已修改尺寸为{0}*{1}点击此处以恢复输出图像尺寸{2}*{3}", w, h,lastW,lastH),
					TitleColor = Color.FromArgb(255, 50, 200, 50),
					ExistTime = 5000,
					CallBack = () => {
						canvas.ScaleWidth = lastW;
						canvas.ScaleHeight = lastH;
						var info2 = new InfoShower()
						{
							Title = "尺寸已恢复",
							Info = string.Format("已恢复尺寸为{0}*{1}", lastW, lastH),
							TitleColor = Color.FromArgb(255, 50, 200, 50),
							ExistTime = 5000
						};
						InfoShower.ShowOnce(info2);
						canvas.OnSizeModefy?.Invoke(canvas.ScaleWidth, canvas.ScaleHeight);
						canvasNeedRefresh = true;
					}
				};
				InfoShower.ShowOnce(info);
			});
			f.Show();
		}
	}
}
