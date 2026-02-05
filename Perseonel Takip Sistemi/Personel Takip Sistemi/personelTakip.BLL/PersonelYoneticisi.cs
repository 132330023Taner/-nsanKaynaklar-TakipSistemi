using System;
using System.Collections.Generic;
using personelTakip.DAL;
using personelTakip.Entities;

namespace personelTakip.BLL
{
    public class PersonelYoneticisi
    {
        PersonelDeposu depo = new PersonelDeposu();

        public List<Personel> TumunuGetir()
        {
            return depo.Listele();
        }

        public int Ekle(Personel p)
        {
            if (string.IsNullOrEmpty(p.Ad) || string.IsNullOrEmpty(p.Soyad))
            {
                throw new Exception("Ad ve Soyad alanları boş bırakılamaz!");
            }
            if (p.Maas < 0)
            {
                throw new Exception("Maaş sıfırdan küçük veya neagatif olamaz!");
            }
            if (p.TcNo.Length != 11)
            {
                throw new Exception("TC Kimlik No 11 haneli olmalıdır.");
            }

            return depo.Ekle(p);
        }
        public int Sil(int id)
        {
            return depo.Sil(id);
        }
        public int Guncelle(Personel p)
        {
            if (string.IsNullOrEmpty(p.Ad) || string.IsNullOrEmpty(p.Soyad))
            {
                throw new Exception("Ad ve Soyad boş olamaz!");
            }

            return depo.Guncelle(p);
        }
    }
}