using personelTakip.BLL;
using personelTakip.Entities;
using System;
using System.Windows.Forms;

namespace personelTakip.UI
{
    public partial class FormDepartmanlar : Form
    {
        private readonly DepartmanYoneticisi departmanYoneticisi;

        public FormDepartmanlar()
        {
            InitializeComponent();
            departmanYoneticisi = new DepartmanYoneticisi();
            ThemeHelper.ApplyTheme(this);
        }

        // Form yüklendiğinde çalışır
        private void FormDepartmanlar_Load_1(object sender, EventArgs e)
        {
            ThemeHelper.ApplyTheme(this);
            VerileriYenile();
        }

        // Grid'den seçilen kaydı forma yükler
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var secilenSatir = dataGridView1.Rows[e.RowIndex];
                dprtmnTxtBox.Text = secilenSatir.Cells["Ad"].Value?.ToString() ?? string.Empty;
                aciklamaTxtBox.Text = secilenSatir.Cells["Aciklama"].Value?.ToString() ?? string.Empty;
            }
        }

        // Yeni departman ekleme
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var yeniDepartman = new Departman
                {
                    Ad = dprtmnTxtBox.Text.Trim(),
                    Aciklama = aciklamaTxtBox.Text.Trim()
                };

                departmanYoneticisi.Ekle(yeniDepartman);
                MessageBox.Show("Departman başarıyla eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                FormuTemizle();
                VerileriYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Seçili kaydı güncelleme
        private void btnguncelle(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen güncellemek için bir kayıt seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var guncellenecekDepartman = new Departman
                {
                    Id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value),
                    Ad = dprtmnTxtBox.Text.Trim(),
                    Aciklama = aciklamaTxtBox.Text.Trim()
                };

                var guncellemeYoneticisi = new DepartmanYoneticisi();
                guncellemeYoneticisi.Guncelle(guncellenecekDepartman);

                MessageBox.Show("Kayıt başarıyla güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                FormuTemizle();
                VerileriYenile();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Güncelleme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Seçili kaydı silme
        private void btnsil_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Lütfen silmek için listeden bir satır seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var silmeOnayi = MessageBox.Show("Bu kaydı silmek istediğinize emin misiniz?", "Onay", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (silmeOnayi == DialogResult.Yes)
            {
                try
                {
                    int kayitId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["id"].Value);
                    var silmeYoneticisi = new DepartmanYoneticisi();
                    silmeYoneticisi.Sil(kayitId);

                    MessageBox.Show("Kayıt başarıyla silindi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    VerileriYenile();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Silme hatası: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Veri listesini yeniler
        private void ListeyiYenile()
        {
            VerileriYenile();
        }

        // Verileri veritabanından çekip grid'e yükler
        private void VerileriYenile()
        {
            var veriYoneticisi = new DepartmanYoneticisi();
            dataGridView1.DataSource = veriYoneticisi.TumunuGetir();
        }

        // Form alanlarını temizler
        private void FormuTemizle()
        {
            dprtmnTxtBox.Clear();
            aciklamaTxtBox.Clear();
        }
    }
}
