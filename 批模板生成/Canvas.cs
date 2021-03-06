﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using 批模板生成;
using Inst;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DotNet4.Utilities.UtilDrawing
{
	public class Canvas
	{
		private Image backImage;
		[JsonIgnoreAttribute]
		public Action<int, int> OnSizeModefy;
		[JsonIgnoreAttribute]
		public List<Element> List { get; set; }

		private Image BackImage { get => backImage;
			set
			{
				backImage = value;
				RefreshNewBackImage();
			}
		}

		public Image GetBckImage()
		{
			return backImage;
		}

		public string bckImagePath { get; set; }
		public void SetBckImage(string path)
		{
			if(path!=null)
				BackImage = Image.FromFile(path);
			else
			{
				BackImage = null;
			}
			bckImagePath = path;
		}
		private void RefreshNewBackImage()
		{
			var lastW = ScaleWidth;
			var lastH = ScaleHeight;
			int w = backImage?.Width ?? 0;
			int h = backImage?.Height??0;
			ScaleWidth = w;
			ScaleHeight = h;
			OnSizeModefy?.Invoke(w,h);
			var info = new InfoShower()
			{
						
				Title = "背景图已修改",
				Info=string.Format("已修改尺寸为{0}*{1}点击此处以恢复输出图像尺寸",w,h),
				TitleColor = Color.FromArgb(255, 50, 200, 50),
				ExistTime = 5000,
				CallBack = () => {
					ScaleWidth = lastW;
					ScaleHeight = lastH;
					var info2 = new InfoShower()
					{
						Title = "更改已生效",
						TitleColor = Color.FromArgb(255, 50, 200, 50),
						ExistTime = 5000
					};
					InfoShower.ShowOnce(info2);
					OnSizeModefy?.Invoke(ScaleWidth, ScaleHeight);
				}
			};
			InfoShower.ShowOnce(info);
		}
		/// <summary>
		/// 当前操作已取消，可进行新的操作
		/// </summary>
		[JsonIgnoreAttribute]
		public bool ActionIsCancel { get; set; }

		public int ScaleWidth { get; set; }

		public int ScaleHeight { get; set; }

		public double ScalePercent { get; set; } = 1;

		public Canvas()
		{
			List = new List<Element>();

		}
		public void OutPut(Microsoft.Office.Interop.Excel.Worksheet sh,Action callBack)
		{
			ActionIsCancel = false;
			if (sh == null) { NoticeOnCancel(0, -1,"表格未加载"); return; }
			var dic = new Dictionary<string, int>();
			try
			{
				for (var i = 1; i <= sh.UsedRange.Columns.Count; i++)
				{
					var cell = (Microsoft.Office.Interop.Excel.Range)sh.Cells[1, i];
					if (cell.Value != null && !dic.ContainsKey(cell.Value))
					{
						dic.Add(cell.Value, i);
					}
				}
			}
			catch (Exception ex)
			{
				NoticeOnCancel(0, 0, ex.Message);
			}
			EncoderParameters encoderParams = new EncoderParameters(1);
			EncoderParameter encoderParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100);
			encoderParams.Param[0] = encoderParam;
			//获取指定mimeType的mimeType的ImageCodecInfo
			var codecInfos = ImageCodecInfo.GetImageEncoders();
			var codelist = new StringBuilder();
			for(int i= codecInfos.Length-1; i>=0;i--)
			{
				var codec = codecInfos[i];
				codelist.AppendFormat("{0}|", codec.CodecName).AppendFormat("*.{0}|", codec.FormatDescription);
			}
			var f = new SaveFileDialog() {
				Title="输出",
				Filter= codelist.ToString(0,codelist.Length-1),
				FileName=Program.RegMain.In("Settting").GetInfo("OutputFilename"),
			};
			if (f.ShowDialog()==DialogResult.Cancel) {
				NoticeOnCancel(0, 0,"用户取消导出");
				return;
			} ;
			
			var codecInfo = codecInfos[codecInfos.Length- f.FilterIndex];

			new Thread(()=> {
				var rowCount = sh.UsedRange.Rows.Count;
				var fileDic = f.FileName.Replace(Environment.CurrentDirectory, "");
				var lastDicIndex = fileDic.LastIndexOf("\\");

				var fileFormat = fileDic.Substring(lastDicIndex + 1).Replace("."+codecInfo.FormatDescription,"");
				Program.RegMain.In("Settting").SetInfo("OutputFilename",fileFormat);
				fileDic = fileDic.Substring(0, lastDicIndex );
				if (fileDic.StartsWith("\\")) fileDic = fileDic.Substring(1);
				if (fileDic.Length > 0) fileDic += "\\";
				for (int i = 2; i <= rowCount; i++)
				{
					
					if (ActionIsCancel)
					{
						NoticeOnCancel(i-2, rowCount - i+1,"用户主动取消");
						return;
					}
					using (var img = new Bitmap(ScaleWidth, ScaleHeight))
					{
						var tmpG = Graphics.FromImage(img);
						tmpG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
						if(backImage!=null)tmpG.DrawImage(backImage, 0, 0, ScaleWidth, ScaleHeight);
						var fileName =string.Format("{0}{1}{2}",fileDic ,fileFormat, sh.Cells[i, 1].Value);
						foreach (var ctl in List)
						{
							if (ctl.BindingFiled == null) continue;
							bool contains = false;
							contains = dic.ContainsKey(ctl.BindingFiled);
							var thisValue = contains ? ((Microsoft.Office.Interop.Excel.Range)sh.Cells[i, dic[ctl.BindingFiled]]).Value : "";
							ctl.Text = Convert.ToString(thisValue);
							ctl.DrawToCanvas(tmpG,true);
							if (ActionIsCancel)
							{
								NoticeOnCancel(i - 2, rowCount - i + 1,"用户主动取消");
								return;
							}
						}
						
						try
						{
							img.Save(string.Format("{0}.{1}", fileName,codecInfo.FormatDescription),codecInfo, encoderParams);
						}
						catch (Exception)
						{
							img.Save(string.Format("{0}.{1}", (new Random().Next(0, 1000).ToString()), codecInfo.FormatDescription));
						}
						tmpG.Dispose();
					}
					if (ActionIsCancel)
					{
						NoticeOnCancel(i - 2, rowCount - i + 1,"用户主动取消");
						return;
					}
				}
				callBack?.Invoke();
			}).Start();
		}
		private void NoticeOnCancel(int finish,int left,string reason)
		{
			var info = new InfoShower()
			{

				Title = "输出任务已取消",
				Info = string.Format("当前已输出{0}份,剩余{1}份{2}",finish, left, reason==null?"": ("\n原因:" + reason)),
				TitleColor = Color.FromArgb(255, 50, 200, 50),
				ExistTime = 5000,
			};
			InfoShower.ShowOnce(info);
		}
		
	}
	public class Element: IDisposable
	{
		private string text;

		public Action<Element,bool> OnCtlSelected;
		private bool selected;
		public InfoSetting Setting;
		public Element(InfoSetting setting)
		{
			this.Setting = setting;
			RefreshAnySetting();
		}
		public void RefreshAnySetting()
		{
			ForeColor = Setting.ForeColor;
			Font = new Font(Setting.FontName, Setting.FontSize);
			Text = Setting.TagName == "" ? Setting.TargetCol : Setting.TagName;
		}
		public int X { get => Setting.X; set =>Setting.X=value; }
		public int Y { get => Setting.Y; set => Setting.Y = value; }
		public int W { get => Setting.W==0?1:Setting.W; set => Setting.W = value; }
		public int H { get => Setting.H==0?1:Setting.H; set => Setting.H=value; }
		/// <summary>
		/// 预移动
		/// </summary>
		public void MoveTemp(int x,int y,int w,int h)
		{
			X = PrX + x;
			Y = PrY + y;
			W = PrW + w;
			H = PrH + h;
			W = W <= 0 ? 1 : W;
			H = H <= 0 ? 1 : H;
			Text = Text+"";
		}

		/// <summary>
		/// 应用移动
		/// </summary>
		public void MoveConfirm()
		{
			PrX = X;
			PrY = Y;
			PrW = W;
			PrH = H;
		}
		public Color ForeColor { get => Setting.ForeColor;set{
				foreBrush = new SolidBrush(value);
				Setting.ForeColor = value;
			} }
		private Brush foreBrush,backBrush;
		public string BindingFiled { get => Setting.TargetCol; set => Setting.TargetCol = value; }
		public Font Font { get => font; set {
				font = value;
				Setting.FontName = font.Name;
				Setting.FontSize = font.Size;
			} }
		private Font font;
		public string Text { get => text; set {
				text = value;
				if (text == null) {
					img = null;
					return;
				}
				if (Text.ToLower().Contains(".jpg") || Text.ToLower().Contains(".png"))
				{
					try
					{
						var newImgPath = Text.Replace(" ", "");
						if (imgPath != newImgPath)
						{
							imgPath = newImgPath;
							img = Image.FromFile(Text.Replace(" ", ""));
						}
						
					}
					catch (Exception ex)
					{
						img = null;
						Console.WriteLine(ex.Message);
					}
				}
				else
				{
					if (Setting.AutoNewLine)
					{
						autoNewLineInfo = new List<string>();
						var cstr = new StringBuilder();
						int index = 0;
						var g = Graphics.FromImage(new Bitmap(W, H));
						while (index < Text.Length)
						{
							while (g.MeasureString(cstr.ToString(), Font).Width < W & index < Text.Length)
							{
								cstr.Append(Text[index++]);
							}
							if (index < Text.Length) { cstr.Remove(cstr.Length-1, 1);index--; }
							autoNewLineInfo.Add(cstr.ToString());
							cstr.Clear();
						}
					}	
					
					img = null;
				}
			} }
		public bool Selected { get => selected; set => selected = value; }
		public int PrX, PrY,PrW,PrH;

		private List<string> autoNewLineInfo;
		private Image img;
		private string imgPath;

		public void DrawToCanvas( Graphics g,bool outputMode)
		{
			if (!outputMode)
			{
				g.FillRectangle(Brushes.ForestGreen, X,Y,W,H);
			}
			if (img != null)
			{
				g.DrawImage(img,  X, Y, W, H);
			}
			else
			{
				var strSize = g.MeasureString(Text, Font);

				if (Setting.AutoNewLine)
				{
					int index = 0;

					foreach (var info in autoNewLineInfo)
					{
						var y1 = H * index / autoNewLineInfo.Count + (index + 1 == autoNewLineInfo.Count && index == 1 ? H * 0.1677f : 0f);
						index++;

						g.DrawString(info, Font, foreBrush, X,Y+ y1);
					}
				}
				else
				{
					if (Text == null) return;
					string outInfo = "";
					if (Text.Length == 2) outInfo = Text.Substring(0, 1) + " " + Text.Substring(1, 1); else outInfo = Text;
					float newX=X, newY=Y;
					switch (Setting.TextAlign)
					{
						case "Center": {
								newX = X + 0.5f * (W - strSize.Width);
								newY = Y + 0.5f * (H - strSize.Height);
								break;
							}
						case "CenterLeft": {
								newX = X ;
								newY = Y + 0.5f * (H - strSize.Height);
								break;
							}
						case "CenterRight":
							{
								newX = X+W-strSize.Width;
								newY = Y + 0.5f * (H - strSize.Height);
								break;
							}
						case "TopCenter":
							{
								newX = X + 0.5f * (W - strSize.Width);
								newY = Y;
								break;
							}
						case "TopLeft":
							{
								newX = X;
								newY = Y;
								break;
							}
						case "TopRight":
							{
								newX = X + W - strSize.Width;
								newY = Y;
								break;
							}
						case "BottomCenter":
							{
								newX = X + 0.5f * (W - strSize.Width);
								newY = Y + H - strSize.Height;
								break;
							}
						case "BottomLeft":
							{
								newX = X;
								newY = Y + H - strSize.Height;
								break;
							}
						case "BottomRight":
							{
								newX = X + W - strSize.Width;
								newY = Y + H - strSize.Height;
								break;
							}
					}
					if(outInfo != "") g.DrawString(outInfo, Font, foreBrush,newX, newY);
				}

			}
		}
		public void HoverPaint(Graphics g) {
			if (selected)
			{
				g.FillEllipse(Brushes.White, X - dis, Y - dis, size, size);
				g.FillEllipse(Brushes.White, X + W - dis, Y - dis, size, size);
				g.FillEllipse(Brushes.White, X - dis, Y + H - dis, size, size);
				g.FillEllipse(Brushes.White, X + W - dis, Y + H - dis, size, size);
			}
		}
		#region IDisposable Support
		private bool disposedValue = false; // 要检测冗余调用

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					foreBrush.Dispose();
					backBrush.Dispose();
				}
				foreBrush = null;
				backBrush = null;


				disposedValue = true;
			}
		}



		// 添加此代码以正确实现可处置模式。
		public void Dispose()
		{
			// 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
			Dispose(true);
		}


		#endregion
		internal bool Contain(int x, int y)
		{
			return x > X && y > Y && x < X + W && y < Y + H;
		}
		internal short HoverContain(int x, int y)
		{
			if (Math.Pow(x - X - dis, 2) + Math.Pow(y - Y+dis, 2) <= Math.Pow(size, 2)) return 1;
			if (Math.Pow(x - (X+W+dis), 2) + Math.Pow(y - Y+dis, 2) <= Math.Pow(size, 2)) return 2;
			if (Math.Pow(x - X - dis, 2) + Math.Pow(y - (Y+H+dis), 2) <= Math.Pow(size, 2)) return 3;
			if (Math.Pow(x - (X + W + dis), 2) + Math.Pow(y - (Y+H+dis), 2) <= Math.Pow(size, 2)) return 4;
			return 0;
		}
		public static int dis = 5, size = 10;
	}
}
