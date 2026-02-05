using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using personelTakip.Entities;

namespace personelTakip.DAL
{
    public class PersonelDeposu
    { 
        //ekle listele işlemleri
        // 1. Personelleri Listeleme (Departman adıyla )
        public List<Personel> Listele()
        {
            List<Personel> liste = new List<Personel>();

            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                //  Personel tablosuyla Departman tablosunu birleştirmek için
                // ekranda "Departman id" yerine ismini departman ismi
                string sorgu = @"SELECT e.*, d.ad AS DepartmanAdi 
                                 FROM employees e 
                                 LEFT JOIN departments d ON e.departman_id = d.id";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                using (MySqlDataReader okuyucu = komut.ExecuteReader())
                {
                    while (okuyucu.Read())
                    {
                        Personel p = new Personel();
                        p.Id = Convert.ToInt32(okuyucu["id"]);
                        p.Ad = okuyucu["ad"].ToString();
                        p.Soyad = okuyucu["soyad"].ToString();
                        p.TcNo = okuyucu["tc_no"].ToString();
                        p.Maas = Convert.ToDecimal(okuyucu["maas"]);
                        p.DepartmanId = Convert.ToInt32(okuyucu["departman_id"]);
                        p.RolId = Convert.ToInt32(okuyucu["rol_id"]);
                        p.IseGirisTarihi = Convert.ToDateTime(okuyucu["ise_giris_tarihi"]);

                        // Şifreyi de alıyoruz ams ekranda görünmeyecek
                        p.Sifre = okuyucu["sifre"].ToString();

                        liste.Add(p);
                    }
                }
            }
            return liste;
        }

        // 2. Yeni Personel Ekleme
        public int Ekle(Personel p)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = @"INSERT INTO employees 
                                (ad, soyad, tc_no, maas, departman_id, rol_id, sifre, ise_giris_tarihi) 
                                VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8)";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", p.Ad);
                komut.Parameters.AddWithValue("@p2", p.Soyad);
                komut.Parameters.AddWithValue("@p3", p.TcNo);
                komut.Parameters.AddWithValue("@p4", p.Maas);
                komut.Parameters.AddWithValue("@p5", p.DepartmanId);
                komut.Parameters.AddWithValue("@p6", p.RolId);
                komut.Parameters.AddWithValue("@p7", p.TcNo);
                komut.Parameters.AddWithValue("@p8", p.IseGirisTarihi);

                return komut.ExecuteNonQuery();
            }
        }
        // 3. Personel Silme
        public int Sil(int id)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = "DELETE FROM employees WHERE id = @p1";
                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@p1", id);

                 return komut.ExecuteNonQuery();
            }
        }
        // 4. Personel Güncelleme
        public int Guncelle(Personel p)
        {
            using (MySqlConnection baglanti = Veritabani.Baglan())
            {
                string sorgu = @"UPDATE employees SET 
                        ad=@p1, soyad=@p2, tc_no=@p3, maas=@p4, 
                        departman_id=@p5, ise_giris_tarihi=@p8 
                        WHERE id=@p9";

                MySqlCommand komut = new MySqlCommand(sorgu, baglanti);

                komut.Parameters.AddWithValue("@p1", p.Ad);
                komut.Parameters.AddWithValue("@p2", p.Soyad);
                komut.Parameters.AddWithValue("@p3", p.TcNo);
                komut.Parameters.AddWithValue("@p4", p.Maas);
                komut.Parameters.AddWithValue("@p5", p.DepartmanId);
                // Rol ve Şifre güncellemesi yok 
                komut.Parameters.AddWithValue("@p8", p.IseGirisTarihi);
                komut.Parameters.AddWithValue("@p9", p.Id); // Kimi güncelleynecek?

                return komut.ExecuteNonQuery();
            }
        }

    }
}
