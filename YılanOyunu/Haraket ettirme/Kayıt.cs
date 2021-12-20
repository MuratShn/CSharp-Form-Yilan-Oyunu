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
    public partial class Kayıt : Form
    {
        public Kayıt()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection baglanti;
            SqlCommand komut;

            baglanti = new SqlConnection("server=.;Initial Catalog=snake;Integrated Security=True");


            baglanti.Open();

            string kadi = textBox1.Text;
            string sifre = textBox2.Text;


            string sorgu = "select * from kullanici";

            komut = new SqlCommand(sorgu, baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            int sayac = 0;

            while (oku.Read())
            {
                if (oku["username"].ToString() == kadi)
                {
                    sayac += 1;
                }
            }
            oku.Close();

            if (sayac > 0 || kadi == "")
            {
                MessageBox.Show("Bu kullanıcı adına sahip bir kisi bulunmakta lutfen farklı bi kullanıcı adi giriniz");
            }
            else
            {

                sorgu = "insert into kullanici values(@username,@password,@score)";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@username", kadi);
                komut.Parameters.AddWithValue("@password", sifre);
                komut.Parameters.AddWithValue("@score", 0);
                komut.ExecuteNonQuery();
                MessageBox.Show("Kayıt başarılı");

                this.Hide();
                Giris a = new Giris();
                a.ShowDialog();
            }

        }
    }
}
