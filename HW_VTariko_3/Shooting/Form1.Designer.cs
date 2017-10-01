namespace Shooting
{
	partial class Form1
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
			this.btnShoot = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.lblWeapon = new System.Windows.Forms.Label();
			this.btnRecharge = new System.Windows.Forms.Button();
			this.btnTarget = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnShoot
			// 
			this.btnShoot.Location = new System.Drawing.Point(220, 201);
			this.btnShoot.Name = "btnShoot";
			this.btnShoot.Size = new System.Drawing.Size(90, 48);
			this.btnShoot.TabIndex = 0;
			this.btnShoot.Text = "Стрелять!";
			this.btnShoot.UseVisualStyleBackColor = true;
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(12, 52);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtLog.Size = new System.Drawing.Size(298, 143);
			this.txtLog.TabIndex = 1;
			// 
			// lblWeapon
			// 
			this.lblWeapon.AutoSize = true;
			this.lblWeapon.Location = new System.Drawing.Point(12, 13);
			this.lblWeapon.Name = "lblWeapon";
			this.lblWeapon.Size = new System.Drawing.Size(55, 13);
			this.lblWeapon.TabIndex = 2;
			this.lblWeapon.Text = "Пистолет";
			// 
			// btnRecharge
			// 
			this.btnRecharge.Location = new System.Drawing.Point(12, 201);
			this.btnRecharge.Name = "btnRecharge";
			this.btnRecharge.Size = new System.Drawing.Size(90, 48);
			this.btnRecharge.TabIndex = 3;
			this.btnRecharge.Text = "Перезарядка";
			this.btnRecharge.UseVisualStyleBackColor = true;
			// 
			// btnTarget
			// 
			this.btnTarget.Location = new System.Drawing.Point(116, 201);
			this.btnTarget.Name = "btnTarget";
			this.btnTarget.Size = new System.Drawing.Size(90, 48);
			this.btnTarget.TabIndex = 4;
			this.btnTarget.Text = "Прицелиться";
			this.btnTarget.UseVisualStyleBackColor = true;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(322, 261);
			this.Controls.Add(this.btnTarget);
			this.Controls.Add(this.btnRecharge);
			this.Controls.Add(this.lblWeapon);
			this.Controls.Add(this.txtLog);
			this.Controls.Add(this.btnShoot);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shooting";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnShoot;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Label lblWeapon;
		private System.Windows.Forms.Button btnRecharge;
		private System.Windows.Forms.Button btnTarget;
	}
}

