using System;
using System.Windows.Forms;
using personelTakip.BLL;
using personelTakip.Entities;

namespace personelTakip.UI
{
    public partial class FormPerformans : Form
    {
        public FormPerformans()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        private void FormPerformans_Load(object sender, EventArgs e)
        {
            ListeleriDoldur();
            comboBox1.DisplayMember = "AdSoyad";
        }

        void ListeleriDoldur()
        {
            // 1. Personelleri ComboBox'a doldur
            PersonelYoneticisi perYonetici = new PersonelYoneticisi();
            comboBox1.DataSource = perYonetici.TumunuGetir();
            comboBox1.DisplayMember = "Ad"; 
            comboBox1.ValueMember = "Id";

            // 2. Tabloyu Doldur
            PerformansYoneticisi prfYonetici = new PerformansYoneticisi();
            dataGridView1.DataSource = prfYonetici.TumunuGetir();
            dataGridView1.Columns["PersonelId"].Visible = false;
        }

        // EKLE BUTONU
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Performans p = new Performans();
                p.PersonelId = Convert.ToInt32(comboBox1.SelectedValue);
                p.Puan = Convert.ToInt32(numericUpDown1.Value);
                p.Aciklama = textBox1.Text;
                p.DegerlendirmeTarihi = dateTimePicker1.Value;

                PerformansYoneticisi yonetici = new PerformansYoneticisi();
                yonetici.Ekle(p);

                MessageBox.Show("Performans notu kaydedildi! ⭐");
                ListeleriDoldur();
            }
            catch (Exception hata)
            {
                MessageBox.Show("Hata: " + hata.Message);
            }
        }

        // SİL BUTONU
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                if (MessageBox.Show("Silmek istediğine emin misin?", "Onay", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["Id"].Value);
                    PerformansYoneticisi yonetici = new PerformansYoneticisi();
                    yonetici.Sil(id);
                    ListeleriDoldur();
                }
            }
        }
    }
}
