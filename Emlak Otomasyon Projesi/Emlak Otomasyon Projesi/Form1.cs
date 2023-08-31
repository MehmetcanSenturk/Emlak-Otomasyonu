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
    public partial class Form1 : Form

    {
        static public string kullaniciAdi { get; set; }

        public Form1()
        {
            InitializeComponent();


        }
        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");       
        private void Form1_Load(object sender, EventArgs e)
        {
            

            timer1.Start();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fr = new Form3();
            fr.Show();

        }
        private void btnKayitOl_Click(object sender, EventArgs e)
        {
           

        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
            //  && !char.IsSeparator(e.KeyChar);
        }
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            KayiitFormu frkayit = new KayiitFormu();
            frkayit.Show();
        }
       
        private void button1_Click_1(object sender, EventArgs e)
        {
            if (
         textBox1.Text == "" || textBox2.Text == "" || textBox1.Text == String.Empty || textBox2.Text == String.Empty)
            {
                MessageBox.Show("Boş alan bırakmayınız");
            }
            else
            {
                try
                {
                    string gonderadi, gondersifre, gondermail;
                    baglanti.Open();
                    SqlCommand cmd1 = new SqlCommand("SELECT * FROM Giris WHERE kullaniciAdi='" + textBox1.Text + "'", baglanti);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.Read())
                    {
                        gonderadi = dr["kullaniciAdi"].ToString();
                        gondermail = dr["e_mail"].ToString();
                        baglanti.Close();
                        MailMessage ePosta = new MailMessage();
                        ePosta.From = new MailAddress("admin mail adresi gir");
                        ePosta.To.Add(gondermail);
                        ePosta.Subject = "Sistem Mesajı";
                        ePosta.Body = "Sayın ," + gonderadi + "  " + "Emlak Otomasyon Sistemine " + "  " + label30.Text + " " + "Tarihinde Giriş Yapılmıştır Siz Değilseniz Lütfen Yöneticilerimiz ile İletişime Geçiniz ";
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
                        }
                    }
                    //else
                    //{
                    //   MessageBox.Show("Kullanıcı Adı Yanlış..", "Uyarı");
                    //}
                }
                catch
                {
                    // MessageBox.Show("Boş alan.", "Sistem Mesajı");

                }
                kullaniciAdi = textBox1.Text;
                try
                {

                    SqlCommand cmd = new SqlCommand("select yetki from Giris where kullaniciAdi= @KAdi and sifre= @KParola", baglanti);
                    cmd.Parameters.AddWithValue("@KAdi", textBox1.Text);
                    cmd.Parameters.AddWithValue("@KParola", Kriptoloji.Encryption(textBox2.Text, 2));
                    cmd.Connection.Open();
                    SqlDataReader rd = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (rd.HasRows) // KULLANICI ADI PAROLA SORGULAMA  
                    {
                        while (rd.Read()) // reader Okuyabiliyorsa
                        {
                            if (rd["yetki"].ToString() == "admin") // Rolü Admin'e ait olarak Ayarlanmışdır
                            {
                                // Kullanıcı Rolü ADMİN ise Admin Ekranı Aç 
                                Form3 admin = new Form3();
                                admin.Show();
                                this.Hide();
                            }
                            else
                            {
                                // Kullanıcı Rolü KULLANICI İSE FORM2 AÇ
                                Form2 kul = new Form2();
                                kul.Show();
                                this.Hide();
                            }
                        }
                    }
                    else
                    //READER SATIR DÖNDÜREMİYORSA KULLANICI ADI VEYA PAROLA GEÇERSİZDİR 
                    {
                        rd.Close();
                           MessageBox.Show("Kullanıcı Adı veya Parola Geçersizdir", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
                catch
                {
                    baglanti.Close();
                      MessageBox.Show("Kullanıcı Adı veya Parola Geçersizdir", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
          
        }
       private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            SifremiUnuttum frsifredegistirme = new SifremiUnuttum();
            frsifredegistirme.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //if (textBox1.Text == string.Empty)
            //{
            //    MessageBox.Show("Boş Alan Bırakmayınız", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
                
            //}
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            //if (textBox1.Text == string.Empty)
            //{
            //    MessageBox.Show("Boş Alan Bırakmayınız", "Sistem Mesajı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}
            //else
            //{
            //    errorProvider1.Clear();
            //}
        }
        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
               // MessageBox.Show("Boş Alan hatası");
            }
            else
            {
                errorProvider1.Clear();
            }

        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label30.Text = DateTime.Now.ToString();
        }
    }
}
