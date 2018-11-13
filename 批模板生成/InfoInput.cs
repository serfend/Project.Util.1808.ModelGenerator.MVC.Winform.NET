using DotNet4.Utilities.UtilReg;
using Inst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace 批模板生成
{
	public partial class InfoInput : Form
	{
		private Action callBack;
		private InfoSetting setting;	
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
			colorSet.Color = setting.ForeColor;
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
				MessageBox.Show(ex.Message+"\n"+ex.StackTrace);
			}
			callBack.BeginInvoke((x)=> { },new object ());
			this.Close();
		}

		private void CmdDelete_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void CmdFontSet_Click(object sender, EventArgs e)
		{
			try
			{
				OnSelectSetting = true;
				if (fontSet.ShowDialog() == DialogResult.OK)
				{
					setting.FontName = fontSet.Font.Name;
					setting.FontSize = fontSet.Font.Size;
				}
				OnSelectSetting = false;
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
		}

		private void InfoInput_Load(object sender, EventArgs e)
		{

		}

		private void CmdForeColor_Click(object sender, EventArgs e)
		{
			OnSelectSetting = true;
			if (colorSet.ShowDialog() == DialogResult.OK)
			{
				setting.ForeColor = colorSet.Color;
			}
			OnSelectSetting = false;
		}
	}
}
