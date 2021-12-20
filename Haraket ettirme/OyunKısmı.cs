using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.SqlClient;


namespace Haraket_ettirme
{
    public partial class OyunKısmı : Form
    {
        Random random = new Random();

        List<int> kuyruk_x = new List<int>();
        List<int> kuyruk_y = new List<int>();

        bool oyun = false;

        int Skor = 0;
        int yem_x = -1; // yem kordinatları
        int yem_y = -1;

        public int X = 1, Y = 1;
        public Keys yön = Keys.D;

        public OyunKısmı()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            label1.Text = Skor.ToString();
            label5.Text = Giris.kadi.ToUpper();


            kuyruk_x.Add(0); //baslangıc yerini belirlıyporuz buda yılanın kafası oluyo
            kuyruk_y.Add(0); // yani kuyruk[0] bas oluyo


            CalcTable();

            Thread thread = new Thread(new ThreadStart(new Action(() =>
            {

                CalcTable();
                while (true)
                {
                    if (!oyun)
                    {

                        Thread.Sleep(20);
                        if (yön == Keys.W)
                        {
                            Y--;
                        }
                        else if (yön == Keys.S)
                        {
                            Y++;
                        }
                        else if (yön == Keys.A)
                        {
                            X--;
                        }
                        else if (yön == Keys.D)
                        {
                            X++;
                        }



                        CalcTable();
                        
                    }
                    else
                    {
                        try
                        {
                            Invoke(new Action(() => panel1.Visible = true));

                            //Skor kaydetme
                            SqlConnection baglanti;
                            string ad = Giris.kadi;
                            SqlCommand komut;
                            baglanti = new SqlConnection("server=.;Initial Catalog=snake;Integrated Security=True");
                            baglanti.Open();
                            string sorgu = "select username,score from kullanici";
                            komut = new SqlCommand(sorgu, baglanti);
                            SqlDataReader oku = komut.ExecuteReader();
                            while (oku.Read())
                            {
                                if (oku["username"].ToString() == ad)
                                {
                                    int dskor = int.Parse(oku["score"].ToString());

                                    if (dskor < Skor)
                                    {
                                        oku.Close();

                                        sorgu = "UPDATE kullanici SET score = @skor WHERE username = @name";
                                        komut = new SqlCommand(sorgu, baglanti);
                                        komut.Parameters.AddWithValue("@name", ad);
                                        komut.Parameters.AddWithValue("@skor", Skor);
                                        komut.ExecuteNonQuery();
                                        break;
                                    }
                                }
                            }


                        }
                        catch
                        { }
                        Thread.Sleep(100);
                    }
                }
            })));

            thread.Start();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                yön = Keys.W;

            }
            else if (e.KeyCode == Keys.S)
            {
                yön = Keys.S;
            }
            else if (e.KeyCode == Keys.A)
            {
                yön = Keys.A;
            }
            else if (e.KeyCode == Keys.D)
            {
                yön = Keys.D;
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.Focus();

            Skor = 0;
            yem_x = -1; // yem kordinatları
            yem_y = -1;

            X = 1;
            Y = 1;
            yön = Keys.D;

            kuyruk_x.Clear();
            kuyruk_y.Clear();

            kuyruk_x.Add(0);
            kuyruk_y.Add(0);
            try
            {
                Invoke(new Action(() => panel1.Visible = false));
            }
            catch
            { }


            oyun = false;
            this.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            AnaMenü a = new AnaMenü();
            a.ShowDialog();
       
            
        }
        public void CalcTable()
        {
            try
            {
                Invoke(new Action(() => label1.Text = Skor.ToString()));
            }
            catch
            { }

            //label1.Text = Skor.ToString();

            Bitmap bitmap = new Bitmap(500, 400);


            // Kendi kuyrugunu yeme
            if (Skor > 1)
            {

                for (int i = 1; i < kuyruk_x.Count; i++)
                {
                    if (kuyruk_x[i] == X && kuyruk_y[i] == Y) // ((kuyruk_x[i - 1] - 1) * 10 == (X - 1) * 10 && (kuyruk_y[i - 1] - 1) * 10 == (Y - 1) * 10) ### Bunu boyle uzun yoldanda yazabılırsın
                    {
                        oyun = true;
                    }
                }
            }
            /////////


            //alandan cıkma 

            if (X <= 0 || Y <= 0 || X >= 51 || Y >= 41)
            {
                oyun = true;
            }
            else
            {
                //Yılanın kendi Kısmı
                for (int i = (X - 1) * 10; i < X * 10; i++)
                {
                    for (int j = (Y - 1) * 10; j < Y * 10; j++)
                    {
                        bitmap.SetPixel(i, j, Color.Black);
                    }
                }
            }

            // Kuyrukları gosterme
            if (kuyruk_x.Count !=1)
            {
                for (int i = 0; i < kuyruk_x.Count; i++) // Kuyrukların gosterılme yeri
                {
                    for (int j = (kuyruk_x[i] - 1) * 10; j < kuyruk_x[i] * 10; j++)
                    {
                        for (int k = (kuyruk_y[i] - 1) * 10; k < kuyruk_y[i] * 10; k++)
                        {
                            bitmap.SetPixel(j, k, Color.Purple);
                        }
                    }
                }
            }

            // Kuyrukları gosterme

            kuyruk_x[0] = (X);
            kuyruk_y[0] = (Y);

            for (int i = kuyruk_x.Count - 1; i > 0; i--) // bi eksıgıne getirme mantıgı
            {
                kuyruk_x[i] = kuyruk_x[i - 1]; //kuyrakları eklıyoruz 
                kuyruk_y[i] = kuyruk_y[i - 1];

            }


            //Yem Kısmı
            if (yem_x == -1)
            {
                yem_x = random.Next(1, 49);
                yem_y = random.Next(1, 39);
            }
            for (int i = (yem_x - 1) * 10; i < yem_x * 10; i++)
            {
                for (int j = (yem_y - 1) * 10; j < yem_y * 10; j++)
                {
                    bitmap.SetPixel(i, j, Color.Aqua);
                }
            }


            if ((X - 1) * 10 == (yem_x - 1) * 10 && (Y - 1) * 10 == (yem_y - 1) * 10) // yemin yenmiş olma kısmı
            {
                Skor += 1;

                kuyruk_x.Add(yem_x);
                kuyruk_y.Add(yem_y);


                yem_x = random.Next(1, 49);
                yem_y = random.Next(1, 39);

                for (int i = (yem_x - 1) * 10; i < yem_x * 10; i++)
                {
                    for (int j = (yem_y - 1) * 10; j < yem_y * 10; j++)
                    {
                        bitmap.SetPixel(i, j, Color.Red);
                    }
                }
            }
            //////// 


            pictureBox1.Image = bitmap;


        }

    }
}
