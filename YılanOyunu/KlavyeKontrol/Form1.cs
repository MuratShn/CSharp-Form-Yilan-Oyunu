using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlavyeKontrol
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void button1_KeyDown(object sender, KeyEventArgs e)
        {
            int x = button1.Location.X;
            int y = button1.Location.Y;

            
            if (e.KeyCode == Keys.D)
            {
                button1.Location = new Point(x + 50, y);
            }
            if (e.KeyCode == Keys.A)
            {
                button1.Location = new Point(x - 50, y);
            }
            if (e.KeyCode == Keys.S)
            {
                button1.Location = new Point(x, y + 50);

            }
            if (e.KeyCode == Keys.W)
            {
                button1.Location = new Point(x, y - 50);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
