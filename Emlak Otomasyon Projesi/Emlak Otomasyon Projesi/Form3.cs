using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Threading;
using System.Globalization;
using System.Net.Mail;
using System.Windows.Forms;


namespace Emlak_Otomasyon_Projesi
{

    public partial class Form3 : Form
    {
        public Form3 frm3;
        public Form3()
        {
            InitializeComponent();
        }
        MailMessage ePosta = new MailMessage();

        public bool Scrollable { get; set; }


        SqlConnection baglanti = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");
        private void verilerigoster()
        {
            listView3.Refresh();
            listView2.Refresh();
            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From siteBilgi", baglanti);
            SqlDataReader okuyucu = komut.ExecuteReader();
            while (okuyucu.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = okuyucu["id"].ToString();
                ekle.SubItems.Add(okuyucu["il"].ToString());
                ekle.SubItems.Add(okuyucu["ilce"].ToString());
                ekle.SubItems.Add(okuyucu["mahalle"].ToString());
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
                ekle.SubItems.Add(okuyucu["tarih"].ToString());
                listView2.Items.Add(ekle);
            }
            baglanti.Close();
        }
        private void verilerisil()
        {
            listView2.Refresh();
            listView5.Items.Clear();
            listView5.Refresh();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select* from Giris", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["kullaniciAdi"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["e_mail"].ToString());
                ekle.SubItems.Add(oku["telefonNo"].ToString());
                ekle.SubItems.Add(oku["yetki"].ToString());
                listView5.Items.Add(ekle);
            }
            baglanti.Close();
        }
        private void yetkiyonet()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select* from Giris", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["kullaniciAdi"].ToString());
                ekle.SubItems.Add(oku["yetki"].ToString());
                listView3.Items.Add(ekle);
            }
            baglanti.Close();
        }
        private void veriler()
        {
            listView2.Items.Clear();
            listView2.Refresh();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select* from Giris", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["kullaniciAdi"].ToString());
                ekle.SubItems.Add(oku["sifre"].ToString());
                ekle.SubItems.Add(oku["e_mail"].ToString());
                ekle.SubItems.Add(oku["telefonNo"].ToString());
                ekle.SubItems.Add(oku["yetki"].ToString());
                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }
        private void rezervelerilistele()
        {
            listView4.Items.Clear();
            listView4.Refresh();
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("Select* from rezerveler", baglanti);
            SqlDataReader oku = komut2.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["RezerveEdilenIlanId"].ToString();
                ekle.SubItems.Add(oku["RezerveEdenKullanici"].ToString());
                ekle.SubItems.Add(oku["RezerveEdilmeTarihi"].ToString());
                listView4.Items.Add(ekle);
            }
            baglanti.Close();
        }
        int id = 0;
        private void Form3_Load(object sender, EventArgs e)
        {
            txtDaire.MaxLength = 4;
            txtMetre.MaxLength = 10;
            
             // 13.04.2020
            //Siteler:
            SqlCommand komut = new SqlCommand();
            komut.CommandText = "SELECT *FROM konutlar";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbSiteAdi.Items.Add(dr["siteAdlari"]);
            }
            baglanti.Close();
            //Emlak Tipi:       
            komut.CommandText = "SELECT *FROM EmlakTipi";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbEmlakTipi.Items.Add(dr["EmlakTipleri"]);
            }
            baglanti.Close();
            //Oda Sayısı
            komut.CommandText = "SELECT *FROM odaSayisi";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbOdaSayisi.Items.Add(dr["odaSayilari"]);
            }
            baglanti.Close();
            //ISITMA
            komut.CommandText = "SELECT *FROM isitma";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbIsitma.Items.Add(dr["isitmaTuru"]);
            }
            baglanti.Close();
            // Blok Numarası
            komut.CommandText = "SELECT *FROM blokNumarasi";
            komut.Connection = baglanti;
            komut.CommandType = CommandType.Text;
            baglanti.Open();
            dr = komut.ExecuteReader();
            while (dr.Read())
            {
                cmbBlok.Items.Add(dr["blokNumara"]);
            }
            baglanti.Close();
            verilerisil();
            yetkiyonet();
            radioButton3.Checked = true;
            rezervelerilistele();
            veriler();
            int saat = DateTime.Now.Hour;  
            switch (saat)
            {
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                label31.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Sabahlar Mutlu Bir Gün Geçirmen Dileğiyle";
                break;
                case 12:
                case 13:
                case 14:
                case 15:
                case 16:
                case 17:
                label31.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Günler Dileriz";
                break;
                case 18:
                case 19:
                case 20:
                case 21:
                case 22:
                case 23:
                label31.Text = "Hoşgeldin: " + Form1.kullaniciAdi + " " + "İyi Akşamlar Dileriz";
                break;
                case 24:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                label31.Text = "Hoşgeldin: " + Form1.kullaniciAdi+ " "+ "İyi Geceler Dileriz";
                break;
                default:
                label31.Text = "Sistem Saati Hatalı";
                break;                    
            }
            timer1.Start();
            //  label31.Text = "Hoşgeldin: " + Form1.kullaniciAdi;          
        }

        private void lblSifre_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Refresh();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Giris set kullaniciAdi='" + textBox1.Text.ToString() + "',sifre='" + textBox2.Text.ToString() +  "',e_mail='" + textBox17.Text.ToString() + "',telefonNo='" + maskedTextBox2.Text.ToString() + "'where id=" + id + "", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriler();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[2].Text;           
            textBox17.Text = listView1.SelectedItems[0].SubItems[3].Text;
            maskedTextBox2.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Bir önceki sayfaya dönmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (secenek == DialogResult.Yes)
            {
                Form1 Form1 = new Form1();
                Form1.Show();
                this.Hide();
            }
        }

        private void lblGayrimenkul_Click(object sender, EventArgs e)
        {

        }
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void btnGoruntule_Click(object sender, EventArgs e)
        {
            verilerigoster();
        }
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        public static string rezerveDurumu;
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            listView2.Refresh();
            listView2.Items.Clear();            
            if (
          cmbSiteAdi.Text == "" || cmbEmlakTipi.Text == "" || cmbOdaSayisi.Text == "" || txtMetre.Text == "" || cmbIsitma.Text == "" || txtFiyat.Text == ""  || txtDaire.Text == "" || txtAdSoyad.Text == "" || !mskdTel.MaskFull || txtAciklama.Text == "" ||txtil.Text ==""||txtilce.Text =="" || txtMahalle.Text == "" || txtMahalle.Text  ==String.Empty||txtil.Text == String.Empty||txtilce.Text == String.Empty  || cmbSiteAdi.Text == String.Empty || cmbEmlakTipi.Text == String.Empty || cmbOdaSayisi.Text == String.Empty || txtMetre.Text == String.Empty || cmbIsitma.Text == String.Empty || txtFiyat.Text == String.Empty  || txtDaire.Text == String.Empty || txtAdSoyad.Text == String.Empty || mskdTel.Text == String.Empty || txtAciklama.Text == String.Empty)
            {
                //errorProvider10.SetError(label22, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(cmbSiteAdi, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(cmbEmlakTipi, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(cmbOdaSayisi, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(txtMetre, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(cmbIsitma, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(cmbBlok, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(txtDaire, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(txtAdSoyad, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(mskdTel, "Boş alan bırakamazsınız");
                //errorProvider10.SetError(txtAciklama, "Boş alan bırakamazsınız");
                MessageBox.Show("Lütfen Boş Alan Bırmayınız.","Sistem Mesajı");
            }
            else
            {
                if (radioButton5.Checked == true)
                {
                    rezerveDurumu = "Rezerve Edilmiş";
                }
                else if (radioButton6.Checked == true)
                {
                    rezerveDurumu = "Rezerve Edilmemiş";
                }
                listView2.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into siteBilgi (il,ilce,mahalle,siteAdi,satkira,odaSayisi,metre,isitma,fiyat,blok,daireNo,adSoyad,telefon,notlar,RezerveDurumu,tarih) values ('" + txtil.Text.ToString() + "','" + txtilce.Text.ToString() + "','"+txtMahalle.Text.ToString()+ "','" + cmbSiteAdi.Text.ToString() + "','" + cmbEmlakTipi.Text.ToString() + "','" + cmbOdaSayisi.Text.ToString() + "','" + txtMetre.Text.ToString() + "','" + cmbIsitma.Text.ToString() + "','" + txtFiyat.Text.ToString() + "','" + cmbBlok.Text.ToString() + "','" + txtDaire.Text.ToString() + "','" + txtAdSoyad.Text.ToString() + "','" + mskdTel.Text.ToString() + "','" + txtAciklama.Text.ToString() +"','"+rezerveDurumu+ "','" + dateTimePicker1.Text + "')", baglanti);               
                komut.ExecuteNonQuery();  //HATA VAR 
                baglanti.Close();
                verilerigoster();
                //listView2.Refresh();
                //listView2.Items.Clear();
                //baglanti.Open();
                //SqlCommand komut = new SqlCommand("update siteBilgi set id= '" + textBox8.Text.ToString() + "',satkira='" + comboBox2.Text.ToString() + "',siteAdi='" + comboBox1.Text.ToString() + "',odaSayisi='" + comboBox3.Text.ToString() + "',metre='" + textBox5.Text.ToString() + "',isitma='" + comboBox5.Text.ToString() + "',fiyat='" + textBox4.Text.ToString() + "',blok='" + comboBox4.Text.ToString() + "',daireNo='" + textBox9.Text.ToString() + "',adSoyad='" + textBox6.Text.ToString() + "',telefon='" + maskedTextBox1.Text.ToString() + "',notlar='" + textBox3.Text.ToString() + "'where id =" + id + "", baglanti);
                //komut.ExecuteNonQuery();
                //baglanti.Close();
                 //verilerigoster();
            }
        }
        int idSil = 0;
        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydı veritabanından silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (secenek == DialogResult.Yes)
            {
            // HATA    label22.Clear();
                listView2.Refresh();
                listView2.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from siteBilgi where id='"+label22.Text+"'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                verilerigoster();
            }
        }
        private void listView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {  }
        private void btnDuzenle_Click(object sender, EventArgs e)
        {
            listView2.Refresh();
            listView2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update siteBilgi set satkira='" + cmbEmlakTipi.Text.ToString() + "',siteAdi='" + cmbSiteAdi.Text.ToString() + "',odaSayisi='" + cmbOdaSayisi.Text.ToString() + "',metre='" + txtMetre.Text.ToString() + "',isitma='" + cmbIsitma.Text.ToString() + "',fiyat='" + txtFiyat.Text.ToString() + "',blok='" + cmbBlok.Text.ToString() + "',daireNo='" + txtDaire.Text.ToString() + "',adSoyad='" + txtAdSoyad.Text.ToString() + "',telefon='" + mskdTel.Text.ToString() + "',notlar='" + txtAciklama.Text.ToString() + "'where id =" + id + "", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoster();
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {        }
        private void button2_Click(object sender, EventArgs e)
        {
            string a;
            if (radioButton1.Checked)
            {
                a = "Satılık";
            }
            else if (radioButton2.Checked)
            {
                a = "Kiralık";
            }
            else
            {
                a = "4";
            }
            if (a == "Satılık" || a == "Kiralık")
            {
                if (textBox10.Text.Trim() != "")
                {
                    baglanti.Open();
                    //KulAdı v ar mı diye kontrol edelim
                    SqlCommand cmd1 = new SqlCommand("select * from konutlar", baglanti);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["siteAdlari"].ToString() == textBox10.Text.Trim())
                        {
                            MessageBox.Show("Bu alan zaten ekli!");
                            baglanti.Close();
                            textBox10.Text = "";
                            textBox10.Focus();
                            return;
                        }
                    }
                    baglanti.Close();
                }
                if (textBox10.Text.Trim() != "")
                {
                    baglanti.Open();
                    //ekleme
                    SqlCommand cmd = new SqlCommand("insert into konutlar(siteAdlari,durum) values('" + textBox10.Text + "','" + a + "')", baglanti);
                    cmd.ExecuteNonQuery();
                    baglanti.Close();
                    cmbSiteAdi.Items.Clear();
                    SqlCommand komut = new SqlCommand();
                    komut.CommandText = "SELECT *FROM konutlar";
                    komut.Connection = baglanti;
                    komut.CommandType = CommandType.Text;
                    SqlDataReader dr;
                    baglanti.Open();
                    dr = komut.ExecuteReader();
                    while (dr.Read())
                    {
                        cmbSiteAdi.Items.Add(dr["siteAdlari"]);
                    }
                    baglanti.Close();
                    MessageBox.Show("Yeni Site eklenmiştir");
                    //for (int i = 0; i < this.Controls.Count; i++)
                    //{
                    //    if (Controls[i] is TextBox)
                    //    {
                    //        Controls[i].Text = "";
                    //    }
                    //}
                    textBox10.Text = "";
                    radioButton2.Checked = false;
                    radioButton1.Checked = false;
                }
                else
                {
                    MessageBox.Show("Lütfen Boş alan bırakmayınız");
                }
            }
            else
            {
                MessageBox.Show("Lütfen Gayrimenkul Tipini Seçiniz!");
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {        }
        private void textBox10_TextChanged(object sender, EventArgs e)
        {        }
        private void button4_Click(object sender, EventArgs e)
        {
            //silme işlemi 
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı Silmek İstediğinizden Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (cevap == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("delete from konutlar where siteAdlari = '" + cmbSiteAdi.SelectedItem.ToString() + "'", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbSiteAdi.Items.Clear();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT *FROM konutlar";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbSiteAdi.Items.Add(dr["siteAdlari"]);
                }
                baglanti.Close();
                MessageBox.Show("Silme işlemi Gerçekleşmiştir");
                cmbSiteAdi.Text = " ";
            }
        }
        private void lblSite_Click(object sender, EventArgs e)
        {        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmlakTipi.SelectedItem.ToString() == "Satılık")
            {
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT siteAdlari FROM konutlar where durum = 'Satılık'";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                cmbSiteAdi.Items.Clear();
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbSiteAdi.Items.Add(dr["siteAdlari"]);
                }
                baglanti.Close();
            }
            if (cmbEmlakTipi.SelectedItem.ToString() == "Kiralık")
            {
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT siteAdlari FROM konutlar where durum = 'Kiralık'";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                cmbSiteAdi.Items.Clear();
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbSiteAdi.Items.Add(dr["siteAdlari"]);
                }
                baglanti.Close();
            }
        }
        private void textBox11_TextChanged(object sender, EventArgs e)
        {        }
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {        }
        private void comboBox2_DropDown(object sender, EventArgs e)
        {        }
        private void textBox5_TextChanged(object sender, EventArgs e)
        {        }
        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox4_TextChanged(object sender, EventArgs e)
        {        }
        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if ((int)e.KeyChar >= 47 && (int)e.KeyChar <= 58)
            {
                e.Handled = false;
            }
            else if ((int)e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
        private void textBox8_TextChanged(object sender, EventArgs e)
        {        }
        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                 && !char.IsSeparator(e.KeyChar);
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox7.Text.Trim() != "")
            {
                baglanti.Open();
                //KulAdı v ar mı diye kontrol edelim
                SqlCommand cmd1 = new SqlCommand("select * from odaSayisi", baglanti);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["odaSayilari"].ToString() == textBox10.Text.Trim())
                    {
                        MessageBox.Show("Bu alan zaten ekli!");
                        baglanti.Close();
                        textBox7.Text = "";
                        textBox7.Focus();
                        return;
                    }
                }
                baglanti.Close();
            }
            if (textBox7.Text.Trim() != "")
            {
                baglanti.Open();
                //ekleme
                SqlCommand cmd = new SqlCommand("insert into odaSayisi(odaSayilari) values('" + textBox7.Text + "')", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbOdaSayisi.Items.Clear();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT *FROM odaSayisi";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbOdaSayisi.Items.Add(dr["odaSayilari"]);
                }
                baglanti.Close();
                MessageBox.Show("Yeni Oda Sayısı eklenmiştir");
                textBox7.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen Boş alan bırakmayınız");
            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            //silme işlemi 
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı Silmek İstediğinizden Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (cevap == DialogResult.Yes)
            {
                cmbOdaSayisi.Refresh();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("delete from odaSayisi where odaSayilari = '" + cmbOdaSayisi.SelectedItem.ToString() + "'", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbOdaSayisi.Items.Clear();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT *FROM odaSayisi";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbOdaSayisi.Items.Add(dr["odaSayilari"]);
                }
                baglanti.Close();
                MessageBox.Show("Silme işlemi Gerçekleşmiştir");
                cmbOdaSayisi.Text = " ";
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox11.Text.Trim() != "")
            {
                baglanti.Open();
                //KulAdı v ar mı diye kontrol edelim
                SqlCommand cmd1 = new SqlCommand("select * from blokNumarasi", baglanti);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["blokNumara"].ToString() == textBox11.Text.Trim())
                    {
                        MessageBox.Show("Bu alan zaten ekli!");
                        baglanti.Close();
                        textBox11.Text = "";
                        textBox11.Focus();
                        return;
                    }
                }
                baglanti.Close();
            }
            if (textBox11.Text.Trim() != "")
            {
                baglanti.Open();
                //ekleme
                SqlCommand cmd = new SqlCommand("insert into blokNumarasi(blokNumara) values('" + textBox11.Text + "')", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbBlok.Items.Clear();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT *FROM blokNumarasi";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbBlok.Items.Add(dr["blokNumara"]);
                }
                baglanti.Close();

                MessageBox.Show("Yeni Oda Sayısı eklenmiştir");
                textBox11.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen Boş alan bırakmayınız");
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            //silme işlemi 
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı Silmek İstediğinizden Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (cevap == DialogResult.Yes)
            {
                cmbBlok.Refresh();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("delete from blokNumarasi where blokNumara = '" + cmbBlok.SelectedItem.ToString() + "'", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbBlok.Items.Clear();
                SqlCommand komut = new SqlCommand();    // blokNumarasi
                komut.CommandText = "SELECT *FROM blokNumarasi";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;

                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbBlok.Items.Add(dr["blokNumara"]);
                }
                baglanti.Close();
                MessageBox.Show("Silme işlemi Gerçekleşmiştir");
                cmbBlok.Text = " ";
            }
        }
        private void button10_Click(object sender, EventArgs e)
        {
            if (textBox12.Text.Trim() != "")
            {
                baglanti.Open();
                //KulAdı v ar mı diye kontrol edelim
                SqlCommand cmd1 = new SqlCommand("select * from isitma", baglanti);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["isitmaTuru"].ToString() == textBox12.Text.Trim())
                    {
                        MessageBox.Show("Bu alan zaten ekli!");
                        baglanti.Close();
                        textBox12.Text = "";
                        textBox12.Focus();
                        return;
                    }
                }
                baglanti.Close();
            }
            if (textBox12.Text.Trim() != "")
            {
                baglanti.Open();
                //ekleme
                SqlCommand cmd = new SqlCommand("insert into isitma(isitmaTuru) values('" + textBox12.Text + "')", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbIsitma.Items.Clear();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT *FROM isitma";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbIsitma.Items.Add(dr["isitmaTuru"]);
                }
                baglanti.Close();
                MessageBox.Show("Yeni Oda Sayısı eklenmiştir");
                textBox12.Text = "";
            }
            else
            {
                MessageBox.Show("Lütfen Boş alan bırakmayınız");
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            //silme işlemi 
            DialogResult cevap;
            cevap = MessageBox.Show("Kaydı Silmek İstediğinizden Eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            if (cevap == DialogResult.Yes)
            {
                cmbIsitma.Refresh();
                baglanti.Open();
                SqlCommand cmd = new SqlCommand("delete from isitma where isitmaTuru = '" + cmbIsitma.SelectedItem.ToString() + "'", baglanti);
                cmd.ExecuteNonQuery();
                baglanti.Close();
                cmbIsitma.Items.Clear();
                SqlCommand komut = new SqlCommand();
                komut.CommandText = "SELECT *FROM isitma";
                komut.Connection = baglanti;
                komut.CommandType = CommandType.Text;
                SqlDataReader dr;
                baglanti.Open();
                dr = komut.ExecuteReader();
                while (dr.Read())
                {
                    cmbIsitma.Items.Add(dr["isitmaTuru"]);
                }
                baglanti.Close();
                MessageBox.Show("Silme işlemi Gerçekleşmiştir");
                cmbIsitma.Text = " ";
            }
        }
        private void button11_Click(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            listView3.Refresh();
            if (textBox13.Text == "" && textBox14.Text == "" && textBox13.Text == String.Empty && textBox14.Text == String.Empty)
            {

                textBox13.BackColor = Color.DarkRed;
                textBox14.BackColor = Color.DarkRed;
                errorProvider2.SetError(textBox13, "Kırmızı Renkli Alanları Boş Geçemezsiniz");
                errorProvider2.SetError(textBox14, "Kırmızı Renkli Alanları Boş Geçemezsiniz");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update Giris set yetki='" + textBox14.Text.ToString() + "'where id=" + id + "", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                yetkiyonet();
                listView3.Items.Clear();
                listView5.Refresh();
                verilerisil();
                MessageBox.Show("Kayıtlı kullanıcının yetkileri"+" "+  textBox14.Text +" " + "olarak Güncellenmiştir");
            }
        }
        private void btnKulSil_Click_1(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydı veritabanından silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (secenek == DialogResult.Yes)
            {
                listView2.Refresh();
                listView3.Refresh();
                listView5.Refresh();
                listView5.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from Giris where id=(" + id + ")", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                veriler();
                verilerigoster();
                verilerisil();
            }
            //DialogResult secenek = MessageBox.Show("Kaydı veritabanından silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            //if (secenek == DialogResult.Yes)
            //{
            //    listView2.Refresh();
            //    listView2.Items.Clear();
            //    baglanti.Open();
            //    SqlCommand komut = new SqlCommand("Delete from siteBilgi where id=(" + id + ")", baglanti);
            //    komut.ExecuteNonQuery();
            //    baglanti.Close();
            //    verilerigoster();
            //}
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView1.EnsureVisible(listView1.Items.Count - 1);
           // listView1.Items.Clear();
        }
        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {        }
        private void tabPage4_Click(object sender, EventArgs e)
        {        }
        private void listView5_SelectedIndexChanged(object sender, EventArgs e)
        {
            listView5.EnsureVisible(listView5.Items.Count - 1);
        }
        private void listView5_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView5.SelectedItems[0].SubItems[0].Text);
            textBox15.Text = listView5.SelectedItems[0].SubItems[1].Text;
        }
        private void listView3_DoubleClick_1(object sender, EventArgs e)
        {
            id = int.Parse(listView3.SelectedItems[0].SubItems[0].Text);
            textBox13.Text = listView3.SelectedItems[0].SubItems[2].Text;
            textBox14.Text = listView3.SelectedItems[0].SubItems[2].Text;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            string yetki = "";
            if (radioButton3.Checked)
            {
                yetki = "admin";
            }
            else if (radioButton4.Checked)
            {
                yetki = "kullanici";
            }
            if (yetki == "admin" || yetki == "kullanici")
            {
                if (
           textBox21.Text == "" && textBox20.Text == "" && textBox19.Text == "" && textBox18.Text == "" && !maskedTextBox3.MaskFull &&
               textBox21.Text == String.Empty && textBox20.Text == String.Empty && textBox19.Text == String.Empty && textBox18.Text == String.Empty && maskedTextBox3.Text == String.Empty)
                {
                    errorProvider4.SetError(textBox21, "Boş Alan Bırakmayınız");
                    errorProvider5.SetError(textBox20, "Boş Alan Bırakmayınız");
                    errorProvider6.SetError(textBox19, "Boş Alan Bırakmayınız");
                    errorProvider7.SetError(textBox18, "Boş Alan Bırakmayınız");
                    errorProvider8.SetError(maskedTextBox3, "Boş Alan Bırakmayınız");
                    //MessageBox.Show("Kırmızı Reknli Alanları Boş Geçemezsiniz", "Boş Alan Hatası");
                }
                else
                {
                    baglanti.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Giris", baglanti);
                    SqlDataReader dr = cmd1.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr["e_mail"].ToString() == textBox18.Text.Trim())
                        {
                            errorProvider4.SetError(txtFiyat, "E-Mail Zaten Alınmış");
                            // MessageBox.Show("Mail Adresi Alınmış !");
                            baglanti.Close();
                            textBox18.Text = "";
                            return;
                        }
                        else if (dr["kullaniciAdi"].ToString() == textBox21.Text.Trim())
                        {
                            MessageBox.Show("Kullanıcı Adı Alınmış !");
                            baglanti.Close();
                            textBox21.Text = "";
                            return;
                        }
                        else if (dr["telefonNo"].ToString() == maskedTextBox3.Text.Trim())
                        {
                            MessageBox.Show("Telefon Numarası Alınmış !");
                            baglanti.Close();
                            maskedTextBox3.Text = "";
                            return;
                        }
                        if (textBox20.Text != textBox19.Text && textBox19.Text != textBox20.Text)
                        {
                            errorProvider5.SetError(textBox20, "Şifreler Aynı Olmak Zorunda");
                            errorProvider6.SetError(textBox21, "Şifreler Aynı Olmak Zorunda");
                            baglanti.Close();
                            return;
                        }
                    }
                    baglanti.Close();
                    baglanti.Open();
                    SqlCommand command = new SqlCommand("insert into Giris(kullaniciAdi,sifre,e_mail,telefonNo,yetki) values ('" + textBox21.Text + "','" + textBox20.Text + "','" + textBox18.Text + "','" + maskedTextBox3.Text + "','"+ yetki + "')", baglanti);
                    command.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Kayıdınız Oluşturulmuştur");
                    radioButton3.Checked = false;
                    radioButton4.Checked = false;
                    listView5.Refresh();
                    listView1.Refresh();
                    verilerigoster();
                    yetkiyonet();
                    verilerisil();
                }
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {        }
        private void tabPage3_Click(object sender, EventArgs e)
        {        }
        private void button12_Click(object sender, EventArgs e)
        {        }
        private void btnResimGor_Click(object sender, EventArgs e)
        {        }
        private void label21_Click(object sender, EventArgs e)
        {        }
        private void txtResim_TextChanged(object sender, EventArgs e)
        {        }
        private void tabPage5_Click(object sender, EventArgs e)
        {        }
        private void button15_Click(object sender, EventArgs e)
        {        }
        private void listView2_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView2.SelectedItems[0].SubItems[0].Text);
            label22.Text = listView2.SelectedItems[0].SubItems[0].Text;
            txtil.Text = listView2.SelectedItems[0].SubItems[1].Text;
            txtilce.Text = listView2.SelectedItems[0].SubItems[2].Text;
            txtMahalle.Text = listView2.SelectedItems[0].SubItems[3].Text;
            cmbSiteAdi.Text = listView2.SelectedItems[0].SubItems[4].Text;
            cmbEmlakTipi.Text = listView2.SelectedItems[0].SubItems[5].Text;
            cmbOdaSayisi.Text = listView2.SelectedItems[0].SubItems[6].Text;
            txtMetre.Text = listView2.SelectedItems[0].SubItems[7].Text;
            cmbIsitma.Text = listView2.SelectedItems[0].SubItems[8].Text;
            txtFiyat.Text = listView2.SelectedItems[0].SubItems[9].Text;
            cmbBlok.Text = listView2.SelectedItems[0].SubItems[10].Text;
            txtDaire.Text = listView2.SelectedItems[0].SubItems[11].Text;
            txtAdSoyad.Text = listView2.SelectedItems[0].SubItems[12].Text;
            mskdTel.Text = listView2.SelectedItems[0].SubItems[13].Text;
            txtAciklama.Text = listView2.SelectedItems[0].SubItems[14].Text;
        }
        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            AutoScroll = true;
        }
        private void listView3_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listView3.EnsureVisible(listView3.Items.Count - 1);
        }
        private void listView2_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            listView2.EnsureVisible(listView2.Items.Count - 1);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            label30.Text = DateTime.Now.ToString();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Refresh();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Giris set kullaniciAdi='" + textBox1.Text.ToString() + "',sifre='" + textBox2.Text.ToString() + "',e_mail='" + textBox17.Text.ToString() + "',telefonNo='" + maskedTextBox2.Text.ToString() + "'where id=" + id + "", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriler();
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {        }
        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked == true)
            {
                radioButton6.Checked = false;
            }
            else if (radioButton6.Checked == true)
            {
                radioButton5.Checked = false;
            }
        }
        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
         
            //  Application.Exit();
          //  this.Close();
            Form1 frgiris = new Form1();
            frgiris.Show();
        }
        private void listView2_DoubleClick_1(object sender, EventArgs e)
        {
            id = int.Parse(listView2.SelectedItems[0].SubItems[0].Text);
            label22.Text = listView2.SelectedItems[0].SubItems[0].Text;
            txtil.Text = listView2.SelectedItems[0].SubItems[1].Text;
            txtilce.Text = listView2.SelectedItems[0].SubItems[2].Text;
            txtMahalle.Text = listView2.SelectedItems[0].SubItems[3].Text;
            cmbSiteAdi.Text = listView2.SelectedItems[0].SubItems[4].Text;
            cmbEmlakTipi.Text = listView2.SelectedItems[0].SubItems[5].Text;
            cmbOdaSayisi.Text = listView2.SelectedItems[0].SubItems[6].Text;
            txtMetre.Text = listView2.SelectedItems[0].SubItems[7].Text;
            cmbIsitma.Text = listView2.SelectedItems[0].SubItems[8].Text;
            txtFiyat.Text = listView2.SelectedItems[0].SubItems[9].Text;
            cmbBlok.Text = listView2.SelectedItems[0].SubItems[10].Text;
            txtDaire.Text = listView2.SelectedItems[0].SubItems[11].Text;
            txtAdSoyad.Text = listView2.SelectedItems[0].SubItems[12].Text;
            mskdTel.Text = listView2.SelectedItems[0].SubItems[13].Text;
            txtAciklama.Text = listView2.SelectedItems[0].SubItems[14].Text;
            radioButton5.Text = listView2.SelectedItems[0].SubItems[15].Text;
            dateTimePicker1.Text = listView2.SelectedItems[0].SubItems[16].Text;
        }
        private void button12_Click_1(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            listView1.Refresh();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Giris set kullaniciAdi='" + textBox1.Text.ToString() + "',sifre='" + Kriptoloji.Encryption(textBox2.Text,2) + "',e_mail='" + textBox17.Text.ToString() + "',telefonNo='" + maskedTextBox2.Text.ToString() + "'where id=" + id + "", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            veriler();
            listView3.Items.Clear();
            listView5.Refresh();
            listView5.Refresh();
            listView1.Refresh();
            verilerigoster();
            yetkiyonet();
            verilerisil();
        }
        private void listView4_SelectedIndexChanged(object sender, EventArgs e)
        {        }
        private void button1_Click_2(object sender, EventArgs e)
        {        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {        }
        private void button1_Click_3(object sender, EventArgs e)
        {
            DialogResult secenek = MessageBox.Show("Kaydı veritabanından silmek istiyor musunuz?", "Bilgilendirme Penceresi", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (secenek == DialogResult.Yes)
            {
                listView4.Refresh();
                listView4.Items.Clear();
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete from rezerveler where RezerveEdilenIlanId='" + textBox3.Text + "'", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                rezervelerilistele();
            }
        }

        private void listView4_DoubleClick(object sender, EventArgs e)
        {
            id = int.Parse(listView4.SelectedItems[0].SubItems[0].Text);
            textBox3.Text = listView4.SelectedItems[0].SubItems[0].Text;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
            Form1 frgiris = new Form1();
            frgiris.Show();
        }

        private void btnTemizle_Click(object sender, EventArgs e)
        {
            ////  cmbSiteAdi.Clear();
            //cmbEmlakTipi.ResetText();
            //cmbIsitma.Items.Clear();
            //cmbOdaSayisi.Items.Clear();
            //txtMetre.Clear();
            //cmbIsitma.Items.Clear();
            //txtFiyat.Clear();
            //txtDaire.Clear();
            //txtAdSoyad.Clear();
            //txtil.Clear();
            //txtilce.Clear();
            //txtAciklama.Clear();
            //txtMahalle.Clear();
            //mskdTel.Clear();
            //cmbBlok.Items.Clear();
           

        }
    }
}
//private void groupBox1_Enter(object sender, EventArgs e)
      //  {

        //}
    

