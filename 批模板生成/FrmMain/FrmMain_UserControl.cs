using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DotNet4.Utilities.UtilDrawing;
using Inst;

namespace 批模板生成
{
	partial class FrmMain
	{
		private string lastFileName;
		private bool onFileLoading;
		Microsoft.Office.Interop.Excel.Application app;
		Microsoft.Office.Interop.Excel.Workbook bk;
		Microsoft.Office.Interop.Excel.Worksheet sh;

		private void 背景ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var t = new OpenFileDialog()
			{
				Title = "导入背景",
				Filter = "图片|*.jpg;*.png;*.gif;*.jpeg;*.bmp"
			};
			if (t.ShowDialog() != DialogResult.OK) return;
			var nowPath = Application.StartupPath;
			var relatePath = t.FileName.Replace(nowPath, "");
			if (relatePath.StartsWith("\\")) relatePath = relatePath.Substring(1);
			if (relatePath.Contains(":"))
			{
				var targetNewFileName =  $"{nowPath}\\{t.SafeFileName}";
				var id = relatePath;
				if (File.Exists(targetNewFileName))
				{
					id = Guid.NewGuid().ToString();
					targetNewFileName = $"{nowPath}\\{id}";
				}

					
				File.Copy(t.FileName, targetNewFileName);
				relatePath = id;
			}
			canvas.SetBckImage(relatePath);
			canvasNeedRefresh = true;
		}
		private readonly List<Element> selectedCtl=new List<Element>();
		private void 读取ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Invoke((EventHandler) delegate
			{
				var f=new FrmLoadSetting("")
				{
					OnFileSelect = LoadSetting
				};
				f.ShowDialog();
			});
		}

		private void LoadSetting(string path)
		{
			if (CheckNowFileModefied()) return;
			focusSettingFileName = path;
			list.Clear();
			canvas.List.Clear();
			var l = SettingModel.Load(path);//TODO 输入文件
			if (l == null) return;
			canvas.SetBckImage(l.Canvas.bckImagePath);
			foreach (var i in l.Content)
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
			canvasNeedRefresh = true;
		}

		private bool CheckNowFileModefied()
		{
			if (focusSettingFileName!=null && MessageBox.Show("确认关闭当前并新建吗?", "关闭", MessageBoxButtons.YesNo) == DialogResult.No) return true;
			return false;
		}
		private void 新建ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CheckNowFileModefied()) return;
			focusSettingFileName = null;
			canvas.List.Clear();
			canvas.SetBckImage(null);
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
		private string focusSettingFileName;
		private void 保存ToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			if (focusSettingFileName == null)
			{
				另存为ToolStripMenuItem_Click(sender,e);
				return;
			}
			SettingModel.Save(list.List,focusSettingFileName,focusSettingFileName.Substring(focusSettingFileName.LastIndexOf('.') + 1),canvas);
		}
		
		private void 另存为ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var f = new SaveFileDialog()
			{
				Title = "保存当前布局",
				Filter = "卡片模板|*.cmx"
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
			var l = new InfoSetting()
			{
				W = 100,
				H = 100,
				X = (int)(canvas.ScaleWidth*0.5-50),
				Y = (int)(canvas.ScaleHeight*0.5-50),
				TargetCol = "在此处填写Excel中对应的列名"
			};
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
