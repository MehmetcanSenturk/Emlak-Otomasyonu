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
    public partial class RezerveEt : Form
    {
        public RezerveEt()
        {
            InitializeComponent();
        }
        public static string rezerveEdenKullanici;
        public static int rezerveEdilenIlanIDsi;
        public static DateTime rezerveEdilmeTarihi;
        SqlConnection con = new SqlConnection(@"Data Source =DESKTOP-6FT9999; Initial Catalog = emlak; Persist Security Info=false; User ID = root; Password=123");
        private void RezerveEt_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand sqlSorgula = new SqlCommand("select RezerveEdilenIlanId from rezerveler where RezerveEdilenIlanId = '" + numericUpDown1.Value + "'", con);
            SqlDataReader rd = sqlSorgula.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    MessageBox.Show("İlan Önceden Rezerve Edilmiş!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                rd.Close();
                rezerveEdenKullanici = Form1.kullaniciAdi;
                rezerveEdilenIlanIDsi = Convert.ToInt32(numericUpDown1.Value);
                rezerveEdilmeTarihi = DateTime.Now;
                SqlCommand com2 = new SqlCommand("insert into rezerveler (RezerveEdilenIlanId,RezerveEdenKullanici,RezerveEdilmeTarihi) values ('" + rezerveEdilenIlanIDsi + "','" + rezerveEdenKullanici + "','" + rezerveEdilmeTarihi + "')", con);
                com2.ExecuteNonQuery();
                SqlCommand com = new SqlCommand("update siteBilgi set RezerveDurumu = 'Rezerve Edilmiş' where id = '" + rezerveEdilenIlanIDsi + "'", con);
                com.ExecuteNonQuery();
                MessageBox.Show("İlan Rezerve Edildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
           con.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            
            if (Form1.kullaniciAdi != rezerveEdenKullanici)
            {
                MessageBox.Show("İlanı Yalnızca Rezerve Eden Kişi İptal  Edebilir!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            else
            {
                SqlCommand com = new SqlCommand("update siteBilgi set RezerveDurumu = 'Rezerve Edilmemiş' where id = '" + rezerveEdilenIlanIDsi + "'", con);
                com.ExecuteNonQuery();
                MessageBox.Show("İlan Rezervesi İptal Edildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            con.Close();
        }

        private void RezerveEt_Load(object sender, EventArgs e)
        {

        }
    }
}
