using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Emlak_Otomasyon_Projesi
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");
        private void verilerigoster()
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
           
            comboBox1.SelectedIndex = 0;
            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From siteBilgi", baglanti);
            SqlDataReader okuyucu = komut.ExecuteReader();

            while (okuyucu.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okuyucu["id"].ToString();
                ekle.SubItems.Add(okuyucu["siteAdi"].ToString());
                ekle.SubItems.Add(okuyucu["satkira"].ToString());
                ekle.SubItems.Add(okuyucu["odaSayisi"].ToString());
                ekle.SubItems.Add(okuyucu["metre"].ToString());
                ekle.SubItems.Add(okuyucu["isitma"].ToString());
                ekle.SubItems.Add(okuyucu["fiyat"].ToString());
                ekle.SubItems.Add(okuyucu["blok"].ToString());
                ekle.SubItems.Add(okuyucu["daireNo"].ToString());
                ekle.SubItems.Add(okuyucu["adSoyad"].ToString());
                ekle.SubItems.Add(okuyucu["telefon"].ToString());
                ekle.SubItems.Add(okuyucu["notlar"].ToString());
                ekle.SubItems.Add(okuyucu["RezerveDurumu"].ToString());
                listView2.Items.Add(ekle);

            }
            baglanti.Close();
            

            int saat = DateTime.Now.Hour;

            switch (saat)

            {
                

                case 6:

                case 7:

                case 8:

                case 9:

                case 10:

                case 11:

                    label2.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Sabahlar Mutlu Bir Gün Geçirmen Dileğiyle";

                    break;

                case 12:

                case 13:

                case 14:

                case 15:

                case 16:

                case 17:

                    label2.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Günler Dileriz";

                    break;

                case 18:

                case 19:

                case 20:

                case 21:

                case 22:

                case 23:

                    label2.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Akşamlar Dileriz";

                    break;

                case 24:

                case 1:

                case 2:

                case 3:

                case 4:

                case 5:

                    label2.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Geceler Dileriz";

                    break;

                default:

                    label2.Text = "Sistem Saati Hatalı";

                    break;
                    

            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
       
        private void label1_Click(object sender, EventArgs e)
        {

        }

        //private void mailgonder()
        //{
        //    try
        //    {
        //        SmtpClient client = new SmtpClient();
        //        MailMessage message = new MailMessage();
        //        client.Credentials = new NetworkCredential("yonetici.otomasyon.emlak@gmail.com", "admin şifre gir");
        //        client.Port = 587;
        //        client.Host = "smtp.gmail.com";
        //        client.EnableSsl = true;
        //        message.To.Add(KayiitFormu.e_mail);
        //        message.From = new MailAddress("yonetici.otomasyon.emlak@gmail.com");
        //        message.Subject = "Emlak Otomasyonu Onay Kodu";
        //        message.Body = "Onay Kodunuz";



        //        message.IsBodyHtml = true;
        //        client.Send(message);
        //    }
        //    catch // Bağlantı açamayıp Sorgu Çalıştıramıyorsa Veritabanına Ulaşamıyor Demekdir
        //    {
        //        MessageBox.Show("Hatalı Mail Adresi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        Form1 frr1 = new Form1();
        //        frr1.Show();
        //        this.Close();
        //    }
        //}
        private void button1_Click(object sender, EventArgs e)
        {
            KulSifreDegis kulsifdegis = new KulSifreDegis();
            kulsifdegis.Show();
            this.Hide();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            RezerveEt rezerve = new RezerveEt();
            if (Application.OpenForms["RezerveEt"] == null)
            {
                rezerve.Show();
                this.Hide();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            RezerveEt rezerve = new RezerveEt();
            if (Application.OpenForms["RezerveEt"] == null)
            {
                rezerve.Show();
                this.Hide();
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From siteBilgi where siteAdi like '%"+textBox1.Text+"%'", baglanti);
            SqlDataReader okuyucu = komut.ExecuteReader();

            while (okuyucu.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okuyucu["id"].ToString();
                ekle.SubItems.Add(okuyucu["siteAdi"].ToString());
                ekle.SubItems.Add(okuyucu["satkira"].ToString());
                ekle.SubItems.Add(okuyucu["odaSayisi"].ToString());
                ekle.SubItems.Add(okuyucu["metre"].ToString());
                ekle.SubItems.Add(okuyucu["isitma"].ToString());
                ekle.SubItems.Add(okuyucu["fiyat"].ToString());
                ekle.SubItems.Add(okuyucu["blok"].ToString());
                ekle.SubItems.Add(okuyucu["daireNo"].ToString());
                ekle.SubItems.Add(okuyucu["adSoyad"].ToString());
                ekle.SubItems.Add(okuyucu["telefon"].ToString());
                ekle.SubItems.Add(okuyucu["notlar"].ToString());
                ekle.SubItems.Add(okuyucu["RezerveDurumu"].ToString());
                listView2.Items.Add(ekle);

            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBox1.Text == "" || comboBox1.Text == "Hepsi")
            {
                listView2.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * From siteBilgi", baglanti);
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = okuyucu["id"].ToString();
                    ekle.SubItems.Add(okuyucu["siteAdi"].ToString());
                    ekle.SubItems.Add(okuyucu["satkira"].ToString());
                    ekle.SubItems.Add(okuyucu["odaSayisi"].ToString());
                    ekle.SubItems.Add(okuyucu["metre"].ToString());
                    ekle.SubItems.Add(okuyucu["isitma"].ToString());
                    ekle.SubItems.Add(okuyucu["fiyat"].ToString());
                    ekle.SubItems.Add(okuyucu["blok"].ToString());
                    ekle.SubItems.Add(okuyucu["daireNo"].ToString());
                    ekle.SubItems.Add(okuyucu["adSoyad"].ToString());
                    ekle.SubItems.Add(okuyucu["telefon"].ToString());
                    ekle.SubItems.Add(okuyucu["notlar"].ToString());
                    ekle.SubItems.Add(okuyucu["RezerveDurumu"].ToString());
                    listView2.Items.Add(ekle);

                }
                baglanti.Close();
            }
            else if (comboBox1.Text == "Satılık")
            {
                listView2.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * From siteBilgi where satkira like '%" + comboBox1.Text + "%'", baglanti);
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = okuyucu["id"].ToString();
                    ekle.SubItems.Add(okuyucu["siteAdi"].ToString());
                    ekle.SubItems.Add(okuyucu["satkira"].ToString());
                    ekle.SubItems.Add(okuyucu["odaSayisi"].ToString());
                    ekle.SubItems.Add(okuyucu["metre"].ToString());
                    ekle.SubItems.Add(okuyucu["isitma"].ToString());
                    ekle.SubItems.Add(okuyucu["fiyat"].ToString());
                    ekle.SubItems.Add(okuyucu["blok"].ToString());
                    ekle.SubItems.Add(okuyucu["daireNo"].ToString());
                    ekle.SubItems.Add(okuyucu["adSoyad"].ToString());
                    ekle.SubItems.Add(okuyucu["telefon"].ToString());
                    ekle.SubItems.Add(okuyucu["notlar"].ToString());
                    ekle.SubItems.Add(okuyucu["RezerveDurumu"].ToString());
                    listView2.Items.Add(ekle);

                }
                baglanti.Close();
            }
            else if (comboBox1.Text == "Kiralık")
            {
                listView2.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * From siteBilgi where satkira like '%" + comboBox1.Text + "%'", baglanti);
                SqlDataReader okuyucu = komut.ExecuteReader();

                while (okuyucu.Read())
                {
                    ListViewItem ekle = new ListViewItem();
                    ekle.Text = okuyucu["id"].ToString();
                    ekle.SubItems.Add(okuyucu["siteAdi"].ToString());
                    ekle.SubItems.Add(okuyucu["satkira"].ToString());
                    ekle.SubItems.Add(okuyucu["odaSayisi"].ToString());
                    ekle.SubItems.Add(okuyucu["metre"].ToString());
                    ekle.SubItems.Add(okuyucu["isitma"].ToString());
                    ekle.SubItems.Add(okuyucu["fiyat"].ToString());
                    ekle.SubItems.Add(okuyucu["blok"].ToString());
                    ekle.SubItems.Add(okuyucu["daireNo"].ToString());
                    ekle.SubItems.Add(okuyucu["adSoyad"].ToString());
                    ekle.SubItems.Add(okuyucu["telefon"].ToString());
                    ekle.SubItems.Add(okuyucu["notlar"].ToString());
                    ekle.SubItems.Add(okuyucu["RezerveDurumu"].ToString());
                    listView2.Items.Add(ekle);

                }
                baglanti.Close();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}