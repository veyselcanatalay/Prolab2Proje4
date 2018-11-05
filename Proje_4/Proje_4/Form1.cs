using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Proje_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Veysel Can ATALAY 160202047 - Baris Ayberk GUNER 150202001
        //Baglan degiskenine bir MySql baglantisi atadik.
        MySqlConnection connection = new MySqlConnection("server=localhost;database=aracyonetimsistemi;user=root;Pwd=;Encrypt=false;AllowUserVariables=True;UseCompression=True");
        MySqlDataAdapter da;
        DataTable dt;
        string sql = "SELECT Ilan_Adi,Ilan_Fiyat,Ilan_Km,Ilan_Tarih,Ilan_ArabaID FROM tbl_ilan ";
        void Listele(string aranan)
        {
            da = new MySqlDataAdapter(sql, connection);
            dt = new DataTable();
            connection.Open();
            da.Fill(dt);
            connection.Close();
            dataGridView1.DataSource = dt;
        }

        private void kayitGetir()
        {
            connection.Open();
            string kayit = "Select aracyonetimsistemi.tbl_ilan.Ilan_Adi, aracyonetimsistemi.tbl_ilan.Ilan_Fiyat, aracyonetimsistemi.tbl_ilan.Ilan_Km, aracyonetimsistemi.tbl_ilan.Ilan_Tarih, aracyonetimsistemi.tbl_araba.Araba_Marka, aracyonetimsistemi.tbl_renk.Renk, aracyonetimsistemi.tbl_sehir.Sehir, aracyonetimsistemi.tbl_vitesturu.VitesTuru, aracyonetimsistemi.tbl_yakitturu.YakitTuru " +
                "FROM tbl_ilan INNER JOIN tbl_araba ON tbl_ilan.Ilan_ArabaID=tbl_araba.ArabaID " +
                "INNER JOIN tbl_renk ON tbl_araba.Araba_RenkID=tbl_renk.RenkID " +
                "INNER JOIN tbl_sehir ON tbl_ilan.Ilan_SehirID=tbl_sehir.SehirID " +
                "INNER JOIN tbl_yakitturu ON tbl_araba.Araba_YakitTuruID=tbl_yakitturu.YakitTuruID " +
                "INNER JOIN tbl_vitesturu ON tbl_araba.Araba_VitesTuruID=tbl_vitesturu.VitesTuruID";
            //musteriler tablosundaki tüm kayıtları çekecek olan sql sorgusu.
            MySqlCommand komut = new MySqlCommand(kayit, connection);
            //Sorgumuzu ve baglantimizi parametre olarak alan bir SqlCommand nesnesi oluşturuyoruz.        
            MySqlDataAdapter da = new MySqlDataAdapter(komut);
            //SqlDataAdapter sınıfı verilerin databaseden aktarılması işlemini gerçekleştirir.
            DataTable dt = new DataTable();
            da.Fill(dt);
            //Bir DataTable oluşturarak DataAdapter ile getirilen verileri tablo içerisine dolduruyoruz.
            dataGridView1.DataSource = dt;
            //Formumuzdaki DataGridViewin veri kaynağını oluşturduğumuz tablo olarak gösteriyoruz.
            connection.Close();
        }

        private void kayitEkle()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string kayit = "insert into tbl_ilan(Ilan_Adi,Ilan_Fiyat,Ilan_Km,Ilan_Tarih,Ilan_SehirID) values (@Ilan_Adi,@Ilan_Fiyat,@Ilan_Km,@Ilan_Tarih,@Ilan_SehirID)";
                string kayit2 = "insert into tbl_araba(Araba_Marka,Araba_VitesTuruID,Araba_YakitTuruID,Araba_RenkID) values (@Araba_Marka,@Araba_VitesTuruID,@Araba_YakitTuruID,@Araba_RenkID)";
                MySqlCommand komut = new MySqlCommand(kayit, connection);
                MySqlCommand komut2 = new MySqlCommand(kayit2, connection);

                komut.Parameters.AddWithValue("@Ilan_Adi", textBox10.Text);
                komut.Parameters.AddWithValue("@Ilan_Fiyat", textBox7.Text);
                komut.Parameters.AddWithValue("@Ilan_Km", textBox8.Text);
                komut.Parameters.AddWithValue("@Ilan_Tarih", textBox9.Text);
                komut.Parameters.AddWithValue("@Ilan_SehirID", comboBox6.Text);
                komut2.Parameters.AddWithValue("@Araba_Marka", textBox6.Text);
                komut2.Parameters.AddWithValue("@Araba_VitesTuruID", comboBox8.Text);
                komut2.Parameters.AddWithValue("@Araba_YakitTuruID", comboBox7.Text);
                komut2.Parameters.AddWithValue("@Araba_RenkID", comboBox9.Text);

                komut.ExecuteNonQuery();
                komut2.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("Başarıyla eklendi.");

            }
            catch (Exception e)
            {
                MessageBox.Show("Hata oluştu!" + e.Message);
            }
        }



        //private void kayitGuncelleme()
        //{
        //    try
        //    {
        //        if (connection.State == ConnectionState.Closed)
        //            connection.Open();
        //        string kayit = "UPDATE from tbl_ilan set Ilan_Adi='"+textBox10.Text+ "',Ilan_Fiyat='" + textBox7.Text + "',Ilan_Km='" + textBox8.Text + "',Ilan_Tarih='" + textBox9.Text + "',Ilan_SehirID='" + comboBox6.Text + "' WHERE IlanID= '"+textBox12.Text+"'";
        //        string kayit2 = "UPDATE from tbl_araba set Araba_Marka='"+textBox6.Text+ "',Araba_VitesTuruID='" + comboBox8.Text + "',Araba_YakitTuruID='" + comboBox7.Text + "',Araba_RenkID='" + comboBox9.Text + "' WHERE ArabaID='"+textBox12.Text+"'";
        //        MySqlCommand komut = new MySqlCommand(kayit, connection);
        //        MySqlCommand komut2 = new MySqlCommand(kayit2, connection);

        //        komut.Parameters.AddWithValue("@Ilan_Adi", textBox10.Text);
        //        komut.Parameters.AddWithValue("@Ilan_Fiyat", textBox7.Text);
        //        komut.Parameters.AddWithValue("@Ilan_Km", textBox8.Text);
        //        komut.Parameters.AddWithValue("@Ilan_Tarih", textBox9.Text);
        //        komut.Parameters.AddWithValue("@Ilan_SehirID", comboBox6.Text);
        //        komut2.Parameters.AddWithValue("@Araba_Marka", textBox6.Text);
        //        komut2.Parameters.AddWithValue("@Araba_VitesTuruID", comboBox8.Text);
        //        komut2.Parameters.AddWithValue("@Araba_YakitTuruID", comboBox7.Text);
        //        komut2.Parameters.AddWithValue("@Araba_RenkID", comboBox9.Text);

        //        komut.ExecuteNonQuery();
        //        komut2.ExecuteNonQuery();
        //        connection.Close();

        //        MessageBox.Show("Başarıyla eklendi.");

        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Hata oluştu!" + e.Message);
        //    }
        //}

        private void KayitSil()
        {
            connection.Open();
            string secmeSorgusu = "SELECT * from tbl_ilan where IlanID=@IlanID";
            //musterino parametresine bağlı olarak müşteri bilgilerini çeken sql kodu
            MySqlCommand secmeKomutu = new MySqlCommand(secmeSorgusu, connection);
            secmeKomutu.Parameters.AddWithValue("@IlanID", textBox11.Text);
            //musterino parametremize textbox'dan girilen değeri aktarıyoruz.
            MySqlDataAdapter da = new MySqlDataAdapter(secmeKomutu);
            MySqlDataReader dr = secmeKomutu.ExecuteReader();
            //DataReader ile müşteri verilerini veritabanından belleğe aktardık.
            if (dr.Read()) //Datareader herhangi bir okuma yapabiliyorsa aşağıdaki kodlar çalışır.
            {
                string isim = dr["Ilan_Adi"].ToString();
                dr.Close();
                //Datareader ile okunan müşteri ad ve soyadını isim değişkenine atadım.
                //Datareader açık olduğu sürece başka bir sorgu çalıştıramayacağımız için dr nesnesini kapatıyoruz.
                DialogResult durum = MessageBox.Show(isim + " kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                //Kullanıcıya silme onayı penceresi açıp, verdiği cevabı durum değişkenine aktardık.
                if (DialogResult.Yes == durum) // Eğer kullanıcı Evet seçeneğini seçmişse, veritabanından kaydı silecek kodlar çalışır.
                {
                    string silmeSorgusu = "DELETE from tbl_ilan where IlanID='" + textBox11.Text + "'";
                    //musterino parametresine bağlı olarak müşteri kaydını silen sql sorgusu
                    MySqlCommand silKomutu = new MySqlCommand(silmeSorgusu, connection);
                    silKomutu.Parameters.AddWithValue("@IlanID", textBox11.Text);
                    silKomutu.ExecuteNonQuery();
                    MessageBox.Show("Kayıt Silindi...");
                    //Silme işlemini gerçekleştirdikten sonra kullanıcıya mesaj verdik.
                }
            }
            else
                MessageBox.Show("İlan bulunamadı.");
            connection.Close();
        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //connection.Open();
            MessageBox.Show("Bağlantı başarılı.");
            //connection.Close();
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            kayitGetir();
            //Tüm kayıtları gösterecek olan kayitGetir() metodumuzu çağırıyoruz.
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            kayitEkle();
            kayitGetir();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("chrome.exe", "http://localhost/phpmyadmin/db_structure.php?server=1&db=aracyonetimsistemi");
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            KayitSil();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            // string Siralama= "SELECT * from "
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                sql = "SELECT *FROM tbl_araba WHERE ArabaMarka='" + textBox12.Text + "'";
            }
            if (checkBox2.Checked)
            {
                sql = "SELECT *FROM tbl_sehir WHERE Sehir='" + textBox12.Text + "'";
            }
            if (checkBox3.Checked)
            {
                sql = "SELECT *FROM tbl_ilan WHERE Ilan_Fiyat BETWEEN '" + textBox1.Text + "' and '" + textBox2.Text + "'";
            }
            if (checkBox4.Checked)
            {
                sql = "SELECT *FROM tbl_araba WHERE Araba_YakitTuru='" + textBox12.Text + "'";
            }
            if (checkBox5.Checked)
            {
                sql = "SELECT *FROM tbl_araba WHERE Araba_VitesTuru='" + textBox12.Text + "'";
            }
            if (checkBox6.Checked)
            {
                sql = "SELECT *FROM tbl_ilan WHERE Ilan_Km BETWEEN '" + textBox3.Text + "' and '" + textBox4.Text + "'";
            }
            if (checkBox7.Checked)
            {
                sql = "SELECT *FROM tbl_renk WHERE Renk='" + textBox12.Text + "'";
            }
            if (checkBox8.Checked)
            {
                sql = "SELECT *FROM tbl_ilan WHERE Ilan_Tarih='" + textBox12.Text + "'";
            }

            Listele(sql);
        }
    }
}
