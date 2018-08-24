namespace 批模板生成
{
	partial class InfoInput
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
			this.IpBindingCol = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CmdOk = new System.Windows.Forms.Button();
			this.CmdDelete = new System.Windows.Forms.Button();
			this.IpPosX = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.IpPosY = new System.Windows.Forms.TextBox();
			this.fontSet = new System.Windows.Forms.FontDialog();
			this.CmdFontSet = new System.Windows.Forms.Button();
			this.IpPosH = new System.Windows.Forms.TextBox();
			this.IpPosW = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.IpTagName = new System.Windows.Forms.TextBox();
			this.CmdAutoNewLine = new System.Windows.Forms.CheckBox();
			this.CmdForeColor = new System.Windows.Forms.Button();
			this.colorSet = new System.Windows.Forms.ColorDialog();
			this.SuspendLayout();
			// 
			// IpBindingCol
			// 
			this.IpBindingCol.Location = new System.Drawing.Point(62, 43);
			this.IpBindingCol.Name = "IpBindingCol";
			this.IpBindingCol.Size = new System.Drawing.Size(174, 21);
			this.IpBindingCol.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "绑定列名";
			// 
			// CmdOk
			// 
			this.CmdOk.Location = new System.Drawing.Point(172, 95);
			this.CmdOk.Name = "CmdOk";
			this.CmdOk.Size = new System.Drawing.Size(64, 24);
			this.CmdOk.TabIndex = 2;
			this.CmdOk.Text = "确定";
			this.CmdOk.UseVisualStyleBackColor = true;
			this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
			// 
			// CmdDelete
			// 
			this.CmdDelete.Location = new System.Drawing.Point(172, 122);
			this.CmdDelete.Name = "CmdDelete";
			this.CmdDelete.Size = new System.Drawing.Size(64, 24);
			this.CmdDelete.TabIndex = 3;
			this.CmdDelete.Text = "删除";
			this.CmdDelete.UseVisualStyleBackColor = true;
			this.CmdDelete.Click += new System.EventHandler(this.CmdDelete_Click);
			// 
			// IpPosX
			// 
			this.IpPosX.Location = new System.Drawing.Point(62, 16);
			this.IpPosX.Name = "IpPosX";
			this.IpPosX.Size = new System.Drawing.Size(39, 21);
			this.IpPosX.TabIndex = 4;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(3, 19);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 12);
			this.label2.TabIndex = 5;
			this.label2.Text = "坐标";
			// 
			// IpPosY
			// 
			this.IpPosY.Location = new System.Drawing.Point(107, 16);
			this.IpPosY.Name = "IpPosY";
			this.IpPosY.Size = new System.Drawing.Size(39, 21);
			this.IpPosY.TabIndex = 6;
			// 
			// CmdFontSet
			// 
			this.CmdFontSet.Location = new System.Drawing.Point(6, 122);
			this.CmdFontSet.Name = "CmdFontSet";
			this.CmdFontSet.Size = new System.Drawing.Size(64, 24);
			this.CmdFontSet.TabIndex = 7;
			this.CmdFontSet.Text = "字体...";
			this.CmdFontSet.UseVisualStyleBackColor = true;
			this.CmdFontSet.Click += new System.EventHandler(this.CmdFontSet_Click);
			// 
			// IpPosH
			// 
			this.IpPosH.Location = new System.Drawing.Point(197, 16);
			this.IpPosH.Name = "IpPosH";
			this.IpPosH.Size = new System.Drawing.Size(39, 21);
			this.IpPosH.TabIndex = 9;
			this.IpPosH.Text = "100";
			// 
			// IpPosW
			// 
			this.IpPosW.Location = new System.Drawing.Point(152, 16);
			this.IpPosW.Name = "IpPosW";
			this.IpPosW.Size = new System.Drawing.Size(39, 21);
			this.IpPosW.TabIndex = 8;
			this.IpPosW.Text = "200";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(27, 80);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(29, 12);
			this.label3.TabIndex = 10;
			this.label3.Text = "标识";
			// 
			// IpTagName
			// 
			this.IpTagName.Location = new System.Drawing.Point(62, 71);
			this.IpTagName.Name = "IpTagName";
			this.IpTagName.Size = new System.Drawing.Size(174, 21);
			this.IpTagName.TabIndex = 11;
			// 
			// CmdAutoNewLine
			// 
			this.CmdAutoNewLine.AutoSize = true;
			this.CmdAutoNewLine.Location = new System.Drawing.Point(6, 103);
			this.CmdAutoNewLine.Name = "CmdAutoNewLine";
			this.CmdAutoNewLine.Size = new System.Drawing.Size(72, 16);
			this.CmdAutoNewLine.TabIndex = 12;
			this.CmdAutoNewLine.Text = "自动换行";
			this.CmdAutoNewLine.UseVisualStyleBackColor = true;
			// 
			// CmdForeColor
			// 
			this.CmdForeColor.Location = new System.Drawing.Point(76, 122);
			this.CmdForeColor.Name = "CmdForeColor";
			this.CmdForeColor.Size = new System.Drawing.Size(64, 24);
			this.CmdForeColor.TabIndex = 13;
			this.CmdForeColor.Text = "颜色...";
			this.CmdForeColor.UseVisualStyleBackColor = true;
			this.CmdForeColor.Click += new System.EventHandler(this.CmdForeColor_Click);
			// 
			// InfoInput
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(248, 148);
			this.Controls.Add(this.CmdForeColor);
			this.Controls.Add(this.CmdAutoNewLine);
			this.Controls.Add(this.IpTagName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.IpPosH);
			this.Controls.Add(this.IpPosW);
			this.Controls.Add(this.CmdFontSet);
			this.Controls.Add(this.IpPosY);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.IpPosX);
			this.Controls.Add(this.CmdDelete);
			this.Controls.Add(this.CmdOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.IpBindingCol);
			this.Name = "InfoInput";
			this.Text = "InfoInput";
			this.Load += new System.EventHandler(this.InfoInput_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox IpBindingCol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button CmdOk;
		private System.Windows.Forms.Button CmdDelete;
		private System.Windows.Forms.TextBox IpPosX;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox IpPosY;
		private System.Windows.Forms.FontDialog fontSet;
		private System.Windows.Forms.Button CmdFontSet;
		private System.Windows.Forms.TextBox IpPosH;
		private System.Windows.Forms.TextBox IpPosW;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox IpTagName;
		private System.Windows.Forms.CheckBox CmdAutoNewLine;
		private System.Windows.Forms.Button CmdForeColor;
		private System.Windows.Forms.ColorDialog colorSet;
	}
}