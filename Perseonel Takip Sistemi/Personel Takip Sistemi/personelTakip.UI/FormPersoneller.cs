using System;
using System.Collections.Generic;
using System.Windows.Forms;
using personelTakip.BLL;
using personelTakip.Entities;

namespace personelTakip.UI
{
    public partial class FormPersoneller : Form
    {
        public FormPersoneller()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        // Form yüklendiğinde verileri hazırla
        private void FormPersoneller_Load(object sender, EventArgs e)
        {
            DepartmanListesiniYukle();
            PersonelListesiniYukle();
        }

        // Departman listesini ComboBox'a yükler
        private void DepartmanListesiniYukle()
        {
            var departmanYoneticisi = new DepartmanYoneticisi();
            var tumDepartmanlar = departmanYoneticisi.TumunuGetir();

            comboBox1.DataSource = tumDepartmanlar;
            comboBox1.DisplayMember = "Ad";
            comboBox1.ValueMember = "Id";
        }

        // Personel listesini grid'e yükler
        private void PersonelListesiniYukle()
        {
            var personelYoneticisi = new PersonelYoneticisi();
            dataGridView1.DataSource = personelYoneticisi.TumunuGetir();
        }

        // Eski metod adı için uyumluluk
        private void ListeyiYenile()
        {
            PersonelListesiniYukle();
        }
        // Yeni personel ekleme
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var yeniPersonel = new Personel
                {
                    Ad = textBox1.Text.Trim(),
                    Soyad = textBox2.Text.Trim(),
                    TcNo = textBox3.Text.Trim(),
                    DepartmanId = Convert.ToInt32(comboBox1.SelectedValue),
                    IseGirisTarihi = dateTimePicker1.Value,
                    Sifre = textBox3.Text.Trim() // Şifre TC No ile aynı
                };

                // Maaş kontrolü
                if (decimal.TryParse(textBox4.Text, out decimal maasDegeri))
                {
                    yeniPersonel.Maas = maasDegeri;
                }
                else
                {
                    yeniPersonel.Maas = 0;
                }

                var personelYoneticisi = new PersonelYoneticisi();
                personelYoneticisi.Ekle(yeniPersonel);

                MessageBox.Show("Personel başarıyla eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                PersonelListesiniYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Form alanlarını temizler
        private void FormuTemizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        // Seçili personeli silme
        private void btnSil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden silinecek personeli seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var onaySonucu = MessageBox.Show("Bu personeli silmek istediğinize emin misiniz?", 
                "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onaySonucu == DialogResult.Yes)
            {
                try
                {
                    int personelId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["ID"].Value);
                    var personelYoneticisi = new PersonelYoneticisi();
                    personelYoneticisi.Sil(personelId);

                    MessageBox.Show("Personel silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    PersonelListesiniYukle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        // Grid'den seçilen kaydı forma yükler
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var secilenSatir = dataGridView1.Rows[e.RowIndex];
            
            textBox1.Text = secilenSatir.Cells["Ad"].Value?.ToString() ?? string.Empty;
            textBox2.Text = secilenSatir.Cells["Soyad"].Value?.ToString() ?? string.Empty;
            textBox3.Text = secilenSatir.Cells["TcNo"].Value?.ToString() ?? string.Empty;
            textBox4.Text = secilenSatir.Cells["Maas"].Value?.ToString() ?? string.Empty;

            if (secilenSatir.Cells["IseGirisTarihi"].Value != null)
            {
                dateTimePicker1.Value = Convert.ToDateTime(secilenSatir.Cells["IseGirisTarihi"].Value);
            }

            if (secilenSatir.Cells["DepartmanId"].Value != null)
            {
                comboBox1.SelectedValue = Convert.ToInt32(secilenSatir.Cells["DepartmanId"].Value);
            }
        }
        // Seçili personeli güncelleme
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen listeden bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // ID kontrolü
                if (dataGridView1.CurrentRow.Cells["Id"].Value == null)
                {
                    MessageBox.Show("Seçilen satırın ID'si bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var guncellenecekPersonel = new Personel
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value),
                    Ad = textBox1.Text.Trim(),
                    Soyad = textBox2.Text.Trim(),
                    TcNo = textBox3.Text.Trim(),
                    IseGirisTarihi = dateTimePicker1.Value
                };

                // Maaş kontrolü
                if (!decimal.TryParse(textBox4.Text, out decimal maasDegeri))
                {
                    MessageBox.Show("Maaş alanına geçerli bir sayı giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                guncellenecekPersonel.Maas = maasDegeri;

                // Departman kontrolü
                if (comboBox1.SelectedValue == null)
                {
                    MessageBox.Show("Lütfen bir departman seçiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                guncellenecekPersonel.DepartmanId = Convert.ToInt32(comboBox1.SelectedValue);

                // Güncelleme işlemi
                var personelYoneticisi = new PersonelYoneticisi();
                personelYoneticisi.Guncelle(guncellenecekPersonel);

                MessageBox.Show("Güncelleme başarılı! 🎉", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                PersonelListesiniYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Beklenmeyen hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Label tıklama olayı
        }
    }
}
