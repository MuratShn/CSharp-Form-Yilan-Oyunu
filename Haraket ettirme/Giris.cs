using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Haraket_ettirme
{
    public partial class Giris : Form
    {
        SqlConnection baglanti;
        SqlCommand komut;

        public Giris()
        {
            InitializeComponent();
        }
        public static string kadi;
        public static string sifre;

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti = new SqlConnection("server=.;Initial Catalog=snake;Integrated Security=True");
            baglanti.Open();

            kadi = textBox1.Text;
            sifre = textBox2.Text;


            string sorgu = "select * from kullanici where username = '" + kadi + "'";

            komut = new SqlCommand(sorgu, baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            if (oku.Read())
            {
                if (oku["password"].ToString() == sifre)
                {
                    MessageBox.Show("giris basarılı");
                    baglanti.Close();
                    baglanti.Dispose();
                    this.Hide();
                    AnaMenü a = new AnaMenü();
                    a.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Kullanici adi veya sifre hatalı");
                    baglanti.Close();
                    baglanti.Dispose();
                }
            }
            else
            {
                MessageBox.Show("Böyle Bir kullanıcı bulunmamaktadır lütfen kayıt olunuz");
            }


            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
