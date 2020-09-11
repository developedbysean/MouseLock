using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MouseLock
{
    public partial class Form1 : Form
    {
        private int width;
        private int height;
        private bool lockState = false;
        screen s;
        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
        }
        private void InitializeBackgroundWorker()
        {
            backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            do
            {
                int x = Control.MousePosition.X;
                int y = Control.MousePosition.Y;
                if (x < s.Zero)
                {
                    Cursor.Position = new Point(10, y);
                }
                if (y < s.Zero)
                {
                    Cursor.Position = new Point(x, 10);
                }
                if (x > s.Width)
                {
                    Cursor.Position = new Point(s.Width - 10, y);
                }
                if (y > s.Height)
                {
                    Cursor.Position = new Point(x, s.Height - 10);
                }
            } while (lockState);
            backgroundWorker1.CancelAsync();
        }

        private void txtWidth_Leave(object sender, EventArgs e)
        {
            if (txtWidth.Text == null)
            {
                getActiveScreenSize();
            }
        }

        private int[] getActiveScreenSize()
        {
            int[] s = { Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height };
            return s;
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            try
            {
                width = int.Parse(txtWidth.Text);
                height = int.Parse(txtLength.Text);
                
                if (width == 0)
                {
                    throw new Exception();
                }

                s = new screen(width, height);
                lockToggle(true);
                btnStop.Enabled = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (FormatException)
            {
                lblStatus.Text = "Enter a valid numeric value";
            }
            catch (Exception)
            {
                lblStatus.Text = "Enter a non-zero value";
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            lockToggle(false);

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void autoDetect()
        {
            txtWidth.Text = getActiveScreenSize()[0].ToString();
            txtLength.Text = getActiveScreenSize()[1].ToString();
        }

        private void lockToggle(bool state)
        {
            lockState = state;
        }

        private void btnAutoDetect_Click(object sender, EventArgs e)
        {
            autoDetect();
        }

        private void btnStop_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                lockToggle(false);
            }
        }
    }
}
