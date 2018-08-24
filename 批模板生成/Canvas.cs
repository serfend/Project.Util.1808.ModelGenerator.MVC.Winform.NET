using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Drawing.Imaging;
using 批模板生成;
using Inst;
using System.Windows.Forms;

namespace DotNet4.Utilities.UtilDrawing
{
	public class Canvas
	{
		private int height;
		private List<Element> list;
		private int width;
		private Image backImage;
		public int Width { get => width; set => width = value; }
		public int Height { get => height; set => height = value; }
		public List<Element> List { get => list; set => list = value; }
		public Image BackImage { get => backImage; set => backImage = value; }
		public bool Cancel { get => cancel; set => cancel = value; }

		private bool cancel;
		public Canvas()
		{
			width = height = 1;
			List = new List<Element>();
			SettingModel.OnBckSizeModefy = (w,h) => {
				var lastW = Width;
				var lastH =Height;
				Width = w;
				Height = h;
				var info = new InfoShower()
					{
						
						Title = "背景图已修改",
						Info=string.Format("已修改尺寸为{0}*{1}点击此处以恢复输出图像尺寸",w,h),
						TitleColor = Color.FromArgb(255, 50, 200, 50),
						ExistTime = 5000,
						CallBack = () => {
							Width = lastW;
							Height =lastH;
							var info2 = new InfoShower()
							{
								Title = "更改已生效",
								TitleColor = Color.FromArgb(255, 50, 200, 50),
								ExistTime = 5000
							};
							InfoShower.ShowOnce(info2);
						}
					};
					InfoShower.ShowOnce(info);
			};
			SettingModel.OnBackImgModefy = (img) => {
				backImage = img;
			};
		}
		public void OutPut(Microsoft.Office.Interop.Excel.Worksheet sh,Action callBack)
		{
			cancel = false;
			if (sh == null) { NoticeOnCancel(0, -1,"表格未加载"); return; }
			var dic = new Dictionary<string, int>();
			var cstr = new StringBuilder();
			try
			{
				for (int i = 1; i <= sh.UsedRange.Columns.Count; i++)
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
			foreach(var codec in codecInfos)
			{
				codelist.AppendFormat("{0}|", codec.CodecName).AppendFormat("*.{0}|", codec.FormatDescription);
			}
			var f = new SaveFileDialog() {
				Title="输出",
				Filter= codelist.ToString(0,codelist.Length-1),
				FileName=Program.RegMain.In("Settting").GetInfo("OutputFilename"),
				DefaultExt="png",
			};
			if (f.ShowDialog()==DialogResult.Cancel) {
				NoticeOnCancel(0, 0,"用户取消导出");
				return;
			} ;
			
			var codecInfo = codecInfos[f.FilterIndex-1];

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
					
					if (Cancel)
					{
						NoticeOnCancel(i-2, rowCount - i+1,"用户主动取消");
						return;
					}
					using (var img = new Bitmap(Width, Height))
					{
						var tmpG = Graphics.FromImage(img);
						tmpG.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
						if(backImage!=null)tmpG.DrawImage(backImage, 0, 0, Width, Height);
						var fileName =string.Format("{0}{1}{2}",fileDic ,fileFormat, sh.Cells[i, 1].Value);
						foreach (var ctl in list)
						{
							bool contains = false;
							contains = dic.ContainsKey(ctl.BindingFiled);
							var thisValue = contains ? ((Microsoft.Office.Interop.Excel.Range)sh.Cells[i, dic[ctl.BindingFiled]]).Value : "";
							ctl.Text = Convert.ToString(thisValue);
							ctl.DrawToCanvas(ref tmpG);
							if (Cancel)
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
					if (Cancel)
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
	public class Element:IDisposable
	{
		private int h;
		private Color backColor;
		private string bindingFiled;
		private Font font;
		private string text;
		private int x;
		private int y;
		private int w;
		private Color foreColor;
		

		public int X { get => x; set => x = value; }
		public int Y { get => y; set => y = value; }
		public int W { get => w; set
			{
				w = value;
			} }
		public int H { get => h; set {
				h = value;
			} }
		public void Resize(int w,int h)
		{
			this.w = w;
			this.h = h;
		}

		public Color ForeColor { get => foreColor;set{
				foreBrush = new SolidBrush(value);
				foreColor = value;
			} }
		public Color BackColor { get => backColor; set {
				backBrush = new SolidBrush(value);
				backColor = value;
			} }
		private Brush foreBrush,backBrush;
		public string BindingFiled { get => bindingFiled; set => bindingFiled = value; }
		public Font Font { get => font; set => font = value; }
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
						img = Image.FromFile(Text.Replace(" ",""));
					}
					catch (Exception ex)
					{
						img = null;
						Console.WriteLine(ex.Message);
					}
				}
				else
				{
					
					autoNewLineInfo = new List<string>();
					var cstr = new StringBuilder();
					int index = 0;
					var g = Graphics.FromImage(new Bitmap(W,H));
					while (index < Text.Length)
					{
						while (g.MeasureString(cstr.ToString(), Font).Width < W & index < Text.Length)
						{
							cstr.Append(Text[index++]);
						}
						autoNewLineInfo.Add(cstr.ToString());
						cstr.Clear();
					}
					img = null;
				}
			} }
		public bool AutoNewLine { get => autoNewLine; set => autoNewLine = value; }

		private List<string> autoNewLineInfo;
		private bool autoNewLine;
		private Image img;

		public void DrawToCanvas(ref Graphics g)
		{
			if (img != null)
			{
				g.DrawImage(img, X, Y, W, H);
			}
			else
			{
				var strSize = g.MeasureString(Text, Font);

				if (AutoNewLine)
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
					string outInfo = "";
					if (Text.Length == 2) outInfo = Text.Substring(0, 1) + " " + Text.Substring(1, 1); else outInfo = Text;
					g.DrawString(Text, Font, foreBrush, X + 0.5f * (W - strSize.Width), Y + 0.5f * (H - strSize.Height));
				}

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

	}
}
