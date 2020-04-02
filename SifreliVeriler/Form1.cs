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

namespace SifreliVeriler
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection(@"Data Source=.;Initial Catalog=DbSifreleme;Integrated Security=True");
        void Listele()
        {
            SqlDataAdapter da = new SqlDataAdapter("Select Ad,Soyad,Mail,Sifre,HesapNo from tblVeriler",con);
            DataSet ds = new DataSet();
            da.Fill(ds);
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                row["Ad"] = Coz(row["Ad"] as string);
                row["Soyad"] = Coz(row["Soyad"] as string);
                row["Mail"] = Coz(row["Mail"] as string);
                row["Sifre"] = Coz(row["Sifre"] as string);
                row["HesapNo"] = Coz(row["HesapNo"] as string);
            }
            dataGridView1.DataSource = ds.Tables[0];
            
        }

        private object Coz(string v1)
        {
            byte[] cozumdizi = Convert.FromBase64String(v1);
            string adverisi = ASCIIEncoding.ASCII.GetString(cozumdizi);
            return adverisi;
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            Listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            string ad = txtAd.Text;
            byte[] adDizi = ASCIIEncoding.ASCII.GetBytes(ad);
            string adSifre = Convert.ToBase64String(adDizi);

            string soyad = txtSoyad.Text;
            byte[] soyadDizi = ASCIIEncoding.ASCII.GetBytes(soyad);
            string soyadSifre = Convert.ToBase64String(soyadDizi);

            string mail = txtMail.Text;
            byte[] mailDizi = ASCIIEncoding.ASCII.GetBytes(mail);
            string mailSifre = Convert.ToBase64String(mailDizi);

            string sifre = txtSifre.Text;
            byte[] sifreDizi = ASCIIEncoding.ASCII.GetBytes(sifre);
            string sifreSifre = Convert.ToBase64String(sifreDizi);

            string hesap = txtHesapNo.Text;
            byte[] hesapDizi = ASCIIEncoding.ASCII.GetBytes(hesap);
            string hesapSifre = Convert.ToBase64String(hesapDizi);

            con.Open();
            SqlCommand cmd = new SqlCommand("insert into tblVeriler (Ad,Soyad,Mail,Sifre,HesapNo) values(@p1,@p2,@p3,@p4,@p5)", con);
            cmd.Parameters.AddWithValue("@p1", adSifre);
            cmd.Parameters.AddWithValue("@p2", soyadSifre);
            cmd.Parameters.AddWithValue("@p3", mailSifre);
            cmd.Parameters.AddWithValue("@p4", sifreSifre);
            cmd.Parameters.AddWithValue("@p5", hesapSifre);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Verileriniz eklenmiştir.");
            txtAd.Clear();
            txtSoyad.Clear();
            txtMail.Clear();
            txtSifre.Clear();
            txtHesapNo.Clear();
            Listele();

        }
    }
}
