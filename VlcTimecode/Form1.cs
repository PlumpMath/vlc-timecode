using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using FMUtils.KeyboardHook;

namespace VlcTimecode {
	public partial class Form1 : Form
	{
		private Boolean triggered = false;

		public Form1()
		{
			InitializeComponent();

			// Everytime a key is press we want to display it in a TextBox
			var KeyboardHook = new Hook("Global Action Hook");
			KeyboardHook.KeyDownEvent += (args) =>
			{
				if (args.Key == Keys.Q && args.isCtrlPressed && args.isAltPressed)
				{
					Task.Factory.StartNew(
						() =>
						{
							Debug.WriteLine("Waiting for release...");
							while (Control.ModifierKeys != 0)
							{
								// wait;
							}
							Debug.WriteLine("Released, go!");
							TimeCoder.GetTimecode();
						});
				}
			};
		}

		private void button1_Click(Object sender, EventArgs e)
		{
			this.textBox1.Focus();
			this.textBox1.SelectAll();
			TimeCoder.GetTimecode();
		}
	}
}
