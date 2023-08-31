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
    public partial class MailDogrulama : Form
    {

        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");
        static Random random = new Random();
        int rand_code = random.Next(10000, 99999);
        //private object errorProvider11;
        //private object errorProvider7;
        //private object errorProvider4;

        public MailDogrulama()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == rand_code.ToString())
            {
                //if (String.IsNullOrEmpty(KayiitFormu.kullaniciAdi)&& String.IsNullOrEmpty(KayiitFormu.e_mail))
                // {
                //    MessageBox.Show("boş alan bırakmayınız");

                //}
                if (KayiitFormu.kullaniciAdi == "" && KayiitFormu.sifre == "" && KayiitFormu.sifreTekrar == "" && KayiitFormu.e_mail == "" && KayiitFormu.telefon == "" && KayiitFormu.kullaniciAdi == String.Empty && KayiitFormu.sifre == String.Empty && KayiitFormu.sifreTekrar == String.Empty && KayiitFormu.e_mail == String.Empty && KayiitFormu.telefon == String.Empty)
                {
                    MessageBox.Show("Lütfen Boş Alan Bırakmayınız", "Boş Alan Hatası");
                }
                else
                {
                    string yetki = "";
                    if (radioButton1.Checked)
                    {
                        yetki = "kullanici";
                    }

                    baglanti.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Giris", baglanti);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        if ( dr["e_mail"].ToString() == KayiitFormu.e_mail.Trim())
                        {
                            //  errorProvider4.SetError(KayiitFormu.e_mail, "E-Mail Zaten Alınmış");
                            MessageBox.Show("Mail Adresi Alınmış !");
                            baglanti.Close();
                            KayiitFormu.e_mail = "";
                            return;
                        }
                        else if (dr["kullaniciAdi"].ToString() == KayiitFormu.kullaniciAdi.Trim())
                        {
                            MessageBox.Show("Kullanıcı Adı Alınmış !");
                            baglanti.Close();
                            KayiitFormu.kullaniciAdi = "";
                            return;
                        }
                        else if (dr["telefonNo"].ToString() == KayiitFormu.telefon.Trim())
                        {
                            //errorProvider11.SetError(maskedTextBox1, "Kayıtlı Telefon No");
                            baglanti.Close();
                            KayiitFormu.telefon = "";
                            return;
                        }
                        if (KayiitFormu.sifre != KayiitFormu.sifreTekrar && KayiitFormu.sifreTekrar != KayiitFormu.sifre)
                        {
                           //  errorProvider2.SetError(textBox2, "Şifreler Aynı Olmak Zorunda");
                         MessageBox.Show("Şifreler Farklı Olamaz !");

                          baglanti.Close();
                           return;
                        }
                    }
                    string kripto = Kriptoloji.Encryption(KayiitFormu.sifre, 2);
                    string kyetki = "kullanici";
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand command = new SqlCommand("insert into Giris(kullaniciAdi,sifre,sifreTekrar,e_mail,telefonNo,yetki) values ('" + KayiitFormu.kullaniciAdi + "','" + kripto + "','" + KayiitFormu.sifreTekrar + "','" + KayiitFormu.e_mail + "','" + KayiitFormu.telefon + "','" + yetki + "')", baglanti);
                    command.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıdınız Oluşturulmuştur");
                    this.Close();
                    Form formgiris;
                    formgiris = new Form1();
                    formgiris.Show();
                }
            }
            else
            {
                MessageBox.Show("Hata Mail");
            }
        }
        private void MailDogrulama_Load(object sender, EventArgs e)
        {
            radioButton1.Checked = true;
            mailgonder();
         

        }
        private void mailgonder()
        {
            try {
                SmtpClient client = new SmtpClient();
                MailMessage message = new MailMessage();
                client.Credentials = new NetworkCredential("admin mail adresi gir", "admin şifre gir");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                message.To.Add(KayiitFormu.e_mail);
                message.From = new MailAddress("admin mail adresi gir");
                message.Subject = "Emlak Otomasyonu Onay Kodu";
                message.Body = "Onay Kodunuz" + " " + rand_code;
                message.IsBodyHtml = true;
                client.Send(message);
            }
            catch 
            {
                MessageBox.Show("Hatalı Mail Adresi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Form1 frr1 = new Form1();
                frr1.Show();
                this.Close();
            }
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.ActiveControl = button1;
            }
        }
        public static bool Email_Format_Kontrol(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text == rand_code.ToString())
            {
                string yetki = "";
                if (radioButton1.Checked)
                {
                    yetki = "kullanici";
                }
                string kripto = Kriptoloji.Encryption(KayiitFormu.sifre, 2);
                baglanti.Close();
                baglanti.Open();
                SqlCommand command = new SqlCommand("insert into Giris(kullaniciAdi,sifre,e_mail,telefonNo,yetki) values ('" + KayiitFormu.kullaniciAdi + "','" + kripto + "','" + KayiitFormu.e_mail + "','" + KayiitFormu.telefon + "','" + yetki + "')", baglanti);
                command.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıdınız Oluşturulmuştur");
                this.Close();
                Form1 grgirisi = new Form1();
                grgirisi.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Mail Adresi Veya Hatalı Kullanıcı Kodu");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Application.Exit();
            ////ya da this.visible = false;
            //timer1.Enabled = false;
            //// MailDogrulama.visible = true;
            //label2.Text = timer1.ToString();
        }

        private void MailDogrulama_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form1 f1 = new Form1();
            f1.Show();
        }
    }
}
