namespace 批模板生成
{
	partial class FrmPreView
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
			this.ImgMain = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.ImgMain)).BeginInit();
			this.SuspendLayout();
			// 
			// ImgMain
			// 
			this.ImgMain.Location = new System.Drawing.Point(8, 5);
			this.ImgMain.Name = "ImgMain";
			this.ImgMain.Size = new System.Drawing.Size(747, 508);
			this.ImgMain.TabIndex = 0;
			this.ImgMain.TabStop = false;
			// 
			// FrmPreView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(762, 513);
			this.Controls.Add(this.ImgMain);
			this.Name = "FrmPreView";
			this.Text = "FrmPreView";
			((System.ComponentModel.ISupportInitialize)(this.ImgMain)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox ImgMain;
	}
}