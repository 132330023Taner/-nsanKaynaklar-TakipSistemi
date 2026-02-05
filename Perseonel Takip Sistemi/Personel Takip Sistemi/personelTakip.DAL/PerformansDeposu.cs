using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using personelTakip.Entities;

namespace personelTakip.DAL
{
    public class PerformansDeposu
    {
        // 1 Ekle
        public int Ekle(Performans p)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "INSERT INTO performances (personel_id, puan, aciklama, degerlendirme_tarihi) VALUES (@p1, @p2, @p3, @p4)";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", p.PersonelId);
                komut.Parameters.AddWithValue("@p2", p.Puan);
                komut.Parameters.AddWithValue("@p3", p.Aciklama);
                komut.Parameters.AddWithValue("@p4", p.DegerlendirmeTarihi);
                return komut.ExecuteNonQuery();
            }
        }

        // 2 Sil
        public int Sil(int id)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "DELETE FROM performances WHERE id=@p1";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", id);
                return komut.ExecuteNonQuery();
            }
        }

        // 3 Listele
        public List<Performans> TumunuGetir()
        {
            List<Performans> liste = new List<Performans>();
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                //Performans tablosuyla Çalışan tablosunu birleştir
                string sorgu = @"SELECT p.id, p.personel_id, p.puan, p.aciklama, p.degerlendirme_tarihi, e.ad, e.soyad 
                                 FROM performances p 
                                 JOIN employees e ON p.personel_id = e.id";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                MySqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    Performans prf = new Performans();
                    prf.Id = Convert.ToInt32(oku["id"]);
                    prf.PersonelId = Convert.ToInt32(oku["personel_id"]);
                    prf.Puan = Convert.ToInt32(oku["puan"]);
                    prf.Aciklama = oku["aciklama"].ToString();
                    prf.DegerlendirmeTarihi = Convert.ToDateTime(oku["degerlendirme_tarihi"]);
                    prf.PersonelAdSoyad = oku["ad"].ToString() + " " + oku["soyad"].ToString();

                    liste.Add(prf);
                }
            }
            return liste;
        }
    }
}