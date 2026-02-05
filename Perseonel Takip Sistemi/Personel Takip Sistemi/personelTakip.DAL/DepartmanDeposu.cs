using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using personelTakip.Entities; // varlıkları (Entities) tanısın diye

namespace personelTakip.DAL
{
    public class DepartmanDeposu
    {
        // 1. Departmanları Listeleme 
        //işçi
        public List<Departman> Listele()
        {
            List<Departman> liste = new List<Departman>();

            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "SELECT * FROM departments";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Departman d = new Departman();
                        d.Id = Convert.ToInt32(okuyucu["id"]);
                        d.Ad = okuyucu["ad"].ToString();
                        d.Aciklama = okuyucu["aciklama"].ToString();

                        liste.Add(d);
                    }
                }
            }
            return liste;

        }

        // 2. Yeni Departman Ekleme 
        public int Ekle(Departman yeniDepartman)
        {
            int sonuc = 0;
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "INSERT INTO departments (ad, aciklama) VALUES (@p1, @p2)";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                // SQL saldırılarını önlemek için 
                komut.Parameters.AddWithValue("@p1", yeniDepartman.Ad);
                komut.Parameters.AddWithValue("@p2", yeniDepartman.Aciklama);

                sonuc = komut.ExecuteNonQuery(); // Etkilenen satır sayısını döndürmek için
            }
            return sonuc;
        }
        // ID numarasına göre departmanı sil
        public int Sil(int id)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                // "departments" tablosundan sil
                string sorgu = "DELETE FROM departments WHERE id = @p1";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", id);

                return komut.ExecuteNonQuery();
            }
        }


        // 4. Departman Güncelleme 
        public int Guncelle(Departman guncellenecekDept)
        {

            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                // ID numarasına göre bulup Ad ve Açıklamayı değiştiriyoruz
                string sorgu = "UPDATE departments SET ad=@p1, aciklama=@p2 WHERE id=@p3";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", guncellenecekDept.Ad);
                komut.Parameters.AddWithValue("@p2", guncellenecekDept.Aciklama);
                komut.Parameters.AddWithValue("@p3", guncellenecekDept.Id); // Hangi kayıt değişecekse

                return komut.ExecuteNonQuery();
            }
        }
    }

}