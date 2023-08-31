using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Emlak_Otomasyon_Projesi
{
    public partial class KulSifreDegis : Form
    {
        public KulSifreDegis()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");
        private void KulSifreDegis_Load(object sender, EventArgs e)
        {
            label4.Text = Form1.kullaniciAdi + " Hoş Geldiniz!";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();

            if (textBox2.Text == textBox3.Text)
            {
                SqlCommand cmd = new SqlCommand("update Giris set sifre = @KSifre where kullaniciAdi = @KAdi", baglanti);
                cmd.Parameters.AddWithValue("@KSifre", Kriptoloji.Encryption(textBox2.Text,2));
                cmd.Parameters.AddWithValue("@KAdi", Form1.kullaniciAdi);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Şifre Değiştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox2.Clear();
                textBox3.Clear();
            }
            else
            {
                MessageBox.Show("Şifreler Uyuşmuyor!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox2.Text)
            {
                label5.Visible = true;
            }
            else
            {
                label5.Visible = false;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update Giris set e_mail = @KEPosta where kullaniciAdi = @KAdi", baglanti);
            cmd.Parameters.AddWithValue("@KEPosta", textBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@KAdi", Form1.kullaniciAdi);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("E-Posta Değiştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            textBox4.Clear();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand cmd = new SqlCommand("update Giris set telefonNo = @KTelefon where kullaniciAdi = @KAdi", baglanti);
            cmd.Parameters.AddWithValue("@KTelefon", maskedTextBox1.Text);
            cmd.Parameters.AddWithValue("@KAdi", Form1.kullaniciAdi);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Telefon Numarası Değiştirildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            maskedTextBox1.Clear();
        }
        private void KulSifreDegis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
    }
}
