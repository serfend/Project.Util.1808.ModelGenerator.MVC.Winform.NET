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
	public partial class FrmPreView : Form
	{
		public FrmPreView()
		{
			InitializeComponent();
			this.Top = Screen.PrimaryScreen.Bounds.Top;
			this.Left = Screen.PrimaryScreen.Bounds.Left;
			this.Width = Screen.PrimaryScreen.Bounds.Width;
			this.Height = Screen.PrimaryScreen.Bounds.Height;
		}
	}
}
