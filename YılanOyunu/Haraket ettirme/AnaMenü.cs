using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Haraket_ettirme
{
    public partial class AnaMenü : Form
    {
        public AnaMenü()
        {
            InitializeComponent();
            label2.Text = Giris.kadi.ToUpper();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide ();
            OyunKısmı a = new OyunKısmı();
            a.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            SkorTablosu a = new SkorTablosu();
            a.ShowDialog();
        }
    }
}
