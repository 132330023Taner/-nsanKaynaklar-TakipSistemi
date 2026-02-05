using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using personelTakip.Entities;

namespace personelTakip.DAL
{
    public class MaasDeposu
    {
        // 1. Listeleme
        public List<Maas> Listele()
        {
            List<Maas> liste = new List<Maas>();
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                // Personel adıyla birleştir
                string sorgu = @"SELECT s.*, e.ad, e.soyad 
                                 FROM salaries s 
                                 JOIN employees e ON s.personel_id = e.id 
                                 ORDER BY s.yil DESC, s.ay DESC";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Maas m = new Maas();
                        m.Id = Convert.ToInt32(okuyucu["id"]);
                        m.PersonelId = Convert.ToInt32(okuyucu["personel_id"]);
                        m.Ay = Convert.ToInt32(okuyucu["ay"]);
                        m.Yil = Convert.ToInt32(okuyucu["yil"]);
                        m.Tutar = Convert.ToDecimal(okuyucu["tutar"]);
                        m.Durum = okuyucu["durum"].ToString();

                        m.PersonelAd = okuyucu["ad"].ToString();
                        m.PersonelSoyad = okuyucu["soyad"].ToString();

                        liste.Add(m);
                    }
                }
            }
            return liste;
        }

        // 2. Ekle
        public int Ekle(Maas m)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "INSERT INTO salaries (personel_id, ay, yil, tutar, durum) VALUES (@p1, @p2, @p3, @p4, @p5)";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", m.PersonelId);
                komut.Parameters.AddWithValue("@p2", m.Ay);
                komut.Parameters.AddWithValue("@p3", m.Yil);
                komut.Parameters.AddWithValue("@p4", m.Tutar);
                komut.Parameters.AddWithValue("@p5", m.Durum);

                return komut.ExecuteNonQuery();
            }
        }

        // 3. Güncelle
        public int Guncelle(Maas m)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "UPDATE salaries SET personel_id=@p1, ay=@p2, yil=@p3, tutar=@p4, durum=@p5 WHERE id=@p6";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", m.PersonelId);
                komut.Parameters.AddWithValue("@p2", m.Ay);
                komut.Parameters.AddWithValue("@p3", m.Yil);
                komut.Parameters.AddWithValue("@p4", m.Tutar);
                komut.Parameters.AddWithValue("@p5", m.Durum);
                komut.Parameters.AddWithValue("@p6", m.Id);

                return komut.ExecuteNonQuery();
            }
        }

        // 4. Sil
        public int Sil(int id)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "DELETE FROM salaries WHERE id=@p1";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", id);
                return komut.ExecuteNonQuery();
            }
        }
    }
}