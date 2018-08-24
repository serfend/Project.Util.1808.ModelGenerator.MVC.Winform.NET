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
	public partial class FrmSizeSetting : Form
	{
		private Action<int, int> settingModefy;
		public FrmSizeSetting(int width,int height,Action<int, int> settingModefy)
		{
			this.settingModefy = settingModefy;
			InitializeComponent();
			IpWidthtSet.Text = Convert.ToString(width);
			IpHeightSet.Text = Convert.ToString(height);
		}
		private void CmdConfirm_Click(object sender, EventArgs e)
		{
			settingModefy?.Invoke(Convert.ToInt32(IpWidthtSet.Text), Convert.ToInt32(IpHeightSet.Text));
			this.Close();
		}
	}
}
