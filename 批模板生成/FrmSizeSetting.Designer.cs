namespace 批模板生成
{
	partial class FrmSizeSetting
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSizeSetting));
			this.IpWidthtSet = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.IpHeightSet = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.CmdConfirm = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// IpWidthtSet
			// 
			this.IpWidthtSet.Location = new System.Drawing.Point(38, 9);
			this.IpWidthtSet.Name = "IpWidthtSet";
			this.IpWidthtSet.Size = new System.Drawing.Size(99, 21);
			this.IpWidthtSet.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(17, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "宽";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(15, 39);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 12);
			this.label2.TabIndex = 3;
			this.label2.Text = "高";
			// 
			// IpHeightSet
			// 
			this.IpHeightSet.Location = new System.Drawing.Point(38, 36);
			this.IpHeightSet.Name = "IpHeightSet";
			this.IpHeightSet.Size = new System.Drawing.Size(99, 21);
			this.IpHeightSet.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(143, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 12);
			this.label3.TabIndex = 4;
			this.label3.Text = "px";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(143, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 12);
			this.label4.TabIndex = 5;
			this.label4.Text = "px";
			// 
			// CmdConfirm
			// 
			this.CmdConfirm.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.CmdConfirm.Location = new System.Drawing.Point(39, 65);
			this.CmdConfirm.Name = "CmdConfirm";
			this.CmdConfirm.Size = new System.Drawing.Size(97, 28);
			this.CmdConfirm.TabIndex = 6;
			this.CmdConfirm.Text = "保存";
			this.CmdConfirm.UseVisualStyleBackColor = true;
			this.CmdConfirm.Click += new System.EventHandler(this.CmdConfirm_Click);
			// 
			// FrmSizeSetting
			// 
			this.AcceptButton = this.CmdConfirm;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(173, 98);
			this.Controls.Add(this.CmdConfirm);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.IpHeightSet);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.IpWidthtSet);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FrmSizeSetting";
			this.Text = "FrmSizeSetting";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox IpWidthtSet;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox IpHeightSet;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button CmdConfirm;
	}
}