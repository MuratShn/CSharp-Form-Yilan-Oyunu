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
    public partial class SkorTablosu : Form
    {
        public SkorTablosu()
        {
            InitializeComponent();

        }

        private void SkorTablosu_Load(object sender, EventArgs e)
        {
            string[] name = new string[5];
            string[] score = new string[5];

            SqlConnection baglanti;
            SqlCommand komut;

            baglanti = new SqlConnection("server=.;Initial Catalog=snake;Integrated Security=True");
            baglanti.Open();

            string sorgu = "SELECT * FROM kullanici ORDER BY score DESC;";

            komut = new SqlCommand(sorgu, baglanti);

            SqlDataReader oku = komut.ExecuteReader();

            int sayac = 0;

            while (oku.Read())
            {
                if (sayac < 5)
                {
                    name[sayac] = oku["username"].ToString().ToUpper();
                    score[sayac] = oku["score"].ToString().ToUpper();

                    sayac += 1;
                }
            }

            baglanti.Close();

            label1.Text = ("1: " + name[0] + " : " + score[0] + "  👑").ToString();
            label2.Text = ("2: " + name[1] + " : " + score[1]).ToString();
            label3.Text = ("3: " + name[2] + " : " + score[2]).ToString();
            label4.Text = ("4: " + name[3] + " : " + score[3]).ToString();
            label5.Text = ("5: " + name[4] + " : " + score[4]).ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AnaMenü a = new AnaMenü();
            a.ShowDialog();
        }
    }
}
