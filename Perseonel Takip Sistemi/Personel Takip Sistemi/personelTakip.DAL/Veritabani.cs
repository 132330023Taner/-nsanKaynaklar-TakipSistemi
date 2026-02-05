using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace personelTakip.DAL
{
    public class Veritabani
    {
        // Okul Sunucusu Bağlantı Bilgileri
        // NOT: Veritabanı adının (Database) ve Uid'nin (Kullanıcı Adı) doğru olduğundan emin ol.
        private static string baglantiCumlesi = "Server=172.21.54.253; Database=26_132330023; Uid=26_132330023; Pwd=İnif123.; Charset=utf8;";

        public static MySqlConnection Baglan()
        {
            MySqlConnection baglanti = new MySqlConnection(baglantiCumlesi);

            if (baglanti.State == ConnectionState.Closed)
            {
                try
                {
                    baglanti.Open();
                }
                catch (Exception hata)
                {
                    // Bağlantı hatası detayını görmek için
                    throw new Exception("Sunucuya bağlanılamadı! Hata: " + hata.Message);
                }
            }
            return baglanti;
        }
    }
}