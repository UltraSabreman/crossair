namespace crossair {
	partial class TextEnterBox {
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
			this.Ok = new System.Windows.Forms.Button();
			this.Cancel = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.bindButton1 = new crossair.bindButton();
			this.SuspendLayout();
			// 
			// Ok
			// 
			this.Ok.Location = new System.Drawing.Point(206, 51);
			this.Ok.Name = "Ok";
			this.Ok.Size = new System.Drawing.Size(75, 23);
			this.Ok.TabIndex = 0;
			this.Ok.Text = "Ok";
			this.Ok.UseVisualStyleBackColor = true;
			this.Ok.Click += new System.EventHandler(this.Ok_Click);
			// 
			// Cancel
			// 
			this.Cancel.Location = new System.Drawing.Point(287, 51);
			this.Cancel.Name = "Cancel";
			this.Cancel.Size = new System.Drawing.Size(75, 23);
			this.Cancel.TabIndex = 1;
			this.Cancel.Text = "Cancel";
			this.Cancel.UseVisualStyleBackColor = true;
			this.Cancel.Click += new System.EventHandler(this.Cancel_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(15, 25);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(347, 20);
			this.textBox1.TabIndex = 2;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			this.textBox1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyUp);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(350, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Enter the name of the window you want to attach to (you can use regex):";
			// 
			// bindButton1
			// 
			this.bindButton1.Location = new System.Drawing.Point(15, 51);
			this.bindButton1.Name = "bindButton1";
			this.bindButton1.Size = new System.Drawing.Size(185, 23);
			this.bindButton1.TabIndex = 4;
			this.bindButton1.Text = "lol";
			this.bindButton1.UseVisualStyleBackColor = true;
			// 
			// TextEnterBox
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(373, 86);
			this.Controls.Add(this.bindButton1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.Cancel);
			this.Controls.Add(this.Ok);
			this.Name = "TextEnterBox";
			this.Text = "Enter Window Name";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button Ok;
		private System.Windows.Forms.Button Cancel;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private bindButton bindButton1;
	}
}