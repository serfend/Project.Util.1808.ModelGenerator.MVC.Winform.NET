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
		private readonly InfoSettingList list=new InfoSettingList();
		private readonly Canvas canvas;
		private readonly CanvasMain canvasMain=new CanvasMain() ;
		public FrmMain()
		{
			InitializeComponent();
			var setting = Program.RegMain.In("Setting").In("Default").In("Canvas");
			canvas = new Canvas() {
				OnSizeModefy = (w, h) => {
					canvas.ScaleWidth = w;
					canvas.ScaleHeight = h;
					canvasMain.Width = w;
					canvasMain.Height = h;
					setting.SetInfo("ScaleWidth", w.ToString());
					setting.SetInfo("ScaleHeight", h.ToString());
				}
				
			};
			canvasMain.canvas = canvas;
			canvas.ScaleWidth = Convert.ToInt32(setting.GetInfo("ScaleWidth", "1000"));
			canvas.ScaleHeight = Convert.ToInt32(setting.GetInfo("ScaleHeight", "1000"));
			canvas.OnSizeModefy(canvas.ScaleWidth, canvas.ScaleHeight);
			canvasMain.MouseDown += CanvasMain_MouseDown;
			canvasMain.MouseUp += CanvasMain_MouseUp;
			canvasMain.MouseMove += CanvasMain_MouseMove;
			canvasMain.MouseDoubleClick += CanvasMain_MouseDoubleClick;
			canvasMain.MouseWheel += (x, xx) =>
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
			canvasMain.Parent = this;
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
					canvasMain.Invalidate();
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
				canvas.ActionIsCancel = true;
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


		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			base.OnFormClosing(e);
			canvas.ActionIsCancel = true;
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
		
	


	}
}
