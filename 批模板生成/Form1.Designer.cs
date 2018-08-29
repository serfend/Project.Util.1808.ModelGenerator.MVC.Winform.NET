using System;

namespace 批模板生成
{
	partial class FrmMain
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.MenuMain = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.新建ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.读取ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.xls数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.信息框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.背景ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.尺寸ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.复制信息框ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// MenuMain
			// 
			this.MenuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.编辑ToolStripMenuItem,
            this.关于ToolStripMenuItem});
			this.MenuMain.Name = "MenuMain";
			this.MenuMain.Size = new System.Drawing.Size(181, 92);
			// 
			// 文件ToolStripMenuItem
			// 
			this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新建ToolStripMenuItem,
            this.读取ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.toolStripMenuItem1,
            this.导出ToolStripMenuItem});
			this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
			this.文件ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F)));
			this.文件ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.文件ToolStripMenuItem.Text = "文件";
			// 
			// 新建ToolStripMenuItem
			// 
			this.新建ToolStripMenuItem.Name = "新建ToolStripMenuItem";
			this.新建ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.新建ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.新建ToolStripMenuItem.Text = "新建";
			this.新建ToolStripMenuItem.Click += new System.EventHandler(this.新建ToolStripMenuItem_Click);
			// 
			// 读取ToolStripMenuItem
			// 
			this.读取ToolStripMenuItem.Name = "读取ToolStripMenuItem";
			this.读取ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.读取ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.读取ToolStripMenuItem.Text = "读取";
			this.读取ToolStripMenuItem.Click += new System.EventHandler(this.读取ToolStripMenuItem_Click);
			// 
			// 保存ToolStripMenuItem
			// 
			this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
			this.保存ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.保存ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.保存ToolStripMenuItem.Text = "保存";
			this.保存ToolStripMenuItem.Click += new System.EventHandler(this.保存ToolStripMenuItem_Click_1);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(165, 6);
			// 
			// 导出ToolStripMenuItem
			// 
			this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
			this.导出ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.S)));
			this.导出ToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.导出ToolStripMenuItem.Text = "导出";
			this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click_1);
			// 
			// 编辑ToolStripMenuItem
			// 
			this.编辑ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xls数据ToolStripMenuItem,
            this.信息框ToolStripMenuItem,
            this.背景ToolStripMenuItem,
            this.尺寸ToolStripMenuItem,
            this.复制信息框ToolStripMenuItem});
			this.编辑ToolStripMenuItem.Name = "编辑ToolStripMenuItem";
			this.编辑ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.E)));
			this.编辑ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.编辑ToolStripMenuItem.Text = "编辑";
			this.编辑ToolStripMenuItem.Click += new System.EventHandler(this.编辑ToolStripMenuItem_Click);
			// 
			// xls数据ToolStripMenuItem
			// 
			this.xls数据ToolStripMenuItem.Name = "xls数据ToolStripMenuItem";
			this.xls数据ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.xls数据ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.xls数据ToolStripMenuItem.Text = "xls数据";
			this.xls数据ToolStripMenuItem.Click += new System.EventHandler(this.Xls数据ToolStripMenuItem_Click);
			// 
			// 信息框ToolStripMenuItem
			// 
			this.信息框ToolStripMenuItem.Name = "信息框ToolStripMenuItem";
			this.信息框ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.信息框ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.信息框ToolStripMenuItem.Text = "信息框";
			this.信息框ToolStripMenuItem.Click += new System.EventHandler(this.信息框ToolStripMenuItem_Click);
			// 
			// 背景ToolStripMenuItem
			// 
			this.背景ToolStripMenuItem.Name = "背景ToolStripMenuItem";
			this.背景ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
			this.背景ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.背景ToolStripMenuItem.Text = "背景";
			this.背景ToolStripMenuItem.Click += new System.EventHandler(this.背景ToolStripMenuItem_Click);
			// 
			// 尺寸ToolStripMenuItem
			// 
			this.尺寸ToolStripMenuItem.Name = "尺寸ToolStripMenuItem";
			this.尺寸ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
			this.尺寸ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.尺寸ToolStripMenuItem.Text = "尺寸";
			this.尺寸ToolStripMenuItem.Click += new System.EventHandler(this.尺寸ToolStripMenuItem_Click);
			// 
			// 关于ToolStripMenuItem
			// 
			this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
			this.关于ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
			this.关于ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.关于ToolStripMenuItem.Text = "关于";
			this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
			// 
			// 复制信息框ToolStripMenuItem
			// 
			this.复制信息框ToolStripMenuItem.Name = "复制信息框ToolStripMenuItem";
			this.复制信息框ToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
			this.复制信息框ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			this.复制信息框ToolStripMenuItem.Text = "复制信息框";
			this.复制信息框ToolStripMenuItem.Click += new System.EventHandler(this.复制信息框ToolStripMenuItem_Click);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(26F, 57F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
			this.ClientSize = new System.Drawing.Size(726, 462);
			this.ContextMenuStrip = this.MenuMain;
			this.Font = new System.Drawing.Font("微软雅黑", 32.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(13, 15, 13, 15);
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "模板批处理";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.MenuMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}


		#endregion

		private System.Windows.Forms.ContextMenuStrip MenuMain;
		private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 读取ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 编辑ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 新建ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem xls数据ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 信息框ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 背景ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 尺寸ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 复制信息框ToolStripMenuItem;
	}
}

