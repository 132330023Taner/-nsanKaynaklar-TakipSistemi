using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using personelTakip.DAL;
using personelTakip.Entities;

namespace personelTakip.UI
{
    public partial class FormGiris : Form
    {
        public FormGiris()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        // Giriş işlemini başlatır
        private void button1_Click(object sender, EventArgs e)
        {
            var kullaniciAdi = textBox1.Text.Trim();
            var kullaniciSifre = textBox2.Text.Trim();

            if (string.IsNullOrWhiteSpace(kullaniciAdi) || string.IsNullOrWhiteSpace(kullaniciSifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioButton1.Checked)
            {
                YoneticiGirisIslemi(kullaniciAdi, kullaniciSifre);
            }
            else
            {
                PersonelGirisIslemi(kullaniciAdi, kullaniciSifre);
            }
        }

        // Yönetici giriş işlemini gerçekleştirir
        private void YoneticiGirisIslemi(string kullaniciAdi, string sifre)
        {
            if (YoneticiDogrula(kullaniciAdi, sifre))
            {
                Genel.IsAdmin = true;
                var anaMenuForm = new FormAnaMenu();
                this.Hide();
                anaMenuForm.Show();
            }
            else
            {
                MessageBox.Show("Yönetici bilgileri hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Yönetici bilgilerini doğrular
        private bool YoneticiDogrula(string kullaniciAdi, string sifre)
        {
            using (var veritabaniBaglantisi = Veritabani.Baglan())
            {
                var sorguMetni = "SELECT * FROM admins WHERE kullanici_adi=@kullaniciAdi AND sifre=@sifre";
                var sorguKomutu = new MySqlCommand(sorguMetni, veritabaniBaglantisi);
                
                sorguKomutu.Parameters.AddWithValue("@kullaniciAdi", kullaniciAdi);
                sorguKomutu.Parameters.AddWithValue("@sifre", sifre);

                using (var sonucOkuyucu = sorguKomutu.ExecuteReader())
                {
                    return sonucOkuyucu.Read();
                }
            }
        }

        // Personel giriş işlemini gerçekleştirir
        private void PersonelGirisIslemi(string tcNo, string sifre)
        {
            using (var veritabaniBaglantisi = Veritabani.Baglan())
            {
                var sorguMetni = "SELECT * FROM employees WHERE tc_no=@tcNo AND sifre=@sifre";
                var sorguKomutu = new MySqlCommand(sorguMetni, veritabaniBaglantisi);

                sorguKomutu.Parameters.AddWithValue("@tcNo", tcNo.Trim());
                sorguKomutu.Parameters.AddWithValue("@sifre", sifre.Trim());

                using (var sonucOkuyucu = sorguKomutu.ExecuteReader())
                {
                    if (sonucOkuyucu.Read())
                    {
                        PersonelGirisBasarili(sonucOkuyucu);
                    }
                    else
                    {
                        MessageBox.Show("Giriş başarısız!\n\nKontrol edin:\n• TC Kimlik Numaranız\n• Şifreniz (varsayılan: TC No)", 
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        // Başarılı personel girişi sonrası işlemler
        private void PersonelGirisBasarili(MySqlDataReader okuyucu)
        {
            Genel.AktifPersonelId = Convert.ToInt32(okuyucu["id"]);
            Genel.AktifPersonelAdSoyad = $"{okuyucu["ad"]} {okuyucu["soyad"]}";

            int personelDepartmanId = Convert.ToInt32(okuyucu["departman_id"]);
            const int ikDepartmanId = 5;

            if (personelDepartmanId == ikDepartmanId)
            {
                Genel.IsAdmin = true;
                MessageBox.Show("İK Yetkili Girişi Başarılı! 🚀", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                var anaMenuForm = new FormAnaMenu();
                this.Hide();
                anaMenuForm.Show();
            }
            else
            {
                Genel.IsAdmin = false;
                var calisanPanelForm = new FormCalisanPaneli();
                this.Hide();
                calisanPanelForm.Show();
            }
        }
        // Form kapatıldığında uygulamayı sonlandır
        private void FormGiris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // TextBox değişiklik olayı
        }

        private void FormGiris_Load(object sender, EventArgs e)
        {
            // Form yükleme olayı
        }
    }
}