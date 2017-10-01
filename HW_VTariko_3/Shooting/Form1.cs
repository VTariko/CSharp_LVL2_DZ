using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Shooting.Arsenal;

namespace Shooting
{
	public partial class Form1 : Form
	{
		private readonly Weapon weapon;

		public Form1()
		{
			InitializeComponent();
			this.btnRecharge.Click += BtnRecharge_Click;
			this.btnTarget.Click += BtnTarget_Click;
			this.btnShoot.Click += BtnShoot_Click;
			weapon = new Pistol();
			weapon.MessageShoot += ShootLog_TextArea;
			weapon.MessageShoot += ShootLog_Console;
		}

		private void BtnShoot_Click(object sender, EventArgs e)
		{
			weapon.Shoot();
		}

		private void BtnTarget_Click(object sender, EventArgs e)
		{
			weapon.Target();
		}

		private void BtnRecharge_Click(object sender, EventArgs e)
		{
			weapon.Recharge();
		}

		private void ShootLog_Console(string msg)
		{
			Console.WriteLine(msg);
		}

		private void ShootLog_TextArea(string msg)
		{
			txtLog.Text += msg;
			txtLog.Text += Environment.NewLine;
		}
	}
}
