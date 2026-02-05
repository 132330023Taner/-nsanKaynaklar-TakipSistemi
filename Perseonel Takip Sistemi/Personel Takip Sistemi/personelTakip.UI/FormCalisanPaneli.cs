using System;
using System.Collections.Generic;
using System.Windows.Forms;
using personelTakip.BLL;
using personelTakip.Entities;

namespace personelTakip.UI
{
    public partial class FormCalisanPaneli : Form
    {
        public FormCalisanPaneli()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        private void FormCalisanPaneli_Load(object sender, EventArgs e)
        {
            // 1. isimi güncelle
            lblAdSoyad.Text = "Hoşgeldiniz, " + Genel.AktifPersonelAdSoyad;

            // 2. Kendi Verilerini Getir
            VerileriGetir();
        }

        void VerileriGetir()
        {
            int benimId = Genel.AktifPersonelId;
            

            // 3. PERFORMANS LİSTESİ 
            PerformansYoneticisi prfYonetici = new PerformansYoneticisi();
            List<Performans> tumPerformanslar = prfYonetici.TumunuGetir();
            List<Performans> benimPerformanslarim = new List<Performans>();

            foreach (var p in tumPerformanslar)
            {
                if (p.PersonelId == benimId)
                {
                    benimPerformanslarim.Add(p);
                }
            }

            // Listeyi tabloya bas
            dataGridView3.DataSource = benimPerformanslarim;

            // Gereksiz sütunları gizle 
            if (dataGridView3.Columns["PersonelId"] != null)
                dataGridView3.Columns["PersonelId"].Visible = false;

            if (dataGridView3.Columns["PersonelAdSoyad"] != null)
                dataGridView3.Columns["PersonelAdSoyad"].Visible = false;

            // Maaşlar
            MaasYoneticisi maasYonetici = new MaasYoneticisi();
            List<Maas> tumMaaslar = maasYonetici.TumunuGetir();
            List<Maas> benimMaaslarim = new List<Maas>();

            
            foreach (var m in tumMaaslar)
            {
                if (m.PersonelId == benimId)
                {
                    benimMaaslarim.Add(m);
                }
            }
            dataGridView1.DataSource = benimMaaslarim;

            // İzinler
            IzinYoneticisi izinYonetici = new IzinYoneticisi();
            List<Izin> tumIzinler = izinYonetici.TumunuGetir();
            List<Izin> benimIzinlerim = new List<Izin>();

            foreach (var i in tumIzinler)
            {
                if (i.PersonelId == benimId)
                {
                    benimIzinlerim.Add(i);
                }
            }
            dataGridView2.DataSource = benimIzinlerim;


        }

        // Çıkış yapınca uygulamayı kapat
        private void FormCalisanPaneli_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

       

        private void btnTalepEt_Click(object sender, EventArgs e)
        {
            try
            {
                // 1.  Kontrol
                if (string.IsNullOrEmpty(txtAciklama.Text))
                {
                    MessageBox.Show("Lütfen izin nedeninizi (Açıklama) yazınız.");
                    return;
                }

                // 2. İzin Oluştur
                personelTakip.Entities.Izin yeniIzin = new personelTakip.Entities.Izin();

                
                // Kimin adına izin istiyoruz
                yeniIzin.PersonelId = Genel.AktifPersonelId;

                yeniIzin.BaslangicTarihi = dtpBaslangic.Value;
                yeniIzin.BitisTarihi = dtpBitis.Value;
                yeniIzin.Aciklama = txtAciklama.Text;

                // otomatik "Onay Bekliyor"
                yeniIzin.Durum = "Onay Bekliyor";

                // 3. BLL çağır ve Kaydet
                personelTakip.BLL.IzinYoneticisi yonetici = new personelTakip.BLL.IzinYoneticisi();
                yonetici.Ekle(yeniIzin);

                MessageBox.Show("İzin talebiniz yöneticiye iletildi! 📝");

                // 4. Listeyi Yenile 
                VerileriGetir();

                // temizle
                txtAciklama.Clear();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
            }
        }
    }
}