using System;
using System.Collections.Generic;
using System.Windows.Forms;
using personelTakip.BLL;
using personelTakip.Entities;

namespace personelTakip.UI
{
    public partial class FormIzinler : Form
    {
        public FormIzinler()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        // Form yüklendiğinde verileri hazırla
        private void FormIzinler_Load(object sender, EventArgs e)
        {
            VerileriYukle();
        }

        // Tüm listeleri doldurur
        private void VerileriYukle()
        {
            PersonelListesiniYukle();
            IzinListesiniYukle();
        }

        // Eski metod adı için uyumluluk
        private void ListeleriDoldur()
        {
            VerileriYukle();
        }

        // Personel listesini ComboBox'a yükler
        private void PersonelListesiniYukle()
        {
            var personelYoneticisi = new PersonelYoneticisi();
            var tumPersoneller = personelYoneticisi.TumunuGetir();

            comboBox1.DataSource = tumPersoneller;
            comboBox1.DisplayMember = "Ad";
            comboBox1.ValueMember = "Id";
        }

        // İzin listesini grid'e yükler
        private void IzinListesiniYukle()
        {
            var izinYoneticisi = new IzinYoneticisi();
            dataGridView1.DataSource = izinYoneticisi.TumunuGetir();
        }

        // Yeni izin talebi oluşturma
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedValue == null)
            {
                MessageBox.Show("Lütfen bir personel seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var yeniIzinTalebi = new Izin
                {
                    PersonelId = Convert.ToInt32(comboBox1.SelectedValue),
                    BaslangicTarihi = dateTimePicker1.Value,
                    BitisTarihi = dateTimePicker2.Value,
                    Aciklama = textBox1.Text.Trim(),
                    Durum = "Onay Bekliyor"
                };

                var izinYoneticisi = new IzinYoneticisi();
                izinYoneticisi.Ekle(yeniIzinTalebi);

                MessageBox.Show("İzin talebi başarıyla oluşturuldu! 🎉", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                textBox1.Clear();
                IzinListesiniYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Alternatif izin ekleme metodu (eski uyumluluk için)
        private void button1_Click_1(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }

        // Seçili izin kaydını silme
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden silinecek izni seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onaySonucu = MessageBox.Show("Bu izin kaydını silmek istediğinize emin misiniz?", 
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onaySonucu == DialogResult.Yes)
            {
                try
                {
                    int izinId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
                    var izinYoneticisi = new IzinYoneticisi();
                    izinYoneticisi.Sil(izinId);

                    MessageBox.Show("İzin kaydı silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    IzinListesiniYukle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Grid'den seçilen kaydı forma yükler
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var secilenSatir = dataGridView1.Rows[e.RowIndex];

            if (secilenSatir.Cells["PersonelId"].Value != null)
            {
                comboBox1.SelectedValue = Convert.ToInt32(secilenSatir.Cells["PersonelId"].Value);
            }

            if (secilenSatir.Cells["BaslangicTarihi"].Value != null)
            {
                dateTimePicker1.Value = Convert.ToDateTime(secilenSatir.Cells["BaslangicTarihi"].Value);
            }

            if (secilenSatir.Cells["BitisTarihi"].Value != null)
            {
                dateTimePicker2.Value = Convert.ToDateTime(secilenSatir.Cells["BitisTarihi"].Value);
            }

            textBox1.Text = secilenSatir.Cells["Aciklama"].Value?.ToString() ?? string.Empty;
        }

        // Seçili izin kaydını güncelleme
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden güncellenecek satırı seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (dataGridView1.CurrentRow.Cells["Id"].Value == null)
                {
                    MessageBox.Show("Tabloda ID hücresi boş görünüyor!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var guncellenecekIzin = new Izin
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    PersonelId = Convert.ToInt32(comboBox1.SelectedValue),
                    BaslangicTarihi = dateTimePicker1.Value,
                    BitisTarihi = dateTimePicker2.Value,
                    Aciklama = textBox1.Text.Trim(),
                    Durum = cmbDurum.Text
                };

                var izinYoneticisi = new IzinYoneticisi();
                izinYoneticisi.Guncelle(guncellenecekIzin);

                MessageBox.Show("İzin bilgileri güncellendi! ✅", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                IzinListesiniYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Grid içerik tıklama olayı
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var secilenSatir = dataGridView1.Rows[e.RowIndex];

            if (secilenSatir.Cells["PersonelId"].Value != null)
            {
                comboBox1.SelectedValue = Convert.ToInt32(secilenSatir.Cells["PersonelId"].Value);
            }

            if (secilenSatir.Cells["BaslangicTarihi"].Value != null)
            {
                dateTimePicker1.Value = Convert.ToDateTime(secilenSatir.Cells["BaslangicTarihi"].Value);
            }

            if (secilenSatir.Cells["BitisTarihi"].Value != null)
            {
                dateTimePicker2.Value = Convert.ToDateTime(secilenSatir.Cells["BitisTarihi"].Value);
            }

            textBox1.Text = secilenSatir.Cells["Aciklama"].Value?.ToString() ?? string.Empty;

            if (secilenSatir.Cells["Durum"].Value != null)
            {
                cmbDurum.Text = secilenSatir.Cells["Durum"].Value.ToString();
            }
        }
    }
    
}