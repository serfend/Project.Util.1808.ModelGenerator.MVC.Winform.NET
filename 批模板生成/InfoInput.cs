using DotNet4.Utilities.UtilReg;
using Inst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Cyotek.Windows.Forms;

namespace 批模板生成
{
	public partial class InfoInput : Form
	{
		private Action callBack;
		private InfoSetting setting;
		private Control CtlShowTextRenderDemo;
		Cyotek.Windows.Forms.FontDialog fontSet=new Cyotek.Windows.Forms.FontDialog();
		public InfoInput(InfoSetting setting,Action SubmitCallBack)
		{
			this.callBack = SubmitCallBack;
			InitializeComponent();
			
			this.setting = setting;
			this.IpPosX.Text = setting.X.ToString();
			this.IpPosY.Text = setting.Y.ToString();
			this.IpPosW.Text = setting.W.ToString();
			this.IpPosH.Text = setting.H.ToString();
			this.IpBindingCol.Text = setting.TargetCol;
			this.IpTagName.Text = setting.TagName;
			this.CmdAutoNewLine.Checked = setting.AutoNewLine;
			foreach(var ctl in GrpAlign.Controls)
			{
				if(ctl is Button cmd)
				{
					if (cmd.Name.Substring(8)==setting.TextAlign) {
						cmd.Enabled = false;
						break;
					}
				}
			}
			fontSet.Font = new Font(setting.FontName, setting.FontSize);
			fontSet.Color = setting.ForeColor;
			CtlShowTextRenderDemo=new Label()
			{
				Parent = this,
				Left = 0,
				Top = (int) (this.Height*0.55),
				Width = this.Width,
				Height = (int) (this.Height*0.45),
				Text = "文字ABC123",
				ForeColor = fontSet.Color,
				Font = fontSet.Font,
				Visible = true
			};
			CtlShowTextRenderDemo.MouseClick += CmdFontSet;
			foreach (var ctl in GrpAlign.Controls)
			{
				if (ctl is Button cmd)
				{
					if (cmd.Name.Contains("CmdAlign"))
					{
						cmd.Click += (x, xx) =>
						{
							ResetCmdAlign();
							setting.TextAlign = cmd.Name.Substring(8);
							cmd.Enabled = false;
						};
					}
				}
			}
			this.Deactivate += (xx, xxx) => { if(!OnSelectSetting)this.Close(); };
			rectDescription=new Rectangle((int) (Width*0.02),(int)(Height*0.45),(int)(Width*0.96),(int)(Height*0.28));
		}
		private bool OnSelectSetting = false;
		
		private void ResetCmdAlign()
		{
			CmdAlignCenter.Enabled = true;
			CmdAlignCenterLeft.Enabled = true;
			CmdAlignCenterRight.Enabled = true;
			CmdAlignTopLeft.Enabled = true;
			CmdAlignTopCenter.Enabled = true;
			CmdAlignTopRight.Enabled = true;
			CmdAlignBottomLeft.Enabled = true;
			CmdAlignBottomRight.Enabled = true;
			CmdAlignBottomCenter.Enabled = true;
			GrpAlign.Focus();
		}
		private void CmdOk_Click(object sender, EventArgs e)
		{
			if (IpBindingCol.Text.Length == 0)
			{
				IpBindingCol.Text = "未绑定的项";
			}
			setting.TargetCol = IpBindingCol.Text;
			setting.TagName = IpTagName.Text;
			setting.AutoNewLine = CmdAutoNewLine.Checked;
			try
			{
				setting.X = Convert.ToInt32(this.IpPosX.Text);
				setting.Y = Convert.ToInt32(this.IpPosY.Text);
				setting.W = Convert.ToInt32(this.IpPosW.Text);
				setting.H = Convert.ToInt32(this.IpPosH.Text);
				if (setting.W< 10) setting.W= 10;
				if (setting.H < 10) setting.H = 10;
			}
			catch (Exception ex)
			{
				MessageBox.Show($"{ex.Message}\n{ex.StackTrace}");
			}
			callBack.BeginInvoke((x)=> { },new object ());
			this.Close();
		}

		private void CmdDelete_Click(object sender, EventArgs e)
		{
			this.Close();
		}


		private const string description = "标签应与Excel表中列名完全一致，并保证列头位于第一张表的第一行。使用照片路径时，应保证照片的路径正确";
		private Rectangle rectDescription;
		protected override void OnPaint(PaintEventArgs e)
		{
			e.Graphics.DrawString(description,this.Font,Brushes.Black,rectDescription);
			base.OnPaint(e);
		}

		private void CmdFontSet(object sender, EventArgs e)
		{
			OnSelectSetting = true;
			try
			{
				if (fontSet.ShowDialog() == DialogResult.OK)
				{
					setting.ForeColor = fontSet.Color;
					CtlShowTextRenderDemo.ForeColor = fontSet.Color;
					setting.FontName = fontSet.Font.Name;
					setting.FontSize = fontSet.Font.Size;
					CtlShowTextRenderDemo.Font = fontSet.Font;
				}
			}
			catch (Exception ex)
			{
				var f = new InfoShower()
				{
					Title = "设置字体失败",
					Info = ex.Message,
					TitleColor=Color.FromArgb(255,200,100)
				};
				InfoShower.ShowOnce(f);
			}
		

			OnSelectSetting = false;
		}

		private void CmdColor_Click(object sender, EventArgs e)
		{
			OnSelectSetting = true;
			try
			{
				if (ColorSet.ShowDialog() == DialogResult.OK)
				{
					setting.ForeColor = ColorSet.Color;
					CtlShowTextRenderDemo.ForeColor = fontSet.Color;
					CmdColor.BackColor = fontSet.Color;
				}
			}
			catch (Exception ex)
			{
				var f = new InfoShower()
				{
					Title = "设置字体颜色失败",
					Info = ex.Message,
					TitleColor=Color.FromArgb(255,200,100)
				};
				InfoShower.ShowOnce(f);
			}
		

			OnSelectSetting = false;
		}
		}
	}
