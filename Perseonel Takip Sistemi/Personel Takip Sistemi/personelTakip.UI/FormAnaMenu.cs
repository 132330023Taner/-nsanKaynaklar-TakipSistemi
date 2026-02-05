using System;
using System.Windows.Forms;

namespace personelTakip.UI
{
    public partial class FormAnaMenu : Form
    {
        public FormAnaMenu()
        {
            InitializeComponent();
            ThemeHelper.ApplyTheme(this);
        }

        // Personel işlemleri modülü
        private void button1_Click(object sender, EventArgs e)
        {
            var personelForm = new FormPersoneller();
            personelForm.ShowDialog();
        }

        // Çıkış işlemi
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Departman yönetimi
        private void button2_Click(object sender, EventArgs e)
        {
            var departmanForm = new FormDepartmanlar();
            departmanForm.ShowDialog();
        }

        // Performans değerlendirme
        private void button6_Click(object sender, EventArgs e)
        {
            var performansForm = new FormPerformans();
            performansForm.ShowDialog();
        }

        // İzin yönetimi
        private void button3_Click(object sender, EventArgs e)
        {
            var izinForm = new FormIzinler();
            izinForm.ShowDialog();
        }

        // Maaş yönetimi
        private void button4_Click(object sender, EventArgs e)
        {
            var maasForm = new FormMaaslar();
            maasForm.ShowDialog();
        }
    }
}
