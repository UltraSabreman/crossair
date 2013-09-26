namespace crossair {
	partial class Options {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.bindButton1 = new crossair.bindButton();
			this.bindButton2 = new crossair.bindButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(430, 107);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(309, 20);
			this.textBox1.TabIndex = 0;
			// 
			// bindButton1
			// 
			this.bindButton1.Location = new System.Drawing.Point(111, 45);
			this.bindButton1.Name = "bindButton1";
			this.bindButton1.Size = new System.Drawing.Size(180, 23);
			this.bindButton1.TabIndex = 1;
			this.bindButton1.Text = "ReloadBind";
			this.bindButton1.UseVisualStyleBackColor = true;
			this.bindButton1.OnKeyAccept += new crossair.bindButton.KeysDone(this.bindButton1_OnKeyAccept);
			// 
			// bindButton2
			// 
			this.bindButton2.Location = new System.Drawing.Point(111, 16);
			this.bindButton2.Name = "bindButton2";
			this.bindButton2.Size = new System.Drawing.Size(180, 23);
			this.bindButton2.TabIndex = 2;
			this.bindButton2.Text = "Togglebind";
			this.bindButton2.UseVisualStyleBackColor = true;
//			this.bindButton2.OnKeyAccept += new crossair.bindButton.KeysDone(this.bindButton2_OnKeyAccept);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.bindButton2);
			this.groupBox1.Controls.Add(this.bindButton1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new System.Drawing.Point(12, 32);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(297, 75);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Key Binds";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 45);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(76, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Reload Image:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Toggle Overlay:";
			// 
			// Options
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(782, 370);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.textBox1);
			this.Name = "Options";
			this.Text = "Options";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private bindButton bindButton1;
		private bindButton bindButton2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}