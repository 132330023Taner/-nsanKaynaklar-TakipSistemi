using System;
using System.Drawing;
using System.Windows.Forms;

namespace personelTakip.UI
{
    public static class ThemeHelper
    {
        // Renk tanımlamaları
        public static readonly Color FormBackColor = Color.FromArgb(248, 249, 250);
        public static readonly Color ButtonPrimary = Color.FromArgb(0, 122, 204);
        public static readonly Color ButtonSuccess = Color.FromArgb(40, 167, 69);
        public static readonly Color ButtonDanger = Color.FromArgb(220, 53, 69);
        public static readonly Color ButtonWarning = Color.FromArgb(255, 193, 7);
        public static readonly Color ButtonSecondary = Color.FromArgb(108, 117, 125);
        public static readonly Color TextBoxBackColor = Color.White;
        public static readonly Color LabelForeColor = Color.FromArgb(64, 64, 64);
        public static readonly Color DataGridHeaderBackColor = Color.FromArgb(0, 122, 204);
        public static readonly Color DataGridHeaderForeColor = Color.White;
        public static readonly Color DataGridBackColor = Color.White;
        public static readonly Color DataGridGridColor = Color.FromArgb(224, 224, 224);
        public static readonly Color ButtonForeColor = Color.White;

        /// <summary>
        /// Form ve tüm kontrollerine tema uygular
        /// </summary>
        public static void ApplyTheme(Form form)
        {
            if (form == null) return;

            // Form arka plan rengi
            form.BackColor = FormBackColor;

            // Form içindeki tüm kontrolleri işle
            ApplyThemeToControls(form.Controls);
        }

        /// <summary>
        /// Kontrol koleksiyonuna tema uygular (recursive)
        /// </summary>
        private static void ApplyThemeToControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                // Button kontrolleri
                if (control is Button button)
                {
                    ApplyButtonTheme(button);
                }
                // TextBox kontrolleri
                else if (control is TextBox textBox)
                {
                    textBox.BackColor = TextBoxBackColor;
                    textBox.BorderStyle = BorderStyle.FixedSingle;
                }
                // Label kontrolleri
                else if (control is Label label)
                {
                    if (label.BackColor != Color.Transparent)
                    {
                        label.BackColor = Color.Transparent;
                    }
                    label.ForeColor = LabelForeColor;
                }
                // DataGridView kontrolleri
                else if (control is DataGridView dataGrid)
                {
                    ApplyDataGridTheme(dataGrid);
                }
                // ComboBox kontrolleri
                else if (control is ComboBox comboBox)
                {
                    comboBox.BackColor = TextBoxBackColor;
                    comboBox.FlatStyle = FlatStyle.Flat;
                }
                // DateTimePicker kontrolleri
                else if (control is DateTimePicker dateTimePicker)
                {
                    dateTimePicker.BackColor = TextBoxBackColor;
                }
                // NumericUpDown kontrolleri
                else if (control is NumericUpDown numericUpDown)
                {
                    numericUpDown.BackColor = TextBoxBackColor;
                }
                // RichTextBox kontrolleri
                else if (control is RichTextBox richTextBox)
                {
                    richTextBox.BackColor = TextBoxBackColor;
                }
                // GroupBox kontrolleri
                else if (control is GroupBox groupBox)
                {
                    groupBox.BackColor = Color.Transparent;
                    groupBox.ForeColor = LabelForeColor;
                }
                // Panel kontrolleri
                else if (control is Panel panel)
                {
                    panel.BackColor = FormBackColor;
                }

                // İç içe kontroller varsa recursive olarak işle
                if (control.HasChildren)
                {
                    ApplyThemeToControls(control.Controls);
                }
            }
        }

        /// <summary>
        /// Button kontrolüne tema uygular
        /// </summary>
        private static void ApplyButtonTheme(Button button)
        {
            // Buton metnine göre renk belirle
            string buttonText = button.Text.ToUpper().Trim();
            
            if (buttonText.Contains("SİL") || buttonText.Contains("ÇIKIŞ") || 
                buttonText.Contains("DELETE") || buttonText.Contains("EXIT") || 
                buttonText.Contains("ÇIK"))
            {
                button.BackColor = ButtonDanger;
            }
            else if (buttonText.Contains("KAYDET") || buttonText.Contains("EKLE") || 
                     buttonText.Contains("SAVE") || buttonText.Contains("ADD") ||
                     buttonText.Contains("PERSONEL") || buttonText.Contains("DEPARTMAN"))
            {
                button.BackColor = ButtonPrimary;
            }
            else if (buttonText.Contains("GÜNCELLE") || buttonText.Contains("UPDATE") ||
                     buttonText.Contains("İZİN") || buttonText.Contains("PERFORMANS"))
            {
                button.BackColor = ButtonSuccess;
            }
            else if (buttonText.Contains("MAAŞ") || buttonText.Contains("SALARY"))
            {
                button.BackColor = ButtonWarning;
            }
            else
            {
                // Varsayılan olarak primary renk
                button.BackColor = ButtonPrimary;
            }

            button.ForeColor = ButtonForeColor;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.UseVisualStyleBackColor = false;
        }

        /// <summary>
        /// DataGridView kontrolüne tema uygular
        /// </summary>
        private static void ApplyDataGridTheme(DataGridView dataGrid)
        {
            dataGrid.BackgroundColor = DataGridBackColor;
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.GridColor = DataGridGridColor;
            
            // Başlık satırı stilleri
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = DataGridHeaderBackColor;
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = DataGridHeaderForeColor;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font(dataGrid.Font.FontFamily, dataGrid.Font.Size, FontStyle.Bold);
            dataGrid.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            // Satır stilleri
            dataGrid.DefaultCellStyle.BackColor = DataGridBackColor;
            dataGrid.DefaultCellStyle.ForeColor = Color.Black;
            dataGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            
            // Seçili satır stili
            dataGrid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 220, 240);
            dataGrid.DefaultCellStyle.SelectionForeColor = Color.Black;
        }
    }
}
