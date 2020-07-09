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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InfoInput));
			this.IpBindingCol = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CmdOk = new System.Windows.Forms.Button();
			this.IpPosX = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.IpPosY = new System.Windows.Forms.TextBox();
			this.IpPosH = new System.Windows.Forms.TextBox();
			this.IpPosW = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.IpTagName = new System.Windows.Forms.TextBox();
			this.CmdAutoNewLine = new System.Windows.Forms.CheckBox();
			this.GrpAlign = new System.Windows.Forms.GroupBox();
			this.CmdAlignBottomRight = new System.Windows.Forms.Button();
			this.CmdAlignBottomCenter = new System.Windows.Forms.Button();
			this.CmdAlignBottomLeft = new System.Windows.Forms.Button();
			this.CmdAlignCenterRight = new System.Windows.Forms.Button();
			this.CmdAlignCenter = new System.Windows.Forms.Button();
			this.CmdAlignCenterLeft = new System.Windows.Forms.Button();
			this.CmdAlignTopRight = new System.Windows.Forms.Button();
			this.CmdAlignTopCenter = new System.Windows.Forms.Button();
			this.CmdAlignTopLeft = new System.Windows.Forms.Button();
			this.CmdColor = new System.Windows.Forms.Label();
			this.ColorSet = new System.Windows.Forms.ColorDialog();
			this.GrpAlign.SuspendLayout();
			this.SuspendLayout();
			// 
			// IpBindingCol
			// 
			this.IpBindingCol.Location = new System.Drawing.Point(62, 43);
			this.IpBindingCol.Name = "IpBindingCol";
			this.IpBindingCol.Size = new System.Drawing.Size(174, 21);
			this.IpBindingCol.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 52);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(53, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "绑定列名";
			// 
			// CmdOk
			// 
			this.CmdOk.Location = new System.Drawing.Point(772, 122);
			this.CmdOk.Name = "CmdOk";
			this.CmdOk.Size = new System.Drawing.Size(64, 24);
			this.CmdOk.TabIndex = 9;
			this.CmdOk.Text = "确定";
			this.CmdOk.UseVisualStyleBackColor = true;
			this.CmdOk.Click += new System.EventHandler(this.CmdOk_Click);
			// 
			// IpPosX
			// 
			this.IpPosX.Location = new System.Drawing.Point(62, 16);
			this.IpPosX.Name = "IpPosX";
			this.IpPosX.Size = new System.Drawing.Size(39, 21);
			this.IpPosX.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(27, 25);
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
			this.IpPosY.TabIndex = 1;
			// 
			// IpPosH
			// 
			this.IpPosH.Location = new System.Drawing.Point(197, 16);
			this.IpPosH.Name = "IpPosH";
			this.IpPosH.Size = new System.Drawing.Size(39, 21);
			this.IpPosH.TabIndex = 3;
			this.IpPosH.Text = "100";
			// 
			// IpPosW
			// 
			this.IpPosW.Location = new System.Drawing.Point(152, 16);
			this.IpPosW.Name = "IpPosW";
			this.IpPosW.Size = new System.Drawing.Size(39, 21);
			this.IpPosW.TabIndex = 2;
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
			this.IpTagName.TabIndex = 5;
			// 
			// CmdAutoNewLine
			// 
			this.CmdAutoNewLine.AutoSize = true;
			this.CmdAutoNewLine.Location = new System.Drawing.Point(11, 100);
			this.CmdAutoNewLine.Name = "CmdAutoNewLine";
			this.CmdAutoNewLine.Size = new System.Drawing.Size(72, 16);
			this.CmdAutoNewLine.TabIndex = 6;
			this.CmdAutoNewLine.Text = "自动换行";
			this.CmdAutoNewLine.UseVisualStyleBackColor = true;
			// 
			// GrpAlign
			// 
			this.GrpAlign.Controls.Add(this.CmdAlignBottomRight);
			this.GrpAlign.Controls.Add(this.CmdAlignBottomCenter);
			this.GrpAlign.Controls.Add(this.CmdAlignBottomLeft);
			this.GrpAlign.Controls.Add(this.CmdAlignCenterRight);
			this.GrpAlign.Controls.Add(this.CmdAlignCenter);
			this.GrpAlign.Controls.Add(this.CmdAlignCenterLeft);
			this.GrpAlign.Controls.Add(this.CmdAlignTopRight);
			this.GrpAlign.Controls.Add(this.CmdAlignTopCenter);
			this.GrpAlign.Controls.Add(this.CmdAlignTopLeft);
			this.GrpAlign.Location = new System.Drawing.Point(249, 10);
			this.GrpAlign.Name = "GrpAlign";
			this.GrpAlign.Size = new System.Drawing.Size(134, 136);
			this.GrpAlign.TabIndex = 11;
			this.GrpAlign.TabStop = false;
			this.GrpAlign.Text = "文字对齐方式";
			// 
			// CmdAlignBottomRight
			// 
			this.CmdAlignBottomRight.Location = new System.Drawing.Point(91, 98);
			this.CmdAlignBottomRight.Name = "CmdAlignBottomRight";
			this.CmdAlignBottomRight.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignBottomRight.TabIndex = 17;
			this.CmdAlignBottomRight.UseVisualStyleBackColor = true;
			// 
			// CmdAlignBottomCenter
			// 
			this.CmdAlignBottomCenter.Location = new System.Drawing.Point(49, 98);
			this.CmdAlignBottomCenter.Name = "CmdAlignBottomCenter";
			this.CmdAlignBottomCenter.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignBottomCenter.TabIndex = 16;
			this.CmdAlignBottomCenter.UseVisualStyleBackColor = true;
			// 
			// CmdAlignBottomLeft
			// 
			this.CmdAlignBottomLeft.Location = new System.Drawing.Point(7, 98);
			this.CmdAlignBottomLeft.Name = "CmdAlignBottomLeft";
			this.CmdAlignBottomLeft.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignBottomLeft.TabIndex = 15;
			this.CmdAlignBottomLeft.UseVisualStyleBackColor = true;
			// 
			// CmdAlignCenterRight
			// 
			this.CmdAlignCenterRight.Location = new System.Drawing.Point(91, 56);
			this.CmdAlignCenterRight.Name = "CmdAlignCenterRight";
			this.CmdAlignCenterRight.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignCenterRight.TabIndex = 14;
			this.CmdAlignCenterRight.UseVisualStyleBackColor = true;
			// 
			// CmdAlignCenter
			// 
			this.CmdAlignCenter.Location = new System.Drawing.Point(49, 56);
			this.CmdAlignCenter.Name = "CmdAlignCenter";
			this.CmdAlignCenter.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignCenter.TabIndex = 13;
			this.CmdAlignCenter.UseVisualStyleBackColor = true;
			// 
			// CmdAlignCenterLeft
			// 
			this.CmdAlignCenterLeft.Location = new System.Drawing.Point(7, 56);
			this.CmdAlignCenterLeft.Name = "CmdAlignCenterLeft";
			this.CmdAlignCenterLeft.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignCenterLeft.TabIndex = 12;
			this.CmdAlignCenterLeft.UseVisualStyleBackColor = true;
			// 
			// CmdAlignTopRight
			// 
			this.CmdAlignTopRight.Location = new System.Drawing.Point(90, 15);
			this.CmdAlignTopRight.Name = "CmdAlignTopRight";
			this.CmdAlignTopRight.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignTopRight.TabIndex = 11;
			this.CmdAlignTopRight.UseVisualStyleBackColor = true;
			// 
			// CmdAlignTopCenter
			// 
			this.CmdAlignTopCenter.Location = new System.Drawing.Point(48, 15);
			this.CmdAlignTopCenter.Name = "CmdAlignTopCenter";
			this.CmdAlignTopCenter.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignTopCenter.TabIndex = 10;
			this.CmdAlignTopCenter.UseVisualStyleBackColor = true;
			// 
			// CmdAlignTopLeft
			// 
			this.CmdAlignTopLeft.Location = new System.Drawing.Point(6, 15);
			this.CmdAlignTopLeft.Name = "CmdAlignTopLeft";
			this.CmdAlignTopLeft.Size = new System.Drawing.Size(36, 30);
			this.CmdAlignTopLeft.TabIndex = 9;
			this.CmdAlignTopLeft.UseVisualStyleBackColor = true;
			// 
			// CmdColor
			// 
			this.CmdColor.BackColor = System.Drawing.Color.White;
			this.CmdColor.Location = new System.Drawing.Point(13, 119);
			this.CmdColor.Name = "CmdColor";
			this.CmdColor.Size = new System.Drawing.Size(31, 26);
			this.CmdColor.TabIndex = 12;
			this.CmdColor.Click += new System.EventHandler(this.CmdColor_Click);
			// 
			// InfoInput
			// 
			this.AcceptButton = this.CmdOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(390, 316);
			this.Controls.Add(this.CmdColor);
			this.Controls.Add(this.GrpAlign);
			this.Controls.Add(this.CmdAutoNewLine);
			this.Controls.Add(this.IpTagName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.IpPosH);
			this.Controls.Add(this.IpPosW);
			this.Controls.Add(this.IpPosY);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.IpPosX);
			this.Controls.Add(this.CmdOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.IpBindingCol);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "InfoInput";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "InfoInput";
			this.GrpAlign.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox IpBindingCol;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button CmdOk;
		private System.Windows.Forms.TextBox IpPosX;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox IpPosY;
		private System.Windows.Forms.TextBox IpPosH;
		private System.Windows.Forms.TextBox IpPosW;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox IpTagName;
		private System.Windows.Forms.CheckBox CmdAutoNewLine;
		private System.Windows.Forms.GroupBox GrpAlign;
		private System.Windows.Forms.Button CmdAlignBottomRight;
		private System.Windows.Forms.Button CmdAlignBottomCenter;
		private System.Windows.Forms.Button CmdAlignBottomLeft;
		private System.Windows.Forms.Button CmdAlignCenterRight;
		private System.Windows.Forms.Button CmdAlignCenter;
		private System.Windows.Forms.Button CmdAlignCenterLeft;
		private System.Windows.Forms.Button CmdAlignTopRight;
		private System.Windows.Forms.Button CmdAlignTopCenter;
		private System.Windows.Forms.Button CmdAlignTopLeft;
		private System.Windows.Forms.Label CmdColor;
		private System.Windows.Forms.ColorDialog ColorSet;
	}
}