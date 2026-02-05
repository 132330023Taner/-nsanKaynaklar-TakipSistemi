using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using personelTakip.Entities;

namespace personelTakip.DAL
{
    public class IzinDeposu
    {
        public List<Izin> Listele()
        {
            List<Izin> liste = new List<Izin>();

            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = @"SELECT p.*, e.ad, e.soyad 
                                 FROM permissions p 
                                 JOIN employees e ON p.personel_id = e.id
                                 ORDER BY p.baslangic_tarihi DESC";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Izin i = new Izin();
                        i.Id = Convert.ToInt32(okuyucu["id"]);
                        i.PersonelId = Convert.ToInt32(okuyucu["personel_id"]);
                        i.BaslangicTarihi = Convert.ToDateTime(okuyucu["baslangic_tarihi"]);
                        i.BitisTarihi = Convert.ToDateTime(okuyucu["bitis_tarihi"]);
                        i.Aciklama = okuyucu["aciklama"].ToString();

                        if (okuyucu["durum"] != DBNull.Value)
                        {
                            i.Durum = okuyucu["durum"].ToString();
                        }
                        else
                        {
                            i.Durum = "Onay Bekliyor";
                        }
                        

                        i.PersonelAd = okuyucu["ad"].ToString();
                        i.PersonelSoyad = okuyucu["soyad"].ToString();

                        liste.Add(i);
                    }
                }
            }
            return liste;
        }


        // 2. İzin Ekleme
        public int Ekle(Izin i)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = @"INSERT INTO permissions 
                        (personel_id, baslangic_tarihi, bitis_tarihi, aciklama, durum) 
                        VALUES (@p1, @p2, @p3, @p4, @p5)";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", i.PersonelId);

                // Tarihler MySQL  (YYYY-MM-DD) 
                komut.Parameters.AddWithValue("@p2", i.BaslangicTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                komut.Parameters.AddWithValue("@p3", i.BitisTarihi.ToString("yyyy-MM-dd HH:mm:ss"));

                komut.Parameters.AddWithValue("@p4", i.Aciklama);
                komut.Parameters.AddWithValue("@p5", "Onay Bekliyor");

                return komut.ExecuteNonQuery();
            }
        }
        // 3. İzin Silme
        public int Sil(int id)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "DELETE FROM permissions WHERE id = @p1";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", id);

                return komut.ExecuteNonQuery();
            }
        }
        // 4. İzin Güncelleme
        public int Guncelle(Izin i)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = @"UPDATE permissions SET 
                        personel_id=@p1, baslangic_tarihi=@p2, 
                        bitis_tarihi=@p3, aciklama=@p4, durum=@p5 
                        WHERE id=@p6";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", i.PersonelId);
                komut.Parameters.AddWithValue("@p2", i.BaslangicTarihi.ToString("yyyy-MM-dd HH:mm:ss"));
                komut.Parameters.AddWithValue("@p3", i.BitisTarihi.ToString("yyyy-MM-dd HH:mm:ss"));

                komut.Parameters.AddWithValue("@p4", i.Aciklama);
                komut.Parameters.AddWithValue("@p5", i.Durum); //Onay-Red
                komut.Parameters.AddWithValue("@p6", i.Id);    // Hangi kayıt güncellenecek

                return komut.ExecuteNonQuery();
            }
        }
    }
    
}