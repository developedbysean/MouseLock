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
        private bool lockState;
        
        public Form1()
        {
            InitializeComponent();

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
                lockState = true;
                lockToggle(lockState);

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

        private void autoDetect()
        {
            txtWidth.Text = getActiveScreenSize()[0].ToString();
            txtLength.Text = getActiveScreenSize()[1].ToString();
        }

        private void lockToggle(bool state)
        {
            screen screen = new screen(width, height);
            
            do
            {
                int x = Control.MousePosition.X;
                int y = Control.MousePosition.Y;
                if (x < screen.Zero)
                {
                    Cursor.Position = new Point(3, y);
                }
                if (y < screen.Zero)
                {
                    Cursor.Position = new Point(x, 3);
                }
                if (x > screen.Width)
                {
                    Cursor.Position = new Point(screen.Width - 3, y);
                }
                if (y > screen.Height)
                {
                    Cursor.Position = new Point(x, screen.Height - 3);
                }
                if (Control.IsKeyLocked(Keys.NumLock))
                {
                    state = false;
                }
            } while (state);
        }

        
        private void btnStop_Click(object sender, EventArgs e)
        {
            lockToggle(false);
        }

        private void btnAutoDetect_Click(object sender, EventArgs e)
        {
            autoDetect();
        }

    }
}
