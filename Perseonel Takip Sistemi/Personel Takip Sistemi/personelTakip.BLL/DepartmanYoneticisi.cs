using System;
using System.Collections.Generic;
using personelTakip.DAL;      // Alttaki katmanı (İşçiyi) çağırmak için
using personelTakip.Entities; // Varlıkları (Entities) tanımak için

namespace personelTakip.BLL
{
    public class DepartmanYoneticisi
    {
        // İşçi sınıfımızdan (DAL) bir tane örnek oluşturuyoruz
        DepartmanDeposu depo = new DepartmanDeposu();

        // 1. Departmanları Listeleme İşi
        // Bu metod arayüzün veritabanındaki listeyi istemesini sağlar
        public List<Departman> TumunuGetir()
        {
            return depo.Listele();
        }

        // 2. Departman Ekleme İşi (Kurallar)
        public int Ekle(Departman yeniDepartman)
        {
            //  KURAL 1: Departman adı boş olamaz
            if (string.IsNullOrEmpty(yeniDepartman.Ad))
            {
                throw new Exception("Departman adı boş bırakılamaz!");
            }

            // KURAL 2: Departman adı çok kısaysa hata ver
            if (yeniDepartman.Ad.Length < 2)
            {
                throw new Exception("Departman adı en az 2 karakter olmalıdır.");
            }

            // Kuralları geçtiyse işçiye (DAL'a) gönderip kaydet
            return depo.Ekle(yeniDepartman);
        }
        public int Sil(int id)
        {
            return depo.Sil(id);
        }
        public int Guncelle(Departman dept)
        {
            // Burada yine "İsim boş olamaz" kuralı geçerli olmalı
            if (string.IsNullOrEmpty(dept.Ad))
            {
                throw new Exception("Departman adı boş olamaz!");
            }

            return depo.Guncelle(dept);
        }
    }
}