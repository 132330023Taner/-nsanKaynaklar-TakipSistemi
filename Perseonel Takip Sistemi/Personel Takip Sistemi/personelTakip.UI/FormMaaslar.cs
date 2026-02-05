using System;
using System.Collections.Generic;
using System.Windows.Forms;
using personelTakip.BLL;
using personelTakip.Entities;

namespace personelTakip.UI
{
    public partial class FormMaaslar : Form
    {
        public FormMaaslar()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }
    
        private void FormMaaslar_Load(object sender, EventArgs e)
        {
            ListeleriDoldur();
        }

        void ListeleriDoldur()
        {
            // Personel Listesi
            PersonelYoneticisi perYonetici = new PersonelYoneticisi();
            comboBox1.DataSource = perYonetici.TumunuGetir();
            comboBox1.DisplayMember = "Ad";
            comboBox1.ValueMember = "Id";

            // Maaş Listesi 
            MaasYoneticisi maasYonetici = new MaasYoneticisi();
            dataGridView1.DataSource = null; // temizle
            dataGridView1.DataSource = maasYonetici.TumunuGetir();
        }

        // Kaydet
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Maas m = new Maas();
                m.PersonelId = Convert.ToInt32(comboBox1.SelectedValue);
                m.Ay = Convert.ToInt32(textBox1.Text);
                m.Yil = Convert.ToInt32(textBox2.Text);
                m.Tutar = Convert.ToDecimal(textBox3.Text);
                m.Durum = "Ödendi"; // Varsayılan

                MaasYoneticisi yonetici = new MaasYoneticisi();
                yonetici.Ekle(m);

                MessageBox.Show("Maaş kaydı eklendi! 💸");
                ListeleriDoldur();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
            }
        }

        // Sil
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (MessageBox.Show("Silmek istediğinize emin misiniz?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    MaasYoneticisi yonetici = new MaasYoneticisi();
                    yonetici.Sil(id);
                    ListeleriDoldur();
                }
            }
            else
            {
                MessageBox.Show("Lütfen silinecek satırı seçin.");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            // 1. Satır seçili mi?
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden güncellenecek satırı seçin.");
                return;
            }

            try
            {
                Maas m = new Maas();

                // Id kontrolü
                if (dataGridView1.CurrentRow.Cells["Id"].Value != null)
                {
                    m.Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                }
                else
                {
                    MessageBox.Show("HATA: Tabloda ID bulunamadı. Sütun ismi 'Id' olmayabilir.");
                    return;
                }

                // Personel seçimi
                if (comboBox1.SelectedValue != null)
                {
                    m.PersonelId = Convert.ToInt32(comboBox1.SelectedValue);
                }
                else
                {
                    MessageBox.Show("Lütfen Personel seçiniz.");
                    return;
                }

                //Ay kontrolü
                int ayDegeri;
                if (int.TryParse(textBox1.Text, out ayDegeri))
                {
                    m.Ay = ayDegeri;
                }
                else
                {
                    MessageBox.Show("HATA: 'Ay' kutusuna geçerli bir sayı giriniz.");
                    return;
                }

                // yıl kontrolü
                int yilDegeri;
                if (int.TryParse(textBox2.Text, out yilDegeri))
                {
                    m.Yil = yilDegeri;
                }
                else
                {
                    MessageBox.Show("HATA: 'Yıl' kutusuna geçerli bir sayı giriniz.");
                    return;
                }

                //maas kontrolü
                decimal tutarDegeri;
                if (decimal.TryParse(textBox3.Text, out tutarDegeri))
                {
                    m.Tutar = tutarDegeri;
                }
                else
                {
                    MessageBox.Show("HATA: 'Tutar' kutusuna sadece sayı giriniz.");
                    return;
                }

                m.Durum = "Ödendi"; // sabit

                // gönder
                MaasYoneticisi yonetici = new MaasYoneticisi();
                yonetici.Guncelle(m);

                MessageBox.Show("Maaş bilgisi başarıyla güncellendi! ✅");
                ListeleriDoldur();
            }
            catch (Exception hata)
            {
                MessageBox.Show("BEKLENMEYEN HATA: " + hata.Message);
            }
        }


        //Verileri Doldur
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow satir = dataGridView1.Rows[e.RowIndex];

                comboBox1.SelectedValue = Convert.ToInt32(satir.Cells["PersonelId"].Value);
                textBox1.Text = satir.Cells["Ay"].Value.ToString();
                textBox2.Text = satir.Cells["Yil"].Value.ToString();
                textBox3.Text = satir.Cells["Tutar"].Value.ToString();
            }
        }
    }
}