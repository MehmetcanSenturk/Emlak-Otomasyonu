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
namespace Emlak_Otomasyon_Projesi
{


    public partial class KayiitFormu : Form
    {
        static public string kullaniciAdi { get; set; }
        static public string sifre { get; set; }
        static public string sifreTekrar { get; set; }
        static public string e_mail { get; set; }
        static public string telefon { get; set; }
        static public Boolean kullanicibutonu { get; set; }
        static public Button buttongir { get; set; }
        static Random random = new Random();
        int rand_code = random.Next(10000, 99999);



        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");
        public KayiitFormu()
        {
            InitializeComponent();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_Leave(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_Leave(object sender, EventArgs e)
        {

        }

        private void textBox3_Enter(object sender, EventArgs e)
        {

        }

        private void textBox3_Leave(object sender, EventArgs e)
        {

        }

        private void textBox4_Enter(object sender, EventArgs e)
        {

        }

        private void textBox4_Leave(object sender, EventArgs e)
        {

        }

        private void textBox5_Enter(object sender, EventArgs e)
        {

        }

        private void textBox5_Leave(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool durum;


        //void mukerrer()
        //{
        //    baglanti.Open();  // (kullaniciAdi,e_mail) values (@p1,@p2)
        //    SqlCommand komut = new SqlCommand("select * from Giris where kullaniciAdi=@p1", baglanti);
        //    komut.Parameters.AddWithValue("@p1", textBox1.Text); 

        //    SqlDataReader dr = komut.ExecuteReader();
        //    if (dr["e_mail"].ToString().Trim() == textBox4.Text.Trim())
        //    {
        //        durum = false;

        //    }
        //    else if (dr.Read())
        //    {
        //        durum = true;

        //    }
        //    else
        //    {
        //        MessageBox.Show("Mail Adresi Daha Önce Kullanılmış !!!");
        //        baglanti.Close();
        //    }

        //}
        private void button1_Click(object sender, EventArgs e)
        {
          
            if (
               textBox1.Text == "" || txtSifre.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox2.Text == "" ||
                  textBox1.Text == String.Empty ||txtSifre.Text == String.Empty || textBox3.Text == String.Empty || textBox4.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Lütfen Boş Alan Bırakmayınız", "Boş Alan Hatası");
            }
            else
            {
                baglanti.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Giris", baglanti);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["kullaniciAdi"].ToString() == textBox1.Text.Trim())
                    {
                        MessageBox.Show("Kullanıcı Adı Kayıtlı! \n Lütfen Farklı Bir kullanıcı Adı Seçiniz", "Sistem Mesajı");
                        baglanti.Close();
                        textBox1.Text = "";
                        textBox1.Focus();
                        return;
                    }
                    else if (dr["telefonNo"].ToString() == textBox2.Text.Trim())
                    {
                        MessageBox.Show("Telefon Numarası Kayıtlı! \n Lütfen Farklı Bir Telefon Numarası Giriniz", "Sistem Mesajı");
                        baglanti.Close();
                        textBox2.Text = "";
                        textBox2.Focus();
                        return;
                    }
                    else if (dr["e_mail"].ToString() == textBox4.Text.Trim())
                    {
                        //     errorProvider4.SetError(textBox4.Text, "E-Mail Zaten Alınmış");
                        MessageBox.Show("Mail Adresi Alınmış !");
                        baglanti.Close();
                        textBox4.Text = "";
                        textBox4.Focus();
                        return;
                    }
                    if (txtSifre.Text != textBox3.Text && textBox3.Text != txtSifre.Text)
                    {
                        //  errorProvider2.SetError(textBox2, "Şifreler Aynı Olmak Zorunda");
                        MessageBox.Show("Şifreler Farklı Olamaz !");
                        baglanti.Close();
                        return;
                    }
                }
                baglanti.Close();
                kullaniciAdi = textBox1.Text;
                sifre = txtSifre.Text;
                sifreTekrar = textBox3.Text;
                e_mail = textBox4.Text;
                telefon = textBox2.Text;
                this.Close();
                MailDogrulama mailDogrulama = new MailDogrulama();
                mailDogrulama.Show();
                Form1 fr1 = new Form1();
                fr1.Close();
            }
        }
        //public static string GetWithoutMaskValue(MaskedTextBox maskedTextBox)
        //{
        //    MaskedTextProvider maskedTextProvider = maskedTextBox.MaskedTextProvider;

        //    return maskedTextProvider.ToString(false, false);
        //}
        private void KayiitFormu_Load(object sender, EventArgs e)
        {
            textBox2.MaxLength = 10;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Bir önceki sayfaya dönmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                Form1 frGiris = new Form1();
                frGiris.Show();
                this.Hide();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public string SifreSeviye(string Sifre)
        {
            int UzunlukAl = Sifre.Length;
            if (UzunlukAl < 6)
            {
                return "Zayıf Şifre..";
            }
            bool alfaNumerikMi = false;
            string alfalar = "*?+#&!é/-\\";
            bool harfMi = false;
            bool rakamMi = false;
            for (int n = 0; n < Sifre.Length; n++)
            {
                char a = Convert.ToChar(Sifre.Substring(n, 1));
                if (Char.IsLetter(a))
                {
                    harfMi = true;
                }
                else if (Char.IsDigit(a))
                {
                    rakamMi = true;
                }
                else if (alfalar.Contains(a))
                {
                    alfaNumerikMi = true;
                }
            }
            if (alfaNumerikMi == true && harfMi == true && rakamMi == true)
            {
                return "Güçlü Şifre..";
            }
            else if (rakamMi == true && harfMi == true)
            {
                return "Orta Seviye..";
            }
            else
            {
                return "Zayıf Şifre..";
            }
        }
        private void txtSifre_TextChanged(object sender, EventArgs e)
        {
            lblDerece.Text = SifreSeviye(txtSifre.Text);

            if (SifreSeviye(txtSifre.Text) == "Zayıf Şifre..")
            {
                lblDerece.ForeColor = Color.Red;
            }
            else if (SifreSeviye(txtSifre.Text) == "Orta Seviye..")
            {
                lblDerece.ForeColor = Color.Orange;
            }
            else
                lblDerece.ForeColor = Color.Green;
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void KayiitFormu_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Form1 f1 = new Form1();
            //f1.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
        //private void textBox2_TextChanged(object sender, EventArgs e)
        //{
        //    lblDerece.Text = SifreSeviye(txtSifre.Text);

        //    if (SifreSeviye(txtSifre.Text) == "Zayıf Şifre..")
        //    {
        //        lblDerece.ForeColor = Color.Red;
        //    }
        //    else if (SifreSeviye(txtSifre.Text) == "Orta Seviye..")
        //    {
        //        lblDerece.ForeColor = Color.Orange;
        //    }
        //    else
        //        lblDerece.ForeColor = Color.Green;
        //}
    }
    }


