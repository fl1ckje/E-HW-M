namespace E_HW_M
{
	partial class MainForm
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
			if(disposing && (components != null))
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.comComboBox = new System.Windows.Forms.ComboBox();
			this.comLabel = new System.Windows.Forms.Label();
			this.connectionGroupBox = new System.Windows.Forms.GroupBox();
			this.txDataTextBox = new System.Windows.Forms.TextBox();
			this.txLabel = new System.Windows.Forms.Label();
			this.connectButton = new System.Windows.Forms.Button();
			this.updateComButton = new System.Windows.Forms.Button();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.connectionStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.connectionGroupBox.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// comComboBox
			// 
			this.comComboBox.DisplayMember = "ds";
			this.comComboBox.FormattingEnabled = true;
			this.comComboBox.Location = new System.Drawing.Point(69, 16);
			this.comComboBox.Name = "comComboBox";
			this.comComboBox.Size = new System.Drawing.Size(66, 21);
			this.comComboBox.Sorted = true;
			this.comComboBox.TabIndex = 0;
			// 
			// comLabel
			// 
			this.comLabel.AutoSize = true;
			this.comLabel.Location = new System.Drawing.Point(6, 19);
			this.comLabel.Name = "comLabel";
			this.comLabel.Size = new System.Drawing.Size(57, 13);
			this.comLabel.TabIndex = 1;
			this.comLabel.Text = "Serial port:";
			// 
			// connectionGroupBox
			// 
			this.connectionGroupBox.Controls.Add(this.txDataTextBox);
			this.connectionGroupBox.Controls.Add(this.txLabel);
			this.connectionGroupBox.Controls.Add(this.connectButton);
			this.connectionGroupBox.Controls.Add(this.updateComButton);
			this.connectionGroupBox.Controls.Add(this.comLabel);
			this.connectionGroupBox.Controls.Add(this.comComboBox);
			this.connectionGroupBox.Location = new System.Drawing.Point(12, 12);
			this.connectionGroupBox.Name = "connectionGroupBox";
			this.connectionGroupBox.Size = new System.Drawing.Size(279, 73);
			this.connectionGroupBox.TabIndex = 2;
			this.connectionGroupBox.TabStop = false;
			this.connectionGroupBox.Text = "Connection";
			// 
			// txDataTextBox
			// 
			this.txDataTextBox.Location = new System.Drawing.Point(69, 43);
			this.txDataTextBox.Name = "txDataTextBox";
			this.txDataTextBox.ReadOnly = true;
			this.txDataTextBox.Size = new System.Drawing.Size(124, 20);
			this.txDataTextBox.TabIndex = 5;
			// 
			// txLabel
			// 
			this.txLabel.AutoSize = true;
			this.txLabel.Location = new System.Drawing.Point(7, 46);
			this.txLabel.Name = "txLabel";
			this.txLabel.Size = new System.Drawing.Size(56, 13);
			this.txLabel.TabIndex = 4;
			this.txLabel.Text = "Sent data:";
			// 
			// connectButton
			// 
			this.connectButton.Location = new System.Drawing.Point(199, 15);
			this.connectButton.Name = "connectButton";
			this.connectButton.Size = new System.Drawing.Size(74, 23);
			this.connectButton.TabIndex = 3;
			this.connectButton.Text = "Connect";
			this.connectButton.UseVisualStyleBackColor = true;
			this.connectButton.Click += new System.EventHandler(this.ConnectButtonClick);
			// 
			// updateComButton
			// 
			this.updateComButton.Location = new System.Drawing.Point(141, 15);
			this.updateComButton.Name = "updateComButton";
			this.updateComButton.Size = new System.Drawing.Size(52, 23);
			this.updateComButton.TabIndex = 2;
			this.updateComButton.Text = "Update";
			this.updateComButton.UseVisualStyleBackColor = true;
			this.updateComButton.Click += new System.EventHandler(this.UpdateComButtonClick);
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectionStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 91);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(303, 22);
			this.statusStrip1.TabIndex = 3;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// connectionStatusLabel
			// 
			this.connectionStatusLabel.ForeColor = System.Drawing.Color.Red;
			this.connectionStatusLabel.Name = "connectionStatusLabel";
			this.connectionStatusLabel.Size = new System.Drawing.Size(79, 17);
			this.connectionStatusLabel.Text = "Disconnected";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(303, 113);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.connectionGroupBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "E.HW.M.";
			this.connectionGroupBox.ResumeLayout(false);
			this.connectionGroupBox.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox comComboBox;
		private System.Windows.Forms.Label comLabel;
		private System.Windows.Forms.GroupBox connectionGroupBox;
		private System.Windows.Forms.Button updateComButton;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel connectionStatusLabel;
		private System.Windows.Forms.Label txLabel;
		private System.Windows.Forms.TextBox txDataTextBox;
	}
}

