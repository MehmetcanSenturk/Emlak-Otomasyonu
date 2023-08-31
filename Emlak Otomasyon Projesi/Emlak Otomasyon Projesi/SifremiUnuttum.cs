using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail; 
using System.Data.SqlClient;

namespace Emlak_Otomasyon_Projesi
{
    public partial class SifremiUnuttum : Form
    {
        public SifremiUnuttum()
        {
            InitializeComponent();
        }
        public static SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");

        private void SifremiUnuttum_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string gonderadi, gondersifre, gondermail;
            baglanti.Open();
            SqlCommand cmd1 = new SqlCommand("SELECT * FROM Giris WHERE kullaniciAdi='" +txtKullanıcıadi.Text + "'", baglanti);
            //cmd.Parameters.AddWithValue("@KParola", Kriptoloji.Encryption(textBox2.Text,2));
            SqlDataReader dr = cmd1.ExecuteReader();
            if (dr.Read())
            {
               gonderadi = dr["kullaniciAdi"].ToString();
               gondersifre = dr["sifre"].ToString();
               gondermail = dr["e_mail"].ToString();
               baglanti.Close();
               MailMessage ePosta = new MailMessage();
                ePosta.From = new MailAddress("admin mail adresi gir");
                ePosta.To.Add(gondermail);
                ePosta.Subject = "Şifre Hatırlatma";
                ePosta.Body = "Sayın ," + gonderadi +"  "+"Şifreniz: " + Kriptoloji.Decryption(gondersifre, 2) ;
                SmtpClient smtp = new SmtpClient();
                smtp.Credentials = new System.Net.NetworkCredential("admin mail adresi gir", "admin şifre gir");
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                object userState = ePosta;
                try
                {
                    smtp.SendAsync(ePosta, (object)ePosta);
                }
                catch (SmtpException ex)
                {
                    MessageBox.Show(ex.Message, "Mail Gönderme Hatasi");
                }
                finally
                {
                    baglanti.Close();
                 //   SifremiUnuttum.Visible = false;
                    MessageBox.Show("Mail Başarıyla Gönderildi", "Bilgi");
                   Form1 GirisFormu = new Form1();
                    GirisFormu.Show();
                    this.Hide();
                }
            }
            else
            {
            MessageBox.Show("Kullanıcı Adı Yanlış..", "Uyarı");
            }
        }
        private void txtKullanıcıadi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = button1;
            }
        }
        private void txtKullanıcıadi_TextChanged(object sender, EventArgs e)
        {

        }

        private void SifremiUnuttum_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
